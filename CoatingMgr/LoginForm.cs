using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace CoatingMgr
{
    public partial class LoginForm : Form
    {
        string userPermission = "";

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            InitDBPath();
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
                SqlLiteHelper sqlLiteHelper = SqlLiteHelper.GetInstance();
                string query = "select * from account where 账号=" + "'" + name + "'" + " and 密码=" + "'" + pwd + "'";
                SQLiteDataReader dataReader = sqlLiteHelper.ExecuteQuery(query);
                if (dataReader.HasRows && dataReader.Read())
                {
                    userPermission = dataReader["权限"].ToString();
                    result = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
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

        private void TslDBPath_Click(object sender, EventArgs e)
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
            bool result = false;
            if (path == null || path.Length == 0 || path.Equals(""))
            {
                MessageBox.Show("数据库未设置，请先设置数据库文件路径");
            }
            else if (!File.Exists(path))
            {
                MessageBox.Show("数据库文件不存在，请设置数据库文件路径");
            }
            else if (!path.EndsWith(".db"))
            {
                MessageBox.Show("数据库文件无效，请选择后缀名为.db的数据库文件");
            }
            else
            {
                result = true;
            }

            return result;
        }
    }
}
