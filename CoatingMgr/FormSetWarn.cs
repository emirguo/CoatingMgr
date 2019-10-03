using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace CoatingMgr
{
    public partial class FormSetWarn : Form
    {
        private static string _tableName = Common.WARNMANAGERTABLENAME;
        private string _userName = "";
        private FormWarn _fatherForm = null;
        private bool _modifyModel = false;
        private string _modifyID, _modifyProduct, _modifyColor, _modifyType, _modifyMaxmum, _modifyMinimum, _modifyWarnTime;

        private List<string> _cbSearchProduct;
        private List<string> _cbSearchColor;
        private List<string> _cbSearchType;

        public FormSetWarn()
        {
            InitializeComponent();
        }

        public FormSetWarn(FormWarn fatherForm, string userName)
        {
            InitializeComponent();
            _userName = userName;
            _fatherForm = fatherForm;
        }

        public FormSetWarn(FormWarn fatherForm, string userName, bool modifyType, string id, string product, string color, string type, string warnMaxmum, string warnMinimum, string warnTime)
        {
            InitializeComponent();
            _userName = userName;
            _fatherForm = fatherForm;
            _modifyModel = modifyType;
            _modifyID = id;
            _modifyProduct = product;
            _modifyColor = color;
            _modifyType = type;
            _modifyMaxmum = warnMaxmum;
            _modifyMinimum = warnMinimum;
            _modifyWarnTime = warnTime;
        }

        private void FormSetWarn_Load(object sender, EventArgs e)
        {
            InitData();
        }

        private void InitData()
        {
            if (_modifyModel)
            {
                cbProduct.Text = _modifyProduct;
                cbProduct.Enabled = false;
                cbColor.Text = _modifyColor;
                cbColor.Enabled = false;
                cbType.Text = _modifyType;
                cbType.Enabled = false;
                tbMaximum.Text = _modifyMaxmum;
                tbMinimum.Text = _modifyMinimum;
                lbTitle.Text = "修改告警";
                btnSave.Text = "修改";
            }
            else
            {
                _cbSearchType = SQLServerHelper.GetTypesOfColumn(Common.STOCKCOUNTTABLENAME, "类型", null, null, null);
                for (int i = 0; i < _cbSearchType.Count; i++)
                {
                    cbType.Items.Add(_cbSearchType[i]);
                }
            }

            for (int i = 0; i < Common.WARNDATETYPE.Length; i++)
            {
                cbWarnTime.Items.Add(Common.WARNDATETYPE[i]);
                if (_modifyModel && Common.WARNDATETYPE[i].Equals(_modifyWarnTime))
                {
                    cbWarnTime.SelectedIndex = i;
                }
            }
        }


        private void CbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbType.SelectedIndex >= 0)
            {
                cbProduct.Items.Clear();
                cbColor.Items.Clear();
                cbProduct.Text = "请选择涂料";
                cbColor.Text = "请选择颜色";
                _cbSearchProduct = SQLServerHelper.GetTypesOfColumn(Common.STOCKCOUNTTABLENAME, "名称", new string[] { "类型" }, new string[] { "=" }, new string[] { cbType.Text.ToString() });
                for (int i = 0; i < _cbSearchProduct.Count; i++)
                {
                    cbProduct.Items.Add(_cbSearchProduct[i]);
                }
            }
        }

        private void CbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbProduct.SelectedIndex >= 0)
            {
                cbColor.Items.Clear();
                cbColor.Text = "请选择颜色";
                _cbSearchColor = SQLServerHelper.GetTypesOfColumn(Common.STOCKCOUNTTABLENAME, "颜色", new string[] { "类型", "名称" }, new string[] { "=", "=" }, new string[] { cbType.Text.ToString(), cbProduct.Text.ToString() });
                for (int i = 0; i < _cbSearchColor.Count; i++)
                {
                    cbColor.Items.Add(_cbSearchColor[i]);
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (_modifyModel)
            {
                ModifyWarn();
            }
            else
            {
                SaveWarn();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        //"id", "名称", "颜色", "类型", "库存上限", "库存下限", "告警时间", "规则创建人", "规则创建时间"
        private void SaveWarn()
        {
            if (tbMaximum.Text.Length == 0 && tbMinimum.Text.Length == 0 && cbWarnTime.Text.Length == 0)
            {
                MessageBox.Show("请设置告警类型");
                return ;
            }

            //保存告警规则到告警规则表
            DataTable dt = SQLServerHelper.Read(_tableName, new string[] { "名称", "颜色", "类型" }, new string[] { "=", "=", "=" }, new string[] { cbProduct.Text, cbColor.Text, cbType.Text });
            if (dt != null && dt.Rows.Count > 0)//告警规则已经存在
            {
                string id = dt.Rows[0]["id"].ToString();
                SQLServerHelper.Update(_tableName, Common.WARNMANAGERTABLECOLUMNS, new string[] { id, cbProduct.Text, cbColor.Text, cbType.Text, tbMaximum.Text, tbMinimum.Text, cbWarnTime.Text, _userName, DateTime.Now.ToString() }, "id", id);
            }
            else
            {
                SQLServerHelper.Insert(_tableName, new string[] { "名称", "颜色", "类型", "库存上限", "库存下限", "告警时间", "规则创建人", "规则创建时间" }, new string[] { cbProduct.Text, cbColor.Text, cbType.Text, tbMaximum.Text, tbMinimum.Text, cbWarnTime.Text, _userName, DateTime.Now.ToString() });
            }

            //更新库存告警数据
            Common.UpdateStockCountWarn(cbProduct.Text, cbColor.Text, cbType.Text, tbMaximum.Text, tbMinimum.Text, cbWarnTime.Text);

            if (_fatherForm != null)
            {
                _fatherForm.UpdateData();
            }

            Close();
        }

        private void ModifyWarn()
        {
            //更新告警规则到告警规则表
            SQLServerHelper.Update(_tableName, Common.WARNMANAGERTABLECOLUMNS, new string[] { _modifyID, cbProduct.Text, cbColor.Text, cbType.Text, tbMaximum.Text, tbMinimum.Text, cbWarnTime.Text, _userName, DateTime.Now.ToString() }, "id", _modifyID + "");

            //更新库存告警数据
            Common.UpdateStockCountWarn(cbProduct.Text, cbColor.Text, cbType.Text, tbMaximum.Text,tbMinimum.Text, cbWarnTime.Text);

            if (_fatherForm != null)
            {
                _fatherForm.UpdateData();
            }

            Close();
        }
    }
}
