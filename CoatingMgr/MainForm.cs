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
        private SqlLiteHelper sqlLiteHelper = null;
        private string _userName = "";
        private string _userPermission = "";

        private ScanerHook barcodeListener = new ScanerHook();

        public MainForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            InitView();
            InitData();
        }

        public MainForm(string userName, string userPermission)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            _userName = userName;
            _userPermission = userPermission;
            InitView();
            InitData();

            //第一次启动程序时启线程分析告警数据并发通知邮件
            Task task = new Task(AnalysisWarn);
            task.Start();

            //设置定时器每天8：30分析告警数据并发通知邮件
            System.Timers.Timer timer = new System.Timers.Timer
            {
                Enabled = true,
                Interval = 60000//执行间隔时间,单位为毫秒;此时时间间隔为1分钟  
            };
            timer.Start();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(AnalysisWarnOnTime);

            //添加扫码枪监听
            barcodeListener.ScanerEvent += Listener_ScanerEvent;
            barcodeListener.Start();
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

        private void Listener_ScanerEvent(ScanerHook.ScanerCodes codes)
        {
            string barcode = codes.Result;
            Logger.Instance.WriteLog(barcode);
            if (this.mainPanel.Controls[0].Name.Equals("FormIn"))
            {
                FormIn formIn = (FormIn)(this.mainPanel.Controls[0]);
                formIn.BarCodeInputEnd(barcode);
            }
            else if (this.mainPanel.Controls[0].Name.Equals("FormOut"))
            {
                FormOut formOut = (FormOut)(this.mainPanel.Controls[0]);
                formOut.BarCodeInputEnd(barcode);
            }
            else if (this.mainPanel.Controls[0].Name.Equals("FormStir"))
            {
                FormStir formStir = (FormStir)(this.mainPanel.Controls[0]);
                formStir.BarCodeInputEnd(barcode);
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
            FormStock formStock = new FormStock(_userName, _userPermission)
            {
                TopLevel = false,
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.None,
                Dock = System.Windows.Forms.DockStyle.Fill
            };

            this.mainPanel.Controls.Clear();
            this.mainPanel.Controls.Add(formStock);
            formStock.Show();
        }

        private void InitData()
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
                Logger.Instance.WriteLog(e.Message);
            }
        }

        private void FormClose()
        {
            Form form = (Form)(this.mainPanel.Controls[0]);
            form.Close();
            this.mainPanel.Controls.Clear();
        }

        private void BtnStock_Click(object sender, EventArgs e)
        {
            if (this.mainPanel.Controls[0].Name.Equals("FormStock"))
            {
                return;
            }
            FormClose();
            FormStock formStock = new FormStock(_userName, _userPermission)
            {
                TopLevel = false,
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.None,
                Dock = System.Windows.Forms.DockStyle.Fill
            };

            this.mainPanel.Controls.Add(formStock);
            formStock.Show();
        }

        private void BtnStockDetail_Click(object sender, EventArgs e)
        {
            if (this.mainPanel.Controls[0].Name.Equals("FormStockDetail"))
            {
                return;
            }
            FormClose();
            FormStockDetail formStockDetail = new FormStockDetail(_userName, _userPermission)
            {
                TopLevel = false,
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.None,
                Dock = System.Windows.Forms.DockStyle.Fill
            };

            this.mainPanel.Controls.Add(formStockDetail);
            formStockDetail.Show();
        }

        private void BtnIn_Click(object sender, EventArgs e)
        {
            if (this.mainPanel.Controls[0].Name.Equals("FormIn"))
            {
                return;
            }
            FormClose();
            FormIn formIn = new FormIn(_userName)
            {
                TopLevel = false,
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.None,
                Dock = System.Windows.Forms.DockStyle.Fill
            };
            this.mainPanel.Controls.Add(formIn);
            formIn.Show();
        }

        private void BtnOut_Click(object sender, EventArgs e)
        {
            if (this.mainPanel.Controls[0].Name.Equals("FormOut"))
            {
                return;
            }
            FormClose();
            FormOut formOut = new FormOut(_userName)
            {
                TopLevel = false,
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.None,
                Dock = System.Windows.Forms.DockStyle.Fill
            };
            this.mainPanel.Controls.Add(formOut);
            formOut.Show();
        }

        private void BtnStir_Click(object sender, EventArgs e)
        {
            if (this.mainPanel.Controls[0].Name.Equals("FormStir"))
            {
                return;
            }
            FormClose();
            FormStir formStir = new FormStir(_userName, _userPermission)
            {
                TopLevel = false,
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.None,
                Dock = System.Windows.Forms.DockStyle.Fill
            };
            this.mainPanel.Controls.Add(formStir);
            formStir.Show();
        }

        private void BtnWarn_Click(object sender, EventArgs e)
        {
            if (this.mainPanel.Controls[0].Name.Equals("FormWarn"))
            {
                return;
            }
            FormClose();
            FormWarn formWarn = new FormWarn(_userName)
            {
                TopLevel = false,
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.None,
                Dock = System.Windows.Forms.DockStyle.Fill
            };
            this.mainPanel.Controls.Add(formWarn);
            formWarn.Show();
        }

        //添加账号
        private void TSMIAddAccount_Click(object sender, EventArgs e)
        {
            FormAddAccount formAddAccount;
            if (this.mainPanel.Controls[0].Name.Equals("FormAccountManager"))
            {
                FormAccountManager formAccountMgr = (FormAccountManager)(this.mainPanel.Controls[0]);
                formAddAccount = new FormAddAccount(formAccountMgr);
            }
            else
            {
                formAddAccount = new FormAddAccount();
            }
            formAddAccount.Show();
        }

        //账号管理
        private void TSMIManagerAccount_Click(object sender, EventArgs e)
        {
            if (this.mainPanel.Controls[0].Name.Equals("FormAccountManager"))
            {
                return;
            }
            FormClose();
            FormAccountManager formAccountMgr = new FormAccountManager()
            {
                TopLevel = false,
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.None,
                Dock = System.Windows.Forms.DockStyle.Fill
            };
            this.mainPanel.Controls.Add(formAccountMgr);
            formAccountMgr.Show();
        }

        //查看库存日志
        private void TSMIStockLog_Click(object sender, EventArgs e)
        {
            FormClose();
            FormLog formLog = new FormLog()
            {
                TopLevel = false,
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.None,
                Dock = System.Windows.Forms.DockStyle.Fill
            };

            this.mainPanel.Controls.Add(formLog);
            formLog.InitData(Common.STOCKLOGTABLENAME);
            formLog.Show();
        }

        //查看调和日志
        private void TSMIStirLog_Click(object sender, EventArgs e)
        {
            FormClose();
            FormLog formLog = new FormLog()
            {
                TopLevel = false,
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.None,
                Dock = System.Windows.Forms.DockStyle.Fill
            };

            this.mainPanel.Controls.Add(formLog);
            formLog.InitData(Common.STIRLOGTABLENAME);
            formLog.Show();
        }

        //查看master文件
        private void TSMIMaster_Click(object sender, EventArgs e)
        {
            if (this.mainPanel.Controls[0].Name.Equals("FormMaster"))
            {
                return;
            }
            FormClose();
            FormMaster formMaster = new FormMaster(_userName, _userPermission)
            {
                TopLevel = false,
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.None,
                Dock = System.Windows.Forms.DockStyle.Fill
            };

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
            if (this.mainPanel.Controls[0].Name.Equals("FormMaster"))
            {
                FormMaster formMaster = (FormMaster)(this.mainPanel.Controls[0]);
                formMaster.UpdateData();
            }
        }

        //查看告警规则
        private void TSMIWarning_Click(object sender, EventArgs e)
        {
            if (this.mainPanel.Controls[0].Name.Equals("FormWarn"))
            {
                return;
            }
            FormClose();
            FormWarn formWarn = new FormWarn(_userName)
            {
                TopLevel = false,
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.None,
                Dock = System.Windows.Forms.DockStyle.Fill
            };
            this.mainPanel.Controls.Add(formWarn);
            formWarn.Show();
        }

        //设置告警规则
        private void TSMISetWarning_Click(object sender, EventArgs e)
        {
            FormSetWarn formSetWarn;
            if (this.mainPanel.Controls[0].Name.Equals("FormWarn"))
            {
                FormWarn formWarn = (FormWarn)(this.mainPanel.Controls[0]);
                formSetWarn = new FormSetWarn(formWarn, _userName);
            }
            else
            {
                formSetWarn = new FormSetWarn(null, _userName);
            }
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
            if (this.mainPanel.Controls[0].Name.Equals("FormStore"))
            {
                return;
            }
            FormClose();
            FormStore formStore = new FormStore()
            {
                TopLevel = false,
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.None,
                Dock = System.Windows.Forms.DockStyle.Fill
            };

            this.mainPanel.Controls.Add(formStore);
            formStore.Show();
        }

        //添加仓库
        private void TSMIStoreAdd_Click(object sender, EventArgs e)
        {
            FormStoreAdd formStoreAdd;
            if (this.mainPanel.Controls[0].Name.Equals("FormStore"))
            {
                FormStore formStore = (FormStore)(this.mainPanel.Controls[0]);
                formStoreAdd = new FormStoreAdd(formStore);
            }
            else
            {
                formStoreAdd = new FormStoreAdd();
            }

            formStoreAdd.Show();
        }

        //设置PLC的IP和端口
        private void TSMIPLCIP_Click(object sender, EventArgs e)
        {
            FormSetPLCIP formPLCIP = new FormSetPLCIP();
            formPLCIP.Show();
        }

        //设置扫码枪响应时长
        private void TSMIBCResponseTime_Click(object sender, EventArgs e)
        {
            FormSetBCResponse formSetBCResponse = new FormSetBCResponse();
            formSetBCResponse.Show();
        }

        //设置调试日志开关
        private void TSMISetLog_Click(object sender, EventArgs e)
        {
            FormSetDebugLog formSetDebugLog = new FormSetDebugLog();
            formSetDebugLog.Show();
        }

        //关于
        private void TSMIAbout_Click(object sender, EventArgs e)
        {
            FormAbout formAbout = new FormAbout();
            formAbout.Show();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            barcodeListener.Stop();
            GetSqlLiteHelper().CloseConnection();
            Application.Exit();
        }
    }
}
