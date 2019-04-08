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
            if (dr != null && dr.HasRows)
            {
                BindChartData(dr);
            }
        }

        //绑定柱状图数据
        private void BindChartData(SQLiteDataReader dataReader)
        {
            chartStock.Series.Clear();
            chartStock.ChartAreas[0].AxisX.Title = "色剂名";
            chartStock.ChartAreas[0].AxisX.TitleFont = new Font("微软雅黑", 12f, FontStyle.Regular);
            chartStock.ChartAreas[0].AxisY.Title = "库存量（kg）";
            chartStock.ChartAreas[0].AxisY.TitleFont = new Font("微软雅黑", 12f, FontStyle.Regular);

            //重量柱状图
            Series weightSerie = new Series
            {
                ChartType = SeriesChartType.Column,
                Enabled = true,
                IsVisibleInLegend = false
            };
            chartStock.Series.Add(weightSerie);


            //重量、上限、下限叠加柱状图
            Series minSerie = new Series
            {
                ChartType = SeriesChartType.StackedColumn,
                Enabled = false
            };
            chartStock.Series.Add(minSerie);

            Series midSerie = new Series
            {
                ChartType = SeriesChartType.StackedColumn,
                Enabled = false
            };
            chartStock.Series.Add(midSerie);

            Series maxSerie = new Series
            {
                ChartType = SeriesChartType.StackedColumn,
                Enabled = false
            };
            chartStock.Series.Add(maxSerie);

            int i = 0;
            while (dataReader.Read())
            {
                string x = dataReader["名称"].ToString();
                double yWeight = dataReader["重量"].ToString().Equals("") ? 0 : Convert.ToDouble(dataReader["重量"].ToString());
                double yMax = dataReader["库存上限"].ToString().Equals("") ? 0 : Convert.ToDouble(dataReader["库存上限"].ToString());
                double yMin = dataReader["库存下限"].ToString().Equals("") ? 0 : Convert.ToDouble(dataReader["库存下限"].ToString());

                weightSerie.Points.AddXY(x, yWeight);

                if (yMax >= yWeight && yWeight >= yMin)
                {
                    minSerie.Points.AddXY(x, yMin);
                    minSerie.Points[i].Color = Color.DarkRed;
                    minSerie.Points[i].Label = yMin + "";
                    minSerie.Points[i].ToolTip = "库存下限:" + yMin + "";
                    midSerie.Points.AddXY(x, yWeight - yMin);
                    midSerie.Points[i].Color = Color.Green;
                    midSerie.Points[i].Label = yWeight + "";
                    midSerie.Points[i].ToolTip = "库存重量:" + yWeight + "";
                    maxSerie.Points.AddXY(x, yMax - yWeight);
                    maxSerie.Points[i].Color = Color.Yellow;
                    maxSerie.Points[i].Label = yMax + "";
                    maxSerie.Points[i].ToolTip = "库存上限:" + yMax + "";
                }
                else if (yMax >= yMin && yMin >= yWeight)
                {
                    minSerie.Points.AddXY(x, yWeight);
                    minSerie.Points[i].Color = Color.Green;
                    minSerie.Points[i].Label = yWeight + "";
                    minSerie.Points[i].ToolTip = "库存重量:" + yWeight + "";
                    midSerie.Points.AddXY(x, yMin - yWeight);
                    midSerie.Points[i].Color = Color.DarkRed;
                    midSerie.Points[i].Label = yMin + "";
                    midSerie.Points[i].ToolTip = "库存下限:" + yMin + "";
                    maxSerie.Points.AddXY(x, yMax - yMin);
                    maxSerie.Points[i].Color = Color.Yellow;
                    maxSerie.Points[i].Label = yMax + "";
                    maxSerie.Points[i].ToolTip = "库存上限:" + yMax + "";
                }
                else if (yWeight >= yMax && yMax >= yMin)
                {
                    minSerie.Points.AddXY(x, yMin);
                    minSerie.Points[i].Color = Color.DarkRed;
                    minSerie.Points[i].Label = yMin + "";
                    minSerie.Points[i].ToolTip = "库存下限:" + yMin + "";
                    midSerie.Points.AddXY(x, yMax - yMin);
                    midSerie.Points[i].Color = Color.Yellow;
                    midSerie.Points[i].Label = yMax + "";
                    midSerie.Points[i].ToolTip = "库存上限:" + yMax + "";
                    maxSerie.Points.AddXY(x, yWeight - yMax);
                    maxSerie.Points[i].Color = Color.Green;
                    maxSerie.Points[i].Label = yWeight + "";
                    maxSerie.Points[i].ToolTip = "库存重量:" + yWeight + "";
                }
                else if (yWeight >= yMin && yMin >= yMax)
                {
                    minSerie.Points.AddXY(x, yMax);
                    minSerie.Points[i].Color = Color.Yellow;
                    minSerie.Points[i].Label = yMax + "";
                    minSerie.Points[i].ToolTip = "库存上限:" + yMax + "";
                    midSerie.Points.AddXY(x, yMin - yMax);
                    midSerie.Points[i].Color = Color.DarkRed;
                    midSerie.Points[i].Label = yMin + "";
                    midSerie.Points[i].ToolTip = "库存下限:" + yMin + "";
                    maxSerie.Points.AddXY(x, yWeight - yMin);
                    maxSerie.Points[i].Color = Color.Green;
                    maxSerie.Points[i].Label = yWeight + "";
                    maxSerie.Points[i].ToolTip = "库存重量:" + yWeight + "";
                }
                else if (yMin >= yMax && yMax >= yWeight)
                {
                    minSerie.Points.AddXY(x, yWeight);
                    minSerie.Points[i].Color = Color.Green;
                    minSerie.Points[i].Label = yWeight + "";
                    minSerie.Points[i].ToolTip = "库存重量:" + yWeight + "";
                    midSerie.Points.AddXY(x, yMax - yWeight);
                    midSerie.Points[i].Color = Color.Yellow;
                    midSerie.Points[i].Label = yMax + "";
                    midSerie.Points[i].ToolTip = "库存上限:" + yMax + "";
                    maxSerie.Points.AddXY(x, yMin - yMax);
                    maxSerie.Points[i].Color = Color.DarkRed;
                    maxSerie.Points[i].Label = yMin + "";
                    maxSerie.Points[i].ToolTip = "库存下限:" + yMin + "";
                }
                else if (yMin >= yWeight && yWeight >= yMax)
                {
                    minSerie.Points.AddXY(x, yMax);
                    minSerie.Points[i].Color = Color.Yellow;
                    minSerie.Points[i].Label = yMax + "";
                    minSerie.Points[i].ToolTip = "库存上限:" + yMax + "";
                    midSerie.Points.AddXY(x, yWeight - yMax);
                    midSerie.Points[i].Color = Color.Green;
                    midSerie.Points[i].Label = yWeight + "";
                    midSerie.Points[i].ToolTip = "库存重量:" + yWeight + "";
                    maxSerie.Points.AddXY(x, yMin - yWeight);
                    maxSerie.Points[i].Color = Color.DarkRed;
                    maxSerie.Points[i].Label = yMin + "";
                    maxSerie.Points[i].ToolTip = "库存下限:" + yMin + "";
                }
                i++;
            }

            weightSerie.XValueType = ChartValueType.String;  //设置X轴上的值类型
            weightSerie.LabelForeColor = Color.Black;
            weightSerie.Color = Color.Green;
            weightSerie.Label = "#VAL";
            weightSerie.ToolTip = "库存重量:#VAL";    //鼠标移动到对应点显示数值
            weightSerie.IsValueShownAsLabel = true;
            weightSerie.Palette = ChartColorPalette.None;//颜色类型

            minSerie.XValueType = ChartValueType.String;  //设置X轴上的值类型
            minSerie.LabelForeColor = Color.Black;
            minSerie.IsValueShownAsLabel = true;
            minSerie.Palette = ChartColorPalette.None;//颜色类型

            midSerie.XValueType = ChartValueType.String;  //设置X轴上的值类型
            midSerie.LabelForeColor = Color.Black;
            midSerie.IsValueShownAsLabel = true;
            midSerie.Palette = ChartColorPalette.None;//颜色类型

            maxSerie.XValueType = ChartValueType.String;  //设置X轴上的值类型
            maxSerie.LabelForeColor = Color.Black;
            maxSerie.IsValueShownAsLabel = true;
            maxSerie.Palette = ChartColorPalette.None;//颜色类型
        }

        private void CbShowWarn_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowWarn.Checked)
            {
                chartStock.Series[0].Enabled = false;
                chartStock.Series[1].Enabled = true;
                chartStock.Series[2].Enabled = true;
                chartStock.Series[3].Enabled = true;
                chartStock.Legends[0].Enabled = false;
                chartStock.Legends[1].Enabled = true;
            }
            else
            {
                chartStock.Series[0].Enabled = true;
                chartStock.Series[1].Enabled = false;
                chartStock.Series[2].Enabled = false;
                chartStock.Series[3].Enabled = false;
                chartStock.Legends[0].Enabled = true;
                chartStock.Legends[1].Enabled = false;
            }
        }
    }
}
