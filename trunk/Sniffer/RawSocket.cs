using System;
using System.Globalization;
using System.Net;
using System.Net.Sockets;

namespace Sniffer
{
    /// <summary>
    /// 实现数据包监视
    /// </summary>
    public class RawSocket
    {
        //监听所有的数据包
        const int SioRcvall = unchecked((int)0x98000001);
        /// <summary>
        /// 是否继续进行
        /// </summary>
        public bool KeepRunning;
        //得到的数据流的长度
        private static int _lenReceiveBuf;
        //收到的字节
        private readonly byte[] _receiveBufBytes;
        //声明套接字 
        private Socket _socket;

        /// <summary>
        /// 套接字在接收包时是否产生错误
        /// </summary>
        public bool ErrorOccurred { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public RawSocket()                    
        {
            ErrorOccurred = false;
            _lenReceiveBuf = 4096;
            _receiveBufBytes = new byte[_lenReceiveBuf];
        }
        /// <summary>
        /// 建立并绑定套接字
        /// </summary>
        /// <param name="ip"></param>
        public void CreateAndBindSocket(string ip)
        {
            //置socket非阻塞状态
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP) {Blocking = false};
            //绑定套接字
            _socket.Bind(new IPEndPoint(IPAddress.Parse(ip), 0));
            if (SetSocketOption() == false)
            {
                ErrorOccurred = true;
            }
        }
        /// <summary>
        /// 设置raw socket
        /// </summary>
        /// <returns></returns>
        private bool SetSocketOption()
        {
            var retValue = true;
            try
            {
                _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, 1);
                var IN = new byte[] { 1, 0, 0, 0 };
                var OUT = new byte[4];

                //低级别操作模式,接受所有的数据包，这一步是关键，必须把socket设成raw和IP Level才可用SIO_RCVALL
                //设置SIO_RCVALL，表示接收所有网络上的数据包
// ReSharper disable RedundantAssignment
                var retCode = _socket.IOControl(SioRcvall, IN, OUT);
// ReSharper restore RedundantAssignment
                retCode = OUT[0] + OUT[1] + OUT[2] + OUT[3];//把4个8位字节合成一个32位整数
                if (retCode != 0) retValue = false;
            }
            catch (SocketException)
            {
                retValue = false;
            }
            return retValue;
        }
        //解析接收的数据包，形成PacketArrivedEventArgs事件数据类对象，并引发PacketArrival事件
        unsafe private void Receive(byte[] buf, int len)
        {
            var e = new PacketArrivedEventArgs();//新网络数据包信息事件

            fixed (byte* fixedBuf = buf)
            {
                var head = (IpHeader*)fixedBuf;//把数据流整和为IPHeader结构
                e.HeaderLength = (uint)(head->IpVerLen & 0x0F) << 2;

                var tempProtocol = head->IpProtocol;
                switch (tempProtocol) //提取协议类型
                {
                    case 1:
                        e.Protocol = "ICMP";
                        break;
                    case 2:
                        e.Protocol = "IGMP";
                        break;
                    case 6:
                        e.Protocol = "TCP";
                        break;
                    case 17:
                        e.Protocol = "UDP";
                        break;
                    default:
                        e.Protocol = "UNKNOWN";
                        break;
                }

                var tempVersion = (uint)(head->IpVerLen & 0xF0) >> 4;
                e.IpVersion = tempVersion.ToString(CultureInfo.InvariantCulture);

                //以下语句提取出了PacketArrivedEventArgs对象中的其他参数
                var tempIpSrcaddr = head->IpSrcAddr;
                var tempIpDestaddr = head->IpDestAddr;
                var tempIp = new IPAddress(tempIpSrcaddr);
                e.OriginationAddress = tempIp.ToString();
                tempIp = new IPAddress(tempIpDestaddr);
                e.DestinationAddress = tempIp.ToString();

                var tempSrcport = *(short*)&fixedBuf[e.HeaderLength];
                var tempDstport = *(short*)&fixedBuf[e.HeaderLength + 2];
                e.OriginationPort = IPAddress.NetworkToHostOrder(tempSrcport).ToString(CultureInfo.InvariantCulture);
                e.DestinationPort = IPAddress.NetworkToHostOrder(tempDstport).ToString(CultureInfo.InvariantCulture);

                e.PacketLength = (uint)len;
                e.MessageLength = (uint)len - e.HeaderLength;

                e.ReceiveBuffer = buf;
                //把buf中的IP头赋给PacketArrivedEventArgs中的IPHeaderBuffer
                Array.Copy(buf, 0, e.IpHeaderBuffer, 0, (int)e.HeaderLength);
                //把buf中的包中内容赋给PacketArrivedEventArgs中的MessageBuffer
                Array.Copy(buf, (int)e.HeaderLength, e.MessageBuffer, 0, (int)e.MessageLength);
            }
            //引发PacketArrival事件
            OnPacketArrival(e);
        }
        /// <summary>
        /// 开始监听
        /// </summary>
        public void Run() 
        {
#pragma warning disable 168
            var ar = _socket.BeginReceive(_receiveBufBytes, 0, _lenReceiveBuf, SocketFlags.None, CallReceive, this);
#pragma warning restore 168
        }
        /// <summary>
        /// //异步回调
        /// </summary>
        /// <param name="ar"></param>
        private void CallReceive(IAsyncResult ar)
        {
            if (!KeepRunning) return;
            var receivedBytes = _socket.EndReceive(ar);
            Receive(_receiveBufBytes, receivedBytes);
            if (KeepRunning)
            {
                Run();
            }
        }
        /// <summary>
        /// //关闭raw socket
        /// </summary>
        public void Shutdown()                                       
        {
            if (_socket == null) return;
            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
        }


        /// <summary>
        /// 收到数据包事件
        /// </summary>
        public event PacketArrivedEventHandler PacketArrival;

        protected virtual void OnPacketArrival(PacketArrivedEventArgs e)
        {
            if (PacketArrival != null)
            {
                PacketArrival(this, e);
            }
        }
    }
    /// <summary>
    /// 收到数据包委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void PacketArrivedEventHandler(object sender, PacketArrivedEventArgs args);
}