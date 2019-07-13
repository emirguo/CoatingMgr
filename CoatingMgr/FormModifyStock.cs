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
    public partial class FormModifyStock : Form
    {
        private FormStock _fatherForm;
        public FormModifyStock()
        {
            InitializeComponent();
        }

        public FormModifyStock(FormStock fatherForm, string name, string type, string color, string model, string weight, string stock, string maximum, string minimum, string warnTime, string tips)
        {
            InitializeComponent();
            _fatherForm = fatherForm;
            lbName.Text = name;
            lbType.Text = type;
            lbColor.Text = color;
            lbModel.Text = model;
            tbWeight.Text = weight;
            cbStock.Text = stock;
            tbMaximum.Text = maximum;
            tbMinimum.Text = minimum;
            cbWarnTime.Text = warnTime;
            tbTips.Text = tips;
        }

        private void FormModifyStock_Load(object sender, EventArgs e)
        {
            InitView();
        }

        private void InitView()
        {
            List<string> searchContent = SqlLiteHelper.GetInstance().GetValueTypeByColumnFromTable(Common.STORETABLENAME, "名称", null, null, null);
            for (int i = 0; i < searchContent.Count; i++)
            {
                cbStock.Items.Add(searchContent[i]);
                if (searchContent[i].Equals(cbStock.Text))
                {
                    cbStock.SelectedIndex = i;
                }
            }

            for (int i = 0; i < Common.WARNDATETYPE.Length; i++)
            {
                cbWarnTime.Items.Add(Common.WARNDATETYPE[i]);
                if (Common.WARNDATETYPE[i].Equals(cbWarnTime.Text))
                {
                    cbWarnTime.SelectedIndex = i;
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (_fatherForm != null)
            {
                _fatherForm.ModifyCurrentRow(lbName.Text, lbType.Text, lbColor.Text, lbModel.Text, tbWeight.Text, cbStock.Text, tbMaximum.Text, tbMinimum.Text, cbWarnTime.Text, tbTips.Text);
            }
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
