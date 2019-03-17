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
            for (int i = 0; i < Common.STOCKSNAME.Length; i++)
            {
                cbStock.Items.Add(Common.STOCKSNAME[i]);
                if (Common.STOCKSNAME[i].Equals(cbStock.Text))
                {
                    cbStock.SelectedIndex = i;
                }
            }

            for (int i = 0; i < Common.WARNDATES.Length; i++)
            {
                cbWarnTime.Items.Add(Common.WARNDATES[i]);
                if (Common.WARNDATES[i].Equals(cbWarnTime.Text))
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
