using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Threading;
using System.Windows.Forms;

namespace CoatingMgr
{
    public partial class FormStockDetail : Form
    {
        private static SqlLiteHelper sqlLiteHelper = null;
        private static string _tableName = Common.INSTOCKTABLENAME;
        private string _userName = "";
        private string _userPermission = "";
        //"id", "条形码", "名称", "颜色", "类型", "重量", "适用机型", "仓库", "生产日期", "有效期", "操作员", "入库日期", "入库时间", "告警时间", "备注" 
        private static string[] _cbSearchType = { "按名称查找", "按颜色查找", "按类型查找", "按适用机型查找", "按仓库查找", "按生产日期查找", "按有效期查找", "按入库操作员查找", "按入库日期查找", "按告警时间查找" };
        private static string[] _searchType = { "名称", "颜色", "类型", "适用机型", "仓库", "生产日期", "有效期", "操作员", "入库日期", "告警时间" };

        public FormStockDetail()
        {
            InitializeComponent();
        }

        public FormStockDetail(string userName, string userPermission)
        {
            InitializeComponent();
            _userName = userName;
            _userPermission = userPermission;
        }

        private void FormStockDetail_Load(object sender, EventArgs e)
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

        private void InitData()
        {
            lbUser.Text = _userName;
            ShowTime();

            for (int i = 0; i < _cbSearchType.Length; i++)
            {
                cbSearchType.Items.Add(_cbSearchType[i]);
            }

            BindDataGirdView(dgvData, _tableName);//绑定表
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
                lbCount.Text = dataGirdView.RowCount + "";
            }
            else
            {
                MessageBox.Show("未查找到数据");
            }
        }

        public void UpdateData()
        {
            int index = 0;
            if (dgvData.RowCount > 0 && dgvData.CurrentRow != null)
            {
                index = dgvData.CurrentRow.Index;
            }
            BindDataGirdView(dgvData, _tableName);
            if (dgvData.RowCount <= 0)
            {
                return;
            }
            else if ((dgvData.RowCount - 1) > index)
            {
                this.dgvData.CurrentCell = this.dgvData[1, index];
            }
            else
            {
                this.dgvData.CurrentCell = this.dgvData[1, (dgvData.RowCount - 1)];
            }
        }

