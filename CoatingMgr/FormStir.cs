using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace CoatingMgr
{
    public partial class FormStir : Form
    {
        private static string _tableName = Common.MASTERTABLENAME;
        private string _userName = "";
        private string _userPermission = "";
        private string _managerName = "";
        private double currStirTime = 0.0;
        private string ratio1 = "", ratio2 = "", ratio3 = "", ratio4 = "";
        private int w_all = 0, w_C = 0, w_C_Fast = 0, w_C_Slow = 0, w_H = 0, w_H_Fast = 0, w_H_Slow = 0, w_TA = 0, w_TA_Fast = 0, w_TA_Slow = 0, w_TB = 0, w_TB_Fast = 0, w_TB_Slow = 0;
        private bool C_End = false, H_End = false, TA_End = false, TB_End = false;
        private enum Status
        {
            Stop,
            CoatingStart_Fast,
            CoatingPause_Fast,
            CoatingStart_Slow,
            CoatingPause_Slow,
            HardeningAgentStart_Fast,
            HardeningAgentPause_Fast,
            HardeningAgentStart_Slow,
            HardeningAgentPause_Slow,
            ThinnerAStart_Fast,
            ThinnerAPause_Fast,
            ThinnerAStart_Slow,
            ThinnerAPause_Slow,
            ThinnerBStart_Fast,
            ThinnerBPause_Fast,
            ThinnerBStart_Slow,
            ThinnerBPause_Slow
        }
        private Status CurrStatus = Status.Stop;

        private enum StirLogType
        {
            CoatingLog,
            HardeningLog,
            ThinnerALog,
            ThinnerBLog,
        }

        private bool isStirInfoConfirmed = false;

        AutoSize asc = new AutoSize();

        public FormStir()
        {
            InitializeComponent();
        }

        public FormStir(string userName, string userPermission)
        {
            InitializeComponent();
            _userName = userName;
            _userPermission = userPermission;
            this.timer.Stop();
        }

        private void FormStir_Load(object sender, EventArgs e)
        {
            asc.controllInitializeSize(this);
            InitData();
        }

        private void FormStir_SizeChanged(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
        }

        private void InitData()
        {
            lbUser.Text = _userName;
            ShowTime();
                        
            List<string> _cbSearchModel = SQLServerHelper.GetTypesOfColumn(_tableName, "适用机种", null, null, null);
            for (int i = 0; i < _cbSearchModel.Count; i++)
            {
                cbModel.Items.Add(_cbSearchModel[i]);
            }

            GetTemperatureAndHumidity();
        }

        //显示当前时间
        private void ShowTime()
        {
            lbTime.Text = "时间：" + DateTime.Now.ToString();
            new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        lbTime.BeginInvoke(new MethodInvoker(() =>
                            lbTime.Text = "时间：" + DateTime.Now.ToString()));
                    }
                    catch { }
                    Thread.Sleep(1000);
                }
            })
            { IsBackground = true }.Start();
        }

        private void GetTemperatureAndHumidity()
        {
            tbTemperature.Text = "32.6";
            tbHumidity.Text = "64.7";
        }

        private void CbModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbModel.SelectedIndex >= 0)
            {
                cbComponent.Items.Clear();
                cbComponent.Text = "";
                cbColor.Items.Clear();
                cbColor.Text = "";
                cbCoating.Items.Clear();
                cbCoating.Text = "";
                List<string> _cbSearchComponent = SQLServerHelper.GetTypesOfColumn(_tableName, "適用製品", new string[] { "适用机种" }, new string[] { "=" }, new string[] { cbModel.Text });
                for (int i = 0; i < _cbSearchComponent.Count; i++)
                {
                    cbComponent.Items.Add(_cbSearchComponent[i]);
                }
            }
        }

        private void CbComponent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbComponent.SelectedIndex >= 0)
            {
                cbColor.Items.Clear();
                cbColor.Text = "";
                cbCoating.Items.Clear();
                cbCoating.Text = "";
                List<string> _cbSearchColor = SQLServerHelper.GetTypesOfColumn(_tableName, "色番", new string[] { "适用机种", "適用製品" }, new string[] { "=", "=" }, new string[] { cbModel.Text, cbComponent.Text });
                for (int i = 0; i < _cbSearchColor.Count; i++)
                {
                    cbColor.Items.Add(_cbSearchColor[i]);
                }
            }
        }

        private void CbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbColor.SelectedIndex >= 0)
            {
                cbCoating.Items.Clear();
                cbCoating.Text = "";
                List<string> _cbSearchCoating = SQLServerHelper.GetTypesOfColumn(_tableName, "涂层", new string[] { "适用机种", "適用製品", "色番" }, new string[] { "=", "=", "=" }, new string[] { cbModel.Text, cbComponent.Text, cbColor.Text });
                for (int i = 0; i < _cbSearchCoating.Count; i++)
                {
                    cbCoating.Items.Add(_cbSearchCoating[i]);
                }
            }
        }

        private void BtnGetStirValues_Click(object sender, EventArgs e)
        {
            SetStirInfo();
        }

        private void BtnResetStirValues_Click(object sender, EventArgs e)
        {
            FormSetStirData formSetStirData = new FormSetStirData(this, tbTemperature.Text, tbHumidity.Text, tbRatio.Text);
            formSetStirData.Show();
        }

        /// <summary>
        /// 根据机型\部件\颜色等信息从Master文件中查找设置各色剂信息
        /// </summary>
        private void SetStirInfo()
        {
            if (cbModel.Text.Equals(""))
            {
                MessageBox.Show("请选择机型");
                return;
            }
            if (cbComponent.Text.Equals(""))
            {
                MessageBox.Show("请选择製品");
                return;
            }
            if (cbColor.Text.Equals(""))
            {
                MessageBox.Show("请选择色番");
                return;
            }
            if (cbCoating.Text.Equals(""))
            {
                MessageBox.Show("请选择涂层");
                return;
            }
            if (tbInputWeight.Text.Equals(""))
            {
                MessageBox.Show("请输入主剂重量");
                return;
            }
            if (tbSlowWeight.Text.Equals(""))
            {
                MessageBox.Show("请输入慢速重量");
                return;
            }
            if (!cbModel.Text.Equals("") && !cbComponent.Text.Equals("") && !cbColor.Text.Equals("") && !cbCoating.Text.Equals("") && !tbInputWeight.Text.Equals("") && !tbTemperature.Text.Equals("") && !tbHumidity.Text.Equals(""))
            {
                DataTable dt = SQLServerHelper.Read(_tableName, new string[] { "适用机种", "適用製品", "色番", "涂层" }, new string[] { "=", "=", "=", "=", }, new string[] { cbModel.Text, cbComponent.Text, cbColor.Text, cbCoating.Text });
                if (dt != null && dt.Rows.Count > 0)
                {
                    isStirInfoConfirmed = false;
                    ClearStirText();
                    foreach(DataRow dr in dt.Rows)
                    {
                        if (dr["种类"].ToString().Equals("色漆") || dr["种类"].ToString().Equals("清漆"))
                        {
                            tbName1.Text = dr["SAP品番"].ToString();
                            ratio1 = dr["调和比例"].ToString();
                        }
                        else if (dr["种类"].ToString().Equals("固化剂"))
                        {
                            tbName2.Text = dr["SAP品番"].ToString();
                            ratio2 = dr["调和比例"].ToString();
                        }
                        else if (dr["种类"].ToString().Equals("稀释剂"))
                        {
                            if (tbName3.Text.Equals(string.Empty))
                            {
                                tbName3.Text = dr["SAP品番"].ToString();
                                ratio3 = dr["调和比例"].ToString();
                            }
                            else
                            {
                                tbName4.Text = dr["SAP品番"].ToString();
                                ratio4 = dr["调和比例"].ToString();
                            }
                        }
                    }
                    tbRatio.Text = ratio1 + ":" + ratio2 + ":" + ratio3 + (ratio4.Equals("")?"" : ":" + ratio4 + "");
                    SetWeight();
                    ShowConfirmWindow();
                }
                else
                {
                    MessageBox.Show("Master文件中未找到相关数据");
                }
            }
        }

        private void ClearStirText()
        {
            ratio1 = "";
            ratio2 = "";
            ratio3 = "";
            ratio4 = "";
            tbRatio.Text = "";

            tbName1.Text = "";
            tbName2.Text = "";
            tbName3.Text = "";
            tbName4.Text = "";

            tbSetValue1.Text = "";
            tbSetValue2.Text = "";
            tbSetValue3.Text = "";
            tbSetValue4.Text = "";

            tbMeasurementTime1.Text = null;
            tbMeasurementTime2.Text = null;
            tbMeasurementTime3.Text = null;
            tbMeasurementTime4.Text = null;
            
            this.lbCurrentStatus.Text = "停止";

            w_all = 0;
            w_C = 0;
            w_C_Fast = 0;
            w_C_Slow = 0;
            w_H = 0;
            w_H_Fast = 0;
            w_H_Slow = 0;
            w_TA = 0;
            w_TA_Fast = 0;
            w_TA_Slow = 0;
            w_TB = 0;
            w_TB_Fast = 0;
            w_TB_Slow = 0;

            C_End = false;
            H_End = false;
            TA_End = false;
            TB_End = false;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (CurrStatus == Status.Stop)
            {
                return;
            }
            if (!PLCHelper.GetInstance().IsPLCConnect())
            {
                MessageBox.Show("PLC未连接，请先连接PLC设备");
                CurrStatus = Status.Stop;
                this.timer.Stop();
                this.lbCurrentStatus.Text = "停止";
                return;
            }

            currStirTime += 0.1;
            string str = currStirTime.ToString();
            int index = str.IndexOf(".");
            str = str.Substring(0, index + 2);

            GetMeasurementWeight();//称重并按状态注入油漆

            switch (CurrStatus)
            {
                case Status.CoatingStart_Fast:
                case Status.CoatingStart_Slow:
                    this.tbMeasurementTime1.Text = str;
                    int value1 = (int)((Convert.ToSingle(tbMeasurementValue1.Text)*100) / (Convert.ToSingle(tbSetValue1.Text)*100) * 100);
                    this.progressBar1.Value = value1 > 100?0: 100-value1;
                    SetCountProgressBar();
                    break;
                case Status.CoatingPause_Fast://快速注入完成，自动切换为慢速注入
                    this.tbMeasurementTime1.Text = str;
                    CurrStatus = Status.CoatingStart_Slow;
                    DoStir();
                    break;
                case Status.CoatingPause_Slow:
                    this.progressBar1.Value = 100;
                    SetCountProgressBar();
                    if (IsStirEnd())
                    {
                        StopStir();
                    }
                    break;
                case Status.HardeningAgentStart_Fast:
                case Status.HardeningAgentStart_Slow:
                    this.tbMeasurementTime2.Text = str;
                    int value2 = (int)((Convert.ToSingle(tbMeasurementValue2.Text)*100) / (Convert.ToSingle(tbSetValue2.Text)*100) * 100);
                    this.progressBar2.Value = value2 > 100 ? 0 : 100-value2;
                    SetCountProgressBar();
                    break;
                case Status.HardeningAgentPause_Fast:
                    this.tbMeasurementTime2.Text = str;
                    CurrStatus = Status.HardeningAgentStart_Slow;
                    DoStir();
                    break;
                case Status.HardeningAgentPause_Slow:
                    this.progressBar2.Value = 100;
                    SetCountProgressBar();
                    if (IsStirEnd())
                    {
                        StopStir();
                    }
                    break;
                case Status.ThinnerAStart_Fast:
                case Status.ThinnerAStart_Slow:
                    this.tbMeasurementTime3.Text = str;
                    int value3 = (int)((Convert.ToSingle(tbMeasurementValue3.Text)*100) / (Convert.ToSingle(tbSetValue3.Text)*100) * 100);
                    this.progressBar3.Value = value3 > 100 ? 0 : 100-value3;
                    SetCountProgressBar();
                    break;
                case Status.ThinnerAPause_Fast:
                    this.tbMeasurementTime3.Text = str;
                    CurrStatus = Status.ThinnerAStart_Slow;
                    DoStir();
                    break;
                case Status.ThinnerAPause_Slow:
                    this.progressBar3.Value = 100;
                    SetCountProgressBar();
                    if (IsStirEnd())
                    {
                        StopStir();
                    }
                    break;
                case Status.ThinnerBStart_Fast:
                case Status.ThinnerBStart_Slow:
                    this.tbMeasurementTime4.Text = str;
                    int value4 = (int)((Convert.ToSingle(tbMeasurementValue4.Text)*100) / (Convert.ToSingle(tbSetValue4.Text)*100) * 100);
                    this.progressBar4.Value = value4 > 100 ? 0 : 100-value4;
                    SetCountProgressBar();
                    break;
                case Status.ThinnerBPause_Fast:
                    this.tbMeasurementTime4.Text = str;
                    CurrStatus = Status.ThinnerBStart_Slow;
                    DoStir();
                    break;
                case Status.ThinnerBPause_Slow:
                    this.progressBar4.Value = 100;
                    SetCountProgressBar();
                    if (IsStirEnd())
                    {
                        StopStir();
                    }
                    break;
                default:
                    break;
            }
            
        }

        //获取倒入重量
        private void GetMeasurementWeight()
        {
            int weight = PLCHelper.GetInstance().GetWeight();
            this.tbTotalWeight.Text = Convert.ToDouble(weight) / 100 + "";
            switch (CurrStatus)
            {
                case Status.CoatingStart_Fast:
                case Status.CoatingStart_Slow:
                    if (w_C > 0 && w_C_Fast > 0 && w_C_Slow > 0 && !C_End)
                    {
                        double putWeight1 = weight - (H_End ? w_H : 0) - (TA_End ? w_TA : 0) - (TB_End ? w_TB : 0);
                        if (putWeight1 > 0.00001)
                        {
                            tbMeasurementValue1.Text = putWeight1 / 100 + "";
                        }
                        else
                        {
                            tbMeasurementValue1.Text = 0 + "";
                        }
                        if (putWeight1 >= w_C)
                        {
                            CurrStatus = Status.CoatingPause_Slow;
                            DoStir();
                            C_End = true;
                        }
                        else if (putWeight1 >= w_C_Fast)
                        {
                            CurrStatus = Status.CoatingPause_Fast;
                            DoStir();
                        }
                    }
                    break;
                case Status.HardeningAgentStart_Fast:
                case Status.HardeningAgentStart_Slow:
                    if (w_H > 0 && w_H_Fast > 0 && w_H_Slow > 0)
                    {
                        double putWeight2 = weight - (C_End ? w_C : 0) - (TA_End ? w_TA : 0) - (TB_End ? w_TB : 0);
                        if (putWeight2 > 0.00001)
                        {
                            tbMeasurementValue2.Text = putWeight2 / 100 + "";
                        }
                        else
                        {
                            tbMeasurementValue2.Text = 0 + "";
                        }
                        if (putWeight2 >= w_H)
                        {
                            CurrStatus = Status.HardeningAgentPause_Slow;
                            DoStir();
                            H_End = true;
                        }
                        else if (putWeight2 >= w_H_Fast)
                        {
                            CurrStatus = Status.HardeningAgentPause_Fast;
                            DoStir();
                        }
                    }
                    break;
                case Status.ThinnerAStart_Fast:
                case Status.ThinnerAStart_Slow:
                    if (w_TA > 0 && w_TA_Fast > 0 && w_TA_Slow > 0)
                    {
                        double putWeight3 = weight - (C_End ? w_C : 0) - (H_End ? w_H : 0) - (TB_End ? w_TB : 0);
                        if (putWeight3 > 0.00001)
                        {
                            tbMeasurementValue3.Text = putWeight3 / 100 + "";
                        }
                        else
                        {
                            tbMeasurementValue3.Text = 0 + "";
                        }
                        if (putWeight3 >= w_TA)
                        {
                            CurrStatus = Status.ThinnerAPause_Slow;
                            DoStir();
                            TA_End = true;
                        }
                        else if (putWeight3 >= w_TA_Fast)
                        {
                            CurrStatus = Status.ThinnerAPause_Fast;
                            DoStir();
                        }
                    }
                    break;
                case Status.ThinnerBStart_Fast:
                case Status.ThinnerBStart_Slow:
                    if (w_TB > 0 && w_TB_Fast > 0 && w_TB_Slow > 0)
                    {
                        double putWeight4 = weight - (C_End ? w_C : 0) - (H_End ? w_H : 0) - (TA_End ? w_TA : 0);
                        if (putWeight4 > 0.00001)
                        {
                            tbMeasurementValue4.Text = putWeight4 / 100 + "";
                        }
                        else
                        {
                            tbMeasurementValue4.Text = 0 + "";
                        }
                        if (putWeight4 >= w_TB)
                        {
                            CurrStatus = Status.ThinnerBPause_Slow;
                            DoStir();
                            TB_End = true;
                        }
                        else if (putWeight4 >= w_TB_Fast)
                        {
                            CurrStatus = Status.ThinnerBPause_Fast;
                            DoStir();
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        //设置倒入总量进度
        private void SetCountProgressBar()
        {
            double measurementWeight1 = tbMeasurementValue1.Text.Equals("") ? 0.00001f : Convert.ToSingle(tbMeasurementValue1.Text);
            double measurementWeight2 = tbMeasurementValue2.Text.Equals("") ? 0.00001f : Convert.ToSingle(tbMeasurementValue2.Text);
            double measurementWeight3 = tbMeasurementValue3.Text.Equals("") ? 0.00001f : Convert.ToSingle(tbMeasurementValue3.Text);
            double measurementWeight4 = tbMeasurementValue4.Text.Equals("") ? 0.00001f : Convert.ToSingle(tbMeasurementValue4.Text);
            double setWeight1 = tbSetValue1.Text.Equals("") ? 0.00001f : Convert.ToSingle(tbSetValue1.Text);
            double setWeight2 = tbSetValue2.Text.Equals("") ? 0.00001f : Convert.ToSingle(tbSetValue2.Text);
            double setWeight3 = tbSetValue3.Text.Equals("") ? 0.00001f : Convert.ToSingle(tbSetValue3.Text);
            double setWeight4 = tbSetValue4.Text.Equals("") ? 0.00001f : Convert.ToSingle(tbSetValue4.Text);

            int value5 = (int)((measurementWeight1 + measurementWeight2 + measurementWeight3 + measurementWeight4) / (setWeight1 + setWeight2 + setWeight3 + setWeight4) * 100);
            this.progressBar5.Value = value5 > 100 ? 100 : value5;
        }

        private bool IsStirInfoEnough()
        {
            bool result = false;
            if (!tbName1.Text.Equals("") && !tbSetValue1.Text.Equals(""))
            {
                result = true;
            }
            return result;
        }

        private void StopStir()
        {
            CurrStatus = Status.Stop;
            DoStir();
            SaveStirLog(StirLogType.CoatingLog);
            SaveStirLog(StirLogType.HardeningLog);
            SaveStirLog(StirLogType.ThinnerALog);
            SaveStirLog(StirLogType.ThinnerBLog);
            ClearStirText();
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            StopStir();
        }

        /// <summary>
        /// 根据状态实现各调和动作
        /// </summary>
        private void DoStir()
        {
            if (!PLCHelper.GetInstance().IsPLCConnect())
            {
                MessageBox.Show("PLC未连接，请先连接PLC设备");
                CurrStatus = Status.Stop;
                this.timer.Stop();
                this.lbCurrentStatus.Text = "停止";
                return;
            }
            switch (CurrStatus)
            {
                case Status.CoatingStart_Fast:
                    PLCHelper.GetInstance().CoatingFastOn();//开始快速注入涂料
                    SetStartStirText(tbMeasurementTime1);
                    this.lbCurrentStatus.Text = "正在注入涂料";
                    break;
                case Status.CoatingPause_Fast:
                    PLCHelper.GetInstance().CoatingFastOff();//停止快速注入涂料
                    break;
                case Status.CoatingStart_Slow:
                    PLCHelper.GetInstance().CoatingSlowOn();//开始慢速注入涂料
                    break;
                case Status.CoatingPause_Slow:
                    PLCHelper.GetInstance().CoatingSlowOff();//停止慢速注入涂料
                    SetPauseStirText();
                    this.lbCurrentStatus.Text = "暂停";
                    break;
                case Status.HardeningAgentStart_Fast:
                    PLCHelper.GetInstance().HardeningAgentFastOn();//开始快速注入固化剂
                    SetStartStirText(tbMeasurementTime2);
                    this.lbCurrentStatus.Text = "正在注入固化剂";
                    break;
                case Status.HardeningAgentPause_Fast:
                    PLCHelper.GetInstance().HardeningAgentFastOff();//停止快速注入固化剂
                    break;
                case Status.HardeningAgentStart_Slow:
                    PLCHelper.GetInstance().HardeningAgentSlowOn();//开始慢速注入固化剂
                    break;
                case Status.HardeningAgentPause_Slow:
                    PLCHelper.GetInstance().HardeningAgentSlowOff();//停止慢速注入固化剂
                    SetPauseStirText();
                    this.lbCurrentStatus.Text = "暂停";
                    break;
                case Status.ThinnerAStart_Fast:
                    PLCHelper.GetInstance().ThinnerAFastOn();//开始快速注入稀释剂A
                    SetStartStirText(tbMeasurementTime3);
                    this.lbCurrentStatus.Text = "正在注入稀释剂A";
                    break;
                case Status.ThinnerAPause_Fast:
                    PLCHelper.GetInstance().ThinnerAFastOff();//停止快速注入稀释剂A
                    break;
                case Status.ThinnerAStart_Slow:
                    PLCHelper.GetInstance().ThinnerASlowOn();//开始慢速注入稀释剂A
                    break;
                case Status.ThinnerAPause_Slow:
                    PLCHelper.GetInstance().ThinnerASlowOff();//停止慢速注入稀释剂A
                    SetPauseStirText();
                    this.lbCurrentStatus.Text = "暂停";
                    break;
                case Status.ThinnerBStart_Fast:
                    PLCHelper.GetInstance().ThinnerBFastOn();//开始快速注入稀释剂B
                    SetStartStirText(tbMeasurementTime4);
                    this.lbCurrentStatus.Text = "正在注入稀释剂B";
                    break;
                case Status.ThinnerBPause_Fast:
                    PLCHelper.GetInstance().ThinnerBFastOff();//停止快速注入稀释剂B
                    break;
                case Status.ThinnerBStart_Slow:
                    PLCHelper.GetInstance().ThinnerBSlowOn();//开始慢速注入稀释剂B
                    break;
                case Status.ThinnerBPause_Slow:
                    PLCHelper.GetInstance().ThinnerBSlowOff();//停止慢速注入稀释剂B
                    SetPauseStirText();
                    this.lbCurrentStatus.Text = "暂停";
                    break;
                case Status.Stop:
                    PLCHelper.GetInstance().Stop();//PLC停止动作
                    this.timer.Stop();
                    currStirTime = 0;
                    this.lbCurrentStatus.Text = "停止";
                    break;
                default:
                    break;
            }
        }

        private bool IsStirEnd()
        {
            bool result = false;
            if ((w_C > 0 && C_End)  //涂料已完成注入
                && ((w_H > 0 && H_End) || w_H <= 0) //固化剂已完成注入或无需注入固化剂
                && ((w_TA > 0 && TA_End) || w_TA <= 0) //凝固剂A已完成注入或无需注入凝固剂A
                && ((w_TB > 0 && TB_End) || w_TB <= 0)) //凝固剂B已完成注入或无需注入凝固剂B
            {
                result = true;
            }
            return result;
        }

        //保存调和日志
        //"id", "机种", "製品", "颜色", "涂层", "温度", "湿度", "调和比例", "类型", "名称", "条形码", "设定重量", "倒入重量", "计量时间", "操作员", "操作日期", "操作时间", "确认主管", "备注"
        private void SaveStirLog(StirLogType logType)
        {
            switch (logType)
            {
                case StirLogType.CoatingLog:
                    if (!tbName1.Text.Equals("") && !tbBarCode1.Text.Equals("") && !tbSetValue1.Text.Equals("") && !tbMeasurementValue1.Text.Equals("") && !tbMeasurementTime1.Text.Equals(""))
                    {
                        SQLServerHelper.Insert(Common.STIRLOGTABLENAME, new string[] { "机种", "製品", "颜色", "涂层", "温度", "湿度", "调和比例", "类型", "名称", "条形码", "设定重量", "倒入重量", "计量时间", "操作员", "操作日期", "操作时间", "确认主管", "备注" },
                            new string[] { cbModel.Text, cbComponent.Text, cbColor.Text, cbCoating.Text, tbTemperature.Text, tbHumidity.Text, tbRatio.Text, "涂料", tbName1.Text, tbBarCode1.Text, tbSetValue1.Text, tbMeasurementValue1.Text, tbMeasurementTime1.Text, _userName, DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("HH:mm:ss"), _managerName, string.Empty });
                    }
                    break;
                case StirLogType.HardeningLog:
                    if (!tbName2.Text.Equals("") && !tbBarCode2.Text.Equals("") && !tbSetValue2.Text.Equals("") && !tbMeasurementValue2.Text.Equals("") && !tbMeasurementTime2.Text.Equals(""))
                    {
                        SQLServerHelper.Insert(Common.STIRLOGTABLENAME, new string[] { "机种", "製品", "颜色", "涂层", "温度", "湿度", "调和比例", "类型", "名称", "条形码", "设定重量", "倒入重量", "计量时间", "操作员", "操作日期", "操作时间", "确认主管", "备注" },
                            new string[] { cbModel.Text, cbComponent.Text, cbColor.Text, cbCoating.Text, tbTemperature.Text, tbHumidity.Text, tbRatio.Text, "固化剂", tbName2.Text, tbBarCode2.Text, tbSetValue2.Text, tbMeasurementValue2.Text, tbMeasurementTime2.Text, _userName, DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("HH:mm:ss"), _managerName, string.Empty });
                    }
                    break;
                case StirLogType.ThinnerALog:
                    if (!tbName3.Text.Equals("") && !tbBarCode3.Text.Equals("") && !tbSetValue3.Text.Equals("") && !tbMeasurementValue3.Text.Equals("") && !tbMeasurementTime3.Text.Equals(""))
                    {
                        SQLServerHelper.Insert(Common.STIRLOGTABLENAME, new string[] { "机种", "製品", "颜色", "涂层", "温度", "湿度", "调和比例", "类型", "名称", "条形码", "设定重量", "倒入重量", "计量时间", "操作员", "操作日期", "操作时间", "确认主管", "备注" },
                            new string[] { cbModel.Text, cbComponent.Text, cbColor.Text, cbCoating.Text, tbTemperature.Text, tbHumidity.Text, tbRatio.Text, "稀释剂A", tbName3.Text, tbBarCode3.Text, tbSetValue3.Text, tbMeasurementValue3.Text, tbMeasurementTime3.Text, _userName, DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("HH:mm:ss"), _managerName, string.Empty });
                    }
                    break;
                case StirLogType.ThinnerBLog:
                    if (!tbName4.Text.Equals("") && !tbBarCode4.Text.Equals("") && !tbSetValue4.Text.Equals("") && !tbMeasurementValue4.Text.Equals("") && !tbMeasurementTime4.Text.Equals(""))
                    {
                        SQLServerHelper.Insert(Common.STIRLOGTABLENAME, new string[] { "机种", "製品", "颜色", "涂层", "温度", "湿度", "调和比例", "类型", "名称", "条形码", "设定重量", "倒入重量", "计量时间", "操作员", "操作日期", "操作时间", "确认主管", "备注" }, 
                            new string[] { cbModel.Text, cbComponent.Text, cbColor.Text, cbCoating.Text, tbTemperature.Text, tbHumidity.Text, tbRatio.Text, "稀释剂B", tbName4.Text, tbBarCode4.Text, tbSetValue4.Text, tbMeasurementValue4.Text, tbMeasurementTime4.Text, _userName, DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("HH:mm:ss"), _managerName, string.Empty });
                    }
                    break;
                default:
                    break;
            }
            
        }

        private void SetPauseStirText()
        {
            this.timer.Stop();
        }

        /// <summary>
        /// 调和时设置计量时间
        /// </summary>
        private void SetStartStirText(TextBox tb)
        {
            if (tb.Text == null || tb.Text.Equals(""))
            {
                currStirTime = 0;
            }
            else
            {
                currStirTime = double.Parse(tb.Text);
            }
            this.timer.Start();
        }

        private bool IsBarCodeFromStock(string barcode)
        {
            bool result = false;
            bool hadInStock = false;
            bool hadOutStock = false;
            DataTable dt = SQLServerHelper.Read(Common.STOCKLOGTABLENAME, new string[] { "条形码" }, new string[] { "=" }, new string[] { barcode });
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    if (dr["操作类型"].ToString().Equals("入库"))
                    {
                        hadInStock = true;
                    }
                    else if (dr["操作类型"].ToString().Equals("出库"))
                    {
                        hadOutStock = true;
                    }
                }
            }
            else
            {
                MessageBox.Show("此条形码涂料还未入库，请先入库");
                return result;
            }
            if (hadInStock && hadOutStock)
            {
                result = true;
            }
            else if (!hadInStock)
            {
                MessageBox.Show("此条形码涂料还未入库，请先入库");
            }
            else if (!hadOutStock)
            {
                MessageBox.Show("此条形码涂料还未出库，请先出库");
            }
            return result;
        }

        /// <summary>
        /// 判断条形码是否有效
        /// </summary>
        /// SAP品番*种类*厂家*重量*批次号*连番*使用期限,例如：R-241(KAI) YR-614P(TAP)*A*G1000*18*20180219*0001*20190318
        private bool IsBarCodeValid(string barcode, string name)
        {
            bool result = false;
            if (!barcode.Equals(string.Empty))
            {
                string[] sArray = barcode.Split('*');
                if (sArray.Length == 7)
                {
                    if (!name.Equals(sArray[0]))//判断SAP品番是否一致
                    {
                        MessageBox.Show("涂料错误！");
                        return false;
                    }
                    if (DateTime.Compare(DateTime.Now, DateTime.ParseExact(sArray[6], "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)) > 0)
                    {
                        MessageBox.Show("涂料超过有效期！");
                        return false;
                    }
                    result = true;
                }
                else
                {
                    MessageBox.Show("条形码无效");
                }
            }
            else
            {
                MessageBox.Show("条形码无效");
            }
            return result;
        }

        public void BarCodeInputEnd(string barcode)
        {
            if (tbBarCode1.Focused)
            {
                BarCode1InputEnd(barcode);
            }
            else if (tbBarCode2.Focused)
            {
                BarCode2InputEnd(barcode);
            }
            else if (tbBarCode3.Focused)
            {
                BarCode3InputEnd(barcode);
            }
            else if (tbBarCode4.Focused)
            {
                BarCode4InputEnd(barcode);
            }
        }

        private DateTime _dt = DateTime.Now;  //定义一个成员函数用于保存每次的时间点
        private void TbBarCode1_KeyDown(object sender, KeyEventArgs e)
        {
            DateTime tempDt = DateTime.Now;          //保存按键按下时刻的时间点
            TimeSpan ts = tempDt.Subtract(_dt);     //获取时间间隔
            _dt = tempDt;
            if (ts.Milliseconds > 100)      //判断时间间隔，如果时间间隔大于100毫秒，则为手动输入，否则为扫码枪输入
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BarCode1InputEnd(this.tbBarCode1.Text);
                }
            }
        }

        private void TbBarCode2_KeyDown(object sender, KeyEventArgs e)
        {
            DateTime tempDt = DateTime.Now;          //保存按键按下时刻的时间点
            TimeSpan ts = tempDt.Subtract(_dt);     //获取时间间隔
            _dt = tempDt;
            if (ts.Milliseconds > 100)      //判断时间间隔，如果时间间隔大于100毫秒，则为手动输入，否则为扫码枪输入
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BarCode2InputEnd(this.tbBarCode2.Text);
                }
            }
        }

        private void TbBarCode3_KeyDown(object sender, KeyEventArgs e)
        {
            DateTime tempDt = DateTime.Now;          //保存按键按下时刻的时间点
            TimeSpan ts = tempDt.Subtract(_dt);     //获取时间间隔
            _dt = tempDt;
            if (ts.Milliseconds > 100)      //判断时间间隔，如果时间间隔大于100毫秒，则为手动输入，否则为扫码枪输入
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BarCode3InputEnd(this.tbBarCode3.Text);
                }
            }
        }

        private void TbBarCode4_KeyDown(object sender, KeyEventArgs e)
        {
            DateTime tempDt = DateTime.Now;          //保存按键按下时刻的时间点
            TimeSpan ts = tempDt.Subtract(_dt);     //获取时间间隔
            _dt = tempDt;
            if (ts.Milliseconds > 100)      //判断时间间隔，如果时间间隔大于100毫秒，则为手动输入，否则为扫码枪输入
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BarCode4InputEnd(this.tbBarCode4.Text);
                }
            }
        }

        private void BarCode1InputEnd(string barcode)
        {
            if (!barcode.Equals(string.Empty))
            {
                tbBarCode1.Text = barcode;
                if (!IsStirInfoEnough())
                {
                    MessageBox.Show("请先设置调和信息");
                    return;
                }
                if (!isStirInfoConfirmed)
                {
                    ShowConfirmWindow();
                    return;
                }
                if (!IsBarCodeFromStock(barcode))
                {
                    return;
                }
                if (IsBarCodeValid(barcode, tbName1.Text))//条形码正确
                {
                    CurrStatus = Status.CoatingStart_Fast;
                    DoStir();
                    this.tbBarCode2.Focus();
                }
                /*
                tbBarCode1.Text = barcode;
                CurrStatus = Status.CoatingStart_Fast;
                DoStir();
                this.tbBarCode2.Focus();
                */
            }
        }

        private void BarCode2InputEnd(string barcode)
        {
            if (!barcode.Equals(string.Empty))
            {
                this.tbBarCode2.Text = barcode;
                if (!IsStirInfoEnough())
                {
                    MessageBox.Show("请先设置调和信息");
                    return;
                }
                if (!isStirInfoConfirmed)
                {
                    ShowConfirmWindow();
                    return;
                }
                if (!IsBarCodeFromStock(barcode))
                {
                    return;
                }
                if (IsBarCodeValid(barcode, tbName2.Text))//条形码正确
                {
                    CurrStatus = Status.HardeningAgentStart_Fast;
                    DoStir();
                    this.tbBarCode3.Focus();
                }
                /*
                this.tbBarCode2.Text = barcode;
                CurrStatus = Status.HardeningAgentStart_Fast;
                DoStir();
                this.tbBarCode3.Focus();
                */
            }
        }

        private void BarCode3InputEnd(string barcode)
        {
            if (!barcode.Equals(string.Empty))
            {
                this.tbBarCode3.Text = barcode;
                if (!IsStirInfoEnough())
                {
                    MessageBox.Show("请先设置调和信息");
                    return;
                }
                if (!isStirInfoConfirmed)
                {
                    ShowConfirmWindow();
                    return;
                }
                if (!IsBarCodeFromStock(barcode))
                {
                    return;
                }
                if (IsBarCodeValid(barcode, tbName3.Text))//条形码正确
                {
                    CurrStatus = Status.ThinnerAStart_Fast;
                    DoStir();
                    this.tbBarCode4.Focus();
                }
                /*
                this.tbBarCode3.Text = barcode;
                CurrStatus = Status.ThinnerAStart_Fast;
                DoStir();
                this.tbBarCode4.Focus();
                */
            }
        }

        private void BarCode4InputEnd(string barcode)
        {
            if (!barcode.Equals(string.Empty))
            {
                this.tbBarCode4.Text = barcode;
                if (!IsStirInfoEnough())
                {
                    MessageBox.Show("请先设置调和信息");
                    return;
                }
                if (!isStirInfoConfirmed)
                {
                    ShowConfirmWindow();
                    return;
                }
                if (!IsBarCodeFromStock(barcode))
                {
                    return;
                }
                if (IsBarCodeValid(barcode, tbName4.Text))//条形码正确
                {
                    CurrStatus = Status.ThinnerBStart_Fast;
                    DoStir();
                }
                /*
                this.tbBarCode4.Text = barcode;
                CurrStatus = Status.ThinnerBStart_Fast;
                DoStir();
                */
            }
        }

        /// <summary>
        /// 根据调和比例和主剂重量计算其他色剂重量
        /// </summary>
        private void SetWeight()
        {
            if (tbInputWeight.Text != null && !tbInputWeight.Text.Equals("") && !tbName1.Text.ToString().Equals("") && !tbName2.Text.ToString().Equals("") && !tbName3.Text.ToString().Equals(""))
            {
                try
                {
                    double weight1 = double.Parse(tbInputWeight.Text);
                    tbSetValue1.Text = weight1.ToString();
                    tbSlowValue1.Text = weight1 * double.Parse(tbSlowWeight.Text) / 100 + "";

                    w_C = (int)(double.Parse(tbSetValue1.Text)*100);
                    w_C_Slow = (int)(double.Parse(tbSlowValue1.Text)*100);
                    w_C_Fast = w_C - w_C_Slow;

                    if (!ratio2.Equals(""))
                    {
                        double weight2 = weight1 * double.Parse(ratio2) / double.Parse(ratio1);
                        tbSetValue2.Text = weight2.ToString();
                        tbSlowValue2.Text = weight2 * double.Parse(tbSlowWeight.Text) / 100 + "";

                        w_H = (int)(double.Parse(tbSetValue2.Text) * 100);
                        w_H_Slow = (int)(double.Parse(tbSlowValue2.Text) * 100);
                        w_H_Fast = w_H - w_H_Slow;
                    }
                    if (!ratio3.Equals(""))
                    {
                        double weight3 = weight1 * double.Parse(ratio3) / double.Parse(ratio1);
                        tbSetValue3.Text = weight3.ToString();
                        tbSlowValue3.Text = weight3 * double.Parse(tbSlowWeight.Text) / 100 + "";

                        w_TA = (int)(double.Parse(tbSetValue3.Text) * 100);
                        w_TA_Slow = (int)(double.Parse(tbSlowValue3.Text) * 100);
                        w_TA_Fast = w_TA - w_TA_Slow;
                    }
                    if (!ratio4.Equals(""))
                    {
                        double weight4 = weight1 * double.Parse(ratio4) / double.Parse(ratio1);
                        tbSetValue4.Text = weight4.ToString();
                        tbSlowValue4.Text = weight4 * double.Parse(tbSlowWeight.Text) / 100 + "";

                        w_TB = (int)(double.Parse(tbSetValue4.Text) * 100);
                        w_TB_Slow = (int)(double.Parse(tbSlowValue4.Text) * 100);
                        w_TB_Fast = w_TB - w_TB_Slow;
                    }
                    w_all = w_C + w_H + w_TA + w_TB;
                    C_End = false;
                    H_End = false;
                    TA_End = false;
                    TB_End = false;
                }
                catch (Exception e)
                {
                    Logger.Instance.WriteLog(e.Message);
                }
            }
        }

        public void ResetRatioAndWeight(string temperature, string humidity, string ratio)
        {
            this.tbTemperature.Text = temperature;
            this.tbHumidity.Text = humidity;
            this.tbRatio.Text = ratio;
            ratio1 = "";
            ratio2 = "";
            ratio3 = "";
            ratio4 = "";
            if (!this.tbRatio.Text.Equals(""))
            {
                string[] ratioArray = tbRatio.Text.Split(new char[2] { ':', '：' });
                if (ratioArray[0] != null && !ratioArray[0].Equals(""))
                {
                    ratio1 = ratioArray[0];
                }
                if (ratioArray[1] != null && !ratioArray[1].Equals(""))
                {
                    ratio2 = ratioArray[1];
                }
                if (ratioArray[2] != null && !ratioArray[2].Equals(""))
                {
                    ratio3 = ratioArray[2];
                }
                if (ratioArray[3] != null && !ratioArray[3].Equals(""))
                {
                    ratio4 = ratioArray[3];
                }
            }
            SetWeight();
        }

        public void ManagerConfirmStirInfo(string managerName)
        {
            _managerName = managerName;
            isStirInfoConfirmed = true;

            //管理员确认调和数据后，初始化PLC并设置色剂重量
            if (!PLCHelper.GetInstance().SetWeight(w_C, w_H, w_TA, w_TB, w_C_Slow, w_H_Slow, w_TA_Slow, w_TB_Slow))
            {
                MessageBox.Show("PLC连接失败，请查看网络连接是否正常");
            }
        }

        /// <summary>
        /// 管理员确认调和数据窗口
        /// </summary>
        private void ShowConfirmWindow()
        {
            FormConfirmStirInfo formConfirmStirInfo = new FormConfirmStirInfo(this);
            formConfirmStirInfo.Show();
        }

        //判断PLC是否设置了有效的ip和端口
        private bool IsPLCIPInfoValid()
        {
            bool result = true;
            IPAddress ip;
            int port;
            if (!IPAddress.TryParse(Properties.Settings.Default.PLCIP, out ip))
            {
                result = false;
            }

            try
            {
                port = Convert.ToInt32(Properties.Settings.Default.PLCPort);
            }
            catch
            {
                result =  false;
            }
            if (!result)
            {
                MessageBox.Show("PLC设备IP地址和端口未设备或设备错误，请先设置正确的IP地址和端口号");
            }
            return result;
        }

        private void BtnStir_Click(object sender, EventArgs e)
        {
            PLCHelper.GetInstance().Stir();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            PLCHelper.GetInstance().Clear();
        }

        private void BtnBlow_Click(object sender, EventArgs e)
        {
            PLCHelper.GetInstance().Blow();
        }
    }
}
