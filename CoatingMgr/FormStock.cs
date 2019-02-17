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
    public partial class FormStock : Form
    {
        private static SqlLiteHelper sqlLiteHelper = null;
        private static string _tableName = Common.STOCKMANAGERTABLENAME;
        private string _userName = "";

        private static string[] _cbSearchStock = { "1号仓库", "2号仓库","3号仓库", "4号仓库" };

        private static string[] _cbSearchType = { "按名称查找", "按颜色查找", "按类型查找", "按适用机型查找", "按生产日期查找", "按有效期查找", "按操作员查找", "按操作时间查找", "按告警类型查找" };
        private static string[] _searchType = { "名称", "颜色", "类型", "适用机型", "生产日期", "有效期", "操作员", "操作时间", "告警类型" };
        private int rowIndex = 0;

        public FormStock()
        {
            InitializeComponent();
        }

        public FormStock(string userName)
        {
            InitializeComponent();
            _userName = userName;
        }

        private void FormStock_Load(object sender, EventArgs e)
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

            for (int i = 0; i < _cbSearchStock.Length; i++)
            {
                cbSearchStock.Items.Add(_cbSearchStock[i]);
                cbSearchStock.SelectedIndex = 0;
            }
            for (int i = 0; i <_cbSearchType.Length; i++)
            {
                cbSearchType.Items.Add(_cbSearchType[i]);
                cbSearchType.SelectedIndex = 0;
            }

            BindDataGirdView(dgvStockData, _tableName);//绑定数据库表
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
            lbCount.Text = dataGirdView.RowCount + "";
        }

        private void BindDataGirdViewBySearch(DataGridView dataGirdView, string table, string type, string content)
        {
            SQLiteDataReader dataReader = GetSqlLiteHelper().ReadTable(table, new string[] { "*" }, new string[] { type }, new string[] { "=" }, new string[] { content });
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
                lbCount.Text = dataGirdView.RowCount + "";
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

        private void PbSearch_Click(object sender, EventArgs e)
        {
            if (tbSearchContent.Text == null || tbSearchContent.Text.ToString().Equals(""))
            {
                BindDataGirdView(dgvStockData, _tableName);
            }
            else
            {
                BindDataGirdViewBySearch(dgvStockData, _tableName, _searchType[cbSearchType.SelectedIndex], tbSearchContent.Text);
            }
        }

        private void BtDelete_Click(object sender, EventArgs e)
        {
            if (dgvStockData.SelectedCells.Count != 0)
            {
                string id = dgvStockData.CurrentRow.Cells[0].Value.ToString();
                GetSqlLiteHelper().DeleteValuesAND(_tableName, new string[] { "id" }, new string[] { id }, new string[] { "=" });
                UpdateData();
            }
        }

        private void BtModify_Click(object sender, EventArgs e)
        {

        }

        private void DgvStockData_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.dgvStockData.Rows[e.RowIndex].Selected = true;
                rowIndex = e.RowIndex;
                this.dgvStockData.CurrentCell = this.dgvStockData.Rows[e.RowIndex].Cells[1];
                this.contextMenuStrip.Show(this.dgvStockData, e.Location);
                this.contextMenuStrip.Show(Cursor.Position);
            }
        }

        private void TSMIDeleteRow_Click(object sender, EventArgs e)
        {
            if (!this.dgvStockData.Rows[rowIndex].IsNewRow)
            {
                string id = dgvStockData.Rows[rowIndex].Cells[0].Value.ToString();
                this.dgvStockData.Rows.RemoveAt(rowIndex);
                GetSqlLiteHelper().DeleteValuesAND(_tableName, new string[] { "id" }, new string[] { id }, new string[] { "=" });
                UpdateData();
            }
        }
    }
}
