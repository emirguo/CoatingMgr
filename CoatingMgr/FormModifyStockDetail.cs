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
    public partial class FormModifyStockDetail : Form
    {
        private FormStockDetail _fatherForm;

        public FormModifyStockDetail()
        {
            InitializeComponent();
        }

        public FormModifyStockDetail(FormStockDetail fatherForm, string name, string type, string color, string model, string weight, string store, string tips)
        {
            InitializeComponent();
            _fatherForm = fatherForm;
            lbName.Text = name;
            lbType.Text = type;
            lbColor.Text = color;
            lbModel.Text = model;
            tbWeight.Text = weight;
            cbStore.Text = store;
            tbTips.Text = tips;
        }

        private void FormModifyStockDetail_Load(object sender, EventArgs e)
        {
            InitView();
        }

        private void InitView()
        {
            List<string> searchContent = SqlLiteHelper.GetInstance().GetValueTypeByColumnFromTable(Common.STORETABLENAME, "名称", null, null, null);
            for (int i = 0; i < searchContent.Count; i++)
            {
                cbStore.Items.Add(searchContent[i]);
                if (searchContent[i].Equals(cbStore.Text))
                {
                    cbStore.SelectedIndex = i;
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (_fatherForm != null)
            {
                _fatherForm.ModifyCurrentRow(lbName.Text, lbType.Text, lbColor.Text, lbModel.Text, tbWeight.Text, cbStore.Text, tbTips.Text);
            }
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
