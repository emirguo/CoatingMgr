using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using System.Data;

namespace CoatingMgr
{
    public partial class LoginForm : Form
    {
        string userPermission = "";

        public LoginForm()
        {
            InitializeComponent();
            Task task = new Task(InitDBPath);//线程判断并设置DB路径
            task.Start();
        }

        private void InitDBPath()
        {
            if (IsDBPathValid(Properties.Settings.Default.DBPath))
            {
                Common.DBPath = Properties.Settings.Default.DBPath;
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private Boolean IsAccountValid(string name, string pwd)
        {
            Boolean result = false;
            if (Properties.Settings.Default.user.Equals(name) && Properties.Settings.Default.pwd.Equals(pwd))
            {
                userPermission = Common.USER_MANAGER;
                return true;
            }
            try
            {
                DataTable dt = SQLServerHelper.Read(Common.ACCOUNTTABLENAME, new string[] { "账号", "密码" }, new string[] { "=", "=" }, new string[] { name,pwd });
                if (dt != null && dt.Rows.Count > 0)
                {
                    userPermission = dt.Rows[0]["权限"].ToString();
                    result = true;
                }
            }
            catch (Exception e)
            {
                Logger.Instance.WriteLog(e.Message);
            }
            return result;
        }

        private void Login()
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
            if (IsDBPathValid(Common.DBPath))
            {
                if (IsAccountValid(tbUserName.Text.ToString(), tbPwd.Text.ToString()))
                {
                    this.Hide();
                    MainForm mainForm = new MainForm(tbUserName.Text.ToString(), userPermission);
                    mainForm.Show();
                }
                else
                {
                    MessageBox.Show("账号或密码错误！");
                }
            }
        }
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void TslSetDBInfo_Click(object sender, EventArgs e)
        {
            FormSetSQLInfo formSetMySQLInfo = new FormSetSQLInfo();
            formSetMySQLInfo.Show();
        }

        private void TslSetPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.ShowDialog();
            if (File.Exists(file.FileName))
            {
                string path = System.IO.Path.GetFullPath(file.FileName);
                if (IsDBPathValid(path))
                {
                    Properties.Settings.Default.DBPath = path;
                    Properties.Settings.Default.Save();
                    Common.DBPath = path;
                }
            }
        }

        private bool IsDBPathValid(string path)
        {
            if (Properties.Settings.Default.SQLIP.Equals(string.Empty)
                || Properties.Settings.Default.SQLPort.Equals(string.Empty)
                || Properties.Settings.Default.SQLUser.Equals(string.Empty)
                || Properties.Settings.Default.SQLPwd.Equals(string.Empty))
            {
                MessageBox.Show("数据库信息未设置，请先设置数据库信息");
                return false;
            }

            return true;
        }

        private void TbPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login();
            }
        }
    }
}
