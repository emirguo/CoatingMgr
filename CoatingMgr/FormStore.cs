using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;

namespace CoatingMgr
{
    public partial class FormStore : Form
    {
        private static SqlLiteHelper sqlLiteHelper = null;
        private static string _tableName = Common.STORETABLENAME;

        public FormStore()
        {
            InitializeComponent();
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
            BindDataGirdView(dgvData, _tableName);//绑定仓库表
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
                UpdateCBContentItems();
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

        private void UpdateCBContentItems()
        {
            cbSearchContent.Items.Clear();
            List<string> searchContent = GetSqlLiteHelper().GetValueTypeByColumnFromTable(_tableName, "名称", null, null, null);
            for (int i = 0; i < searchContent.Count; i++)
            {
                cbSearchContent.Items.Add(searchContent[i]);
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
            if (e.Button == MouseButtons.Right)
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
        private void TSMIModify_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvData.CurrentRow.Cells[0].Value);
            string name = dgvData.CurrentRow.Cells[1].Value.ToString();
            FormStoreAdd formStoreAdd = new FormStoreAdd(this, id, name);
            formStoreAdd.Show();
        }

        private void TSMIDelete_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedCells.Count > 0)
            {
                foreach (DataGridViewRow row in dgvData.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        string id = row.Cells[0].Value.ToString();
                        GetSqlLiteHelper().DeleteValuesAND(_tableName, new string[] { "id" }, new string[] { id }, new string[] { "=" });
                        dgvData.Rows.Remove(row);
                    }
                }
                lbCount.Text = dgvData.Rows.Count + "";
                UpdateCBContentItems();
            }
        }

        private void CbSearchContent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSearchContent.SelectedIndex >= 0)
            {
                BindDataGirdViewBySearch(dgvData, _tableName, "名称", cbSearchContent.SelectedItem.ToString());
            }
        }

        private void BtShowAll_Click(object sender, EventArgs e)
        {
            cbSearchContent.SelectedIndex = -1;
            cbSearchContent.Text = "选择仓库";
            cbSearchContent.Items.Clear();
            BindDataGirdView(dgvData, _tableName);
        }

        private void BtAdd_Click(object sender, EventArgs e)
        {
            FormStoreAdd formStoreAdd = new FormStoreAdd(this);
            formStoreAdd.Show();
        }

    }
}
