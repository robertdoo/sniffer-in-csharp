using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Sniffer
{
    public partial class MainFrm : Form
    {
        private delegate void ShowMessage(string msg);
        private string _addr;
        private RawSocket _myRawSocket;
        private readonly List<Rule> _filters = new List<Rule>();
        private readonly List<Rule> _allows = new List<Rule>();
        public MainFrm()
        {
            InitializeComponent();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            var host = Dns.GetHostName();//获得主机名
            var iph = Dns.GetHostEntry(host);//解析主机地址
            var address = iph.AddressList[0];
            _addr = address.ToString();
            _myRawSocket =new RawSocket();
            _myRawSocket.CreateAndBindSocket(_addr);
            _myRawSocket.PacketArrival += myRawSocket_PacketArrival;
        }

        private void myRawSocket_PacketArrival(object sender, PacketArrivedEventArgs args)
        {
            if (_allows.Any(allow => allow.ShouldBeAllowed(args)))
            {
                AppendLog(args.ToString());
                if (args.Protocol.Equals("TCP") && args.MessageLength > 20)
                {
                    AppendLog(Encoding.UTF8.GetString(args.MessageBuffer, 20, (int)args.MessageLength - 20));
                }
                //if (args.Protocol.Equals("UDP") && args.MessageLength > 8)
                //{
                //    AppendLog(Encoding.UTF8.GetString(args.MessageBuffer, 8, (int)args.MessageLength - 8));
                //}
                return;
            }
            if (_filters.Any(filter => filter.ShouldBeFiltered(args)))
            {
                return;
            }
            AppendLog(args.ToString());
            if (args.Protocol.Equals("TCP") && args.MessageLength > 20)
            {
                AppendLog(Encoding.UTF8.GetString(args.MessageBuffer, 20, (int) args.MessageLength - 20));
            }
            //if (args.Protocol.Equals("UDP") && args.MessageLength > 8)
            //{
            //    AppendLog(Encoding.UTF8.GetString(args.MessageBuffer, 8, (int) args.MessageLength - 8));
            //}
            //AppendLog(Encoding.UTF8.GetString(args.MessageBuffer, 4, (int) args.MessageLength - 4));
        }

        private const int MaxLines = 5;
        private int _lines;
        private void AppendLog(string msg)
        {
            if (txtLog.InvokeRequired)
            {
                var d = new ShowMessage(AppendLog);
                Invoke(d, new object[] {msg});
            }
            else
            {
                if (_lines >= MaxLines)
                {
                    txtLog.Text = "";
                    _lines = 0;
                }
                txtLog.Text += msg + Environment.NewLine;
                txtLog.Select(txtLog.Text.Length, 0);
                _lines++;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (_myRawSocket == null) return;
            _myRawSocket.KeepRunning = true;
            _myRawSocket.Run();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (_myRawSocket == null) return;
            _myRawSocket.KeepRunning = false;
        }

        private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_myRawSocket == null) return;
            _myRawSocket.KeepRunning = false;
            _myRawSocket.Shutdown();
        }

        private void btnAddFilter_Click(object sender, EventArgs e)
        {
            var frm = new InputFrm();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                AddFilter(frm.GetRule());
            }
        }

        private void btnRemoveFilter_Click(object sender, EventArgs e)
        {
            foreach (var filter in cklFilter.SelectedItems)
            {
                _filters.Remove((Rule) filter);
            }
            cklFilter.Items.Clear();
            foreach (var filter in _filters)
            {
                cklFilter.Items.Add(filter);
            }
        }

        private void btnAddAllow_Click(object sender, EventArgs e)
        {
            var frm = new InputFrm();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                AddAllow(frm.GetRule());
            }
        }

        private void btnRemoveAllow_Click(object sender, EventArgs e)
        {
            foreach (var allow in cklAllow.SelectedItems)
            {
                _allows.Remove((Rule)allow);
            }
            cklAllow.Items.Clear();
            foreach (var allow in _allows)
            {
                cklAllow.Items.Add(allow);
            }
        }

        private void btnFilterAll_Click(object sender, EventArgs e)
        {
            AddFilter(new Rule("*", "", 0, "", 0));
        }

        private void AddFilter(Rule rule)
        {
            _filters.Add(rule);
            cklFilter.Items.Clear();
            foreach (var filter in _filters)
            {
                cklFilter.Items.Add(filter);
            }
        }
        private void AddAllow(Rule rule)
        {
            _allows.Add(rule);
            cklAllow.Items.Clear();
            foreach (var allow in _allows)
            {
                cklAllow.Items.Add(allow);
            }
        }
    }

    public class Rule 
    {
        /// <summary>
        /// 协议类型
        /// </summary>
        public string Protocol { get; set; }

        /// <summary>
        /// 目标地址
        /// </summary>
        public string DestinationAddress { get; set; }

        /// <summary>
        /// 源端口
        /// </summary>
        public int OriginationPort { get; set; }

        /// <summary>
        /// 目标端口
        /// </summary>
        public int DestinationPort { get; set; }



        /// <summary>
        /// 源地址
        /// </summary>
        public string OriginationAddress { get; set; }
        public Rule() { }
        public Rule(string protocol,string srcAddr,int srcPort, string destAddr, int destPort)
        {
            Protocol = protocol;
            OriginationAddress = srcAddr;
            OriginationPort = srcPort;
            DestinationAddress = destAddr;
            DestinationPort = destPort;
        }
        public bool ShouldBeAllowed(PacketArrivedEventArgs eventArgs)
        {
            if (Protocol != "*" && !eventArgs.Protocol.Equals(Protocol)) return false;
            if (!string.IsNullOrEmpty(OriginationAddress))
            {
                if (!eventArgs.OriginationAddress.Equals(OriginationAddress)) return false;
            }
            if (OriginationPort > 0)
            {
                if (int.Parse(eventArgs.OriginationPort) != OriginationPort) return false;
            }
            if (!string.IsNullOrEmpty(DestinationAddress))
            {
                if (!eventArgs.DestinationAddress.Equals(DestinationAddress)) return false;
            }
            if (DestinationPort > 0)
            {
                if (int.Parse(eventArgs.DestinationPort) != DestinationPort) return false;
            }
            return true;
        }

        public bool ShouldBeFiltered(PacketArrivedEventArgs eventArgs)
        {
            if (Protocol != "*" && !eventArgs.Protocol.Equals(Protocol)) return false;
            if (!string.IsNullOrEmpty(OriginationAddress))
            {
                if (!eventArgs.OriginationAddress.Equals(OriginationAddress)) return false;
            }
            if (OriginationPort > 0)
            {
                if (int.Parse(eventArgs.OriginationPort) != OriginationPort) return false;
            }
            if (!string.IsNullOrEmpty(DestinationAddress))
            {
                if (!eventArgs.DestinationAddress.Equals(DestinationAddress)) return false;
            }
            if (DestinationPort > 0)
            {
                if (int.Parse(eventArgs.DestinationPort) != DestinationPort) return false;
            }
            return true;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}:{2}:{3}:{4}", Protocol, OriginationAddress, OriginationPort,
                                 DestinationAddress, DestinationPort);
        }
    }
}
