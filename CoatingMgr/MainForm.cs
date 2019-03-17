﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoatingMgr
{
    public partial class MainForm : Form
    {
        private FormStock formStock;
        private FormIn formIn;
        private FormOut formOut;
        private FormStir formStir;
        private FormWarn formWarn;
        private FormMaster formMaster;
        private FormAccountManager formAccountMgr;
        private FormLog formLog;

        private SqlLiteHelper sqlLiteHelper = null;
        private string _userName = "";
        private string _userPermission = "";

        public MainForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            InitView();
            InitDataBase();
        }

        public MainForm(string userName, string userPermission)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            _userName = userName;
            _userPermission = userPermission;
            InitView();
            InitDataBase();
        }

        private SqlLiteHelper GetSqlLiteHelper()
        {
            if (sqlLiteHelper == null)
            {
                sqlLiteHelper = SqlLiteHelper.GetInstance();
            }
            return sqlLiteHelper;
        }

        private void InitView()
        {
            formStock = new FormStock(_userName, _userPermission)
            {
                TopLevel = false,
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.None,
                Dock = System.Windows.Forms.DockStyle.Fill
            };

            formIn = new FormIn(_userName)
            {
                TopLevel = false,
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.None,
                Dock = System.Windows.Forms.DockStyle.Fill
            };

            formOut = new FormOut(_userName)
            {
                TopLevel = false,
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.None,
                Dock = System.Windows.Forms.DockStyle.Fill
            };

            formStir = new FormStir(_userName, _userPermission)
            {
                TopLevel = false,
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.None,
                Dock = System.Windows.Forms.DockStyle.Fill
            };

            formWarn = new FormWarn(_userName)
            {
                TopLevel = false,
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.None,
                Dock = System.Windows.Forms.DockStyle.Fill
            };

            formAccountMgr = new FormAccountManager()
            {
                TopLevel = false,
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.None,
                Dock = System.Windows.Forms.DockStyle.Fill
            };

            formLog = new FormLog()
            {
                TopLevel = false,
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.None,
                Dock = System.Windows.Forms.DockStyle.Fill
            };

            formMaster = new FormMaster(_userName, _userPermission)
            {
                TopLevel = false,
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.None,
                Dock = System.Windows.Forms.DockStyle.Fill
            };

            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(formStock);
            formStock.Show();

            //Common.SendMailLocalhost();

        }

        private void InitDataBase()
        {
            try
            {
                //创建账户数据表
                GetSqlLiteHelper().CreateTable(Common.ACCOUNTTABLENAME, Common.ACCOUNTTABLECOLUMNS, Common.ACCOUNTTABLECOLUMNSTYPE);
                //创建入库数据表
                GetSqlLiteHelper().CreateTable(Common.INSTOCKTABLENAME, Common.INSTOCKTABLECOLUMNS, Common.INSTOCKTABLECOLUMNSTYPE);
                //创建出库数据表
                //GetSqlLiteHelper().CreateTable(Common.OUTSTOCKTABLENAME, Common.OUTSTOCKTABLECOLUMNS, Common.OUTSTOCKTABLECOLUMNSTYPE);
                //创建库存管理数据表
                GetSqlLiteHelper().CreateTable(Common.STOCKCOUNTTABLENAME, Common.STOCKCOUNTTABLECOLUMNS, Common.STOCKCOUNTTABLECOLUMNSTYPE);
                //创建告警数据表
                GetSqlLiteHelper().CreateTable(Common.WARNMANAGERTABLENAME, Common.WARNMANAGERTABLECOLUMNS, Common.WARNMANAGERTABLECOLUMNSTYPE);
                //创建库存日志数据表
                GetSqlLiteHelper().CreateTable(Common.STOCKLOGTABLENAME, Common.STOCKLOGTABLECOLUMNS, Common.STOCKLOGTABLECOLUMNSTYPE);
                //创建调和日志数据表
                GetSqlLiteHelper().CreateTable(Common.STIRLOGTABLENAME, Common.STIRLOGTABLECOLUMNS, Common.STIRLOGTABLECOLUMNSTYPE);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
        private void BtnStock_Click(object sender, EventArgs e)
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(formStock);
            formStock.Show();
            formStock.UpdateData();
        }

        private void BtnIn_Click(object sender, EventArgs e)
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(formIn);
            formIn.Show();
        }

        private void BtnOut_Click(object sender, EventArgs e)
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(formOut);
            formOut.Show();
        }

        private void BtnStir_Click(object sender, EventArgs e)
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(formStir);
            formStir.Show();
        }

        private void BtnWarn_Click(object sender, EventArgs e)
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(formWarn);
            formWarn.Show();
        }

        //添加账号
        private void TSMIAddAccount_Click(object sender, EventArgs e)
        {
            Form formAddAccount = new FormAddAccount(this.formAccountMgr);
            formAddAccount.Show();
        }

        //账号管理
        private void TSMIManagerAccount_Click(object sender, EventArgs e)
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(formAccountMgr);
            formAccountMgr.Show();
            formAccountMgr.UpdateData();
        }

        //查看库存日志
        private void TSMIStockLog_Click(object sender, EventArgs e)
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(formLog);
            formLog.InitData(Common.STOCKLOGTABLENAME);
            formLog.Show();
            formLog.UpdateData();
        }

        //查看调和日志
        private void TSMIStirLog_Click(object sender, EventArgs e)
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(formLog);
            formLog.InitData(Common.STIRLOGTABLENAME);
            formLog.Show();
            formLog.UpdateData();
        }

        //查看master文件
        private void TSMIMaster_Click(object sender, EventArgs e)
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(formMaster);
            formMaster.Show();
        }

        //导入master文件
        private void TSMIImportMaster_Click(object sender, EventArgs e)
        {
            DataTable datatable = ExcelHelper.ImportExcel();
            if (datatable != null)
            {
                GetSqlLiteHelper().SaveDataTableToDB(datatable, Common.MASTERTABLENAME);
            }
            formWarn.UpdateData();
        }

        //查看告警规则
        private void TSMIWarning_Click(object sender, EventArgs e)
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(formWarn);
            formWarn.Show();
            formWarn.UpdateData();
        }

        //设置告警规则
        private void TSMISetWarning_Click(object sender, EventArgs e)
        {
            FormSetWarn formSetWarn = new FormSetWarn(formWarn, _userName);
            formSetWarn.Show();
        }

        //设置告警邮件信息
        private void TSMIMailInfo_Click(object sender, EventArgs e)
        {
            FormWarnMailInfo formWarnMailInfo = new FormWarnMailInfo();
            formWarnMailInfo.Show();
        }

        //关于
        private void TSMIAbout_Click(object sender, EventArgs e)
        {
            FormAbout formAbout = new FormAbout();
            formAbout.Show();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            GetSqlLiteHelper().CloseConnection();
        }
                
    }
}
