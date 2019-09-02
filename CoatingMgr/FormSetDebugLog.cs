using System.Windows.Forms;

namespace CoatingMgr
{
    public partial class FormSetDebugLog : Form
    {
        public FormSetDebugLog()
        {
            InitializeComponent();
        }

        private void FormSetDebugLog_Load(object sender, System.EventArgs e)
        {
            if (Common.DebugLog)
            {
                this.rbOpen.Checked = true;
                this.rbClose.Checked = false;
            }
            else
            {
                this.rbOpen.Checked = false;
                this.rbClose.Checked = true;
            }
        }

        private void RbOpen_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.rbOpen.Checked)
            {
                Common.DebugLog = true;
            }
            else
            {
                Common.DebugLog = false;
            }
        }
    }
}
