namespace CoatingMgr
{
    partial class FormWarn
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lbCount = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvWarnMgr = new System.Windows.Forms.DataGridView();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btAdd = new System.Windows.Forms.Button();
            this.cbSearchType = new System.Windows.Forms.ComboBox();
            this.btShowAll = new System.Windows.Forms.Button();
            this.cbSearchContent = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TSMIModify = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWarnMgr)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.dgvWarnMgr);
            this.panel2.Location = new System.Drawing.Point(0, 84);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1180, 472);
            this.panel2.TabIndex = 35;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.Controls.Add(this.lbCount);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Location = new System.Drawing.Point(204, 441);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(972, 28);
            this.panel5.TabIndex = 30;
            // 
            // lbCount
            // 
            this.lbCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCount.AutoSize = true;
            this.lbCount.Font = new System.Drawing.Font("宋体", 12F);
            this.lbCount.Location = new System.Drawing.Point(914, 7);
            this.lbCount.Name = "lbCount";
            this.lbCount.Size = new System.Drawing.Size(16, 16);
            this.lbCount.TabIndex = 14;
            this.lbCount.Text = "0";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F);
            this.label5.Location = new System.Drawing.Point(864, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 7;
            this.label5.Text = "总数：";
            // 
            // dgvWarnMgr
            // 
            this.dgvWarnMgr.AllowUserToAddRows = false;
            this.dgvWarnMgr.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvWarnMgr.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWarnMgr.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvWarnMgr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvWarnMgr.Location = new System.Drawing.Point(2, 2);
            this.dgvWarnMgr.Name = "dgvWarnMgr";
            this.dgvWarnMgr.RowTemplate.Height = 23;
            this.dgvWarnMgr.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvWarnMgr.Size = new System.Drawing.Size(1176, 436);
            this.dgvWarnMgr.TabIndex = 29;
            this.dgvWarnMgr.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DgvWarnMgr_CellMouseUp);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.label14);
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1182, 80);
            this.panel4.TabIndex = 39;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(516, 4);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(110, 24);
            this.label14.TabIndex = 55;
            this.label14.Text = "告警规则";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btAdd);
            this.panel1.Controls.Add(this.cbSearchType);
            this.panel1.Controls.Add(this.btShowAll);
            this.panel1.Controls.Add(this.cbSearchContent);
            this.panel1.Location = new System.Drawing.Point(2, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1176, 64);
            this.panel1.TabIndex = 31;
            // 
            // btAdd
            // 
            this.btAdd.Font = new System.Drawing.Font("宋体", 12F);
            this.btAdd.Location = new System.Drawing.Point(1035, 22);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(122, 30);
            this.btAdd.TabIndex = 40;
            this.btAdd.Text = "添加告警规则";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.BtAdd_Click);
            // 
            // cbSearchType
            // 
            this.cbSearchType.Font = new System.Drawing.Font("宋体", 12F);
            this.cbSearchType.FormattingEnabled = true;
            this.cbSearchType.Location = new System.Drawing.Point(15, 26);
            this.cbSearchType.Name = "cbSearchType";
            this.cbSearchType.Size = new System.Drawing.Size(204, 24);
            this.cbSearchType.TabIndex = 0;
            this.cbSearchType.Text = "请选择过滤方式";
            this.cbSearchType.SelectedIndexChanged += new System.EventHandler(this.CbSearchType_SelectedIndexChanged);
            // 
            // btShowAll
            // 
            this.btShowAll.Font = new System.Drawing.Font("宋体", 12F);
            this.btShowAll.Location = new System.Drawing.Point(573, 22);
            this.btShowAll.Name = "btShowAll";
            this.btShowAll.Size = new System.Drawing.Size(122, 30);
            this.btShowAll.TabIndex = 11;
            this.btShowAll.Text = "显示全部规则";
            this.btShowAll.UseVisualStyleBackColor = true;
            this.btShowAll.Click += new System.EventHandler(this.BtShowAll_Click);
            // 
            // cbSearchContent
            // 
            this.cbSearchContent.Font = new System.Drawing.Font("宋体", 12F);
            this.cbSearchContent.FormattingEnabled = true;
            this.cbSearchContent.Location = new System.Drawing.Point(227, 26);
            this.cbSearchContent.Name = "cbSearchContent";
            this.cbSearchContent.Size = new System.Drawing.Size(330, 24);
            this.cbSearchContent.TabIndex = 1;
            this.cbSearchContent.Text = "请选择过滤内容";
            this.cbSearchContent.SelectedIndexChanged += new System.EventHandler(this.CbSearchContent_SelectedIndexChanged);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMIModify,
            this.TSMIDelete});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(101, 48);
            // 
            // TSMIModify
            // 
            this.TSMIModify.Name = "TSMIModify";
            this.TSMIModify.Size = new System.Drawing.Size(100, 22);
            this.TSMIModify.Text = "修改";
            this.TSMIModify.Click += new System.EventHandler(this.TSMIModify_Click);
            // 
            // TSMIDelete
            // 
            this.TSMIDelete.Name = "TSMIDelete";
            this.TSMIDelete.Size = new System.Drawing.Size(100, 22);
            this.TSMIDelete.Text = "删除";
            this.TSMIDelete.Click += new System.EventHandler(this.TSMIDelete_Click);
            // 
            // FormWarn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 562);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Name = "FormWarn";
            this.Text = "FormWarn";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormWarn_Load);
            this.SizeChanged += new System.EventHandler(this.FormWarn_SizeChanged);
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWarnMgr)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvWarnMgr;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ComboBox cbSearchContent;
        private System.Windows.Forms.ComboBox cbSearchType;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lbCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem TSMIDelete;
        private System.Windows.Forms.ToolStripMenuItem TSMIModify;
        private System.Windows.Forms.Button btShowAll;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel panel1;
    }
}