using System;
using System.Net;
using System.Windows.Forms;

namespace CoatingMgr
{
    public partial class FormSetMySQLInfo : Form
    {
        public FormSetMySQLInfo()
        {
            InitializeComponent();
            if (!Properties.Settings.Default.MySQLIP.Equals(string.Empty))
            {
                this.tbIP.Text = Properties.Settings.Default.MySQLIP;
            }
            if (Properties.Settings.Default.MySQLPort > 0)
            {
                this.tbPort.Text = Properties.Settings.Default.MySQLPort+"";
            }
            if (!Properties.Settings.Default.MySQLUser.Equals(string.Empty))
            {
                this.tbUser.Text = Properties.Settings.Default.MySQLUser;
            }
            if (!Properties.Settings.Default.MySQLPwd.Equals(string.Empty))
            {
                this.tbPwd.Text = Properties.Settings.Default.MySQLPwd;
            }
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (this.tbIP.Text.Equals(string.Empty))
            {
                MessageBox.Show("数据库服务器IP地址未设置");
                return;
            }
            if (this.tbUser.Text.Equals(string.Empty))
            {
                MessageBox.Show("数据库服务器账号未设置");
                return;
            }
            if (this.tbPwd.Text.Equals(string.Empty))
            {
                MessageBox.Show("数据库服务器密码未设置");
                return;
            }

            IPAddress ip;
            int port;
            if (!this.tbIP.Text.Equals("localhost") && !IPAddress.TryParse(this.tbIP.Text, out ip))
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

            Properties.Settings.Default.MySQLIP = this.tbIP.Text;
            Properties.Settings.Default.MySQLPort = port;
            Properties.Settings.Default.MySQLUser = this.tbUser.Text;
            Properties.Settings.Default.MySQLPwd = this.tbPwd.Text;
            Properties.Settings.Default.Save();

            MySQLHelper.CreateDB();//创建数据库

            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
