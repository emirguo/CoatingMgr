using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoatingMgr
{
    public partial class FormStir : Form
    {
        private static SqlLiteHelper sqlLiteHelper = null;
        private static string _tableName = Common.MASTERTABLENAME;
        private string _userName = "";
        private string _userPermission = "";
        private string _managerName = "";
        private double currStirTime = 0.0;
        private string ratio1 = "", ratio2 = "", ratio3 = "", ratio4 = "";
        private enum Status
        {
            Stop,
            CoatingStart,
            CoatingPause,
            HardeningAgentStart,
            HardeningAgentPause,
            ThinnerAStart,
            ThinnerAPause,
            ThinnerBStart,
            ThinnerBPause
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

        public FormStir()
        {
            InitializeComponent();
        }

        public FormStir(string userName, string userPermission)
        {
            InitializeComponent();
            _userName = userName;
            _userPermission = userPermission;
        }

        private void FormStir_Load(object sender, EventArgs e)
        {
            InitData();
        }

        private SqlLiteHelper GetSqlLiteHelper()
        {
            if (sqlLiteHelper == null)
            {
                sqlLiteHelper = SqlLiteHelper.GetInstance();
            }
            return sqlLiteHelper;
        }

        private void InitData()
        {
            lbUser.Text = _userName;
            ShowTime();
                        
            List<string> _cbSearchModel = GetSqlLiteHelper().GetValueTypeByColumnFromTable(_tableName, "适用机种", null, null, null);
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
                List<string> _cbSearchComponent = GetSqlLiteHelper().GetValueTypeByColumnFromTable(_tableName, "適用製品", new string[] { "适用机种" }, new string[] { "=" }, new string[] { cbModel.Text });
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
                List<string> _cbSearchColor = GetSqlLiteHelper().GetValueTypeByColumnFromTable(_tableName, "色番", new string[] { "适用机种", "適用製品" }, new string[] { "=", "=" }, new string[] { cbModel.Text, cbComponent.Text });
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
                List<string> _cbSearchCoating = GetSqlLiteHelper().GetValueTypeByColumnFromTable(_tableName, "涂层", new string[] { "适用机种", "適用製品", "色番" }, new string[] { "=", "=", "=" }, new string[] { cbModel.Text, cbComponent.Text, cbColor.Text });
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
                SQLiteDataReader dataReader = GetSqlLiteHelper().ReadTable(_tableName, new string[] { "适用机种", "適用製品", "色番", "涂层" }, new string[] { "=", "=", "=", "=", }, new string[] { cbModel.Text, cbComponent.Text, cbColor.Text, cbCoating.Text });
                if (dataReader != null && dataReader.HasRows)
                {
                    isStirInfoConfirmed = false;
                    ClearStirText();
                    while (dataReader.Read())
                    {
                        if (dataReader["种类"].ToString().Equals("色漆") || dataReader["种类"].ToString().Equals("涂料"))
                        {
                            tbName1.Text = dataReader["涂料名"].ToString();
                            ratio1 = dataReader["调和比例"].ToString();
                        }
                        else if (dataReader["种类"].ToString().Equals("固化剂"))
                        {
                            tbName2.Text = dataReader["涂料名"].ToString();
                            ratio2 = dataReader["调和比例"].ToString();
                        }
                        else if (dataReader["种类"].ToString().Equals("稀释剂"))
                        {
                            if (tbName3.Text.Equals(""))
                            {
                                tbName3.Text = dataReader["涂料名"].ToString();
                                ratio3 = dataReader["调和比例"].ToString();
                            }
                            else
                            {
                                tbName4.Text = dataReader["涂料名"].ToString();
                                ratio4 = dataReader["调和比例"].ToString();
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
            
            this.btnPause.Text = "暂停";
            this.lbCurrentStatus.Text = "停止";
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            currStirTime += 0.1;
            string str = currStirTime.ToString();
            int index = str.IndexOf(".");
            str = str.Substring(0, index + 2);
            switch (CurrStatus)
            {
                case Status.CoatingStart:
                    this.tbMeasurementTime1.Text = str;
                    GetMeasurementWeight();
                    int value1 = (int)(Convert.ToSingle(tbMeasurementValue1.Text) / Convert.ToSingle(tbSetValue1.Text) * 100);
                    this.progressBar1.Value = value1 > 100?0: 100-value1;
                    SetCountProgressBar();
                    break;
                case Status.HardeningAgentStart:
                    this.tbMeasurementTime2.Text = str;
                    GetMeasurementWeight();
                    int value2 = (int)(Convert.ToSingle(tbMeasurementValue2.Text) / Convert.ToSingle(tbSetValue2.Text) * 100);
                    this.progressBar2.Value = value2 > 100 ? 0 : 100-value2;
                    SetCountProgressBar();
                    break;
                case Status.ThinnerAStart:
                    this.tbMeasurementTime3.Text = str;
                    GetMeasurementWeight();
                    int value3 = (int)(Convert.ToSingle(tbMeasurementValue3.Text) / Convert.ToSingle(tbSetValue3.Text) * 100);
                    this.progressBar3.Value = value3 > 100 ? 0 : 100-value3;
                    SetCountProgressBar();
                    break;
                case Status.ThinnerBStart:
                    this.tbMeasurementTime4.Text = str;
                    GetMeasurementWeight();
                    int value4 = (int)(Convert.ToSingle(tbMeasurementValue4.Text) / Convert.ToSingle(tbSetValue4.Text) * 100);
                    this.progressBar4.Value = value4 > 100 ? 0 : 100-value4;
                    SetCountProgressBar();
                    break;
                default:
                    break;
            }
            
        }

        //获取倒入重量
        private void GetMeasurementWeight()
        {
            switch (CurrStatus)
            {
                case Status.CoatingStart:
                    double setWeight1 = tbSetValue1.Text.Equals("") ? 0.00001f : Convert.ToSingle(tbSetValue1.Text);
                    double putWeight1 = tbMeasurementValue1.Text.Equals("") ? 0.00001f : Convert.ToSingle(tbMeasurementValue1.Text);
                    if (setWeight1 > putWeight1)
                    {
                        tbMeasurementValue1.Text = string.Format("{0:f2}", Convert.ToSingle(tbMeasurementTime1.Text) / 10 * setWeight1);//只取小数点后2位
                    }
                    else
                    {
                        CurrStatus = Status.CoatingPause;
                        tbMeasurementValue1.Text = tbSetValue1.Text;
                        progressBar1.Value = 100;
                        SetCountProgressBar();
                        DoStir();
                    }
                    break;
                case Status.HardeningAgentStart:
                    double setWeight2 = tbSetValue2.Text.Equals("") ? 0.00001f : Convert.ToSingle(tbSetValue2.Text);
                    double putWeight2 = tbMeasurementValue2.Text.Equals("") ? 0.00001f : Convert.ToSingle(tbMeasurementValue2.Text);
                    if (setWeight2 > putWeight2)
                    {
                        tbMeasurementValue2.Text = string.Format("{0:f2}", Convert.ToSingle(tbMeasurementTime2.Text) / 10 * setWeight2);
                    }
                    else
                    {
                        CurrStatus = Status.HardeningAgentPause;
                        tbMeasurementValue2.Text = tbSetValue2.Text;
                        progressBar2.Value = 100;
                        SetCountProgressBar();
                        DoStir();
                    }
                    break;
                case Status.ThinnerAStart:
                    double setWeight3 = tbSetValue3.Text.Equals("") ? 0.00001f : Convert.ToSingle(tbSetValue3.Text);
                    double putWeight3 = tbMeasurementValue3.Text.Equals("") ? 0.00001f : Convert.ToSingle(tbMeasurementValue3.Text);
                    if (setWeight3 > putWeight3)
                    {
                        tbMeasurementValue3.Text = string.Format("{0:f2}", Convert.ToSingle(tbMeasurementTime3.Text) / 10 * setWeight3);
                    }
                    else
                    {
                        CurrStatus = Status.ThinnerAPause;
                        tbMeasurementValue3.Text = tbSetValue3.Text;
                        progressBar3.Value = 100;
                        SetCountProgressBar();
                        DoStir();
                    }
                    break;
                case Status.ThinnerBStart:
                    double setWeight4 = tbSetValue4.Text.Equals("") ? 0.00001f : Convert.ToSingle(tbSetValue4.Text);
                    double putWeight4 = tbMeasurementValue4.Text.Equals("") ? 0.00001f : Convert.ToSingle(tbMeasurementValue4.Text);
                    if (setWeight4 > putWeight4)
                    {
                        tbMeasurementValue4.Text = string.Format("{0:f2}", Convert.ToSingle(tbMeasurementTime4.Text) / 10 * setWeight4);
                    }
                    else
                    {
                        CurrStatus = Status.ThinnerBPause;
                        tbMeasurementValue4.Text = tbSetValue4.Text;
                        progressBar4.Value = 100;
                        SetCountProgressBar();
                        DoStir();
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

        private void BtnPause_Click(object sender, EventArgs e)
        {
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
            switch (CurrStatus)
            {
                case Status.CoatingStart:
                    CurrStatus = Status.CoatingPause;
                    break;
                case Status.CoatingPause:
                    CurrStatus = Status.CoatingStart;
                    break;
                case Status.HardeningAgentStart:
                    CurrStatus = Status.HardeningAgentPause;
                    break;
                case Status.HardeningAgentPause:
                    CurrStatus = Status.HardeningAgentStart;
                    break;
                case Status.ThinnerAStart:
                    CurrStatus = Status.ThinnerAPause;
                    break;
                case Status.ThinnerAPause:
                    CurrStatus = Status.ThinnerAStart;
                    break;
                case Status.ThinnerBStart:
                    CurrStatus = Status.ThinnerBPause;
                    break;
                case Status.ThinnerBPause:
                    CurrStatus = Status.ThinnerBStart;
                    break;
                default:
                    break;
            }
            DoStir();
        }
        
        private void BtnStop_Click(object sender, EventArgs e)
        {
            CurrStatus = Status.Stop;
            DoStir();
            SaveStirLog(StirLogType.CoatingLog);
            SaveStirLog(StirLogType.HardeningLog);
            SaveStirLog(StirLogType.ThinnerALog);
            SaveStirLog(StirLogType.ThinnerBLog);
            ClearStirText();
        }

        /// <summary>
        /// 根据状态实现各调和动作
        /// </summary>
        private void DoStir()
        {
            switch (CurrStatus)
            {
                case Status.CoatingStart:
                    SetStartStirText(tbMeasurementTime1);
                    this.lbCurrentStatus.Text = "正在倒入涂料";
                    //连接PLC，开始倒涂料
                    PLCHelper.GetInstance().PLCRead();
                    break;
                case Status.CoatingPause:
                    SetPauseStirText();
                    this.lbCurrentStatus.Text = "暂停";
                    break;
                case Status.HardeningAgentStart:
                    SetStartStirText(tbMeasurementTime2);
                    this.lbCurrentStatus.Text = "正在倒入固化剂";
                    break;
                case Status.HardeningAgentPause:
                    SetPauseStirText();
                    this.lbCurrentStatus.Text = "暂停";
                    break;
                case Status.ThinnerAStart:
                    SetStartStirText(tbMeasurementTime3);
                    this.lbCurrentStatus.Text = "正在倒入稀释剂";
                    break;
                case Status.ThinnerAPause:
                    SetPauseStirText();
                    this.lbCurrentStatus.Text = "暂停";
                    break;
                case Status.ThinnerBStart:
                    SetStartStirText(tbMeasurementTime4);
                    this.lbCurrentStatus.Text = "正在倒入稀释剂";
                    break;
                case Status.ThinnerBPause:
                    SetPauseStirText();
                    this.lbCurrentStatus.Text = "暂停";
                    break;
                case Status.Stop:
                    this.timer.Stop();
                    currStirTime = 0;
                    this.lbCurrentStatus.Text = "停止";
                    this.btnPause.Text = "暂停";
                    break;
                default:
                    break;
            }
        }

        private void SaveStirLog(StirLogType logType)
        {
            switch (logType)
            {
                case StirLogType.CoatingLog:
                    if (!tbName1.Text.Equals("") && !tbBarCode1.Text.Equals("") && !tbSetValue1.Text.Equals("") && !tbMeasurementValue1.Text.Equals("") && !tbMeasurementTime1.Text.Equals(""))
                    {
                        GetSqlLiteHelper().InsertValues(Common.STIRLOGTABLENAME, new string[] { cbModel.Text, cbComponent.Text, cbColor.Text, cbCoating.Text, tbTemperature.Text, tbHumidity.Text, tbRatio.Text, "涂料", tbName1.Text, tbBarCode1.Text, tbSetValue1.Text, tbMeasurementValue1.Text, tbMeasurementTime1.Text, _userName, DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("HH:mm:ss"), _managerName, " " });
                    }
                    break;
                case StirLogType.HardeningLog:
                    if (!tbName2.Text.Equals("") && !tbBarCode2.Text.Equals("") && !tbSetValue2.Text.Equals("") && !tbMeasurementValue2.Text.Equals("") && !tbMeasurementTime2.Text.Equals(""))
                    {
                        GetSqlLiteHelper().InsertValues(Common.STIRLOGTABLENAME, new string[] { cbModel.Text, cbComponent.Text, cbColor.Text, cbCoating.Text, tbTemperature.Text, tbHumidity.Text, tbRatio.Text, "固化剂", tbName2.Text, tbBarCode2.Text, tbSetValue2.Text, tbMeasurementValue2.Text, tbMeasurementTime2.Text, _userName, DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("HH:mm:ss"), _managerName, " " });
                    }
                    break;
                case StirLogType.ThinnerALog:
                    if (!tbName3.Text.Equals("") && !tbBarCode3.Text.Equals("") && !tbSetValue3.Text.Equals("") && !tbMeasurementValue3.Text.Equals("") && !tbMeasurementTime3.Text.Equals(""))
                    {
                        GetSqlLiteHelper().InsertValues(Common.STIRLOGTABLENAME, new string[] { cbModel.Text, cbComponent.Text, cbColor.Text, cbCoating.Text, tbTemperature.Text, tbHumidity.Text, tbRatio.Text, "稀释剂A", tbName3.Text, tbBarCode3.Text, tbSetValue3.Text, tbMeasurementValue3.Text, tbMeasurementTime3.Text, _userName, DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("HH:mm:ss"), _managerName, " " });
                    }
                    break;
                case StirLogType.ThinnerBLog:
                    if (!tbName4.Text.Equals("") && !tbBarCode4.Text.Equals("") && !tbSetValue4.Text.Equals("") && !tbMeasurementValue4.Text.Equals("") && !tbMeasurementTime4.Text.Equals(""))
                    {
                        GetSqlLiteHelper().InsertValues(Common.STIRLOGTABLENAME, new string[] { cbModel.Text, cbComponent.Text, cbColor.Text, cbCoating.Text, tbTemperature.Text, tbHumidity.Text, tbRatio.Text, "稀释剂B", tbName4.Text, tbBarCode4.Text, tbSetValue4.Text, tbMeasurementValue4.Text, tbMeasurementTime4.Text, _userName, DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("HH:mm:ss"), _managerName, " " });
                    }
                    break;
                default:
                    break;
            }
            
        }

        private void SetPauseStirText()
        {
            this.timer.Stop();
            btnPause.Text = "继续";
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
            btnPause.Text = "暂停";
        }

        private bool IsBarCodeFromStock(string barcode)
        {
            bool result = false;
            bool hadInStock = false;
            bool hadOutStock = false;
            SQLiteDataReader dataReader = GetSqlLiteHelper().ReadTable(Common.STOCKLOGTABLENAME, new string[] { "条形码" }, new string[] { "=" }, new string[] { barcode });
            if (dataReader != null && dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    if (dataReader["操作类型"].ToString().Equals("入库"))
                    {
                        hadInStock = true;
                    }
                    else if (dataReader["操作类型"].ToString().Equals("出库"))
                    {
                        hadOutStock = true;
                    }
                }
            }
            else
            {
                MessageBox.Show("此条形码涂料还未入库，请先入库");
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
        /// 涂料名*种类*厂家*重量*批次号*连番*使用期限,例如：R-241(KAI) YR-614P(TAP)*A*G1000*18*20180219*0001*20190318
        private bool IsBarCodeValid(string barcode, string name)
        {
            bool result = false;
            if (!barcode.Equals(""))
            {
                string[] sArray = barcode.Split('*');
                if (sArray.Length >= 7)
                {
                    if (!name.Equals(sArray[0]))//判断涂料名称是否一致
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

        public void BarCodeInputEnd()
        {
            if (tbBarCode1.Focused)
            {
                BarCode1InputEnd();
            }
            else if (tbBarCode2.Focused)
            {
                BarCode2InputEnd();
            }
            else if (tbBarCode3.Focused)
            {
                BarCode3InputEnd();
            }
            else if (tbBarCode4.Focused)
            {
                BarCode4InputEnd();
            }
        }

        private void TbBarCode1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BarCode1InputEnd();
            }
        }

        private void TbBarCode2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BarCode2InputEnd();
            }
        }

        private void TbBarCode3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BarCode3InputEnd();
            }
        }

        private void TbBarCode4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BarCode4InputEnd();
            }
        }

        private void BarCode1InputEnd()
        {
            if (!tbBarCode1.Text.Equals(string.Empty))
            {
                if (!IsStirInfoEnough())
                {
                    tbBarCode1.Text = string.Empty;
                    MessageBox.Show("请先设置调和信息");
                    return;
                }
                if (!isStirInfoConfirmed)
                {
                    tbBarCode1.Text = string.Empty;
                    ShowConfirmWindow();
                    return;
                }
                if (!IsBarCodeFromStock(tbBarCode1.Text))
                {
                    tbBarCode1.Text = string.Empty;
                    return;
                }
                if (IsBarCodeValid(tbBarCode1.Text, tbName1.Text))//条形码正确
                {
                    CurrStatus = Status.CoatingStart;
                    DoStir();
                    this.tbBarCode2.Focus();
                }
                tbBarCode1.Text = string.Empty;
            }
        }

        private void BarCode2InputEnd()
        {
            if (!tbBarCode2.Text.Equals(string.Empty))
            {
                if (!IsStirInfoEnough())
                {
                    tbBarCode2.Text = string.Empty;
                    MessageBox.Show("请先设置调和信息");
                    return;
                }
                if (!isStirInfoConfirmed)
                {
                    tbBarCode2.Text = string.Empty;
                    ShowConfirmWindow();
                    return;
                }
                if (!IsBarCodeFromStock(tbBarCode2.Text))
                {
                    tbBarCode2.Text = string.Empty;
                    return;
                }
                if (IsBarCodeValid(tbBarCode2.Text, tbName2.Text))//条形码正确
                {
                    CurrStatus = Status.HardeningAgentStart;
                    DoStir();
                    this.tbBarCode3.Focus();
                }
                tbBarCode2.Text = string.Empty;
            }
        }

        private void BarCode3InputEnd()
        {
            if (!tbBarCode3.Text.Equals(string.Empty))
            {
                if (!IsStirInfoEnough())
                {
                    tbBarCode3.Text = string.Empty;
                    MessageBox.Show("请先设置调和信息");
                    return;
                }
                if (!isStirInfoConfirmed)
                {
                    tbBarCode3.Text = string.Empty;
                    ShowConfirmWindow();
                    return;
                }
                if (!IsBarCodeFromStock(tbBarCode3.Text))
                {
                    tbBarCode3.Text = string.Empty;
                    return;
                }
                if (IsBarCodeValid(tbBarCode3.Text, tbName3.Text))//条形码正确
                {
                    CurrStatus = Status.ThinnerAStart;
                    DoStir();
                    this.tbBarCode4.Focus();
                }
                tbBarCode3.Text = string.Empty;
            }
        }

        private void BarCode4InputEnd()
        {
            if (!tbBarCode4.Text.Equals(string.Empty))
            {

                if (!IsStirInfoEnough())
                {
                    tbBarCode4.Text = string.Empty;
                    MessageBox.Show("请先设置调和信息");
                    return;
                }
                if (!isStirInfoConfirmed)
                {
                    tbBarCode4.Text = string.Empty;
                    ShowConfirmWindow();
                    return;
                }
                if (!IsBarCodeFromStock(tbBarCode4.Text))
                {
                    tbBarCode4.Text = string.Empty;
                    return;
                }
                if (IsBarCodeValid(tbBarCode4.Text, tbName4.Text))//条形码正确
                {
                    CurrStatus = Status.ThinnerBStart;
                    DoStir();
                }
                tbBarCode4.Text = string.Empty;
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
                    tbSlowValue1.Text = tbSlowWeight.Text;
                    if (!ratio2.Equals(""))
                    {
                        double weight2 = weight1 * double.Parse(ratio2) / double.Parse(ratio1);
                        tbSetValue2.Text = weight2.ToString();
                        tbSlowValue2.Text = tbSlowWeight.Text;
                    }
                    if (!ratio3.Equals(""))
                    {
                        double weight3 = weight1 * double.Parse(ratio3) / double.Parse(ratio1);
                        tbSetValue3.Text = weight3.ToString();
                        tbSlowValue3.Text = tbSlowWeight.Text;
                    }
                    if (!ratio4.Equals(""))
                    {
                        double weight4 = weight1 * double.Parse(ratio4) / double.Parse(ratio1);
                        tbSetValue4.Text = weight4.ToString();
                        tbSlowValue4.Text = tbSlowWeight.Text;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
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
            PLCHelper.GetInstance().PLCSetWeight();
        }

        /// <summary>
        /// 管理员确认调和数据窗口
        /// </summary>
        private void ShowConfirmWindow()
        {
            FormConfirmStirInfo formConfirmStirInfo = new FormConfirmStirInfo(this);
            formConfirmStirInfo.Show();
        }

        TcpClient tcp = new TcpClient(); //定义TCP\IP连接
        private string SendMessage(string mes)//写一个发送指令的方法
        {
            NetworkStream DataSend = tcp.GetStream();//定义网络流
            string SendW = mes;
            var ByteSendW = Encoding.ASCII.GetBytes(SendW);//把发送数据转换为ASCII数组          
            DataSend.Write(ByteSendW, 0, ByteSendW.Length); //发送通讯数据
            byte[] data = new byte[16];//设定接收数据为16位数组，接收数据不足用0填充
            DataSend.Read(data, 0, data.Length);       //读取返回数据
            var ByteRD = "未返回数据";
            ByteRD = Encoding.ASCII.GetString(data);//接收数据从ASCII数组转换为字符串
            return ByteRD;
        }

        private void ConnectPLC(string ip, int port)//连接PLC 
        {
            try
            {
                tcp.Connect(ip, port);//tcp.Connect("192.168.10.10", 8500); //设置IP与Port 8500为默认上位链路通讯端口


                if (!tcp.Connected)
                {
                    MessageBox.Show("PLC连接失败，请查询网络是否正常！PLC地址：" + ip);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PLC连接失败," + ex.ToString());
            }
        }

        private void ClosePLC()
        {
            tcp.Close();
        }

        private void ResetR(string addr)
        {
            try
            {
                if (!tcp.Connected)
                {
                    MessageBox.Show("请先连接网口");
                    return;
                }
                string SendW = "";
                string RD = "RD R" + addr + @"\r";
                string value = SendMessage(RD);
                if (value == ("0\r\n" + "\0\0\0\0\0\0\0\0\0\0\0\0\0"))//如果该R值为0则发送置位指令
                {
                    SendW = "ST R" + addr + @"\r";
                }
                else if (value == ("1\r\n" + "\0\0\0\0\0\0\0\0\0\0\0\0\0"))//如果该R值为1则发送复位指令
                {
                    SendW = "RS R" + addr + @"\r";
                }
                if (SendMessage(SendW) == ("OK\r\n" + "\0\0\0\0\0\0\0\0\0\0\0\0"))//发送成功
                {
                    if (SendMessage(RD) == ("1\r\n" + "\0\0\0\0\0\0\0\0\0\0\0\0\0"))//接收成功
                    {
                    }
                    else//接收失败
                    {
                    }

                }
                else //发送失败
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ReadDM(string addr)//读DM
        {
            try
            {
                if (!tcp.Connected)
                {
                    MessageBox.Show("请先连接网口");
                    return;
                }

                string RD = "RD DM" + addr + @".L\r";
                string value = SendMessage(RD);
                if (value == ("E0\r\n" + "\0\0\0\0\0\0\0\0\0\0\0\0") || value == ("E1\r\n" + "\0\0\0\0\0\0\0\0\0\0\0\0") || value == "未返回数据")
                {
                    //发送失败
                }
                else
                {
                    //发送成功
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void WriteDM(string addr, string value)//写DM
        {
            try
            {
                if (!tcp.Connected)
                {
                    MessageBox.Show("请先连接网口");
                    return;
                }
                string RD = "WR DM" + addr + @".L " + value + "\r";
                if (SendMessage(RD) == ("OK\r\n" + "\0\0\0\0\0\0\0\0\0\0\0\0"))
                {
                    //写成功

                }
                else
                {
                    //写失败
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
