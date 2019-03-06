using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoatingMgr
{
    public partial class FormSetWarn : Form
    {
        private static SqlLiteHelper sqlLiteHelper = null;
        private string _userName = "";
        private FormWarn _fatherForm = null;
        private static string _tableName = Common.WARNMANAGERTABLENAME;

        private List<string> _cbSearchStock;
        private List<string> _cbSearchProduct;
        private List<string> _cbSearchColor;
        private List<string> _cbSearchType;

        private static string[] _cbWarnDate = { "有效期前1天", "有效期前1周", "有效期前15天", "有效期前1月", "有效期前3月" };

        private enum WARNTYPE
        {
            库存上限告警,
            库存下限告警,
            有效期告警,
            库存上限告警和库存下限告警,
            库存上限告警和有效期告警,
            库存下限告警和有效期告警,
            库存上限告警和库存下限告警和有效期告警
        }

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

        private void FormSetWarn_Load(object sender, EventArgs e)
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
            _cbSearchStock = GetSqlLiteHelper().GetValueTypeByColumnFromTable(Common.STOCKCOUNTTABLENAME, "仓库");
            for (int i = 0; i < _cbSearchStock.Count; i++)
            {
                cbStock.Items.Add(_cbSearchStock[i]);
            }

            _cbSearchProduct = GetSqlLiteHelper().GetValueTypeByColumnFromTable(Common.STOCKCOUNTTABLENAME, "名称");
            for (int i = 0; i < _cbSearchProduct.Count; i++)
            {
                cbProduct.Items.Add(_cbSearchProduct[i]);
            }

            _cbSearchColor = GetSqlLiteHelper().GetValueTypeByColumnFromTable(Common.STOCKCOUNTTABLENAME, "颜色");
            for (int i = 0; i < _cbSearchColor.Count; i++)
            {
                cbColor.Items.Add(_cbSearchColor[i]);
            }

            _cbSearchType = GetSqlLiteHelper().GetValueTypeByColumnFromTable(Common.STOCKCOUNTTABLENAME, "类型");
            for (int i = 0; i < _cbSearchType.Count; i++)
            {
                cbType.Items.Add(_cbSearchType[i]);
            }

            for (int i = 0; i < _cbWarnDate.Length; i++)
            {
                cbWarnTime.Items.Add(_cbWarnDate[i]);
            }

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (SaveWarn())
            {
                Close();
            }
            
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private Boolean SaveWarn()
        {
            bool result = false;
            WARNTYPE warnType = WARNTYPE.库存上限告警和库存下限告警和有效期告警;
            if (tbMaximum.Text.Length == 0 && tbMinimum.Text.Length == 0 && cbWarnTime.Text.Length == 0)
            {
                MessageBox.Show("请设置告警类型");
                return result;
            }
            else if (tbMaximum.Text != null && tbMaximum.Text.Length > 0 && int.Parse(tbMaximum.Text) > 0 
                && tbMinimum.Text!= null && tbMinimum.Text.Length > 0 && int.Parse(tbMinimum.Text) > 0 
                && cbWarnTime.Text != null && cbWarnTime.Text.Length > 0 && cbWarnTime.Text.Length > 0)
            {
                warnType = WARNTYPE.库存上限告警和库存下限告警和有效期告警;
            }
            else if (tbMaximum.Text != null && tbMaximum.Text.Length > 0 && int.Parse(tbMaximum.Text) > 0 
                && tbMinimum.Text != null && tbMinimum.Text.Length > 0 && int.Parse(tbMinimum.Text) > 0 
                && cbWarnTime.Text.Length == 0)
            {
                warnType = WARNTYPE.库存上限告警和库存下限告警;
            }
            else if (tbMaximum.Text != null && tbMaximum.Text.Length > 0 && int.Parse(tbMaximum.Text) > 0 
                && tbMinimum.Text.Length == 0
                && cbWarnTime.Text != null && cbWarnTime.Text.Length > 0 && cbWarnTime.Text.Length > 0)
            {
                warnType = WARNTYPE.库存上限告警和有效期告警;
            }
            else if (tbMaximum.Text.Length == 0 
                && tbMinimum.Text != null && tbMinimum.Text.Length > 0 && int.Parse(tbMinimum.Text) > 0
                && cbWarnTime.Text != null && cbWarnTime.Text.Length > 0 && cbWarnTime.Text.Length > 0)
            {
                warnType = WARNTYPE.库存下限告警和有效期告警;
            }
            else if (tbMaximum.Text != null && tbMaximum.Text.Length > 0 && int.Parse(tbMaximum.Text) > 0
                && tbMinimum.Text.Length == 0 
                && cbWarnTime.Text.Length == 0)
            {
                warnType = WARNTYPE.库存上限告警;
            }
            else if (tbMaximum.Text.Length == 0 
                && tbMinimum.Text != null && tbMinimum.Text.Length > 0 && int.Parse(tbMinimum.Text) > 0 
                && cbWarnTime.Text.Length == 0)
            {
                warnType = WARNTYPE.库存下限告警;
            }
            else if (tbMaximum.Text.Length == 0 
                && tbMinimum.Text.Length == 0 
                && cbWarnTime.Text != null && cbWarnTime.Text.Length > 0 && cbWarnTime.Text.Length > 0)
            {
                warnType = WARNTYPE.有效期告警;
            }
            GetSqlLiteHelper().InsertValues(_tableName, new string[] { cbStock.Text, cbProduct.Text, cbColor.Text, cbType.Text, tbMaximum.Text, tbMinimum.Text, cbWarnTime.Text, warnType.ToString(), _userName, DateTime.Now.ToString() });

            if (_fatherForm != null)
            {
                _fatherForm.UpdateData();
            }
            result = true;
            return result;
        }
    }
}
