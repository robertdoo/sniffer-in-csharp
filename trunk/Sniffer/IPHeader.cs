using System.Runtime.InteropServices;

namespace Sniffer
{
    /// <summary>
    /// IP头结构，来暂时存放一些有关网络封包的信息
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct IpHeader
    {
        /// <summary>
        /// //I4位首部长度+4位IP版本号
        /// </summary>
        [FieldOffset(0)]
        public byte IpVerLen;        
        /// <summary>
        /// //8位服务类型TOS
        /// </summary>
        [FieldOffset(1)]
        public byte IpTos;            
        /// <summary>
        /// //16位数据包总长度（字节）
        /// </summary>
        [FieldOffset(2)]
        public ushort IpTotalLength; 
        /// <summary>
        /// //16位标识
        /// </summary>
        [FieldOffset(4)]
        public ushort IpId;             
        /// <summary>
        /// //3位标志位
        /// </summary>
        [FieldOffset(6)]
        public ushort IpFlagOffset;       
        /// <summary>
        /// //8位生存时间 TTL
        /// </summary>
        [FieldOffset(8)]
        public byte IpTtl;            
        /// <summary>
        /// //8位协议(TCP, UDP, ICMP, Etc.)
        /// </summary>
        [FieldOffset(9)]
        public byte IpProtocol;    
        /// <summary>
        ///  //16位IP首部校验和
        /// </summary>
        [FieldOffset(10)]
        public ushort IpChecksum;
        /// <summary>
        /// //32位源IP地址
        /// </summary>
        [FieldOffset(12)]
        public uint IpSrcAddr;     
        /// <summary>
        /// //32位目的IP地址
        /// </summary>
        [FieldOffset(16)]
        public uint IpDestAddr;   
    }
    /// <summary>
    /// Tcp头结构
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct TcpHeader
    {
        [FieldOffset(0)]
        public IpHeader IpHeader;
        /// <summary>
        /// 16位源端口号
        /// </summary>
        [FieldOffset(20)]
        public ushort OriginationPort;
        /// <summary>
        /// 16位目的端口号
        /// </summary>
        [FieldOffset(22)]
        public ushort DestinationPort;
        /// <summary>
        /// 32位序列号
        /// </summary>
        [FieldOffset(24)]
        public uint SerialNumber;
        /// <summary>
        /// 32位确认号
        /// </summary>
        [FieldOffset(28)]
        public uint ConfirmNumber;
        /// <summary>
        /// 首部长度，仅前四位
        /// </summary>
        [FieldOffset(32)]
        public byte HeaderLength;
        /// <summary>
        /// URG,ACK,PSH,PST,SYN,FIN,后六位
        /// </summary>
        [FieldOffset(33)]
        public byte TcpFlags;
        /// <summary>
        /// 16位窗口大小
        /// </summary>
        [FieldOffset(34)]
        public ushort WindowSize;
        /// <summary>
        /// 16位TCP检验和
        /// </summary>
        [FieldOffset(36)]
        public ushort TcpChecksum;
        /// <summary>
        /// 16位紧急指针
        /// </summary>
        [FieldOffset(38)]
        public ushort Ptr;
    }
    /// <summary>
    /// Udp头结构
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct UdpHeader
    {
        [FieldOffset(0)]
        public IpHeader IpHeader;
        /// <summary>
        /// 16位源端口号
        /// </summary>
        [FieldOffset(20)]
        public ushort OriginationPort;
        /// <summary>
        /// 16位目的端口号
        /// </summary>
        [FieldOffset(22)]
        public ushort DestinationPort;
        /// <summary>
        /// 16位UDP长度
        /// </summary>
        [FieldOffset(24)]
        public ushort UdpMessageLength;
        /// <summary>
        /// 16位TCP检验和
        /// </summary>
        [FieldOffset(26)]
        public ushort UdpChecksum;

    }
}