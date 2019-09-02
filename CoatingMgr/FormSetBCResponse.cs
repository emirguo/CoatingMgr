using System;
using System.Windows.Forms;

namespace CoatingMgr
{
    public partial class FormSetBCResponse : Form
    {
        public FormSetBCResponse()
        {
            InitializeComponent();
            if (Properties.Settings.Default.BCResponseTime > 0)
            {
                double time = (double)(Properties.Settings.Default.BCResponseTime) / 1000;
                this.tbTime.Text = time + "";
            }
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            double time = 500;
            try
            {
                time = Convert.ToDouble(this.tbTime.Text)*1000;
            }
            catch
            {
                MessageBox.Show("响应时长设置错误，请重新输入");
                return;
            }
            Properties.Settings.Default.BCResponseTime = (int) time;
            Properties.Settings.Default.Save();
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
