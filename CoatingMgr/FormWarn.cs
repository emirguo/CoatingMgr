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
    public partial class FormWarn : Form
    {
        private static SqlLiteHelper sqlLiteHelper = null;
        private static string _tableName = Common.WARNMANAGERTABLENAME;
        private string _userName = "";

        private List<string> _cbSelectStock;
        private static string[] _cbSearchType = { "按产品查找", "按颜色查找", "按类型查找" };
        private static string[] _searchType = { "产品", "颜色", "类型" };

        private int rowIndex = 0;

        public FormWarn()
        {
            InitializeComponent();
        }

        public FormWarn(string userName)
        {
            InitializeComponent();
            _userName = userName;
        }

        private void FormWarn_Load(object sender, EventArgs e)
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
            _cbSelectStock = GetSqlLiteHelper().GetValueTypeByColumnFromTable(_tableName, "仓库");
            for (int i = 0; i < _cbSelectStock.Count; i++)
            {
                cbSelectStock.Items.Add(_cbSelectStock[i]);
            }

            for (int i = 0; i < _cbSearchType.Length; i++)
            {
                cbSelectType.Items.Add(_cbSearchType[i]);
                cbSelectType.SelectedIndex = 0;
            }

            BindDataGirdView(dgvWarnMgr, _tableName);//绑定数据库表
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
                //SetDefaultColumns(dataGirdView, new string[] { "id", "仓库", "产品", "颜色", "类型", "库存上限", "库存下限", "告警时间", "告警类型", "规则创建人", "规则创建时间" });
            }
            lbCount.Text = dataGirdView.RowCount + "";
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
            if (dgvWarnMgr.RowCount > 0 && dgvWarnMgr.CurrentRow != null)
            {
                index = dgvWarnMgr.CurrentRow.Index;
            }
            BindDataGirdView(dgvWarnMgr, _tableName);
            if (dgvWarnMgr.RowCount <= 0)
            {
                return;
            }
            else if ((dgvWarnMgr.RowCount - 1) > index)
            {
                this.dgvWarnMgr.CurrentCell = this.dgvWarnMgr[1, index];
            }
            else
            {
                this.dgvWarnMgr.CurrentCell = this.dgvWarnMgr[1, (dgvWarnMgr.RowCount - 1)];
            }
        }

        private void PbSearch_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text == null || tbSearch.Text.ToString().Equals(""))
            {
                BindDataGirdView(dgvWarnMgr, _tableName);
            }
            else
            {
                BindDataGirdViewBySearch(dgvWarnMgr, _tableName, _searchType[cbSelectType.SelectedIndex], tbSearch.Text);
            }
        }

        private void DgvWarnMgr_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.dgvWarnMgr.Rows[e.RowIndex].Selected = true;
                rowIndex = e.RowIndex;
                this.dgvWarnMgr.CurrentCell = this.dgvWarnMgr.Rows[e.RowIndex].Cells[1];
                this.contextMenuStrip.Show(this.dgvWarnMgr, e.Location);
                this.contextMenuStrip.Show(Cursor.Position);
            }
        }

        private void TSMIDeleteRow_Click(object sender, EventArgs e)
        {
            if (!this.dgvWarnMgr.Rows[rowIndex].IsNewRow)
            {
                string id = dgvWarnMgr.Rows[rowIndex].Cells[0].Value.ToString();
                this.dgvWarnMgr.Rows.RemoveAt(rowIndex);
                GetSqlLiteHelper().DeleteValuesAND(_tableName, new string[] { "id" }, new string[] { id }, new string[] { "=" });
                UpdateData();
            }
        }
    }
}
