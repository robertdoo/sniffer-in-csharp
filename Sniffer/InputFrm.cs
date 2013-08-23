using System;
using System.Windows.Forms;

namespace Sniffer
{
    public partial class InputFrm : Form
    {
        public InputFrm()
        {
            InitializeComponent();
        }

        internal Rule GetRule()
        {
            return new Rule(cboProtocol.Text, txtSrcAddr.Text, (int) numSrcPort.Value, txtDestAddr.Text,
                            (int) numDestPort.Value);
        }

        private void InputFrm_Load(object sender, EventArgs e)
        {
            cboProtocol.SelectedIndex = 0;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
