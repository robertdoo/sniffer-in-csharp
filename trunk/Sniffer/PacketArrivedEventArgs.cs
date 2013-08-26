using System;
using System.Globalization;
using System.Net;
using System.Text;

namespace Sniffer
{
    /// <summary>
    /// 收到IP数据包
    /// </summary>
    public class PacketArrivedEventArgs : EventArgs
    {
        /// <summary>
        /// 得到的数据流的长度
        /// </summary>
        protected const int LenReceiveBuf = 4096;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public PacketArrivedEventArgs()
        {
            Protocol = "";
            DestinationPort = "";
            OriginationPort = "";
            DestinationAddress = "";
            OriginationAddress = "";
            IpVersion = "";

            PacketLength = 0;
            MessageLength = 0;
            HeaderLength = 0;

            ReceiveBuffer = new byte[LenReceiveBuf];
            IpHeaderBuffer = new byte[LenReceiveBuf];
            MessageBuffer = new byte[LenReceiveBuf];
        }

        /// <summary>
        /// 协议类型
        /// </summary>
        public string Protocol { get; set; }

        /// <summary>
        /// 目标端口
        /// </summary>
        public string DestinationPort { get; set; }

        /// <summary>
        /// 源端口
        /// </summary>
        public string OriginationPort { get; set; }

        /// <summary>
        /// 目标地址
        /// </summary>
        public string DestinationAddress { get; set; }

        /// <summary>
        /// 源地址
        /// </summary>
        public string OriginationAddress { get; set; }

        /// <summary>
        /// IP版本号
        /// </summary>
        public string IpVersion { get; set; }

        /// <summary>
        /// 包长度
        /// </summary>
        public uint PacketLength { get; set; }

        /// <summary>
        /// 消息长度
        /// </summary>
        public uint MessageLength { get; set; }

        /// <summary>
        /// 包头长度
        /// </summary>
        public uint HeaderLength { get; set; }

        /// <summary>
        /// 接收缓冲区
        /// </summary>
        public byte[] ReceiveBuffer { get; set; }

        /// <summary>
        /// 包头缓冲
        /// </summary>
        public byte[] IpHeaderBuffer { get; set; }

        /// <summary>
        /// 消息缓冲
        /// </summary>
        public byte[] MessageBuffer { get; set; }

