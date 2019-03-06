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

namespace CoatingMgr
{
    public partial class FormOut : Form
    {
        private static SqlLiteHelper sqlLiteHelper = null;
        private static string _tableName = Common.OUTSTOCKTABLENAME;
        private string _userName = "";
        private static string[] _cbSearchStock = { "1号仓库", "2号仓库", "3号仓库", "4号仓库" };
        private int rowIndex = 0;

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

        private string GetBarCodeDetails(string barCode)
        {
            string result = "";
            string title = "【条形码明细】\n";
            string barCodeType = "1.代码种类" + "DataMartrix码" + "\n";
            string constitute = "涂料名称：" + "双虎" + "\n" + "重量：" + "1.5kg" + "\n" + "厂商名：" + "双虎涂料有限公司" + "\n" + "生产日期：" + "2018.12.23";
            result = title + barCodeType + constitute;
            return result;
        }

        private void InitData()
        {
            lbUser.Text = _userName;
            ShowTime();

            for (int i = 0; i < _cbSearchStock.Length; i++)
            {
                cbSearchStock.Items.Add(_cbSearchStock[i]);
                cbSearchStock.SelectedIndex = 0;
            }
            BindDataGirdView(dgvOutStockData, _tableName);//绑定数据库表

            this.lbProDescription.Text = GetBarCodeDetails("121212");

        }

        private void BindDataGirdView(DataGridView dataGirdView, string table)
        {
            dataGirdView.Rows.Clear();
            SQLiteDataReader dataReader = GetSqlLiteHelper().ReadFullTable(table);
            if (dataReader.HasRows)
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
            else
            {
                //SetDefaultColumns(dataGirdView, new string[] { "id", "条形码", "名称", "颜色", "类型", "标准重量", "适用机型", "生产日期", "有效期", "仓库名称", "操作员", "操作时间", "操作类型", "告警类型", "备注" });
            }
            lbNumCount.Text = dataGirdView.RowCount + "";
        }

        private void BindDataGirdViewBySearch(DataGridView dataGirdView, string table, string type, string content)
        {
            SQLiteDataReader dataReader = GetSqlLiteHelper().ReadTable(table, new string[] { type }, new string[] { "=" }, new string[] { content });
            if (dataReader.HasRows)
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
                lbNumCount.Text = dataGirdView.RowCount + "";
            }
            else
            {
                MessageBox.Show("未查找到数据");
            }
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

        private void TbBarCode_TextChanged(object sender, EventArgs e)
        {
            if (IsBarCodeValid(tbBarCode.Text.ToString()))//条形码正确，插入数据到入库表中
            {
                GetSqlLiteHelper().InsertValues(_tableName, new string[] { tbBarCode.Text, tbName.Text, "红色", tbType.Text, tbWeight.Text, tbModel.Text, "2019.01.20", tbDate.Text, cbSearchStock.Text, _userName, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "出库", " ", " " });
                UpdateData();
            }
            else
            {
                MessageBox.Show("条形码无效");
            }
        }

        private bool IsBarCodeValid(string barcode)
        {
            bool result = false;
            if (!barcode.Equals("") && barcode.Length > 10)
            {
                tbName.Text = "三鹿";
                tbType.Text = "固化剂";
                tbWeight.Text = "1kg";
                tbModel.Text = "门";
                tbDate.Text = "2020/1/8";
                result = true;
            }
            return result;
        }

        private void UpdateData()
        {
            int index = 0;
            if (dgvOutStockData.RowCount > 0 && dgvOutStockData.CurrentRow != null)
            {
                index = dgvOutStockData.CurrentRow.Index;
            }
            BindDataGirdView(dgvOutStockData, _tableName);
            if (dgvOutStockData.RowCount <= 0)
            {
                return;
            }
            else if ((dgvOutStockData.RowCount - 1) > index)
            {
                this.dgvOutStockData.CurrentCell = this.dgvOutStockData[1, index];
            }
            else
            {
                this.dgvOutStockData.CurrentCell = this.dgvOutStockData[1, (dgvOutStockData.RowCount - 1)];
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvOutStockData.SelectedCells.Count != 0)
            {
                string id = dgvOutStockData.CurrentRow.Cells[0].Value.ToString();
                GetSqlLiteHelper().DeleteValuesAND(_tableName, new string[] { "id" }, new string[] { id }, new string[] { "=" });
                UpdateData();
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (dgvOutStockData.RowCount > 0)
            {
                GetSqlLiteHelper().InsertDataWithoutIdFromOtherTable(Common.STOCKLOGTABLENAME, _tableName);
                GetSqlLiteHelper().ClearTable(_tableName);
                GetSqlLiteHelper().ResetTableId(_tableName);
                UpdateData();
            }
        }

        private void BtModify_Click(object sender, EventArgs e)
        {

        }

        private void DgvOutStockData_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.dgvOutStockData.Rows[e.RowIndex].Selected = true;
                rowIndex = e.RowIndex;
                this.dgvOutStockData.CurrentCell = this.dgvOutStockData.Rows[e.RowIndex].Cells[1];
                this.contextMenuStrip.Show(this.dgvOutStockData, e.Location);
                this.contextMenuStrip.Show(Cursor.Position);
            }
        }

        private void TSMIDeleteRow_Click(object sender, EventArgs e)
        {
            if (!this.dgvOutStockData.Rows[rowIndex].IsNewRow)
            {
                string id = dgvOutStockData.Rows[rowIndex].Cells[0].Value.ToString();
                this.dgvOutStockData.Rows.RemoveAt(rowIndex);
                GetSqlLiteHelper().DeleteValuesAND(_tableName, new string[] { "id" }, new string[] { id }, new string[] { "=" });
                UpdateData();
            }
        }
    }
}
