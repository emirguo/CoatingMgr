using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoatingMgr
{
    public partial class FormSetPLCIP : Form
    {
        public FormSetPLCIP()
        {
            InitializeComponent();
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            IPAddress ip;
            int port;
            if (!IPAddress.TryParse(this.tbIPAddr.Text, out ip))
            {
                MessageBox.Show("IP地址非法，请重新输入");
                return;
            }

            try
            {
                port = Convert.ToInt32(this.tbPort.Text);
            }
            catch
            {
                MessageBox.Show("端口应全为数字，请重新输入");
                return;
            }
            Properties.Settings.Default.PLCIP = this.tbIPAddr.Text;
            Properties.Settings.Default.PLCPort = port;
            Properties.Settings.Default.Save();
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
