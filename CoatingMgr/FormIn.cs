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
    public partial class FormIn : Form
    {
        private static SqlLiteHelper sqlLiteHelper = null;
        private static string _tableName = Common.INSTOCKTABLENAME;
        private string _userName = "";
        private static string[] _cbSearchStock = { "1号仓库", "2号仓库", "3号仓库", "4号仓库" };

        public FormIn()
        {
            InitializeComponent();
        }

        public FormIn(string userName)
        {
            InitializeComponent();
            _userName = userName;
            
        }

        private void FormIn_Load(object sender, EventArgs e)
        {
            InitData();
        }

        private string GetBarCodeDetails(string barCode)
        {
            string result = "";
            string title = "【条形码明细】\n";
            string barCodeType = "1.代码种类" + "DataMartrix码"+"\n";
            string constitute = "涂料名称：" + "双虎" + "\n" + "重量：" + "1.5kg" + "\n" + "厂商名：" + "双虎涂料有限公司" + "\n" + "生产日期：" + "2018.12.23";
            result = title + barCodeType + constitute;
            return result;
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

            SetDefaultColumns(dgvInStockData, Common.INSTOCKTABLECOLUMNS);

            this.lbProDescription.Text = GetBarCodeDetails("121212");
        }

        private void SetDefaultColumns(DataGridView dataGirdView, string[] columns)
        {
            for (int i = 0; i < columns.Length; i++)
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn
                {
                    HeaderText = columns[i]
                };
                dataGirdView.Columns.Add(column);
            }

            dataGirdView.Columns[0].Visible = false;
            dataGirdView.Columns[14].Visible = false;
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
                            lbTime.Text ="时间："+ DateTime.Now.ToString()));
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
                this.dgvInStockData.Rows.Add(0, tbBarCode.Text, tbName.Text, tbColor.Text, tbType.Text, tbWeight.Text, tbModel.Text, "2019.02.20", tbDate.Text, cbSearchStock.Text, _userName, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "入库", " ", " ");
                int count = this.dgvInStockData.RowCount;
                this.dgvInStockData.CurrentCell = this.dgvInStockData[1, (count>1)?(count - 1):0];
                lbCount.Text = count + "";
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
                tbName.Text = barcode;
                tbType.Text = "固化剂";
                tbColor.Text = "红色";
                tbWeight.Text = "10kg";
                tbModel.Text = "引擎盖";
                tbDate.Text = "2020/9/8";
                result = true;
            }
            return result;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (dgvInStockData.RowCount > 0)
            {
                foreach (DataGridViewRow dataRow in dgvInStockData.Rows)
                {
                    string name = dataRow.Cells[2].Value.ToString();
                    string color = dataRow.Cells[3].Value.ToString();
                    string type = dataRow.Cells[4].Value.ToString();
                    string weight = dataRow.Cells[5].Value.ToString();
                    string model = dataRow.Cells[6].Value.ToString();
                    string stock = dataRow.Cells[9].Value.ToString();
                    string warning = dataRow.Cells[14].Value.ToString();
                    string tip = dataRow.Cells[15].Value.ToString();
                    //存入库存
                    SQLiteDataReader dataReader = GetSqlLiteHelper().ReadTable(Common.STOCKCOUNTTABLENAME, new string[] { "类型", "名称", "颜色", "适用机型" }, new string[] { "=", "=", "=", "=" }, new string[] {type,name,color,model });
                    if (dataReader.HasRows && dataReader.Read())//色剂已经存在
                    {
                        float inStockWeight = Convert.ToSingle(Common.FilterChar(dataReader["重量"].ToString()));
                        float inputWeight = Convert.ToSingle(Common.FilterChar(weight));
                        inStockWeight += inputWeight;
                        GetSqlLiteHelper().UpdateValues(Common.STOCKCOUNTTABLENAME, new string[] { "重量" }, new string[] { inStockWeight.ToString()+"kg" }, "id", dataReader["id"].ToString() );
                    }
                    else
                    {
                        GetSqlLiteHelper().InsertValues(Common.STOCKCOUNTTABLENAME, new string[] { type,name,color,model,stock,weight,warning,tip });
                    }

                    //存入日志     
                    List<string> values = new List<string>();
                    for (int i = 1; i < dataRow.Cells.Count; i++)
                    {
                        values.Add(dataRow.Cells[i].Value.ToString());
                    }
                    GetSqlLiteHelper().InsertValues(Common.STOCKLOGTABLENAME, values);

                }
                dgvInStockData.Rows.Clear();
            }
            
        }

        private void DgvInStockData_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.contextMenuStrip.Show(this.dgvInStockData, e.Location);
                this.contextMenuStrip.Show(Cursor.Position);
            }
        }

        private void TSMIDelete_Click(object sender, EventArgs e)
        {
            if (dgvInStockData.SelectedCells.Count > 0)
            {
                foreach (DataGridViewRow row in dgvInStockData.Rows)
                {
                    if (row.Selected && !row.IsNewRow)
                    {
                        dgvInStockData.Rows.Remove(row);
                    }
                }
            }
        }

        private void TSMIModify_Click(object sender, EventArgs e)
        {

        }
    }
}
