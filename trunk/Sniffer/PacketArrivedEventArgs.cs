using System;
using System.Text;

namespace Sniffer
{
    /// <summary>
    /// 收到数据包
    /// </summary>
    public class PacketArrivedEventArgs : EventArgs
    {
        //得到的数据流的长度
        private const int LenReceiveBuf = 4096;

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
}