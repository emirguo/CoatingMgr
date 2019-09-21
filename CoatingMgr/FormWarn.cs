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

        //"id", "名称", "颜色", "类型", "库存上限", "库存下限", "告警时间", "规则创建人", "规则创建时间" 
        private static string[] _cbSearchType = { "按名称查找", "按颜色查找", "按类型查找", "按库存上限查找", "按库存下限查找", "按告警时间查找", "按规则创建人查找", "按规则创建时间查找" };
        private static string[] _searchType = { "名称", "颜色", "类型", "库存上限", "库存下限", "告警时间", "规则创建人", "规则创建时间" };

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
            for (int i = 0; i < _cbSearchType.Length; i++)
            {
                cbSearchType.Items.Add(_cbSearchType[i]);
            }

            BindDataGirdView(dgvWarnMgr, _tableName);//绑定数据库表
        }

        private void BindDataGirdView(DataGridView dataGirdView, string table)
        {
            dataGirdView.Rows.Clear();
            SQLiteDataReader dataReader = GetSqlLiteHelper().Read(table);
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
            SQLiteDataReader dataReader = GetSqlLiteHelper().Read(table, new string[] { type }, new string[] { "=" }, new string[] { content });
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

        private void DgvWarnMgr_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.contextMenuStrip.Show(this.dgvWarnMgr, e.Location);
                this.contextMenuStrip.Show(Cursor.Position);
                if (dgvWarnMgr.SelectedRows.Count == 1)
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
            if (dgvWarnMgr.SelectedCells.Count > 0)
            {
                foreach (DataGridViewRow row in dgvWarnMgr.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        string id = row.Cells[0].Value.ToString();
                        GetSqlLiteHelper().DeleteValuesAND(_tableName, new string[] { "id" }, new string[] { id }, new string[] { "=" });

                        dgvWarnMgr.Rows.Remove(row);
                    }
                }
            }
        }

        private void TSMIModify_Click(object sender, EventArgs e)
        {
            //"id", "名称", "颜色", "类型", "库存上限", "库存下限", "告警时间", "规则创建人", "规则创建时间" 
            string id = dgvWarnMgr.CurrentRow.Cells[0].Value.ToString();
            string name = dgvWarnMgr.CurrentRow.Cells[1].Value.ToString();
            string color = dgvWarnMgr.CurrentRow.Cells[2].Value.ToString();
            string type = dgvWarnMgr.CurrentRow.Cells[3].Value.ToString();
            string warnMaxmum = dgvWarnMgr.CurrentRow.Cells[4].Value.ToString();
            string warnMinimum = dgvWarnMgr.CurrentRow.Cells[5].Value.ToString();
            string warnTime = dgvWarnMgr.CurrentRow.Cells[6].Value.ToString();
            FormSetWarn formSetWarn = new FormSetWarn(this, _userName, true, id, name, color, type, warnMaxmum, warnMinimum, warnTime);
            formSetWarn.Show();
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
                BindDataGirdViewBySearch(dgvWarnMgr, _tableName, _searchType[cbSearchType.SelectedIndex], cbSearchContent.SelectedItem.ToString());
            }
        }

        private void BtShowAll_Click(object sender, EventArgs e)
        {
            cbSearchType.SelectedIndex = -1;
            cbSearchType.Text = "请选择过滤方式";
            cbSearchContent.SelectedIndex = -1;
            cbSearchContent.Text = "请选择过滤内容";
            cbSearchContent.Items.Clear();
            BindDataGirdView(dgvWarnMgr, _tableName);
        }

        private void BtAdd_Click(object sender, EventArgs e)
        {
            FormSetWarn formSetWarn = new FormSetWarn(this, _userName);
            formSetWarn.Show();
        }
    }
}
