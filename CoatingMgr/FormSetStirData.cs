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
    public partial class FormSetStirData : Form
    {
        private FormStir _fatherForm = null;

        public FormSetStirData()
        {
            InitializeComponent();
        }

        public FormSetStirData(FormStir fatherForm, string temperature, string humidity, string ratio)
        {
            InitializeComponent();
            _fatherForm = fatherForm;
            this.tbTemperature.Text = temperature;
            this.tbHumidity.Text = humidity;
            this.tbRatio.Text = ratio;
        }

        private void BtnModify_Click(object sender, EventArgs e)
        {
            _fatherForm.ResetRatioAndWeight(this.tbTemperature.Text, this.tbHumidity.Text, this.tbRatio.Text);
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        
    }
}
