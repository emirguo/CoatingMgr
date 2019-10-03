using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.SQLite;

namespace CoatingMgr
{
    public partial class FormAddAccount : Form
    {
        private bool _modifyModel = false;
        private int _id = 0;
        private string _name = "";
        private FormAccountManager _fatherForm = null;
        private static string _tableName = Common.ACCOUNTTABLENAME;

        public FormAddAccount()
        {
            InitializeComponent();
            InitData();
        }
        public FormAddAccount(FormAccountManager fatherForm)
        {
            InitializeComponent();
            InitData();
            _fatherForm = fatherForm;
        }

        public FormAddAccount(FormAccountManager fatherForm, int id, string name, string pwd, string permission)
        {
            InitializeComponent();
            InitData();
            _fatherForm = fatherForm;
            _id = id;
            _name = name;
            _modifyModel = true;
            this.Text = "修改账户";
            tbUserName.Text = name;
            tbPwd.Text = pwd;
            if (permission.Equals("管理员"))
            {
                cbAccountPermission.SelectedIndex = 1;
            }
            lbTitle.Text = "修改账户";
            btnAdd.Text = "修改";
        }

        private void InitData()
        {
            cbAccountPermission.Items.Add("操作员");
            cbAccountPermission.Items.Add("管理员");
            cbAccountPermission.SelectedIndex = 0;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
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

            if (_modifyModel)
            {
                DataTable dt = SQLServerHelper.Read(_tableName, new string[] { "账号" }, new string[] { "=" }, new string[] { tbUserName.Text.ToString() });
                if (dt.Rows.Count > 0 && dt.Rows[0]["账号"].Equals(_name))//判断账号是否已经存在且不是当前账号
                {
                    MessageBox.Show("账户已经存在");
                    return;
                }
                else
                {
                    SQLServerHelper.Update(_tableName, new string[] { "账号", "密码", "权限" }, new string[] { tbUserName.Text.ToString(), tbPwd.Text.ToString(), cbAccountPermission.Text.ToString() }, "id", _id + "");
                }                
            }
            else
            {
                DataTable dt = SQLServerHelper.Read(_tableName, new string[] { "账号" }, new string[] { "=" }, new string[] { tbUserName.Text.ToString() });
                if (dt != null && dt.Rows.Count > 0)//判断账号是否已经存在
                {
                    MessageBox.Show("账户已经存在");
                    return;
                }
                else
                {
                    SQLServerHelper.Insert(_tableName, new string[] { "账号", "密码", "权限" },
                        new string[] { tbUserName.Text.ToString(), tbPwd.Text.ToString(), cbAccountPermission.Text.ToString() });
                }
            }
            if (_fatherForm != null)
            {
                _fatherForm.UpdateData();
            }
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
