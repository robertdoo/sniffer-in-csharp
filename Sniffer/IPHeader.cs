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
}