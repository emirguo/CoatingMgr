using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
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
        private List<string> _cbSearchModel ;
        private List<string> _cbSearchComponent;
        private List<string> _cbSearchColor ;
        private double currStirTime = 0.0;
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

            _cbSearchModel = GetSqlLiteHelper().GetValueTypeByColumnFromTable(_tableName, "适用机种", null, null, null);
            for (int i = 0; i < _cbSearchModel.Count; i++)
            {
                cbModel.Items.Add(_cbSearchModel[i]);
            }
            _cbSearchComponent = GetSqlLiteHelper().GetValueTypeByColumnFromTable(_tableName, "适用制品", null, null, null);
            for (int i = 0; i < _cbSearchComponent.Count; i++)
            {
                cbComponent.Items.Add(_cbSearchComponent[i]);
            }
            _cbSearchColor = GetSqlLiteHelper().GetValueTypeByColumnFromTable(_tableName, "色番", null, null, null);
            for (int i = 0; i < _cbSearchColor.Count; i++)
            {
                cbColor.Items.Add(_cbSearchColor[i]);
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

        private void ClearStirText()
        {
            tbRatio.Text = "";

            tbName1.Text = "";
            tbName2.Text = "";
            tbName3.Text = "";
            tbName4.Text = "";

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
                    this.lbCurrentStatus.Text = "正在倒入主剂";
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
                    InsertLogToDB("主剂", tbName1.Text, tbBarCode1.Text, tbSetValue1.Text, tbMeasurementValue1.Text, tbMeasurementTime1.Text);
                    break;
                case StirLogType.HardeningLog:
                    InsertLogToDB("固化剂", tbName2.Text, tbBarCode2.Text, tbSetValue2.Text, tbMeasurementValue2.Text, tbMeasurementTime2.Text);
                    break;
                case StirLogType.ThinnerALog:
                    InsertLogToDB("稀释剂A", tbName3.Text, tbBarCode3.Text, tbSetValue3.Text, tbMeasurementValue3.Text, tbMeasurementTime3.Text);
                    break;
                case StirLogType.ThinnerBLog:
                    InsertLogToDB("稀释剂B", tbName4.Text, tbBarCode4.Text, tbSetValue4.Text, tbMeasurementValue4.Text, tbMeasurementTime4.Text);
                    break;
                default:
                    break;
            }
            
        }

        private void InsertLogToDB(string type, string name, string barCode, string setWeight, string measurementWeight, string measurementTime)
        {
            GetSqlLiteHelper().InsertValues(Common.STIRLOGTABLENAME, new string[] { cbModel.Text, cbComponent.Text, cbColor.Text, tbTemperature.Text, tbHumidity.Text, tbRatio.Text, type, name, barCode, setWeight, measurementWeight, measurementTime, _userName, DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("HH:mm:ss"), _managerName, " " });
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

        /// <summary>
        /// 判断条形码是否有效
        /// </summary>
        private bool IsBarCodeValid(string barcode)
        {
            bool result = false;
            if (!barcode.Equals("") && barcode.Length > 10)
            {
                result = true;
            }
            return result;
        }

        private void TbBarCode1_TextChanged(object sender, EventArgs e)
        {
            if (!IsStirInfoEnough())
            {
                tbBarCode1.Text = "";
                MessageBox.Show("请先设置调和信息");
                return;
            }
            if (!isStirInfoConfirmed)
            {
                tbBarCode1.Text = "";
                ShowConfirmWindow();
                return;
            }
            if (IsBarCodeValid(tbBarCode1.Text.ToString()))//条形码正确
            {
                CurrStatus = Status.CoatingStart;
                DoStir();
                this.tbBarCode2.Focus();
            }
            else
            {
                //MessageBox.Show("条形码无效");
            }
        }

        private void TbBarCode2_TextChanged(object sender, EventArgs e)
        {
            if (!IsStirInfoEnough())
            {
                tbBarCode2.Text = "";
                MessageBox.Show("请先设置调和信息");
                return;
            }
            if (!isStirInfoConfirmed)
            {
                tbBarCode2.Text = "";
                ShowConfirmWindow();
                return;
            }
            if (IsBarCodeValid(tbBarCode2.Text.ToString()))//条形码正确
            {
                CurrStatus = Status.HardeningAgentStart;
                DoStir();
                this.tbBarCode3.Focus();
            }
            else
            {
                //MessageBox.Show("条形码无效");
            }
        }

        private void TbBarCode3_TextChanged(object sender, EventArgs e)
        {
            if (!IsStirInfoEnough())
            {
                tbBarCode3.Text = "";
                MessageBox.Show("请先设置调和信息");
                return;
            }
            if (!isStirInfoConfirmed)
            {
                tbBarCode3.Text = "";
                ShowConfirmWindow();
                return;
            }
            if (IsBarCodeValid(tbBarCode3.Text.ToString()))//条形码正确
            {
                CurrStatus = Status.ThinnerAStart;
                DoStir();
                this.tbBarCode4.Focus();
            }
            else
            {
                //MessageBox.Show("条形码无效");
            }
        }

        private void TbBarCode4_TextChanged(object sender, EventArgs e)
        {
            if (!IsStirInfoEnough())
            {
                tbBarCode4.Text = "";
                MessageBox.Show("请先设置调和信息");
                return;
            }
            if (!isStirInfoConfirmed)
            {
                tbBarCode4.Text = "";
                ShowConfirmWindow();
                return;
            }
            if (IsBarCodeValid(tbBarCode4.Text.ToString()))//条形码正确
            {
                CurrStatus = Status.ThinnerBStart;
                DoStir();
            }
            else
            {
                //MessageBox.Show("条形码无效");

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
                MessageBox.Show("请选择部件");
                return;
            }
            if (cbColor.Text.Equals(""))
            {
                MessageBox.Show("请选择颜色");
                return;
            }
            if (tbInputWeight.Text.Equals(""))
            {
                MessageBox.Show("请输入主剂重量");
                return;
            }
            if (!cbModel.Text.Equals("") && !cbComponent.Text.Equals("") && !cbColor.Text.Equals("") && !tbInputWeight.Text.Equals("") && !tbTemperature.Text.Equals("") && !tbHumidity.Text.Equals(""))
            {
                SQLiteDataReader dataReader = GetSqlLiteHelper().ReadTable(_tableName, new string[] { "适用机种", "适用制品", "色番" }, new string[] { "=", "=", "=" }, new string[] { cbModel.Text, cbComponent.Text, cbColor.Text });
                if (dataReader.HasRows)
                {
                    isStirInfoConfirmed = false;
                    ClearStirText();
                    dataReader.Read();
                    tbName1.Text = dataReader["主剂"].ToString();
                    tbName2.Text = dataReader["固化剂"].ToString();
                    tbName3.Text = dataReader["稀释剂1"].ToString();
                    tbName4.Text = dataReader["稀释剂2"].ToString();
                    tbRatio.Text = dataReader["比例"].ToString();
                    SetWeight();
                    ShowConfirmWindow();
                }
                else
                {
                    MessageBox.Show("Master文件中未找到相关数据");
                }
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
                    string ratio1 = "",ratio2 = "", ratio3 = "",ratio4 = "";
                    if (!tbRatio.Text.Equals(""))
                    {
                        string[] ratioArray = tbRatio.Text.Split(new char[2] { ':', '：' });
                        ratio1 = ratioArray[0].ToString();
                        ratio2 = ratioArray[1].ToString();
                        ratio3 = ratioArray[2].ToString();
                        if (ratioArray.Length >= 4)
                        {
                            ratio4 = ratioArray[3].ToString();
                        }
                        
                    }

                    tbSetValue1.Text = weight1.ToString();
                    if (!ratio2.Equals(""))
                    {
                        double weight2 = weight1 * double.Parse(ratio2) / double.Parse(ratio1);
                        tbSetValue2.Text = weight2.ToString();
                    }
                    if (!ratio3.Equals(""))
                    {
                        double weight3 = weight1 * double.Parse(ratio3) / double.Parse(ratio1);
                        tbSetValue3.Text = weight3.ToString();
                    }
                    if (!ratio4.Equals(""))
                    {
                        double weight4 = weight1 * double.Parse(ratio4) / double.Parse(ratio1);
                        tbSetValue4.Text = weight4.ToString();
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
            SetWeight();
        }

        public void ManagerConfirmStirInfo(string managerName)
        {
            _managerName = managerName;
            isStirInfoConfirmed = true;
        }

        /// <summary>
        /// 管理员确认调和数据窗口
        /// </summary>
        private void ShowConfirmWindow()
        {
            FormConfirmStirInfo formConfirmStirInfo = new FormConfirmStirInfo(this);
            formConfirmStirInfo.Show();
        }

        /// <summary>
        /// 画黑色边框
        /// </summary>
        private void Panel3_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black, 1);
            Point point1 = new Point(0, 1);
            Point point2 = new Point(745, 1);
            g.DrawLine(pen, point1, point2);
            Point point3 = new Point(0, 560);
            Point point4 = new Point(745, 560);
            g.DrawLine(pen, point3, point4);
            Point point5 = new Point(0, 1);
            Point point6 = new Point(0, 560);
            g.DrawLine(pen, point5, point6);
            Point point7 = new Point(745, 1);
            Point point8 = new Point(745, 560);
            g.DrawLine(pen, point7, point8);
        }
    }

}
