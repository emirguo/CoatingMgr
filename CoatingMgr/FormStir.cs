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
    public partial class FormStir : Form
    {
        private static SqlLiteHelper sqlLiteHelper = null;
        private string _userName = "";
        private static string[] _cbSearchModel = { "本田", "福特", "吉利", "长城" };
        private static string[] _cbSearchColor = { "红色", "白色", "黑色", "蓝色" };
        private double currentCount = 0.0;
        private enum Status
        {
            Stop,
            CoatingStart,
            CoatingPause,
            HardeningAgentStart,
            HardeningAgentPause,
            thinnerAStart,
            thinnerAPause,
            thinnerBStart,
            thinnerBPause
        }

        private Status CurrStatus = Status.Stop;

        public FormStir()
        {
            InitializeComponent();
        }

        public FormStir(string userName)
        {
            InitializeComponent();
            _userName = userName;
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

            for (int i = 0; i < _cbSearchModel.Length; i++)
            {
                cbModel.Items.Add(_cbSearchModel[i]);
                //cbModel.SelectedIndex = 0;
            }

            for (int i = 0; i < _cbSearchColor.Length; i++)
            {
                cbColor.Items.Add(_cbSearchColor[i]);
                //cbColor.SelectedIndex = 0;
            }

            GetTemperatureAndHumidity();


        }

        private void GetTemperatureAndHumidity()
        {
            tbTemperature.Text = "32.6";
            tbHumidity.Text = "64.7";
        }

        private void BtnStart1_Click(object sender, EventArgs e)
        {
            if (CurrStatus == Status.CoatingStart)
            {
                CurrStatus = Status.CoatingPause;
                btnStart1.Text = "开始";
                this.timer.Stop();
            }
            else
            {
                CurrStatus = Status.CoatingStart;
                btnStart1.Text = "暂停";
                btnStart2.Text = "开始";
                btnStart3.Text = "开始";
                btnStart4.Text = "开始";
                if (tbMeasurementTime1.Text == null || tbMeasurementTime1.Text.Equals(""))
                {
                    currentCount = 0;
                }
                else
                {
                    currentCount = double.Parse(tbMeasurementTime1.Text); 
                }
                this.timer.Start();
            }
        }

        private void BtnStart2_Click(object sender, EventArgs e)
        {
            if (CurrStatus == Status.HardeningAgentStart)
            {
                CurrStatus = Status.HardeningAgentPause;
                btnStart2.Text = "开始";
                this.timer.Stop();
            }
            else
            {
                CurrStatus = Status.HardeningAgentStart;
                btnStart2.Text = "暂停";
                btnStart1.Text = "开始";
                btnStart3.Text = "开始";
                btnStart4.Text = "开始";
                if (tbMeasurementTime2.Text == null || tbMeasurementTime2.Text.Equals(""))
                {
                    currentCount = 0;
                }
                else
                {
                    currentCount = double.Parse(tbMeasurementTime2.Text);
                }
                this.timer.Start();
            }
        }

        private void BtnStart3_Click(object sender, EventArgs e)
        {
            if (CurrStatus == Status.thinnerAStart)
            {
                CurrStatus = Status.thinnerAPause;
                btnStart3.Text = "开始";
                this.timer.Stop();
            }
            else
            {
                CurrStatus = Status.thinnerAStart;
                btnStart3.Text = "暂停";
                btnStart1.Text = "开始";
                btnStart2.Text = "开始";
                btnStart4.Text = "开始";
                if (tbMeasurementTime3.Text == null || tbMeasurementTime3.Text.Equals(""))
                {
                    currentCount = 0;
                }
                else
                {
                    currentCount = double.Parse(tbMeasurementTime3.Text);
                }
                this.timer.Start();
            }
        }

        private void BtnStart4_Click(object sender, EventArgs e)
        {
            if (CurrStatus == Status.thinnerBStart)
            {
                CurrStatus = Status.thinnerBPause;
                btnStart4.Text = "开始";
                this.timer.Stop();
            }
            else
            {
                CurrStatus = Status.thinnerBStart;
                btnStart4.Text = "暂停";
                btnStart1.Text = "开始";
                btnStart2.Text = "开始";
                btnStart3.Text = "开始";
                if (tbMeasurementTime4.Text == null || tbMeasurementTime4.Text.Equals(""))
                {
                    currentCount = 0;
                }
                else
                {
                    currentCount = double.Parse(tbMeasurementTime4.Text);
                }
                this.timer.Start();
            }
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            CurrStatus = Status.Stop;
            this.timer.Stop();
            currentCount = 0;

            btnStart1.Text = "开始";
            tbMeasurementTime1.Text = null;
            btnStart2.Text = "开始";
            tbMeasurementTime2.Text = null;
            btnStart3.Text = "开始";
            tbMeasurementTime3.Text = null;
            btnStart4.Text = "开始";
            tbMeasurementTime4.Text = null;
        }

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
            if (IsBarCodeValid(tbBarCode1.Text.ToString()))//条形码正确
            {
                tbName1.Text = "一猪";
            }
            else
            {
                MessageBox.Show("条形码无效");
            }
        }

        private void TbBarCode2_TextChanged(object sender, EventArgs e)
        {
            if (IsBarCodeValid(tbBarCode2.Text.ToString()))//条形码正确
            {
                tbName2.Text = "双虎";
            }
            else
            {
                MessageBox.Show("条形码无效");
            }
        }

        private void TbBarCode3_TextChanged(object sender, EventArgs e)
        {
            if (IsBarCodeValid(tbBarCode3.Text.ToString()))//条形码正确
            {
                tbName3.Text = "三鹿";
            }
            else
            {
                MessageBox.Show("条形码无效");
            }
        }

        private void TbBarCode4_TextChanged(object sender, EventArgs e)
        {
            if (IsBarCodeValid(tbBarCode4.Text.ToString()))//条形码正确
            {
                tbName4.Text = "四兔";
            }
            else
            {
                MessageBox.Show("条形码无效");
            }
        }

        private void TbRatio1_TextChanged(object sender, EventArgs e)
        {
            SetWeight();
        }

        private void TbRatio2_TextChanged(object sender, EventArgs e)
        {
            SetWeight();
        }

        private void TbRatio3_TextChanged(object sender, EventArgs e)
        {
            SetWeight();
        }

        private void TbRatio4_TextChanged(object sender, EventArgs e)
        {
            SetWeight();
        }

        private void TbInputWeight_TextChanged(object sender, EventArgs e)
        {
            tbSetValue1.Text = tbInputWeight.Text;
            SetWeight();
        }

        private void SetWeight()
        {
            if (tbSetValue1.Text != null && !tbSetValue1.Text.Equals(""))
            {
                try
                {
                    float value1 = float.Parse(tbSetValue1.Text);
                    if (tbRatio1.Text != null && !tbRatio1.Text.Equals(""))
                    {
                        if (tbRatio2.Text != null && !tbRatio2.Text.Equals(""))
                        {
                            float value2 = value1 * float.Parse(tbRatio2.Text) / float.Parse(tbRatio1.Text);
                            tbSetValue2.Text = value2.ToString();
                        }
                        if (tbRatio2.Text != null && !tbRatio2.Text.Equals(""))
                        {
                            float value3 = value1 * float.Parse(tbRatio3.Text) / float.Parse(tbRatio1.Text);
                            tbSetValue3.Text = value3.ToString();
                        }
                        if (tbRatio2.Text != null && !tbRatio2.Text.Equals(""))
                        {
                            float value4 = value1 * float.Parse(tbRatio4.Text) / float.Parse(tbRatio1.Text);
                            tbSetValue4.Text = value4.ToString();
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                
            }
        }

        private void TbTemperature_TextChanged(object sender, EventArgs e)
        {
            SetRatio(cbModel.Text, cbColor.Text, tbTemperature.Text, tbHumidity.Text);
        }

        private void TbHumidity_TextChanged(object sender, EventArgs e)
        {
            SetRatio(cbModel.Text, cbColor.Text, tbTemperature.Text, tbHumidity.Text);
        }

        private void CbModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetRatio(cbModel.Text, cbColor.Text, tbTemperature.Text, tbHumidity.Text);
        }

        private void CbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetRatio(cbModel.Text, cbColor.Text, tbTemperature.Text, tbHumidity.Text);
        }

        private void SetRatio(string model, string color, string temperature, string humidity)
        {
            if (!cbModel.Text.Equals("") && !cbColor.Text.Equals("") && !tbTemperature.Text.Equals("") && !tbHumidity.Text.Equals(""))
            {
                tbRatio1.Text = "6";
                tbRatio2.Text = "2";
                tbRatio3.Text = "1";
                tbRatio4.Text = "1";
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            currentCount += 0.1;
            string str = currentCount.ToString();
            int index = str.IndexOf(".");
            str = str.Substring(0, index + 2);
            switch (CurrStatus)
            {
                case Status.CoatingStart:
                    this.tbMeasurementTime1.Text = str;
                    break;
                case Status.HardeningAgentStart:
                    this.tbMeasurementTime2.Text = str;
                    break;
                case Status.thinnerAStart:
                    this.tbMeasurementTime3.Text = str;
                    break;
                case Status.thinnerBStart:
                    this.tbMeasurementTime4.Text = str;
                    break;
                default:
                    break;

            }
            
        }

        private void Panel3_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black, 1);
            Point point1 = new Point(0, 1);
            Point point2 = new Point(745, 1);
            g.DrawLine(pen, point1, point2);
            Point point3 = new Point(0, 620);
            Point point4 = new Point(745, 620);
            g.DrawLine(pen, point3, point4);
            Point point5 = new Point(0, 1);
            Point point6 = new Point(0, 620);
            g.DrawLine(pen, point5, point6);
            Point point7 = new Point(745, 1);
            Point point8 = new Point(745, 620);
            g.DrawLine(pen, point7, point8);
        }
    }
}
