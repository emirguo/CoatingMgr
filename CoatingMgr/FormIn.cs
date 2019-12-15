﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace CoatingMgr
{
    public partial class FormIn : Form
    {
        private string _userName = "";
        private string errorMessage = string.Empty;
        AutoSize asc = new AutoSize();

        public FormIn()
        {
            InitializeComponent();
        }

        public FormIn(string userName)
        {
            InitializeComponent();
            _userName = userName;
        }

        private void FormIn_Load(object sender, EventArgs e)
        {
            asc.controllInitializeSize(this);
            InitData();
        }

        private void FormIn_SizeChanged(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
        }

        private void InitData()
        {
            lbUser.Text = _userName;
            ShowTime();

            cbSearchStock.Items.Clear();
            List<string> searchContent = SQLServerHelper.GetTypesOfColumn(Common.STORETABLENAME, "名称", null, null, null);
            for (int i = 0; i < searchContent.Count; i++)
            {
                cbSearchStock.Items.Add(searchContent[i]);
            }

            SetDefaultColumns(dgvData, Common.INSTOCKTABLECOLUMNS);
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

            //"id", "条形码", "名称", "颜色", "类型", "重量", "适用机型", "仓库", "生产日期", "有效期", "操作员", "入库日期", "入库时间", "告警时间", "备注"
            dataGirdView.Columns[0].Visible = false;
            dataGirdView.Columns[12].Visible = false;
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
                            lbTime.Text = "时间：" + DateTime.Now.ToString()));
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
            cbSearchStock.Text = string.Empty;
            tbName.Text = string.Empty;
            tbType.Text = string.Empty;
            tbColor.Text = string.Empty;
            tbModel.Text = string.Empty;
            tbWeight.Text = string.Empty;
            tbProductionDate.Text = string.Empty;
            tbExpiryDate.Text = string.Empty;
            lbProDescription.Text = string.Empty;
            lbCount.Text = 0 + string.Empty;
            dgvData.Rows.Clear();
            lbResult.Text = string.Empty;
            panelResult.BackColor = Color.Gainsboro;
        }

        public void BarCodeInputEnd(string barcode)
        {
            if (cbSearchStock.Text.Equals(string.Empty))
            {
                MessageBox.Show("请先选择仓库");
                this.tbBarCode.Text = string.Empty;
                return;
            }
            
            if (!barcode.Equals(string.Empty))
            {
                errorMessage = string.Empty;
                int checkResult = CheckBarCodeInStock(barcode);
                if (checkResult > 0)
                {
                    this.tbBarCode.Text = string.Empty;
                    this.lbResult.Text = "NG";
                    this.panelResult.BackColor = Color.Red;
                    if (checkResult == 1)
                    {
                        MessageBox.Show("此条形码涂料已入库，无法重复入库");
                    }
                    else if (checkResult == 2)
                    {
                        MessageBox.Show("数据库读取失败，请检查网络是否正常或者再次一次");
                    }
                    Common.PlayVoice(2);
                    return;
                }
                if (AnalysisBarCode(barcode))
                {
                    //"id", "条形码", "名称", "颜色", "类型", "重量", "适用机型", "仓库", "生产日期", "有效期", "操作员", "入库日期", "入库时间", "告警时间", "备注"
                    //dgvData id = 0,与数据库中的不一致
                    this.dgvData.Rows.Add(0, barcode, tbName.Text, tbColor.Text, tbType.Text, tbWeight.Text, tbModel.Text, cbSearchStock.Text, tbProductionDate.Text, tbExpiryDate.Text, _userName, DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("HH:mm:ss"), string.Empty, string.Empty);
                    int count = this.dgvData.RowCount;
                    this.dgvData.CurrentCell = this.dgvData[1, (count > 1) ? (count - 1) : 0];
                    lbCount.Text = count + "";
                    this.lbResult.Text = "OK";
                    this.panelResult.BackColor = Color.Green;
                    Common.PlayVoice(1);
                    SaveRowToDB(dgvData.CurrentRow);
                }
                else
                {
                    this.lbResult.Text = "NG";
                    this.panelResult.BackColor = Color.Red;
                    MessageBox.Show(errorMessage);
                    Common.PlayVoice(2);
                }
                this.tbBarCode.Text = string.Empty;
                errorMessage = string.Empty;
            }
        }

        private DateTime _dt = DateTime.Now;  //定义一个成员函数用于保存每次的时间点
        private void TbBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            DateTime tempDt = DateTime.Now;          //保存按键按下时刻的时间点
            TimeSpan ts = tempDt.Subtract(_dt);     //获取时间间隔
            _dt = tempDt;
            if (ts.Milliseconds > 100)      //判断时间间隔，如果时间间隔大于100毫秒，则为手动输入，否则为扫码枪输入
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (cbSearchStock.Text.Equals(string.Empty))
                    {
                        MessageBox.Show("请先选择仓库");
                        this.tbBarCode.Text = string.Empty;
                        return;
                    }

                    if (!tbBarCode.Text.Equals(string.Empty))
                    {
                        errorMessage = string.Empty;
                        int checkResult = CheckBarCodeInStock(tbBarCode.Text);
                        if (checkResult > 0)
                        {
                            this.tbBarCode.Text = string.Empty;
                            this.lbResult.Text = "NG";
                            this.panelResult.BackColor = Color.Red;
                            if (checkResult == 1)
                            {
                                MessageBox.Show("此条形码涂料已入库，无法重复入库");
                            }
                            else if (checkResult == 2)
                            {
                                MessageBox.Show("数据库读取失败，请检查网络是否正常或者再次一次");
                            }
                            Common.PlayVoice(2);
                            return;
                        }
                        if (AnalysisBarCode(tbBarCode.Text))
                        {
                            //dgvData id = 0,与数据库中的不一致
                            this.dgvData.Rows.Add(0, tbBarCode.Text, tbName.Text, tbColor.Text, tbType.Text, tbWeight.Text, tbModel.Text, cbSearchStock.Text, tbProductionDate.Text, tbExpiryDate.Text, _userName, DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("HH:mm:ss"), string.Empty, string.Empty);
                            int count = this.dgvData.RowCount;
                            this.dgvData.CurrentCell = this.dgvData[1, (count > 1) ? (count - 1) : 0];
                            lbCount.Text = count + "";
                            this.lbResult.Text = "OK";
                            this.panelResult.BackColor = Color.Green;
                            Common.PlayVoice(1);
                            SaveRowToDB(dgvData.CurrentRow);
                        }
                        else
                        {
                            this.lbResult.Text = "NG";
                            this.panelResult.BackColor = Color.Red;
                            Common.PlayVoice(2);
                            MessageBox.Show(errorMessage);
                        }
                        this.tbBarCode.Text = string.Empty;
                        errorMessage = string.Empty;
                    }
                }
            }
        }

        private int CheckBarCodeInStock(string barcode)
        {
            int result = 0;
            DataTable dt = SQLServerHelper.Read(Common.INSTOCKTABLENAME, new string[] { "条形码" }, new string[] { "=" }, new string[] { barcode });
            if (dt != null && dt.Rows.Count > 0)
            {
                result = 1;
            }
            if (dt == null)//数据库读取失败
            {
                result = 2;
            }
            return result;
        }

        //SAP品番*种类*厂家*重量*批次号*连番*使用期限,例如：CL-5000H(TAP)*A*G1012*18*20180219*0001*20190318
        private bool AnalysisBarCode(string barcode)
        {
            bool result = false;
            if (!barcode.Equals(string.Empty))
            {
                string[] sArray = barcode.Split('*');
                if (sArray.Length == 7)
                {
                    tbName.Text = sArray[0];
                    tbWeight.Text = sArray[3];
                    tbProductionDate.Text = sArray[4];
                    tbExpiryDate.Text = sArray[6];

                    DataTable dt = SQLServerHelper.Read(Common.MASTERTABLENAME, new string[] { "SAP品番" }, new string[] { "=" }, new string[] { tbName.Text });
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        tbColor.Text = dt.Rows[0]["色番"].ToString();
                        tbModel.Text = dt.Rows[0]["适用机种"].ToString();
                        tbType.Text = dt.Rows[0]["种类"].ToString();
                        if (tbType.Text.Equals(string.Empty))
                        {
                            tbType.Text = Common.COATINGTYPE[sArray[1]];
                        }
                    }
                    else
                    {
                        tbName.Text = string.Empty;
                        tbType.Text = string.Empty;
                        tbWeight.Text = string.Empty;
                        tbProductionDate.Text = string.Empty;
                        tbExpiryDate.Text = string.Empty;
                        tbColor.Text = string.Empty;
                        tbModel.Text = string.Empty;
                        errorMessage = "Master文件中未找到此涂料";
                        return result;
                    }

                    string title = "【产品明细】\n";
                    string name = "名称：" + tbName.Text + "\n";
                    string type = "类型：" + tbType.Text + "\n";
                    string weight = "重量：" + tbWeight.Text + "kg" + "\n";
                    string color = "颜色：" + tbColor.Text + "\n";
                    string model = "适用机种：" + tbModel.Text + "\n";
                    string manufacturer = "厂商：" + dt.Rows[0]["制造商"].ToString() + "\n";//Common.FACTORY[sArray[2]] + "\n";
                    string productionDate = "生产日期：" + tbProductionDate.Text + "\n";
                    string expiryDate = "有效期：" + tbExpiryDate.Text + "\n";
                    this.lbProDescription.Text = title + name + type + color + weight + model + manufacturer + productionDate + expiryDate;
                    result = true;
                }
                else
                {
                    errorMessage = "条形码无效";
                }
            }
            else
            {
                errorMessage = "条形码无效";
            }
            return result;
        }

        private void SaveRowToDB(DataGridViewRow dataRow)
        {
            //"id", "条形码", "名称", "颜色", "类型", "重量", "适用机型", "仓库", "生产日期", "有效期", "操作员", "入库日期", "入库时间", "告警时间", "备注"
            string barcode = dataRow.Cells[1].Value.ToString();
            string name = dataRow.Cells[2].Value.ToString();
            string color = dataRow.Cells[3].Value.ToString();
            string type = dataRow.Cells[4].Value.ToString();
            string weight = dataRow.Cells[5].Value.ToString();
            string model = dataRow.Cells[6].Value.ToString();
            string store = dataRow.Cells[7].Value.ToString();
            
            //从库存统计数量中增加入库
            //"id", "类型", "名称", "颜色", "适用机型", "重量", "库存上限", "库存下限", "告警时间", "备注"
            DataTable dt = SQLServerHelper.Read(Common.STOCKCOUNTTABLENAME, new string[] { "类型", "名称", "颜色", "适用机型" }, new string[] { "=", "=", "=", "=" }, new string[] { type, name, color, model });
            if (dt != null && dt.Rows.Count > 0)//色剂已经存在
            {
                double inStockWeight = Convert.ToSingle(Common.FilterChar(dt.Rows[0]["重量"].ToString()));
                double inputWeight = Convert.ToSingle(Common.FilterChar(weight));
                inStockWeight += inputWeight;
                SQLServerHelper.Update(Common.STOCKCOUNTTABLENAME, new string[] { "重量" }, new string[] { inStockWeight.ToString() }, "id", dt.Rows[0]["id"].ToString());
            }
            else
            {
                SQLServerHelper.Insert(Common.STOCKCOUNTTABLENAME, new string[] { "类型", "名称", "颜色", "适用机型", "重量", "库存上限", "库存下限", "告警时间", "备注" },
                        new string[] { type, name, color, model, weight, string.Empty, string.Empty, string.Empty, string.Empty });
            }

            List<string> values = new List<string>();
            for (int i = 1; i < dataRow.Cells.Count; i++)
            {
                values.Add(dataRow.Cells[i].Value.ToString());
            }
            //增加到在库表中
            SQLServerHelper.Insert(Common.INSTOCKTABLENAME, new string[] { "条形码", "名称", "颜色", "类型", "重量", "适用机型", "仓库", "生产日期", "有效期", "操作员", "入库日期", "入库时间", "告警时间", "备注" }, values.ToArray());

            //存入日志 
            //"id", "条形码", "名称", "颜色", "类型", "重量", "适用机型", "仓库", "生产日期", "有效期", "操作员", "操作日期", "操作时间", "操作类型", "告警时间", "备注"
            values.Insert(12, "入库");
            SQLServerHelper.Insert(Common.STOCKLOGTABLENAME, new string[] { "条形码", "名称", "颜色", "类型", "重量", "适用机型", "仓库", "生产日期", "有效期", "操作员", "操作日期", "操作时间", "操作类型", "告警时间", "备注" }, values.ToArray());
        }

        private void DgvData_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.contextMenuStrip.Show(this.dgvData, e.Location);
                this.contextMenuStrip.Show(Cursor.Position);
            }
        }

        private void TSMIDelete_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedCells.Count > 0)
            {
                foreach (DataGridViewRow dataRow in dgvData.SelectedRows)
                {
                    if (!dataRow.IsNewRow)
                    {
                        //"id", "条形码", "名称", "颜色", "类型", "重量", "适用机型", "仓库", "生产日期", "有效期", "操作员", "入库日期", "入库时间", "告警时间", "备注"
                        string barcode = dataRow.Cells[1].Value.ToString();
                        string name = dataRow.Cells[2].Value.ToString();
                        string color = dataRow.Cells[3].Value.ToString();
                        string type = dataRow.Cells[4].Value.ToString();
                        string weight = dataRow.Cells[5].Value.ToString();
                        string model = dataRow.Cells[6].Value.ToString();
                        string store = dataRow.Cells[7].Value.ToString();

                        //从库存数量中减去入库数量
                        DataTable dt = SQLServerHelper.Read(Common.STOCKCOUNTTABLENAME, new string[] { "类型", "名称", "颜色", "适用机型" }, new string[] { "=", "=", "=", "=" }, new string[] { type, name, color, model });
                        if (dt != null && dt.Rows.Count > 0)//色剂已经存在
                        {
                            double inStockWeight = Convert.ToSingle(Common.FilterChar(dt.Rows[0]["重量"].ToString()));
                            double inputWeight = Convert.ToSingle(Common.FilterChar(weight));
                            inStockWeight -= inputWeight;
                            if (inStockWeight < 0.000001)//总量为0时
                            {
                                SQLServerHelper.Delete(Common.STOCKCOUNTTABLENAME, new string[] { "id" }, new string[] { dt.Rows[0]["id"].ToString() }, new string[] { "=" }, null);
                            }
                            else
                            {
                                SQLServerHelper.Update(Common.STOCKCOUNTTABLENAME, new string[] { "重量" }, new string[] { inStockWeight.ToString() }, "id", dt.Rows[0]["id"].ToString());
                            }
                        }

                        //从在库表中删除
                        SQLServerHelper.Delete(Common.INSTOCKTABLENAME, new string[] { "条形码" }, new string[] { barcode }, new string[] { "=" }, null);

                        //存入日志
                        //"id", "条形码", "名称", "颜色", "类型", "重量", "适用机型", "仓库", "生产日期", "有效期", "操作员", "操作日期", "操作时间", "操作类型", "告警时间", "备注"
                        List<string> values = new List<string>();
                        for (int i = 1; i < dataRow.Cells.Count; i++)
                        {
                            values.Add(dataRow.Cells[i].Value.ToString());
                        }
                        values.Insert(12, "入库后删除入库");
                        SQLServerHelper.Insert(Common.STOCKLOGTABLENAME, new string[] { "条形码", "名称", "颜色", "类型", "重量", "适用机型", "仓库", "生产日期", "有效期", "操作员", "操作日期", "操作时间", "操作类型", "告警时间", "备注" }, values.ToArray());

                        dgvData.Rows.Remove(dataRow);
                    }
                }
                lbCount.Text = dgvData.Rows.Count + "";
            }
        }

    }
}
