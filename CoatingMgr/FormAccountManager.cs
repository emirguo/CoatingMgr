using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace CoatingMgr
{
    public partial class FormAccountManager : Form
    {
        private static string _tableName = Common.ACCOUNTTABLENAME;

        private static string[] _cbSearchType = {"按账号查找","按权限查找"};
        private static string[] _searchType = { "账号", "权限" };

        AutoSize asc = new AutoSize();

        public FormAccountManager()
        {
            InitializeComponent();
        }

        private void FormAccountManager_Load(object sender, EventArgs e)
        {
            asc.controllInitializeSize(this);
            InitData();
        }

        private void FormAccountManager_SizeChanged(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
        }

        private void InitData()
        {
            for (int i = 0; i < _cbSearchType.Length; i++)
            {
                cbSearchType.Items.Add(_cbSearchType[i]);
            }

            BindDataGirdView(dgvAccountData, _tableName);//绑定account表
        }

        private void BindDataGirdView(DataGridView dataGirdView, string table)
        {
            DataTable dt = SQLServerHelper.Read(table);
            if (dt != null && dt.Rows.Count > 0)
            {
                BindingSource bs = new BindingSource
                {
                    DataSource = dt
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
            DataTable dt = SQLServerHelper.Read(table, new string[] { type }, new string[] { "=" }, new string[] { content });
            if (dt != null && dt.Rows.Count > 0)
            {
                BindingSource bs = new BindingSource
                {
                    DataSource = dt
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
            if (dgvAccountData.RowCount > 0 && dgvAccountData.CurrentRow != null)
            {
                index = dgvAccountData.CurrentRow.Index;
            }
            BindDataGirdView(dgvAccountData, _tableName);
            if (dgvAccountData.RowCount <= 0)
            {
                return;
            }
            else if ((dgvAccountData.RowCount - 1) > index)
            {
                this.dgvAccountData.CurrentCell = this.dgvAccountData[1, index];
            }
            else
            {
                this.dgvAccountData.CurrentCell = this.dgvAccountData[1, (dgvAccountData.RowCount - 1)];
            }
        }

        private void BtAdd_Click(object sender, EventArgs e)
        {
            FormAddAccount formAddAccount = new FormAddAccount(this);
            formAddAccount.Show();
        }

        private void DgvAccountData_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.contextMenuStrip.Show(this.dgvAccountData, e.Location);
                this.contextMenuStrip.Show(Cursor.Position);
                if (dgvAccountData.SelectedRows.Count == 1)
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
            int id = Convert.ToInt32(dgvAccountData.CurrentRow.Cells[0].Value);
            string name = dgvAccountData.CurrentRow.Cells[1].Value.ToString();
            string pwd = dgvAccountData.CurrentRow.Cells[2].Value.ToString();
            string permission = dgvAccountData.CurrentRow.Cells[3].Value.ToString();
            FormAddAccount formAddAccount = new FormAddAccount(this, id, name, pwd, permission);
            formAddAccount.Show();
        }

        private void TSMIDelete_Click(object sender, EventArgs e)
        {
            if (dgvAccountData.SelectedCells.Count > 0)
            {
                foreach (DataGridViewRow row in dgvAccountData.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        string id = row.Cells[0].Value.ToString();
                        SQLServerHelper.Delete(_tableName, new string[] { "id" }, new string[] { id }, new string[] { "=" }, null);
                        dgvAccountData.Rows.Remove(row);
                    }
                }
                lbCount.Text = dgvAccountData.Rows.Count + "";
            }
        }

        private void CbSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSearchType.SelectedIndex >= 0)
            {
                cbSearchContent.Items.Clear();
                List<string> searchContent = SQLServerHelper.GetTypesOfColumn(_tableName, _searchType[cbSearchType.SelectedIndex], null, null, null);
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
                BindDataGirdViewBySearch(dgvAccountData, _tableName, _searchType[cbSearchType.SelectedIndex], cbSearchContent.SelectedItem.ToString());
            }
        }

        private void BtShowAll_Click(object sender, EventArgs e)
        {
            cbSearchType.SelectedIndex = -1;
            cbSearchType.Text = "请选择过滤方式";
            cbSearchContent.SelectedIndex = -1;
            cbSearchContent.Text = "请选择过滤内容";
            cbSearchContent.Items.Clear();
            BindDataGirdView(dgvAccountData, _tableName);
        }

    }
}
