using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Threading;
using System.Windows.Forms;

namespace CoatingMgr
{
    public partial class FormMaster : Form
    {
        private static string _tableName = Common.MASTERTABLENAME;
        private string _userName = "";
        private string _userPermission = "";

        private static string[] _cbSearchType = { "按SAP品番查找", "按种类查找", "按制造商查找", "按适用机种查找", "按适用制品查找", "按色番查找" };
        private static string[] _searchType = { "SAP品番", "种类", "制造商", "适用机种", "适用制品", "色番" };
        private int rowIndex = 0;

        public FormMaster()
        {
            InitializeComponent();
        }

        public FormMaster(string userName, string userPermission)
        {
            InitializeComponent();
            _userName = userName;
            _userPermission = userPermission;
        }

        private void FormMaster_Load(object sender, EventArgs e)
        {
            InitData();
        }

        private void InitData()
        {
            lbUser.Text = _userName;
            ShowTime();

            for (int i = 0; i < _cbSearchType.Length; i++)
            {
                cbSearchType.Items.Add(_cbSearchType[i]);
            }

            BindDataGirdView(dgvMasterData, _tableName);//绑定数据库表
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
            else
            {
                MessageBox.Show("请先导入Master文件");
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
            if (dgvMasterData.RowCount > 0 && dgvMasterData.CurrentRow != null)
            {
                index = dgvMasterData.CurrentRow.Index;
            }
            BindDataGirdView(dgvMasterData, _tableName);
            if (dgvMasterData.RowCount <= 0)
            {
                return;
            }
            else if ((dgvMasterData.RowCount - 1) > index)
            {
                this.dgvMasterData.CurrentCell = this.dgvMasterData[1, index];
            }
            else
            {
                this.dgvMasterData.CurrentCell = this.dgvMasterData[1, (dgvMasterData.RowCount - 1)];
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

        private void DgvMasterData_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.contextMenuStrip.Show(this.dgvMasterData, e.Location);
                this.contextMenuStrip.Show(Cursor.Position);
            }
        }

        private void TSMIDelete_Click(object sender, EventArgs e)
        {
            if (!this.dgvMasterData.Rows[rowIndex].IsNewRow)
            {
                string id = dgvMasterData.Rows[rowIndex].Cells[0].Value.ToString();
                this.dgvMasterData.Rows.RemoveAt(rowIndex);
                SQLServerHelper.Delete(_tableName, new string[] { "id" }, new string[] { id }, new string[] { "=" }, null);
                UpdateData();
            }
        }

        private SynchronizationContext m_SyncContext = null;
        private void BtImportMaster_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Filter = "Excel文件|*.xls;*.xlsx"
            };
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                m_SyncContext = SynchronizationContext.Current;
                Common.ShowProgress();
                string fileName = fileDialog.FileName;//得到文件所在位置。
                Thread t = new Thread(new ParameterizedThreadStart(ImportExceAndSaveToDB));//起线程导入excel并存表
                t.Start(fileName);//线程传递文件名
            }
        }

        private void ImportExceAndSaveToDB(object fileName)
        {
            ExcelHelper.ImportExcel(fileName.ToString());
            m_SyncContext.Post(UpdateUIAfterThread, "");//线程结束后更新UI
        }

        private void UpdateUIAfterThread(object obj)
        {
            Common.CloseProgress();
            UpdateData();
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
                BindDataGirdViewBySearch(dgvMasterData, _tableName, _searchType[cbSearchType.SelectedIndex], cbSearchContent.SelectedItem.ToString());
            }
        }

        private void BtShowAll_Click(object sender, EventArgs e)
        {
            cbSearchType.SelectedIndex = -1;
            cbSearchType.Text = "请选择过滤方式";
            cbSearchContent.SelectedIndex = -1;
            cbSearchContent.Text = "请选择过滤内容";
            cbSearchContent.Items.Clear();
            BindDataGirdView(dgvMasterData, _tableName);
        }
    }
}
