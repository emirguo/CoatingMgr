using System;
using System.Net;
using System.Windows.Forms;

namespace CoatingMgr
{
    public partial class FormSetSQLInfo : Form
    {
        public FormSetSQLInfo()
        {
            InitializeComponent();
            if (!Properties.Settings.Default.SQLIP.Equals(string.Empty))
            {
                this.tbIP.Text = Properties.Settings.Default.SQLIP;
            }
            if (Properties.Settings.Default.SQLPort > 0)
            {
                this.tbPort.Text = Properties.Settings.Default.SQLPort+"";
            }
            if (!Properties.Settings.Default.SQLUser.Equals(string.Empty))
            {
                this.tbUser.Text = Properties.Settings.Default.SQLUser;
            }
            if (!Properties.Settings.Default.SQLPwd.Equals(string.Empty))
            {
                this.tbPwd.Text = Properties.Settings.Default.SQLPwd;
            }
        }

        private void SaveInfo()
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

            Properties.Settings.Default.SQLIP = this.tbIP.Text;
            Properties.Settings.Default.SQLPort = port;
            Properties.Settings.Default.SQLUser = this.tbUser.Text;
            Properties.Settings.Default.SQLPwd = this.tbPwd.Text;
            Properties.Settings.Default.Save();

            //MySQLHelper.CreateDB();//创建数据库

            Close();
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            SaveInfo();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TbPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SaveInfo();
            }
        }
    }
}
