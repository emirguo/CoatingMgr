using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace CoatingMgr
{
    public partial class FormConfirmStirInfo : Form
    {
        private FormStir _fatherForm;
        public FormConfirmStirInfo()
        {
            InitializeComponent();
        }

        public FormConfirmStirInfo(FormStir fatherForm)
        {
            InitializeComponent();
            _fatherForm = fatherForm;
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (tbUserName.Text.Length == 0)
            {
                MessageBox.Show("请输入账号");
                return;
            }
            if (tbPwd.Text.Length == 0)
            {
                MessageBox.Show("请输入密码");
                return;
            }

            DataTable dt = SQLServerHelper.Read(Common.ACCOUNTTABLENAME, new string[] { "账号", "密码" }, new string[] { "=", "=" }, new string[] { tbUserName.Text.ToString(), tbPwd.Text.ToString() });
            if (dt != null && dt.Rows.Count > 0)//判断账号是否已经存在
            {
                if (dt.Rows[0]["权限"].ToString().Equals("管理员"))
                {
                    _fatherForm.ManagerConfirmStirInfo(tbUserName.Text);
                    Close();
                }
                else
                {
                    MessageBox.Show("调和信息只有管理员才能确认！");
                }
            }
            else
            {
                MessageBox.Show("账号不存在");
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
