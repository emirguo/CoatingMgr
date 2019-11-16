namespace CoatingMgr
{
    partial class FormStockDetail
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btShowAll = new System.Windows.Forms.Button();
            this.cbSearchContent = new System.Windows.Forms.ComboBox();
            this.btExport = new System.Windows.Forms.Button();
            this.cbSearchType = new System.Windows.Forms.ComboBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.TSMIDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIModify = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.lbCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbUser = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lbTime = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1184, 84);
            this.panel3.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(516, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 24);
            this.label1.TabIndex = 12;
            this.label1.Text = "库存明细";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btShowAll);
            this.panel1.Controls.Add(this.cbSearchContent);
            this.panel1.Controls.Add(this.btExport);
            this.panel1.Controls.Add(this.cbSearchType);
            this.panel1.Location = new System.Drawing.Point(2, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1176, 62);
            this.panel1.TabIndex = 2;
            // 
            // btShowAll
            // 
            this.btShowAll.Font = new System.Drawing.Font("宋体", 12F);
            this.btShowAll.Location = new System.Drawing.Point(405, 23);
            this.btShowAll.Name = "btShowAll";
            this.btShowAll.Size = new System.Drawing.Size(122, 27);
            this.btShowAll.TabIndex = 11;
            this.btShowAll.Text = "显示全部库存";
            this.btShowAll.UseVisualStyleBackColor = true;
            this.btShowAll.Click += new System.EventHandler(this.BtShowAll_Click);
            // 
            // cbSearchContent
            // 
            this.cbSearchContent.Font = new System.Drawing.Font("宋体", 12F);
            this.cbSearchContent.FormattingEnabled = true;
            this.cbSearchContent.Location = new System.Drawing.Point(205, 24);
            this.cbSearchContent.Name = "cbSearchContent";
            this.cbSearchContent.Size = new System.Drawing.Size(184, 24);
            this.cbSearchContent.TabIndex = 6;
            this.cbSearchContent.Text = "请选择过滤内容";
            this.cbSearchContent.SelectedIndexChanged += new System.EventHandler(this.CbSearchContent_SelectedIndexChanged);
            // 
            // btExport
            // 
            this.btExport.Font = new System.Drawing.Font("宋体", 12F);
            this.btExport.Location = new System.Drawing.Point(1010, 23);
            this.btExport.Name = "btExport";
            this.btExport.Size = new System.Drawing.Size(136, 27);
            this.btExport.TabIndex = 5;
            this.btExport.Text = "导出Excel表格";
            this.btExport.UseVisualStyleBackColor = true;
            this.btExport.Click += new System.EventHandler(this.BtExport_Click);
            // 
            // cbSearchType
            // 
            this.cbSearchType.Font = new System.Drawing.Font("宋体", 12F);
            this.cbSearchType.FormattingEnabled = true;
            this.cbSearchType.Location = new System.Drawing.Point(20, 24);
            this.cbSearchType.Name = "cbSearchType";
            this.cbSearchType.Size = new System.Drawing.Size(168, 24);
            this.cbSearchType.TabIndex = 0;
            this.cbSearchType.Text = "请选择过滤方式";
            this.cbSearchType.SelectedIndexChanged += new System.EventHandler(this.CbSearchType_SelectedIndexChanged);
            // 
            // TSMIDelete
            // 
            this.TSMIDelete.Name = "TSMIDelete";
            this.TSMIDelete.Size = new System.Drawing.Size(100, 22);
            this.TSMIDelete.Text = "删除";
            this.TSMIDelete.Click += new System.EventHandler(this.TSMIDelete_Click);
            // 
            // TSMIModify
            // 
            this.TSMIModify.Name = "TSMIModify";
            this.TSMIModify.Size = new System.Drawing.Size(100, 22);
            this.TSMIModify.Text = "修改";
            this.TSMIModify.Click += new System.EventHandler(this.TSMIModify_Click);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMIModify,
            this.TSMIDelete});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(101, 48);
            // 
            // lbCount
            // 
            this.lbCount.AutoSize = true;
            this.lbCount.Font = new System.Drawing.Font("宋体", 12F);
            this.lbCount.Location = new System.Drawing.Point(912, 6);
            this.lbCount.Margin = new System.Windows.Forms.Padding(3);
            this.lbCount.Name = "lbCount";
            this.lbCount.Size = new System.Drawing.Size(16, 16);
            this.lbCount.TabIndex = 9;
            this.lbCount.Text = "0";
            this.lbCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F);
            this.label4.Location = new System.Drawing.Point(852, 6);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "总数量：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.lbCount);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Font = new System.Drawing.Font("宋体", 12F);
            this.panel4.Location = new System.Drawing.Point(200, 450);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(983, 28);
            this.panel4.TabIndex = 15;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.lbUser);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.lbTime);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.dgvData);
            this.panel2.Location = new System.Drawing.Point(0, 84);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1182, 478);
            this.panel2.TabIndex = 5;
            // 
            // lbUser
            // 
            this.lbUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbUser.AutoSize = true;
            this.lbUser.Font = new System.Drawing.Font("宋体", 12F);
            this.lbUser.Location = new System.Drawing.Point(70, 460);
            this.lbUser.Name = "lbUser";
            this.lbUser.Size = new System.Drawing.Size(40, 16);
            this.lbUser.TabIndex = 56;
            this.lbUser.Text = "张三";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 12F);
            this.label13.Location = new System.Drawing.Point(10, 460);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 16);
            this.label13.TabIndex = 55;
            this.label13.Text = "操作员：";
            // 
            // lbTime
            // 
            this.lbTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbTime.AutoSize = true;
            this.lbTime.Font = new System.Drawing.Font("宋体", 12F);
            this.lbTime.Location = new System.Drawing.Point(128, 460);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(56, 16);
            this.lbTime.TabIndex = 54;
            this.lbTime.Text = "时间：";
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToOrderColumns = true;
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvData.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvData.Location = new System.Drawing.Point(2, 2);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowTemplate.Height = 23;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(1178, 448);
            this.dgvData.TabIndex = 0;
            this.dgvData.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DgvData_CellMouseUp);
            // 
            // FormStockDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 562);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Name = "FormStockDetail";
            this.Text = "库存明细";
            this.Load += new System.EventHandler(this.FormStockDetail_Load);
            this.SizeChanged += new System.EventHandler(this.FormStockDetail_SizeChanged);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btShowAll;
        private System.Windows.Forms.ComboBox cbSearchContent;
        private System.Windows.Forms.ComboBox cbSearchType;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolStripMenuItem TSMIDelete;
        private System.Windows.Forms.ToolStripMenuItem TSMIModify;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.Label lbCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Button btExport;
        private System.Windows.Forms.Label lbUser;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lbTime;
    }
}