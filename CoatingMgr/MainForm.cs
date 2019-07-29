using System;
using System.Data.SQLite;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
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
        private FormStore formStore;
        private FormLog formLog;

        private SqlLiteHelper sqlLiteHelper = null;
        private string _userName = "";
        private string _userPermission = "";

        private ScanerHook listener = new ScanerHook();

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

            //第一次启动程序时启线程分析告警数据并发通知邮件
            Task task = new Task(AnalysisWarn);
            task.Start();

            //设置定时器每天8：30分析告警数据并发通知邮件
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Enabled = true;
            timer.Interval = 60000;//执行间隔时间,单位为毫秒;此时时间间隔为1分钟  
            timer.Start();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(AnalysisWarnOnTime);
        }

        private void AnalysisWarnOnTime(object source, ElapsedEventArgs e)
        {
            if (DateTime.Now.Hour == 8 && DateTime.Now.Minute == 30)  //如果当前时间是8点30分
            {
                AnalysisWarn();
            }    
        }

        private void AnalysisWarn()
        {
            SQLiteDataReader dataReader = GetSqlLiteHelper().ReadFullTable(Common.MASTERTABLENAME);
            if (dataReader == null || !dataReader.HasRows)
            {
                MessageBox.Show("Master文件不存在，请先导入Master文件！");
            }

            Common.AnalysisWarn();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(formStock);
            formStock.Show();

            listener.ScanerEvent += Listener_ScanerEvent;
            listener.Start();
        }

        private void Listener_ScanerEvent(ScanerHook.ScanerCodes codes)
        {
            string barcode = codes.Result;
            if (this.mainPanel.Controls[0].Name.Equals("FormIn"))
            {
                formIn.BarCodeInputEnd();
            }
            else if (this.mainPanel.Controls[0].Name.Equals("FormOut"))
            {
                formOut.BarCodeInputEnd();
            }
            else if (this.mainPanel.Controls[0].Name.Equals("FormStir"))
            {
                formStir.BarCodeInputEnd();
            }
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

            formStore = new FormStore()
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
        }

        private void InitDataBase()
        {
            try
            {
                //创建账户数据表
                GetSqlLiteHelper().CreateTable(Common.ACCOUNTTABLENAME, Common.ACCOUNTTABLECOLUMNS, Common.ACCOUNTTABLECOLUMNSTYPE);
                //创建仓库数据表
                GetSqlLiteHelper().CreateTable(Common.STORETABLENAME, Common.STORETABLECOLUMNS, Common.STORETABLECOLUMNSTYPE);
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
            if (this.mainPanel.Controls[0].Name.Equals("FormStock"))
            {
                return;
            }
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(formStock);
            formStock.Show();
            formStock.UpdateData();
        }

        private void BtnIn_Click(object sender, EventArgs e)
        {
            if (this.mainPanel.Controls[0].Name.Equals("FormIn"))
            {
                return;
            }
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(formIn);
            formIn.Clear();
            formIn.Show();
        }

        private void BtnOut_Click(object sender, EventArgs e)
        {
            if (this.mainPanel.Controls[0].Name.Equals("FormOut"))
            {
                return;
            }
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(formOut);
            formOut.Clear();
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
        private SynchronizationContext m_SyncContext = null;
        private void TSMIImportMaster_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Filter = "Excel文件|*.xls;*.xlsx"
            };
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                m_SyncContext = SynchronizationContext.Current;
                Common.ShowProgress();
                string fileName = fileDialog.FileName;//得到文件所在位置。
                Thread t = new Thread(new ParameterizedThreadStart(ImportExceAndSaveToDB));//起线程导入excel并存表
                t.Start(fileName);//线程传递文件名
            }
        }

        private void ImportExceAndSaveToDB(object fileName)
        {
            ExcelHelper.ImportExcel(fileName.ToString());
            m_SyncContext.Post(UpdateUIAfterThread, "");//线程结束后更新UI
        }

        private void UpdateUIAfterThread(object obj)
        {
            Common.CloseProgress();
            formMaster.UpdateData();
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

        //管理仓库
        private void TSMIStore_Click(object sender, EventArgs e)
        {
            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(formStore);
            formStore.Show();
            formStore.UpdateData();
        }

        //添加仓库
        private void TSMIStoreAdd_Click(object sender, EventArgs e)
        {
            Form formStoreAdd = new FormStoreAdd(this.formStore);
            formStoreAdd.Show();
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
            Application.Exit();
        }
    }
}
