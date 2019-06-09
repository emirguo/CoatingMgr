using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CoatingMgr
{
    public partial class FormStock : Form
    {
        private static SqlLiteHelper sqlLiteHelper = null;
        private static string _tableName = Common.STOCKCOUNTTABLENAME;
        private string _userName = "";
        private string _userPermission = "";
        private string _chartSearchType = "";
        private string _chartSearchContent = "";

        private static string[] _cbSearchType = {"按仓库查找", "按名称查找", "按颜色查找", "按类型查找", "按适用机型查找", "按生产日期查找", "按有效期查找", "按操作员查找", "按操作时间查找", "按告警类型查找" };
        private static string[] _searchType = {"仓库", "名称", "颜色", "类型", "适用机型", "生产日期", "有效期", "操作员", "操作时间", "告警类型" };

        public FormStock()
        {
            InitializeComponent();
        }

        public FormStock(string userName, string userPermission)
        {
            InitializeComponent();
            _userName = userName;
            _userPermission = userPermission;
        }

        private void FormStock_Load(object sender, EventArgs e)
        {
            InitData();
        }

        /// <summary>
        /// 画黑色边框
        /// </summary>
        private void BorderLine_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black, 1);
            Point point1 = new Point(2, 16);
            Point point2 = new Point(1172, 16);
            g.DrawLine(pen, point1, point2);
            Point point3 = new Point(2, 16);
            Point point4 = new Point(2, 74);
            g.DrawLine(pen, point3, point4);
            Point point5 = new Point(1172, 16);
            Point point6 = new Point(1172, 74);
            g.DrawLine(pen, point5, point6);
            Point point7 = new Point(2, 74);
            Point point8 = new Point(1172, 74);
            g.DrawLine(pen, point7, point8);
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

            for (int i = 0; i <_cbSearchType.Length; i++)
            {
                cbSearchType.Items.Add(_cbSearchType[i]);
            }

            BindDataGirdView(dgvStockData, _tableName);//绑定数据库表
            this.chartStock.Visible = false;
        }

        private void BindDataGirdView(DataGridView dataGirdView, string table)
        {
            dataGirdView.Rows.Clear();
            SQLiteDataReader dataReader = GetSqlLiteHelper().ReadFullTable(table);
            if (dataReader != null && dataReader.HasRows)
            {
                BindingSource bs = new BindingSource
                {
                    DataSource = dataReader
                };
                dataGirdView.DataSource = bs;
                if (dataGirdView.ColumnCount > 0)
                {
                    dataGirdView.Columns[0].Visible = false;
                }

                SQLiteDataReader dr = GetSqlLiteHelper().ReadFullTable(table);
                BindChartData(dr);
                cbShowHistogram.Visible = true;
                _chartSearchType = "";
                _chartSearchContent = "";
            }
            lbCount.Text = dataGirdView.RowCount + "";
        }

        private void BindDataGirdViewBySearch(DataGridView dataGirdView, string table, string type, string content)
        {
            SQLiteDataReader dataReader = GetSqlLiteHelper().ReadTable(table, new string[] { type }, new string[] { "=" }, new string[] { content });
            if (dataReader != null && dataReader.HasRows)
            {
                BindingSource bs = new BindingSource
                {
                    DataSource = dataReader
                };
                dataGirdView.DataSource = bs;
                if (dataGirdView.ColumnCount > 0)
                {
                    dataGirdView.Columns[0].Visible = false;
                }
                
                SQLiteDataReader dr = GetSqlLiteHelper().ReadTable(table, new string[] { type }, new string[] { "=" }, new string[] { content });
                BindChartData(dr);
                _chartSearchType = type;
                _chartSearchContent = content;
            }
            else
            {
                MessageBox.Show("未查找到数据");
            }
            lbCount.Text = dataGirdView.RowCount + "";
        }

        private void SetDefaultColumns(DataGridView dataGirdView, string[] columns)
        {
            dataGirdView.DataSource = null;
            for (int i = 0; i < columns.Length; i++)
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn
                {
                    HeaderText = columns[i]
                };
                dataGirdView.Columns.Add(column);
            }

            dataGirdView.Columns[0].Visible = false;
        }

        public void UpdateData()
        {
            int index = 0;
            if (dgvStockData.RowCount > 0 && dgvStockData.CurrentRow != null)
            {
                index = dgvStockData.CurrentRow.Index;
            }
            BindDataGirdView(dgvStockData, _tableName);
            if (dgvStockData.RowCount <= 0)
            {
                return;
            }
            else if ((dgvStockData.RowCount - 1) > index)
            {
                this.dgvStockData.CurrentCell = this.dgvStockData[1, index];
            }
            else
            {
                this.dgvStockData.CurrentCell = this.dgvStockData[1, (dgvStockData.RowCount - 1)];
            }
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

        private void CbSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSearchType.SelectedIndex >= 0)
            {
                cbSearchContent.Items.Clear();
                List<string> searchContent = GetSqlLiteHelper().GetValueTypeByColumnFromTable(_tableName, _searchType[cbSearchType.SelectedIndex], null , null, null);
                for (int i = 0; i < searchContent.Count; i++)
                {
                    cbSearchContent.Items.Add(searchContent[i]);
                }
                cbSearchContent.Text = "选择过滤内容";
            }
        }

        private void CbSearchContent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSearchType.SelectedIndex >= 0 && cbSearchContent.SelectedIndex >= 0)
            {
                BindDataGirdViewBySearch(dgvStockData, _tableName, _searchType[cbSearchType.SelectedIndex], cbSearchContent.SelectedItem.ToString());
            }
        }

        //显示所有库存数据，清除过滤内容
        private void BtShowAll_Click(object sender, EventArgs e)
        {
            cbSearchType.SelectedIndex = -1;
            cbSearchType.Text = "选择过滤方式";
            cbSearchContent.SelectedIndex = -1;
            cbSearchContent.Text = "选择过滤内容";
            cbSearchContent.Items.Clear();
            BindDataGirdView(dgvStockData, _tableName);
        }

        //弹出右键菜单
        private void DgvStockData_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && Common.USER_MANAGER.Equals(_userPermission))
            {
                this.contextMenuStrip.Show(this.dgvStockData, e.Location);
                this.contextMenuStrip.Show(System.Windows.Forms.Cursor.Position);
                if (this.dgvStockData.SelectedRows.Count == 1)
                {
                    this.TSMIModify.Visible = true;
                }
                else
                {
                    this.TSMIModify.Visible = false;
                }

            }
        }

        //右键选择删除
        private void TSMIDelete_Click(object sender, EventArgs e)
        {
            if (this.dgvStockData.SelectedRows.Count > 0)
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确认删除所选库存?", "删除", messButton);
                if (dr == DialogResult.OK)
                {
                    foreach (DataGridViewRow row in dgvStockData.SelectedRows)
                    {
                        if (!row.IsNewRow)
                        {
                            string id = row.Cells[0].Value.ToString();
                            //删除库存统计表中数据
                            GetSqlLiteHelper().DeleteValuesAND(_tableName, new string[] { "id" }, new string[] { id }, new string[] { "=" });

                            //记录删除日志
                            GetSqlLiteHelper().InsertValues(Common.STOCKLOGTABLENAME, new string[] { "", row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString(), row.Cells[1].Value.ToString(), row.Cells[6].Value.ToString(), row.Cells[4].Value.ToString(), row.Cells[5].Value.ToString(), "", "", _userName, DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("HH:mm:ss"), "删除", "", "" });

                            this.dgvStockData.Rows.Remove(row);
                        }
                    }
                    lbCount.Text = this.dgvStockData.Rows.Count + "";
                }
            }
        }

        private void TSMIModify_Click(object sender, EventArgs e)
        {
            if (!this.dgvStockData.CurrentRow.IsNewRow)
            {
                string type = dgvStockData.CurrentRow.Cells[1].Value.ToString();
                string name = dgvStockData.CurrentRow.Cells[2].Value.ToString();
                string color = dgvStockData.CurrentRow.Cells[3].Value.ToString();
                string model = dgvStockData.CurrentRow.Cells[4].Value.ToString();
                string stock = dgvStockData.CurrentRow.Cells[5].Value.ToString();
                string weight = dgvStockData.CurrentRow.Cells[6].Value.ToString();
                string maximum = dgvStockData.CurrentRow.Cells[7].Value.ToString();
                string minimum = dgvStockData.CurrentRow.Cells[8].Value.ToString();
                string warnTime = dgvStockData.CurrentRow.Cells[9].Value.ToString();
                string tips = dgvStockData.CurrentRow.Cells[10].Value.ToString();
                FormModifyStock formModifyStock = new FormModifyStock(this, name, type, color, model, weight, stock, maximum, minimum, warnTime, tips);
                formModifyStock.Show();
            }
        }

        public void ModifyCurrentRow(string name, string type, string color, string model, string weight, string stock, string maximum, string minimum, string warnTime, string tips)
        {
            string curType = dgvStockData.CurrentRow.Cells[1].Value.ToString();
            string curName = dgvStockData.CurrentRow.Cells[2].Value.ToString();
            string curColor = dgvStockData.CurrentRow.Cells[3].Value.ToString();
            string curModel = dgvStockData.CurrentRow.Cells[4].Value.ToString();
            string curStock = dgvStockData.CurrentRow.Cells[5].Value.ToString();
            string curWeight = dgvStockData.CurrentRow.Cells[6].Value.ToString();
            string curMaximum = dgvStockData.CurrentRow.Cells[7].Value.ToString();
            string curMinimum = dgvStockData.CurrentRow.Cells[8].Value.ToString();
            string curWarnTime = dgvStockData.CurrentRow.Cells[9].Value.ToString();
            string curTips = dgvStockData.CurrentRow.Cells[10].Value.ToString();

            if (curName.Equals(name) && curType.Equals(type) && curColor.Equals(color) && curModel.Equals(model))
            {
                if (!curWeight.Equals(weight) || !curStock.Equals(stock) || !curTips.Equals(tips) || !curMaximum.Equals(maximum) || !curMinimum.Equals(minimum) || !curWarnTime.Equals(warnTime))//修改任一数据都需要更新库存统计表
                {
                    //更新库存统计表
                    GetSqlLiteHelper().UpdateValues(Common.STOCKCOUNTTABLENAME, new string[] { "类型", "名称", "颜色", "适用机型", "仓库", "重量", "库存上限", "库存下限", "告警时间", "备注" }, new string[] { type, name, color, model, stock, weight, maximum, minimum, warnTime, tips }, "id", dgvStockData.CurrentRow.Cells[0].Value.ToString());
                    UpdateData();
                    
                    //如果修改了库存重量或库存仓库，则记录修改日志
                    if (!curStock.Equals(stock) || !curWeight.Equals(weight))
                    {
                        string stockLogTip = "";
                        if (!curStock.Equals(stock))
                        {
                            stockLogTip += "库存仓库从" + curStock + "修改为" + stock + ";";
                        }
                        if (!curWeight.Equals(weight))
                        {
                            stockLogTip += "库存重量从" + curWeight + "kg修改为" + weight + "kg";
                        }
                        GetSqlLiteHelper().InsertValues(Common.STOCKLOGTABLENAME, new string[] { "", name, color, type, weight, model, stock, "", "", _userName, DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("HH:mm:ss"), "修改", "", stockLogTip });
                    }

                    //如果修改了告警数据，则更新告警规则表
                    if (!curMaximum.Equals(maximum) || !curMinimum.Equals(minimum) || !curWarnTime.Equals(warnTime))
                    {
                        SQLiteDataReader dataReader = GetSqlLiteHelper().ReadTable(Common.WARNMANAGERTABLENAME, new string[] { "仓库", "名称", "颜色", "类型", }, new string[] { "=", "=", "=", "=" }, new string[] { stock, name, color, type });
                        if (dataReader != null && dataReader.HasRows && dataReader.Read())//告警规则存在，更新告警规则
                        {
                            GetSqlLiteHelper().UpdateValues(Common.WARNMANAGERTABLENAME, new string[] { "库存上限", "库存下限", "告警时间" }, new string[] { maximum, minimum, warnTime }, "id", dataReader["id"].ToString());
                        }
                        else //告警规则不存在，创建告警规则
                        {
                            GetSqlLiteHelper().InsertValues(Common.WARNMANAGERTABLENAME, new string[] { stock, name, color, type, maximum, minimum, warnTime, _userName, DateTime.Now.ToString() });
                        }
                    }
                }
            }
        }

        //显示柱状图
        private void CbShowHistogram_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbShowHistogram.Checked && dgvStockData.DataSource != null)
            {
                this.chartStock.Visible = true;
                this.cbFillWindow.Visible = true;
                this.cbShowWarn.Visible = true;
            }
            else
            {
                this.chartStock.Visible = false;
                this.cbFillWindow.Visible = false;
                this.cbShowWarn.Visible = false;
            }
        }

        //绑定柱状图数据
        private void BindChartData(SQLiteDataReader dataReader)
        {
            chartStock.Series.Clear();
            chartStock.ChartAreas[0].AxisX.Title = "色剂名";
            chartStock.ChartAreas[0].AxisX.TitleFont = new Font("微软雅黑", 12f, FontStyle.Regular) ;
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
                double yWeight = dataReader["重量"].ToString().Equals("")?0: Convert.ToDouble(dataReader["重量"].ToString());
                double yMax = dataReader["库存上限"].ToString().Equals("") ? 0 : Convert.ToDouble(dataReader["库存上限"].ToString());
                double yMin = dataReader["库存下限"].ToString().Equals("") ? 0 : Convert.ToDouble(dataReader["库存下限"].ToString());

                weightSerie.Points.AddXY(x, yWeight);
                
                if (yMax >= yWeight && yWeight >=  yMin)
                {
                    minSerie.Points.AddXY(x, yMin);
                    minSerie.Points[i].Color = Color.DarkRed;
                    minSerie.Points[i].Label = yMin + "";
                    minSerie.Points[i].ToolTip = "库存下限:"+ yMin +"";
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

        private void BtnExport_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Load(GetSqlLiteHelper().ReadFullTable(_tableName));
            ExcelHelper.ExportExcel(dt);
        }

        private void CbFillWindow_CheckedChanged(object sender, EventArgs e)
        {
            if (cbFillWindow.Checked)
            {
                FormChartFillWindow formChartFillWindow = new FormChartFillWindow(_chartSearchType, _chartSearchContent);
                formChartFillWindow.Show();
                this.cbFillWindow.CheckState = CheckState.Unchecked;
            }
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
