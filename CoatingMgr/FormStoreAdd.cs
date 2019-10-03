using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace CoatingMgr
{
    public partial class FormStoreAdd : Form
    {
        private bool _modifyModel = false;
        private int _id = 0;
        private string _name = "";
        private FormStore _fatherForm = null;
        private static string _tableName = Common.STORETABLENAME;

        public FormStoreAdd()
        {
            InitializeComponent();
        }

        public FormStoreAdd(FormStore fatherForm)
        {
            InitializeComponent();
            _fatherForm = fatherForm;
        }

        public FormStoreAdd(FormStore fatherForm, int id, string name)
        {
            InitializeComponent();
            _fatherForm = fatherForm;
            _id = id;
            _name = name;
            _modifyModel = true;
            this.Text = "修改仓库";
            tbName.Text = name;
            lbTitle.Text = "修改仓库";
            btnAdd.Text = "修改";
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (tbName.Text.Length == 0)
            {
                MessageBox.Show("请输入仓库名称");
                return;
            }
            if (_modifyModel)
            {
                SQLServerHelper.Update(_tableName, new string[] { "名称" }, new string[] { tbName.Text.ToString() }, "id", _id + "");
            }
            else
            {
                DataTable dt = SQLServerHelper.Read(_tableName, new string[] { "名称" }, new string[] { "=" }, new string[] { tbName.Text.ToString() });
                if (dt != null && dt.Rows.Count > 0)//判断账号是否已经存在
                {
                    MessageBox.Show("仓库已经存在");
                    return;
                }
                else
                {
                    SQLServerHelper.Insert(_tableName, new string[] { "名称" }, new string[] { tbName.Text.ToString() });
                }
            }
            if (_fatherForm != null)
            {
                _fatherForm.UpdateData();
            }
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