        private void DgvData_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && Common.USER_MANAGER.Equals(_userPermission))
            {
                this.contextMenuStrip.Show(this.dgvData, e.Location);
                this.contextMenuStrip.Show(Cursor.Position);
                if (dgvData.SelectedRows.Count == 1)
                {
                    this.TSMIModify.Visible = true;
                }
                else
                {
                    this.TSMIModify.Visible = false;
                }
            }
        }

        private void TSMIDelete_Click(object sender, EventArgs e)
        {
            if (this.dgvData.SelectedRows.Count > 0)
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确认删除所选库存?", "删除", messButton);
                if (dr == DialogResult.OK)
                {
                    foreach (DataGridViewRow row in dgvData.SelectedRows)
                    {
                        if (!row.IsNewRow)
                        {
                            //库存表："id", "条形码", "名称", "颜色", "类型", "重量", "适用机型", "仓库", "生产日期", "有效期", "操作员", "入库日期", "入库时间", "告警时间", "备注" 
                            string id = row.Cells[0].Value.ToString();
                            string barcode = row.Cells[1].Value.ToString();
                            string name = row.Cells[2].Value.ToString();
                            string color = row.Cells[3].Value.ToString();
                            string type = row.Cells[4].Value.ToString();
                            string weight = row.Cells[5].Value.ToString();
                            string model = row.Cells[6].Value.ToString();
                            string store = row.Cells[7].Value.ToString();
                            string productDate = row.Cells[8].Value.ToString();
                            string expiryDate = row.Cells[9].Value.ToString();
                            string date = row.Cells[11].Value.ToString();
                            string time = row.Cells[12].Value.ToString();
                            string warnDate = row.Cells[13].Value.ToString();
                            string tips = row.Cells[14].Value.ToString();
                            //删除库存表中数据
                            GetSqlLiteHelper().DeleteValuesAND(_tableName, new string[] { "id" }, new string[] { id }, new string[] { "=" });

                            //更新库存统计表中的数据
                            // "id", "类型", "名称", "颜色", "适用机型", "重量", "库存上限", "库存下限", "告警时间", "备注"
                            SQLiteDataReader dataReader = GetSqlLiteHelper().ReadTable(Common.STOCKCOUNTTABLENAME, new string[] { "类型", "名称", "颜色", "适用机型" }, new string[] { "=", "=", "=", "=" }, new string[] { type,name,color,model });
                            if (dataReader != null && dataReader.HasRows && dataReader.Read())
                            {
                                double inStockWeight = Convert.ToSingle(Common.FilterChar(dataReader["重量"].ToString()));
                                double inputWeight = Convert.ToSingle(Common.FilterChar(weight));
                                inStockWeight -= inputWeight;
                                if (inStockWeight < 0.000001)//总量 < 0，删除
                                {
                                    GetSqlLiteHelper().DeleteValuesAND(Common.STOCKCOUNTTABLENAME, new string[] { "类型", "名称", "颜色", "适用机型" }, new string[] { type, name, color, model }, new string[] { "=", "=", "=", "=" });
                                }
                                else
                                {
                                    GetSqlLiteHelper().UpdateValues(Common.STOCKCOUNTTABLENAME, new string[] { "重量" }, new string[] { inStockWeight.ToString() }, "id", dataReader["id"].ToString());
                                }
                            }

                            //记录删除日志
                            //库存日志表："id", "条形码", "名称", "颜色", "类型", "重量", "适用机型", "仓库", "生产日期", "有效期", "操作员", "操作日期", "操作时间", "操作类型", "告警时间", "备注"
                            GetSqlLiteHelper().InsertValues(Common.STOCKLOGTABLENAME, new string[] { barcode, name, color, type, weight, model, store, productDate, expiryDate, _userName, DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("HH:mm:ss"), "删除", warnDate, tips });

                            this.dgvData.Rows.Remove(row);
                        }
                    }
                    lbCount.Text = this.dgvData.Rows.Count + "";
                }
            }
        }

        private void TSMIModify_Click(object sender, EventArgs e)
        {
            if (!this.dgvData.CurrentRow.IsNewRow)
            {
                //在库表："id", "条形码", "名称", "颜色", "类型", "重量", "适用机型", "仓库", "生产日期", "有效期", "操作员", "入库日期", "入库时间", "告警时间", "备注" 
                string name = dgvData.CurrentRow.Cells[2].Value.ToString();
                string color = dgvData.CurrentRow.Cells[3].Value.ToString();
                string type = dgvData.CurrentRow.Cells[4].Value.ToString();
                string weight = dgvData.CurrentRow.Cells[5].Value.ToString();
                string model = dgvData.CurrentRow.Cells[6].Value.ToString();
                string store = dgvData.CurrentRow.Cells[7].Value.ToString();
                string tips = dgvData.CurrentRow.Cells[14].Value.ToString();

                FormModifyStockDetail formModifyStockDetail = new FormModifyStockDetail(this, name, type, color, model, weight, store, tips);
                formModifyStockDetail.Show();
            }

        }

        public void ModifyCurrentRow(string name, string type, string color, string model, string weight, string store, string tips)
        {
            ////在库表："id", "条形码", "名称", "颜色", "类型", "重量", "适用机型", "仓库", "生产日期", "有效期", "操作员", "入库日期", "入库时间", "告警时间", "备注" 
            string curBarCode = dgvData.CurrentRow.Cells[1].Value.ToString();
            string curName = dgvData.CurrentRow.Cells[2].Value.ToString();
            string curColor = dgvData.CurrentRow.Cells[3].Value.ToString();
            string curType = dgvData.CurrentRow.Cells[4].Value.ToString();
            string curWeight = dgvData.CurrentRow.Cells[5].Value.ToString();
            string curModel = dgvData.CurrentRow.Cells[6].Value.ToString();
            string curStore = dgvData.CurrentRow.Cells[7].Value.ToString();
            string curProductDate = dgvData.CurrentRow.Cells[8].Value.ToString();
            string curExpiryDate = dgvData.CurrentRow.Cells[9].Value.ToString();
            string curWarnDate = dgvData.CurrentRow.Cells[13].Value.ToString();
            string curTips = dgvData.CurrentRow.Cells[14].Value.ToString();
            if (curName.Equals(name) && curType.Equals(type) && curColor.Equals(color) && curModel.Equals(model))
            {
                if (!curWeight.Equals(weight) || !curStore.Equals(store) || !curTips.Equals(tips))//修改任一数据都需要更新在库表
                {
                    //更新库存表
                    GetSqlLiteHelper().UpdateValues(_tableName, new string[] { "重量", "仓库", "备注" }, new string[] { weight, store, tips }, "id", dgvData.CurrentRow.Cells[0].Value.ToString());
                    UpdateData();

                    //如果更新了重量，则更新库存统计表中的重量
                    if (!curWeight.Equals(weight))
                    {
                        // "id", "类型", "名称", "颜色", "适用机型", "重量", "库存上限", "库存下限", "告警时间", "备注"
                        SQLiteDataReader dataReader = GetSqlLiteHelper().ReadTable(Common.STOCKCOUNTTABLENAME, new string[] { "类型", "名称", "颜色", "适用机型" }, new string[] { "=", "=", "=", "=" }, new string[] { type, name, color, model });
                        if (dataReader != null && dataReader.HasRows && dataReader.Read())
                        {
                            double inStockWeight = Convert.ToSingle(Common.FilterChar(dataReader["重量"].ToString()));
                            double oldWeight = Convert.ToSingle(Common.FilterChar(curWeight));
                            double newWeight = Convert.ToSingle(Common.FilterChar(weight));
                            if (newWeight > oldWeight)
                            {
                                inStockWeight += (newWeight - oldWeight);
                            }
                            else
                            {
                                inStockWeight -= (oldWeight - newWeight);
                            }
                            if (inStockWeight < 0.000001)//总量 < 0，删除
                            {
                                GetSqlLiteHelper().DeleteValuesAND(Common.STOCKCOUNTTABLENAME, new string[] { "类型", "名称", "颜色", "适用机型" }, new string[] { type, name, color, model }, new string[] { "=", "=", "=", "=" });
                            }
                            else
                            {
                                GetSqlLiteHelper().UpdateValues(Common.STOCKCOUNTTABLENAME, new string[] { "重量" }, new string[] { inStockWeight.ToString() }, "id", dataReader["id"].ToString());
                            }
                        }
                    }

                    //如果修改了库存重量或库存仓库，则记录修改日志
                    if (!curStore.Equals(store) || !curWeight.Equals(weight))
                    {
                        string stockLogTip = "";
                        if (!curStore.Equals(store))
                        {
                            stockLogTip += "库存仓库从" + curStore + "修改为" + store + ";";
                        }
                        if (!curWeight.Equals(weight))
                        {
                            stockLogTip += "库存重量从" + curWeight + "kg修改为" + weight + "kg";
                        }
                        //"id", "条形码", "名称", "颜色", "类型", "重量", "适用机型", "仓库", "生产日期", "有效期", "操作员", "操作日期", "操作时间", "操作类型", "告警时间", "备注" 
                        GetSqlLiteHelper().InsertValues(Common.STOCKLOGTABLENAME, new string[] { curBarCode, curName, curColor, curType, weight, curModel, store, curProductDate, curExpiryDate, _userName, DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("HH:mm:ss"), "修改", curWarnDate, stockLogTip });
                    }
                }
            }
        }

        private void CbSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSearchType.SelectedIndex >= 0)
            {
                cbSearchContent.Items.Clear();
                List<string> searchContent = GetSqlLiteHelper().GetValueTypeByColumnFromTable(_tableName, _searchType[cbSearchType.SelectedIndex], null, null, null);
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
                BindDataGirdViewBySearch(dgvData, _tableName, _searchType[cbSearchType.SelectedIndex], cbSearchContent.SelectedItem.ToString());
            }
        }

        private void BtShowAll_Click(object sender, EventArgs e)
        {
            cbSearchType.SelectedIndex = -1;
            cbSearchType.Text = "请选择过滤方式";
            cbSearchContent.SelectedIndex = -1;
            cbSearchContent.Text = "请选择过滤内容";
            cbSearchContent.Items.Clear();
            BindDataGirdView(dgvData, _tableName);
        }

        private void BtExport_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Load(GetSqlLiteHelper().ReadFullTable(_tableName));
            ExcelHelper.ExportExcel(dt);
        }
    }
}
