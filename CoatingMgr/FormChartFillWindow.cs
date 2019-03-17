using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CoatingMgr
{
    public partial class FormChartFillWindow : Form
    {
        private static string _tableName = Common.STOCKCOUNTTABLENAME;
        private static string _searchType = "";
        private static string _searchContent = "";

        public FormChartFillWindow()
        {
            InitializeComponent();
        }

        public FormChartFillWindow(string searchType, string searchContent)
        {
            InitializeComponent();
            _searchType = searchType;
            _searchContent = searchContent;
        }

        private void FormChartFillWindow_Load(object sender, EventArgs e)
        {
            SQLiteDataReader dr;
            if (_searchType.Equals("") || _searchContent.Equals(""))
            {
                dr = SqlLiteHelper.GetInstance().ReadFullTable(_tableName);
            }
            else
            {
                dr = SqlLiteHelper.GetInstance().ReadTable(_tableName, new string[] { _searchType }, new string[] { "=" }, new string[] { _searchContent });
            }
            BindChartData(dr);
        }

        //绑定柱状图数据
        private void BindChartData(SQLiteDataReader dataReader)
        {
            chartStock.Series.Clear();
            chartStock.ChartAreas[0].AxisX.Title = "色剂名";
            chartStock.ChartAreas[0].AxisX.TitleFont = new Font("微软雅黑", 12f, FontStyle.Regular);
            chartStock.ChartAreas[0].AxisY.Title = "库存量（kg）";
            chartStock.ChartAreas[0].AxisY.TitleFont = new Font("微软雅黑", 12f, FontStyle.Regular);

            Series serie = new Series();
            chartStock.Series.Add(serie);
            List<string> xValues = new List<string>();
            List<string> yValues = new List<string>();
            while (dataReader.Read())
            {
                string w = dataReader["重量"].ToString();
                double weight = Convert.ToSingle(Common.FilterChar(w));
                yValues.Add(weight + "");
                xValues.Add(dataReader["名称"].ToString());
            }
            chartStock.Series[0].XValueType = ChartValueType.String;  //设置X轴上的值类型
            chartStock.Series[0].Label = "#VAL";
            chartStock.Series[0].LabelForeColor = Color.Black;
            chartStock.Series[0].ToolTip = "#VALX:#VAL";    //鼠标移动到对应点显示数值
            chartStock.Series[0].IsValueShownAsLabel = true;
            chartStock.Series[0].Palette = ChartColorPalette.None;//颜色类型
            chartStock.Series[0].Points.DataBindXY(xValues, yValues);

        }
        
    }
}
