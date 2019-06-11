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
        private string _tableName = Common.STOCKLOGTABLENAME;

        private static string[] _cbStockSearchType = { "按仓库查找", "按名称查找", "按颜色查找", "按类型查找", "按适用机型查找", "按生产日期查找", "按有效期查找", "按操作员查找", "按操作类型查找" };
        private static string[] _stockSearchType = { "仓库", "名称", "颜色", "类型", "适用机型", "生产日期", "有效期", "操作员", "操作类型" };

        private static string[] _cbStirSearchType = { "按机种查找", "按製品查找", "按色番查找", "按涂层查找", "按调和比例查找", "按类型查找", "按名称查找", "按操作员查找", "按确认主管查找" };
        private static string[] _stirSearchType = { "机种", "製品", "色番", "涂层", "调和比例", "类型", "名称", "操作员", "确认主管", };

        public FormLog()
        {
            InitializeComponent();
        }

        public void InitData(string tableName)
        {
            _tableName = tableName;

            this.dateTimePickerStart.Format = DateTimePickerFormat.Custom;
            this.dateTimePickerStart.CustomFormat = " ";//必须为空格，为空时显示当前时间
            this.dateTimePickerEnd.Format = DateTimePickerFormat.Custom;
            this.dateTimePickerEnd.CustomFormat = " ";

            cbSearchType.Items.Clear();
            cbSearchContent.Items.Clear();

            if (Common.STOCKLOGTABLENAME.Equals(_tableName))
            {
                this.lbTitle.Text = "库存日志查询";
                for (int i = 0; i < _cbStockSearchType.Length; i++)
                {
                    cbSearchType.Items.Add(_cbStockSearchType[i]);
                }
            }
            else if (Common.STIRLOGTABLENAME.Equals(_tableName))
            {
                this.lbTitle.Text = "调和日志查询";
                for (int i = 0; i < _cbStirSearchType.Length; i++)
                {
                    cbSearchType.Items.Add(_cbStirSearchType[i]);
                }
            }
        }

        private void FormLog_Load(object sender, EventArgs e)
        {
            BindDataGirdView(dgvLogData, _tableName);//绑定数据库表
        }

        private SqlLiteHelper GetSqlLiteHelper()
        {
            if (sqlLiteHelper == null)
            {
                sqlLiteHelper = SqlLiteHelper.GetInstance();
            }
            return sqlLiteHelper;
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
            }
            lbCount.Text = dataGirdView.RowCount + "";
        }

        private void BindDataGirdViewBySearch(DataGridView dataGirdView, string table)
        {
            string type = "";
            if (cbSearchType.SelectedIndex >= 0 && cbSearchContent.SelectedIndex >= 0)
            {
                if (Common.STOCKLOGTABLENAME.Equals(_tableName))
                {
                    type = _stockSearchType[cbSearchType.SelectedIndex];
                }
                else
                {
                    type = _stirSearchType[cbSearchType.SelectedIndex];
                }
            }
            string startDate = dateTimePickerStart.CustomFormat.Equals(" ") ? " " : dateTimePickerStart.Value.ToString("yyyyMMdd");
            string endDate = dateTimePickerEnd.CustomFormat.Equals(" ") ? " " : dateTimePickerEnd.Value.ToString("yyyyMMdd");
            SQLiteDataReader dataReader = null;
            if (!type.Equals("") && !startDate.Equals(" ") && !endDate.Equals(" "))
            {
                dataReader = GetSqlLiteHelper().ReadTable(table, new string[] { type, "操作日期", "操作日期" }, new string[] { "=", ">=", "<=" }, new string[] { cbSearchContent.SelectedItem.ToString(), startDate, endDate });
            }
            else if (type.Equals("") && !startDate.Equals(" ") && !endDate.Equals(" "))
            {
                dataReader = GetSqlLiteHelper().ReadTable(table, new string[] { "操作日期", "操作日期" }, new string[] { ">=", "<=" }, new string[] { startDate, endDate });
            }
            else if (!type.Equals("") && startDate.Equals(" ") && endDate.Equals(" "))
            {
                dataReader = GetSqlLiteHelper().ReadTable(table, new string[] { type }, new string[] { "=" }, new string[] { cbSearchContent.SelectedItem.ToString() });
            }
            else
            {
                return;
            }
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

        private void BtnExport_Click(object sender, EventArgs e)
        {
            string type = "";
            if (cbSearchType.SelectedIndex >= 0 && cbSearchContent.SelectedIndex >= 0)
            {
                if (Common.STOCKLOGTABLENAME.Equals(_tableName))
                {
                    type = _stockSearchType[cbSearchType.SelectedIndex];
                }
                else
                {
                    type = _stirSearchType[cbSearchType.SelectedIndex];
                }
            }
            string startDate = dateTimePickerStart.CustomFormat.Equals(" ") ? " " : dateTimePickerStart.Value.ToString("yyyyMMdd");
            string endDate = dateTimePickerEnd.CustomFormat.Equals(" ") ? " " : dateTimePickerEnd.Value.ToString("yyyyMMdd");
            SQLiteDataReader dataReader = null;
            if (!type.Equals("") && !startDate.Equals(" ") && !endDate.Equals(" "))
            {
                dataReader = GetSqlLiteHelper().ReadTable(_tableName, new string[] { type, "操作日期", "操作日期" }, new string[] { "=", ">=", "<=" }, new string[] { cbSearchContent.SelectedItem.ToString(), startDate, endDate });
            }
            else if (type.Equals("") && !startDate.Equals(" ") && !endDate.Equals(" "))
            {
                dataReader = GetSqlLiteHelper().ReadTable(_tableName, new string[] { "操作日期", "操作日期" }, new string[] { ">=", "<=" }, new string[] { startDate, endDate });
            }
            else if (!type.Equals("") && startDate.Equals(" ") && endDate.Equals(" "))
            {
                dataReader = GetSqlLiteHelper().ReadTable(_tableName, new string[] { type }, new string[] { "=" }, new string[] { cbSearchContent.SelectedItem.ToString() });
            }
            else
            {
                dataReader = GetSqlLiteHelper().ReadFullTable(_tableName);
            }

            DataTable dt = new DataTable();
            dt.Load(dataReader);
            ExcelHelper.ExportExcel(dt);
        }

        private void CbSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSearchType.SelectedIndex >= 0)
            {
                cbSearchContent.Items.Clear();
                string searchType = "";
                if (Common.STOCKLOGTABLENAME.Equals(_tableName))
                {
                    searchType = _stockSearchType[cbSearchType.SelectedIndex];
                }
                else
                {
                    searchType = _stirSearchType[cbSearchType.SelectedIndex];
                }

                List<string> searchContent = GetSqlLiteHelper().GetValueTypeByColumnFromTable(_tableName, searchType, null, null, null);
                for (int i = 0; i < searchContent.Count; i++)
                {
                    cbSearchContent.Items.Add(searchContent[i]);
                }
            }
        }

        private void CbSearchContent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSearchType.SelectedIndex >= 0 && cbSearchContent.SelectedIndex >= 0)
            {
                BindDataGirdViewBySearch(dgvLogData, _tableName);
            }
        }

        //显示所有数据，清除过滤内容
        private void BtShowAll_Click(object sender, EventArgs e)
        {
            cbSearchType.SelectedIndex = -1;
            cbSearchType.Text = "选择过滤方式";
            cbSearchContent.SelectedIndex = -1;
            cbSearchContent.Text = "选择过滤内容";
            cbSearchContent.Items.Clear();
            dateTimePickerStart.CustomFormat = " ";
            dateTimePickerEnd.CustomFormat = " ";
            BindDataGirdView(dgvLogData, _tableName);
        }

        private void DateTimePickerStart_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerStart.CustomFormat = "yyyyMMdd";
        }

        private void DateTimePickerEnd_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePickerStart.Value.Equals(" "))
            {
                MessageBox.Show("请先设置开始时间");
                return;
            }
            dateTimePickerEnd.CustomFormat = "yyyyMMdd";
            BindDataGirdViewBySearch(dgvLogData, _tableName);
        }
    }
}
