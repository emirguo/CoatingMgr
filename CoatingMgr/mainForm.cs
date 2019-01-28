using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        private FormAccountManager formAccountMgr;
        private FormLog formLog;

        private SqlLiteHelper sqlLiteHelper;
        private string _userName = "";

        public MainForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            InitView();
            InitDataBase();
        }

        public MainForm(string userName)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            _userName = userName;
            InitView();
            InitDataBase();
        }

        private void InitView()
        {
            formStock = new FormStock(_userName)
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

            formStir = new FormStir(_userName)
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

            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(formStock);
            formStock.Show();

        }

        private void InitDataBase()
        {
            try
            {
                sqlLiteHelper = SqlLiteHelper.GetInstance();

                //创建账户数据表
                sqlLiteHelper.CreateTable(Common.ACCOUNTTABLENAME, new string[] { "id","账号", "密码", "权限" }, new string[] { "INTEGER PRIMARY KEY AUTOINCREMENT", "TEXT", "TEXT", "TEXT" });
                //创建入库数据表
                sqlLiteHelper.CreateTable(Common.INSTOCKTABLENAME, new string[] { "id","条形码", "名称", "颜色", "类型", "标准重量", "适用机型", "生产日期", "有效期", "仓库名称", "操作员", "操作时间", "操作类型", "告警类型", "备注" },
                            new string[] { "INTEGER PRIMARY KEY AUTOINCREMENT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT" });
                //创建出库数据表
                sqlLiteHelper.CreateTable(Common.OUTSTOCKTABLENAME, new string[] { "id", "条形码", "名称", "颜色", "类型", "标准重量", "适用机型", "生产日期", "有效期", "仓库名称", "操作员", "操作时间", "操作类型", "告警类型", "备注" },
                            new string[] { "INTEGER PRIMARY KEY AUTOINCREMENT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT" });
                //创建库存管理数据表
                sqlLiteHelper.CreateTable(Common.STOCKMANAGERTABLENAME, new string[] { "id", "条形码", "名称", "颜色", "类型", "标准重量", "适用机型", "生产日期", "有效期", "仓库名称", "操作员", "操作时间", "操作类型", "告警类型", "备注" },
                            new string[] { "INTEGER PRIMARY KEY AUTOINCREMENT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT" });
                //创建告警数据表
                sqlLiteHelper.CreateTable(Common.WARNMANAGERTABLENAME, new string[] { "id", "条形码", "名称", "颜色", "类型", "标准重量", "适用机型", "生产日期", "有效期", "仓库名称", "操作员", "操作时间", "操作类型", "告警类型", "备注" },
                            new string[] { "INTEGER PRIMARY KEY AUTOINCREMENT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT" });
                //创建日志数据表
                sqlLiteHelper.CreateTable(Common.LOGTABLENAME, new string[] { "id", "条形码", "名称", "颜色", "类型", "标准重量", "适用机型", "生产日期", "有效期", "仓库名称", "操作员", "操作时间", "操作类型", "告警类型", "备注" },
                            new string[] { "INTEGER PRIMARY KEY AUTOINCREMENT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT" });

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

        private void TSMIAddAccount_Click(object sender, EventArgs e)
        {
            Form formAddAccount = new FormAddAccount(this.formAccountMgr);
            formAddAccount.Show();
        }

        private void TSMIManagerAccount_Click(object sender, EventArgs e)
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(formAccountMgr);
            formAccountMgr.Show();
            formAccountMgr.UpdateData();
        }

        private void TSMILog_Click(object sender, EventArgs e)
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(formLog);
            formLog.Show();
            formLog.UpdateData();
        }

        private void TSMIExportLog_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Load(sqlLiteHelper.ReadFullTable(Common.LOGTABLENAME));
            ExcelHelper.ExportExcel(dt);
        }

        private void TSMIImportMaster_Click(object sender, EventArgs e)
        {
            ExcelHelper.ImportExcel();
        }
    }
}