        /// <summary>
        /// 解析数据包，形成PacketArrivedEventArgs事件数据类对象
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public unsafe static PacketArrivedEventArgs ParseFrom(byte[] buf, int len)
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
            return e;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append('*', 120);
            builder.AppendLine();
            builder.AppendFormat("Protocol Type:{0}\tIP Version:{1}\t", Protocol, IpVersion);
            builder.AppendFormat("Destination Address:{0}:{1}\t", DestinationAddress, DestinationPort);
            builder.AppendFormat("Source Address:{0}:{1}\t", OriginationAddress, OriginationPort);
            builder.AppendFormat("Header Legth:{0}\tMessage Length:{1}", HeaderLength, MessageLength);
            builder.AppendLine();
            if (HeaderLength > 0 && IpHeaderBuffer != null && IpHeaderBuffer.Length > 0)
            {
                builder.Append("Header:");
                for (var i = 0; i < HeaderLength; i++)
                {
                    builder.AppendFormat("\\{0}", IpHeaderBuffer[i]);
                }
                builder.AppendLine();
            }
            if (MessageLength > 0 && MessageBuffer != null && MessageBuffer.Length > 0)
            {
                builder.Append("Message:");
                for (var i = 0; i < MessageLength; i++)
                {
                    builder.AppendFormat("\\{0}", MessageBuffer[i]);
                }
                builder.AppendLine();
            }
            builder.Append('*', 120);
            builder.AppendLine();
            return builder.ToString();
        }
    }
    /// <summary>
    /// 收到的TCP数据包
    /// </summary>
    public class TcpPacketArrivedEventArgs : PacketArrivedEventArgs
    {
        public string SerialNumber { get; set; }
        public string ConfirmNumber { get; set; }
        public string WindowSize { get; set; }
        public bool UrgFlag { get; set; }
        public bool AckFlag { get; set; }
        public bool PshFlag { get; set; }
        public bool PstFlag { get; set; }
        public bool SynFlag { get; set; }
        public bool FinFlag { get; set; }
        public uint TcpHeaderLength { get; set; }
        /// <summary>
        /// TCP包头缓冲
        /// </summary>
        public byte[] TcpHeaderBuffer { get; set; }

        public uint TcpMessageLength { get; set; }
        /// <summary>
        /// TCP消息缓冲
        /// </summary>
        public byte[] TcpMessageBuffer { get; set; }
        public TcpPacketArrivedEventArgs()
        {
            SerialNumber = "";
            ConfirmNumber = "";
            WindowSize = "";

            TcpHeaderLength = 0;
            TcpMessageLength = 0;
            TcpHeaderBuffer = new byte[LenReceiveBuf];
            TcpMessageBuffer = new byte[LenReceiveBuf];
        }
        /// <summary>
        /// 解析TCP包
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public unsafe static TcpPacketArrivedEventArgs ParseTcpFrom(byte[] buf, int len)
        {
            var e = new TcpPacketArrivedEventArgs();//新网络数据包信息事件

            fixed (byte* fixedBuf = buf)
            {
                var head = (TcpHeader*)fixedBuf;//把数据流整和为IPHeader结构
                e.HeaderLength = (uint)(head->IpHeader.IpVerLen & 0x0F) << 2;
                e.TcpHeaderLength = (uint) (head->HeaderLength & 0xF0) >> 2;
                var tempProtocol = head->IpHeader.IpProtocol;
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

                var tempVersion = (uint)(head->IpHeader.IpVerLen & 0xF0) >> 4;
                e.IpVersion = tempVersion.ToString(CultureInfo.InvariantCulture);

                //以下语句提取出了PacketArrivedEventArgs对象中的其他参数
                var tempIpSrcaddr = head->IpHeader.IpSrcAddr;
                var tempIpDestaddr = head->IpHeader.IpDestAddr;
                var tempIp = new IPAddress(tempIpSrcaddr);
                e.OriginationAddress = tempIp.ToString();
                tempIp = new IPAddress(tempIpDestaddr);
                e.DestinationAddress = tempIp.ToString();

                var tempSrcport = *(short*)&fixedBuf[e.HeaderLength];
                var tempDstport = *(short*)&fixedBuf[e.HeaderLength + 2];
                e.OriginationPort = IPAddress.NetworkToHostOrder(tempSrcport).ToString(CultureInfo.InvariantCulture);
                e.DestinationPort = IPAddress.NetworkToHostOrder(tempDstport).ToString(CultureInfo.InvariantCulture);
                e.SerialNumber = IPAddress.NetworkToHostOrder(head->SerialNumber).ToString(CultureInfo.InvariantCulture);
                e.ConfirmNumber =
                    IPAddress.NetworkToHostOrder(head->ConfirmNumber).ToString(CultureInfo.InvariantCulture);
                e.UrgFlag = (head->TcpFlags & 0x20) >> 5 > 0;
                e.AckFlag = (head->TcpFlags & 0x10) >> 4 > 0;
                e.PshFlag = (head->TcpFlags & 0x08) >> 3 > 0;
                e.PstFlag = (head->TcpFlags & 0x04) >> 2 > 0;
                e.SynFlag = (head->TcpFlags & 0x02) >> 1 > 0;
                e.FinFlag = (head->TcpFlags & 0x01) > 0;
                e.WindowSize = IPAddress.NetworkToHostOrder(head->WindowSize).ToString(CultureInfo.InvariantCulture);

                e.PacketLength = (uint)len;
                e.MessageLength = (uint)len - e.HeaderLength;
                e.TcpMessageLength = (uint) len - e.HeaderLength - e.TcpHeaderLength;
                e.ReceiveBuffer = buf;
                //把buf中的IP头赋给PacketArrivedEventArgs中的IPHeaderBuffer
                Array.Copy(buf, 0, e.IpHeaderBuffer, 0, (int)e.HeaderLength);
                //把buf中的包中内容赋给PacketArrivedEventArgs中的MessageBuffer
                Array.Copy(buf, (int)e.HeaderLength, e.MessageBuffer, 0, (int)e.MessageLength);
                //TCP Header;
                Array.Copy(buf, (int) (e.HeaderLength), e.TcpHeaderBuffer, 0, (int) e.TcpHeaderLength);
                //TCP Message;
                Array.Copy(buf, (int) (e.HeaderLength + e.TcpHeaderLength), e.TcpMessageBuffer, 0,
                           (int) e.TcpMessageLength);
            }
            return e;
        }
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append('*', 120);
            builder.AppendLine();
            builder.AppendFormat("Protocol Type:{0}\tIP Version:{1}\t", Protocol, IpVersion);
            builder.AppendFormat("Destination Address:{0}:{1}\t", DestinationAddress, DestinationPort);
            builder.AppendFormat("Source Address:{0}:{1}\t", OriginationAddress, OriginationPort);
            builder.AppendFormat("Header Legth:{0}\tMessage Length:{1}", HeaderLength, MessageLength);
            builder.AppendLine();
            builder.AppendFormat("TCP Header Legth:{0}\tTCP Message Length:{1}\t", TcpHeaderLength, TcpMessageLength);
            builder.AppendFormat("URG:{0}\tACK:{1}\tPSH:{2}\tRST:{3}\tSYN:{4}\tFIN:{5}", UrgFlag, AckFlag, PshFlag,
                                 PstFlag, SynFlag, FinFlag);
            builder.AppendLine();
            if (HeaderLength > 0 && IpHeaderBuffer != null && IpHeaderBuffer.Length > 0)
            {
                builder.Append("Header:");
                for (var i = 0; i < HeaderLength; i++)
                {
                    builder.AppendFormat("\\{0}", IpHeaderBuffer[i]);
                }
                builder.AppendLine();
            }
            if (TcpHeaderLength > 0 && TcpHeaderBuffer != null && TcpHeaderBuffer.Length > 0)
            {
                builder.Append("TCP Header:");
                for (var i = 0; i < TcpHeaderLength; i++)
                {
                    builder.AppendFormat("\\{0}", TcpHeaderBuffer[i]);
                }
                builder.AppendLine();
            }
            if (TcpMessageLength > 0 && TcpMessageBuffer != null && TcpMessageBuffer.Length > 0)
            {
                builder.Append("TCP Message:");
                for (var i = 0; i < TcpMessageLength; i++)
                {
                    builder.AppendFormat("\\{0}", TcpMessageBuffer[i]);
                }
                builder.AppendLine();
            }
            builder.Append('*', 120);
            builder.AppendLine();
            return builder.ToString();
        }
    }
    public class UdpPacketArrivedEventArgs : PacketArrivedEventArgs
    {
        public uint UdpHeaderLength { get; set; }
        public uint UdpMessageLength { get; set; }
        /// <summary>
        /// UDP包头缓冲
        /// </summary>
        public byte[] UdpHeaderBuffer { get; set; }

        /// <summary>
        /// UDP消息缓冲
        /// </summary>
        public byte[] UdpMessageBuffer { get; set; }
        public UdpPacketArrivedEventArgs()
        {
            UdpHeaderLength = 0;
            UdpMessageLength = 0;
            UdpHeaderBuffer = new byte[LenReceiveBuf];
            UdpMessageBuffer = new byte[LenReceiveBuf];
        }
        /// <summary>
        /// 解析UDP包
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public unsafe static UdpPacketArrivedEventArgs ParseUdpFrom(byte[] buf, int len)
        {
            var e = new UdpPacketArrivedEventArgs();//新网络数据包信息事件

            fixed (byte* fixedBuf = buf)
            {
                var head = (UdpHeader*)fixedBuf;//把数据流整和为IPHeader结构
                e.HeaderLength = (uint)(head->IpHeader.IpVerLen & 0x0F) << 2;
                e.UdpHeaderLength = 8;
                var tempProtocol = head->IpHeader.IpProtocol;
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

                var tempVersion = (uint)(head->IpHeader.IpVerLen & 0xF0) >> 4;
                e.IpVersion = tempVersion.ToString(CultureInfo.InvariantCulture);

                //以下语句提取出了PacketArrivedEventArgs对象中的其他参数
                var tempIpSrcaddr = head->IpHeader.IpSrcAddr;
                var tempIpDestaddr = head->IpHeader.IpDestAddr;
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
                e.UdpMessageLength = e.MessageLength - e.UdpHeaderLength;

                e.ReceiveBuffer = buf;
                //把buf中的IP头赋给PacketArrivedEventArgs中的IPHeaderBuffer
                Array.Copy(buf, 0, e.IpHeaderBuffer, 0, (int)e.HeaderLength);
                //把buf中的包中内容赋给PacketArrivedEventArgs中的MessageBuffer
                Array.Copy(buf, (int)e.HeaderLength, e.MessageBuffer, 0, (int)e.MessageLength);
                //
                Array.Copy(buf, (int) e.HeaderLength, e.UdpHeaderBuffer, 0, (int) e.UdpHeaderLength);
                //
                Array.Copy(buf, (int) (e.HeaderLength + e.UdpHeaderLength), e.UdpMessageBuffer, 0,
                           (int) e.UdpMessageLength);
            }
            return e;
        }
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append('*', 120);
            builder.AppendLine();
            builder.AppendFormat("Protocol Type:{0}\tIP Version:{1}\t", Protocol, IpVersion);
            builder.AppendFormat("Destination Address:{0}:{1}\t", DestinationAddress, DestinationPort);
            builder.AppendFormat("Source Address:{0}:{1}\t", OriginationAddress, OriginationPort);
            builder.AppendFormat("Header Legth:{0}\tMessage Length:{1}\t", HeaderLength, MessageLength);
            builder.AppendFormat("UDP Header Legth:{0}\tUDP Message Length:{1}\t", UdpHeaderLength, UdpMessageLength);
            builder.AppendLine();
            if (HeaderLength > 0 && IpHeaderBuffer != null && IpHeaderBuffer.Length > 0)
            {
                builder.Append("Header:");
                for (var i = 0; i < HeaderLength; i++)
                {
                    builder.AppendFormat("\\{0}", IpHeaderBuffer[i]);
                }
                builder.AppendLine();
            }
            if (UdpHeaderLength > 0 && UdpHeaderBuffer != null && UdpHeaderBuffer.Length > 0)
            {
                builder.Append("UDP Header:");
                for (var i = 0; i < UdpHeaderLength; i++)
                {
                    builder.AppendFormat("\\{0}", UdpHeaderBuffer[i]);
                }
                builder.AppendLine();
            }
            if (UdpMessageLength > 0 && UdpMessageBuffer != null && UdpMessageBuffer.Length > 0)
            {
                builder.Append("UDP Message:");
                for (var i = 0; i < UdpMessageLength; i++)
                {
                    builder.AppendFormat("\\{0}", UdpMessageBuffer[i]);
                }
                builder.AppendLine();
            }
            builder.Append('*', 120);
            builder.AppendLine();
            return builder.ToString();
        }
    }
}