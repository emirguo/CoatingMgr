using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Eval;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace CoatingMgr
{
    public partial class FormLog : Form
    {
        private static SqlLiteHelper sqlLiteHelper = null;
        private static string _tableName = Common.LOGTABLENAME;

        private static string[] _cbSearchStock = { "1号仓库", "2号仓库", "3号仓库", "4号仓库" };

        private static string[] _cbSearchType = { "按名称查找", "按颜色查找", "按类型查找", "按适用机型查找", "按生产日期查找", "按有效期查找", "按操作员查找", "按操作时间查找", "按操作类型查找", "按告警类型查找" };
        private static string[] _searchType = { "名称", "颜色", "类型", "适用机型", "生产日期", "有效期", "操作员", "操作时间", "操作类型", "告警类型" };

        public FormLog()
        {
            InitializeComponent();
        }

        private void FormLog_Load(object sender, EventArgs e)
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
            for (int i = 0; i < _cbSearchStock.Length; i++)
            {
                cbSearchStock.Items.Add(_cbSearchStock[i]);
                cbSearchStock.SelectedIndex = 0;
            }
            for (int i = 0; i < _cbSearchType.Length; i++)
            {
                cbSearchType.Items.Add(_cbSearchType[i]);
                cbSearchType.SelectedIndex = 0;
            }

            BindDataGirdView(dgvLogData, _tableName);//绑定数据库表
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
            if (dgvLogData.RowCount > 0 && dgvLogData.CurrentRow != null)
            {
                index = dgvLogData.CurrentRow.Index;
            }
            BindDataGirdView(dgvLogData, _tableName);
            if (dgvLogData.RowCount <= 0)
            {
                return;
            }
            else if ((dgvLogData.RowCount - 1) > index)
            {
                this.dgvLogData.CurrentCell = this.dgvLogData[1, index];
            }
            else
            {
                this.dgvLogData.CurrentCell = this.dgvLogData[1, (dgvLogData.RowCount - 1)];
            }
        }

        private void PbSearch_Click(object sender, EventArgs e)
        {
            if (tbSearchContent.Text == null || tbSearchContent.Text.ToString().Equals(""))
            {
                BindDataGirdView(dgvLogData, _tableName);
            }
            else
            {
                BindDataGirdViewBySearch(dgvLogData, _tableName, _searchType[cbSearchType.SelectedIndex], tbSearchContent.Text);
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Load(GetSqlLiteHelper().ReadFullTable(_tableName));
            ExcelHelper.ExportExcel(dt);
        }

        
    }
}
