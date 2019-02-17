using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoatingMgr
{
    public partial class FormAccountManager : Form
    {
        private static SqlLiteHelper sqlLiteHelper = null;
        private static string _tableName = Common.ACCOUNTTABLENAME;

        private static string[] _cbSearchType = {"按账号查找","按权限查找"};
        private static string[] _searchType = { "账号", "权限" };
        private int rowIndex = 0;

        public FormAccountManager()
        {
            InitializeComponent();
        }

        private void BtAdd_Click(object sender, EventArgs e)
        {
            FormAddAccount formAddAccount = new FormAddAccount(this);
            formAddAccount.Show();
        }

        private void BtModify_Click(object sender, EventArgs e)
        {
            if (dgvAccountData.SelectedCells.Count != 0)
            {
                int id = Convert.ToInt32(dgvAccountData.CurrentRow.Cells[0].Value);
                string name = dgvAccountData.CurrentRow.Cells[1].Value.ToString();
                string pwd = dgvAccountData.CurrentRow.Cells[2].Value.ToString();
                string permission = dgvAccountData.CurrentRow.Cells[3].Value.ToString();
                FormAddAccount formAddAccount = new FormAddAccount(this, id, name, pwd, permission);
                formAddAccount.Show();
            }
            else
            {
                MessageBox.Show("请选择要修改的账户");
            }

        }

        private void BtDelete_Click(object sender, EventArgs e)
        {
            if (dgvAccountData.SelectedCells.Count != 0)
            {
                string id = dgvAccountData.CurrentRow.Cells[0].Value.ToString();
                GetSqlLiteHelper().DeleteValuesAND(_tableName, new string[] { "id" }, new string[] { id }, new string[] { "=" });
                UpdateData();
            }
        }

        private void FormAccountManager_Load(object sender, EventArgs e)
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
            cbQueryType.Items.Add(_cbSearchType[0]);
            cbQueryType.Items.Add(_cbSearchType[1]);
            cbQueryType.SelectedIndex = 0;

            BindDataGirdView(dgvAccountData, _tableName);//绑定account表
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
                //SetDefaultColumns(dataGirdView,new string[] { "id", "账号", "密码", "权限" });
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

        private void PbQuery_Click(object sender, EventArgs e)
        {
            if (tbQueryContent.Text == null || tbQueryContent.Text.ToString().Equals(""))
            {
                BindDataGirdView(dgvAccountData, _tableName);
            }
            else
            {
                BindDataGirdViewBySearch(dgvAccountData, _tableName, _searchType[cbQueryType.SelectedIndex], tbQueryContent.Text);
            }
        }

        private void DgvAccountData_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.dgvAccountData.Rows[e.RowIndex].Selected = true;
                rowIndex = e.RowIndex;
                this.dgvAccountData.CurrentCell = this.dgvAccountData.Rows[e.RowIndex].Cells[1];
                this.contextMenuStrip.Show(this.dgvAccountData, e.Location);
                this.contextMenuStrip.Show(Cursor.Position);
            }
        }

        private void TSMIDeleteRow_Click(object sender, EventArgs e)
        {
            if (!this.dgvAccountData.Rows[rowIndex].IsNewRow)
            {
                string id = dgvAccountData.Rows[rowIndex].Cells[0].Value.ToString();
                this.dgvAccountData.Rows.RemoveAt(rowIndex);
                GetSqlLiteHelper().DeleteValuesAND(_tableName, new string[] { "id" }, new string[] { id }, new string[] { "=" });
                UpdateData();
            }
        }
    }
}
