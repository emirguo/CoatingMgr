﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading;
using System.Windows.Forms;

namespace CoatingMgr
{
    public partial class FormOut : Form
    {
        private static SqlLiteHelper sqlLiteHelper = null;
        private string _userName = "";

        public FormOut()
        {
            InitializeComponent();
        }

        public FormOut(string userName)
        {
            InitializeComponent();
            _userName = userName;
        }

        private void FormOut_Load(object sender, EventArgs e)
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

            SetDefaultColumns(dgvStockData, Common.INSTOCKTABLECOLUMNS);
        }

        private void SetDefaultColumns(DataGridView dataGirdView, string[] columns)
        {
            for (int i = 0; i < columns.Length; i++)
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn
                {
                    HeaderText = columns[i]
                };
                dataGirdView.Columns.Add(column);
            }

            dataGirdView.Columns[0].Visible = false;
            dataGirdView.Columns[14].Visible = false;
        }

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
                            lbTime.Text = "时间："+DateTime.Now.ToString()));
                    }
                    catch { }
                    Thread.Sleep(1000);
                }
            })
            { IsBackground = true }.Start();
        }

        public void Clear()
        {
            tbBarCode.Text = string.Empty;
            tbStore.Text = string.Empty;
            tbName.Text = string.Empty;
            tbType.Text = string.Empty;
            tbColor.Text = string.Empty;
            tbModel.Text = string.Empty;
            tbWeight.Text = string.Empty;
            tbProductionDate.Text = string.Empty;
            tbExpiryDate.Text = string.Empty;
            lbProDescription.Text = string.Empty;
            lbCount.Text = 0 + string.Empty;
            dgvStockData.Rows.Clear();
        }

        public void BarCodeInputEnd(string barcode)
        {
            if (tbBarCode.Focused && !tbBarCode.Text.ToString().Equals(""))
            {
                if (!IsBarCodeInStock(tbBarCode.Text.ToString()))
                {
                    tbBarCode.Text = string.Empty;
                    MessageBox.Show("此条形码涂料还未入库，请先入库");
                    return;
                }
                if (AnalysisBarCode(tbBarCode.Text.ToString()))
                {
                    this.dgvStockData.Rows.Add(0, tbBarCode.Text, tbName.Text, tbColor.Text, tbType.Text, tbWeight.Text, tbModel.Text, tbStore.Text, tbProductionDate.Text, tbExpiryDate.Text, _userName, DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("HH:mm:ss"), "出库", " ", " ");
                    int count = this.dgvStockData.RowCount;
                    this.dgvStockData.CurrentCell = this.dgvStockData[1, (count > 1) ? (count - 1) : 0];
                    lbCount.Text = count + "";
                    SaveRowToDB(dgvStockData.CurrentRow);
                }
                else
                {
                    MessageBox.Show("条形码无效");
                }
                tbBarCode.Text = string.Empty;
            }
        }

        private void TbBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BarCodeInputEnd("");
            }
        }

        private bool IsBarCodeInStock(string barcode)
        {
            bool result = false;
            SQLiteDataReader dataReader = GetSqlLiteHelper().ReadTable(Common.INSTOCKTABLENAME, new string[] { "条形码" }, new string[] { "=" }, new string[] { barcode });
            if (dataReader != null && dataReader.HasRows && dataReader.Read())
            {
                tbStore.Text = dataReader["仓库"].ToString();
                result = true;
            }
            return result;
        }

        //涂料名*种类*厂家*重量*批次号*连番*使用期限,例如：R-255 HARDENER  (TAP)*A*G1000*18*20190219*0001*20190818
        private bool AnalysisBarCode(string barcode)
        {
            bool result = false;
            if (!barcode.Equals(""))
            {
                string[] sArray = barcode.Split('*');
                if (sArray.Length >= 7)
                {
                    tbName.Text = sArray[0];
                    tbType.Text = Common.COATINGTYPE[sArray[1]];
                    tbWeight.Text = sArray[3];
                    tbProductionDate.Text = sArray[4];
                    tbExpiryDate.Text = sArray[6];

                    SQLiteDataReader dataReader = GetSqlLiteHelper().ReadTable(Common.MASTERTABLENAME, new string[] { "涂料名" }, new string[] { "=" }, new string[] { tbName.Text });
                    if (dataReader != null && dataReader.HasRows && dataReader.Read())
                    {
                        tbColor.Text = dataReader["色番"].ToString();
                        tbModel.Text = dataReader["适用机种"].ToString();
                    }
                    else
                    {
                        tbColor.Text = "";
                        tbModel.Text = "";
                        MessageBox.Show("Master文件中未找到此涂料");
                    }

                    string title = "【产品明细】\n";
                    string name = "名称：" + tbName.Text + "\n";
                    string type = "类型：" + tbType.Text + "\n";
                    string weight = "重量：" + tbWeight.Text + "kg" + "\n";
                    string color = "颜色：" + tbColor.Text + "\n";
                    string model = "适用机种：" + tbModel.Text + "\n";
                    string manufacturer = "厂商：" + sArray[2] + "\n";
                    string productionDate = "生产日期：" + tbProductionDate.Text + "\n";
                    string expiryDate = "有效期：" + tbExpiryDate.Text + "\n";
                    this.lbProDescription.Text = title + name + type + color + weight + model + manufacturer + productionDate + expiryDate;
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }

        private SynchronizationContext m_SyncContext = null;
        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (dgvStockData.RowCount > 0)
            {
                m_SyncContext = SynchronizationContext.Current;
                Common.ShowProgress();
                Thread t = new Thread(new ThreadStart(SaveDataToDB));//起线程保存数据
                t.Start();
            }
        }

        private void SaveRowToDB(DataGridViewRow dataRow)
        {
            string barcode = dataRow.Cells[1].Value.ToString();
            string name = dataRow.Cells[2].Value.ToString();
            string color = dataRow.Cells[3].Value.ToString();
            string type = dataRow.Cells[4].Value.ToString();
            string weight = dataRow.Cells[5].Value.ToString();
            string model = dataRow.Cells[6].Value.ToString();
            string stock = dataRow.Cells[7].Value.ToString();
            //从库存数量中减去出库
            SQLiteDataReader dataReader = GetSqlLiteHelper().ReadTable(Common.STOCKCOUNTTABLENAME, new string[] { "类型", "名称", "颜色", "适用机型" }, new string[] { "=", "=", "=", "=" }, new string[] { type, name, color, model });
            if (dataReader != null && dataReader.HasRows && dataReader.Read())//色剂已经存在
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
            else
            {
                MessageBox.Show("色剂" + name + "不在库存中");
            }

            //从在库表中删除
            GetSqlLiteHelper().DeleteValuesAND(Common.INSTOCKTABLENAME, new string[] { "条形码" }, new string[] { barcode }, new string[] { "=" });

            //存入日志 
            List<string> values = new List<string>();
            for (int i = 1; i < dataRow.Cells.Count; i++)
            {
                values.Add(dataRow.Cells[i].Value.ToString());
            }
            GetSqlLiteHelper().InsertValues(Common.STOCKLOGTABLENAME, values);
        }

        private void SaveDataToDB()
        {
            if (dgvStockData.RowCount > 0)
            {
                foreach (DataGridViewRow dataRow in dgvStockData.Rows)
                {
                    string barcode = dataRow.Cells[1].Value.ToString();
                    string name = dataRow.Cells[2].Value.ToString();
                    string color = dataRow.Cells[3].Value.ToString();
                    string type = dataRow.Cells[4].Value.ToString();
                    string weight = dataRow.Cells[5].Value.ToString();
                    string model = dataRow.Cells[6].Value.ToString();
                    string stock = dataRow.Cells[7].Value.ToString();
                    //从库存数量中减去出库
                    SQLiteDataReader dataReader = GetSqlLiteHelper().ReadTable(Common.STOCKCOUNTTABLENAME, new string[] { "类型", "名称", "颜色", "适用机型" }, new string[] { "=", "=", "=", "=" }, new string[] { type, name, color, model });
                    if (dataReader != null && dataReader.HasRows && dataReader.Read())//色剂已经存在
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
                    else
                    {
                        MessageBox.Show("色剂" + name + "不在库存中");
                    }

                    //从在库表中删除
                    GetSqlLiteHelper().DeleteValuesAND(Common.INSTOCKTABLENAME, new string[] { "条形码" }, new string[] { barcode }, new string[] { "=" });

                    //存入日志 
                    List<string> values = new List<string>();
                    for (int i = 1; i < dataRow.Cells.Count; i++)
                    {
                        values.Add(dataRow.Cells[i].Value.ToString());
                    }
                    GetSqlLiteHelper().InsertValues(Common.STOCKLOGTABLENAME, values);
                }
            }
            m_SyncContext.Post(UpdateUIAfterThread, "");//线程结束后更新UI
        }

        private void UpdateUIAfterThread(object obj)
        {
            Common.CloseProgress();
            dgvStockData.Rows.Clear();
            lbCount.Text = 0 + "";
        }

        private void DgvOutStockData_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.contextMenuStrip.Show(this.dgvStockData, e.Location);
                this.contextMenuStrip.Show(Cursor.Position);
            }
        }

        private void TSMIDelete_Click(object sender, EventArgs e)
        {
            if (dgvStockData.SelectedCells.Count > 0)
            {
                foreach (DataGridViewRow dataRow in dgvStockData.SelectedRows)
                {
                    if (!dataRow.IsNewRow)
                    {
                        string barCode = dataRow.Cells[1].Value.ToString();
                        string name = dataRow.Cells[2].Value.ToString();
                        string color = dataRow.Cells[3].Value.ToString();
                        string type = dataRow.Cells[4].Value.ToString();
                        string weight = dataRow.Cells[5].Value.ToString();
                        string model = dataRow.Cells[6].Value.ToString();
                        string stock = dataRow.Cells[7].Value.ToString();
                        string tip = dataRow.Cells[15].Value.ToString();

                        //从库存数量中还原已出库数据
                        SQLiteDataReader dataReader = GetSqlLiteHelper().ReadTable(Common.STOCKCOUNTTABLENAME, new string[] { "类型", "名称", "颜色", "适用机型" }, new string[] { "=", "=", "=", "=" }, new string[] { type, name, color, model });
                        if (dataReader != null && dataReader.HasRows && dataReader.Read())//色剂已经存在
                        {
                            double inStockWeight = Convert.ToSingle(Common.FilterChar(dataReader["重量"].ToString()));
                            double inputWeight = Convert.ToSingle(Common.FilterChar(weight));
                            inStockWeight += inputWeight;
                            GetSqlLiteHelper().UpdateValues(Common.STOCKCOUNTTABLENAME, new string[] { "重量" }, new string[] { inStockWeight.ToString() }, "id", dataReader["id"].ToString());
                        }
                        else
                        {
                            GetSqlLiteHelper().InsertValues(Common.STOCKCOUNTTABLENAME, new string[] { type, name, color, model, stock, weight, "", "", "", tip });
                        }
                        
                        List<string> values = new List<string>();
                        for (int i = 1; i < dataRow.Cells.Count; i++)
                        {
                            values.Add(dataRow.Cells[i].Value.ToString());
                        }
                        int index = values.LastIndexOf("出库");
                        values.RemoveAt(index);
                        values.Insert(index, "入库");
                        //添加到入库表中
                        GetSqlLiteHelper().InsertValues(Common.INSTOCKTABLENAME, values);
                        //存入日志
                        values.RemoveAt(index);
                        values.Insert(index, "删除");
                        values.RemoveAt(14);
                        values.Insert(14, "出库后删除出库");
                        GetSqlLiteHelper().InsertValues(Common.STOCKLOGTABLENAME, values);

                        dgvStockData.Rows.Remove(dataRow);
                    }
                }
                lbCount.Text = dgvStockData.Rows.Count + "";
            }
        }
        
    }
}
