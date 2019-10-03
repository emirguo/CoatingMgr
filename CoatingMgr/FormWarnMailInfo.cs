using System;
using System.Windows.Forms;

namespace CoatingMgr
{
    public partial class FormWarnMailInfo : Form
    {
        public FormWarnMailInfo()
        {
            InitializeComponent();
        }

        private void FormWarnMailInfo_Load(object sender, EventArgs e)
        {
            InitView();
        }

        private void InitView()
        {
            tbMailCount.Text = Properties.Settings.Default.MailCount;
            tbMailPassword.Text = Properties.Settings.Default.MailPassword;
            tbMailSMTP.Text = Properties.Settings.Default.MailSMTP;
            tbMailPort.Text = Properties.Settings.Default.MailPort;
            tbMailTo.Text = Properties.Settings.Default.MailTo;
            tbMailCC.Text = Properties.Settings.Default.MailCC;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (tbMailCount.Text.Equals(""))
            {
                MessageBox.Show("请设置发送邮箱账号");
                return;
            }
            if (tbMailPassword.Text.Equals(""))
            {
                MessageBox.Show("请设置发送邮箱密码");
                return;
            }
            if (tbMailSMTP.Text.Equals(""))
            {
                MessageBox.Show("请设置发送邮箱SMTP服务器");
                return;
            }
            if (tbMailPort.Text.Equals(""))
            {
                MessageBox.Show("请设置发送端口");
                return;
            }
            if (tbMailTo.Text.Equals(""))
            {
                MessageBox.Show("请设置收件人");
                return;
            }
            if (!tbMailCount.Text.Contains("@"))
            {
                MessageBox.Show("邮箱账号错误，请重新设置");
                return;
            }
            Properties.Settings.Default.MailCount = tbMailCount.Text;
            Properties.Settings.Default.MailPassword = tbMailPassword.Text;
            Properties.Settings.Default.MailSMTP = tbMailSMTP.Text;
            Properties.Settings.Default.MailPort = tbMailPort.Text;
            Properties.Settings.Default.MailTo = tbMailTo.Text;
            Properties.Settings.Default.MailCC = tbMailCC.Text;
            Properties.Settings.Default.Save();
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
