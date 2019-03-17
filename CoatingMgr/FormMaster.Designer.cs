﻿namespace CoatingMgr
{
    partial class FormMaster
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btShowAll = new System.Windows.Forms.Button();
            this.btImportMaster = new System.Windows.Forms.Button();
            this.cbSearchContent = new System.Windows.Forms.ComboBox();
            this.cbSearchType = new System.Windows.Forms.ComboBox();
            this.dgvMasterData = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbUser = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lbCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbTime = new System.Windows.Forms.Label();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TSMIDeleteRow = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMasterData)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.btShowAll);
            this.panel1.Controls.Add(this.btImportMaster);
            this.panel1.Controls.Add(this.cbSearchContent);
            this.panel1.Controls.Add(this.cbSearchType);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(983, 36);
            this.panel1.TabIndex = 2;
            // 
            // btShowAll
            // 
            this.btShowAll.Font = new System.Drawing.Font("宋体", 12F);
            this.btShowAll.Location = new System.Drawing.Point(418, 8);
            this.btShowAll.Name = "btShowAll";
            this.btShowAll.Size = new System.Drawing.Size(122, 27);
            this.btShowAll.TabIndex = 11;
            this.btShowAll.Text = "显示全部";
            this.btShowAll.UseVisualStyleBackColor = true;
            this.btShowAll.Click += new System.EventHandler(this.BtShowAll_Click);
            // 
            // btImportMaster
            // 
            this.btImportMaster.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btImportMaster.Font = new System.Drawing.Font("宋体", 12F);
            this.btImportMaster.Location = new System.Drawing.Point(835, 8);
            this.btImportMaster.Name = "btImportMaster";
            this.btImportMaster.Size = new System.Drawing.Size(139, 27);
            this.btImportMaster.TabIndex = 13;
            this.btImportMaster.Text = "导入Master文件";
            this.btImportMaster.UseVisualStyleBackColor = true;
            this.btImportMaster.Click += new System.EventHandler(this.BtImportMaster_Click);
            // 
            // cbSearchContent
            // 
            this.cbSearchContent.Font = new System.Drawing.Font("宋体", 12F);
            this.cbSearchContent.FormattingEnabled = true;
            this.cbSearchContent.Location = new System.Drawing.Point(180, 9);
            this.cbSearchContent.Name = "cbSearchContent";
            this.cbSearchContent.Size = new System.Drawing.Size(226, 24);
            this.cbSearchContent.TabIndex = 1;
            this.cbSearchContent.Text = "选择过滤内容";
            this.cbSearchContent.SelectedIndexChanged += new System.EventHandler(this.CbSearchContent_SelectedIndexChanged);
            // 
            // cbSearchType
            // 
            this.cbSearchType.Font = new System.Drawing.Font("宋体", 12F);
            this.cbSearchType.FormattingEnabled = true;
            this.cbSearchType.Location = new System.Drawing.Point(3, 9);
            this.cbSearchType.Name = "cbSearchType";
            this.cbSearchType.Size = new System.Drawing.Size(171, 24);
            this.cbSearchType.TabIndex = 0;
            this.cbSearchType.Text = "选择过滤方式";
            this.cbSearchType.SelectedIndexChanged += new System.EventHandler(this.CbSearchType_SelectedIndexChanged);
            // 
            // dgvMasterData
            // 
            this.dgvMasterData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMasterData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMasterData.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvMasterData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMasterData.Location = new System.Drawing.Point(2, 2);
            this.dgvMasterData.Name = "dgvMasterData";
            this.dgvMasterData.RowTemplate.Height = 23;
            this.dgvMasterData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMasterData.Size = new System.Drawing.Size(978, 392);
            this.dgvMasterData.TabIndex = 0;
            this.dgvMasterData.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DgvMasterData_CellMouseUp);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.lbUser);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.lbTime);
            this.panel2.Controls.Add(this.dgvMasterData);
            this.panel2.Location = new System.Drawing.Point(1, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(982, 422);
            this.panel2.TabIndex = 3;
            // 
            // lbUser
            // 
            this.lbUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbUser.AutoSize = true;
            this.lbUser.Font = new System.Drawing.Font("宋体", 12F);
            this.lbUser.Location = new System.Drawing.Point(70, 400);
            this.lbUser.Name = "lbUser";
            this.lbUser.Size = new System.Drawing.Size(40, 16);
            this.lbUser.TabIndex = 53;
            this.lbUser.Text = "张三";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 12F);
            this.label13.Location = new System.Drawing.Point(10, 400);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 16);
            this.label13.TabIndex = 52;
            this.label13.Text = "操作员：";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.lbCount);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Font = new System.Drawing.Font("宋体", 12F);
            this.panel4.Location = new System.Drawing.Point(796, 394);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(187, 28);
            this.panel4.TabIndex = 16;
            // 
            // lbCount
            // 
            this.lbCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCount.AutoSize = true;
            this.lbCount.Font = new System.Drawing.Font("宋体", 12F);
            this.lbCount.Location = new System.Drawing.Point(116, 6);
            this.lbCount.Margin = new System.Windows.Forms.Padding(3);
            this.lbCount.Name = "lbCount";
            this.lbCount.Size = new System.Drawing.Size(16, 16);
            this.lbCount.TabIndex = 9;
            this.lbCount.Text = "0";
            this.lbCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F);
            this.label4.Location = new System.Drawing.Point(56, 6);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "总数量：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbTime
            // 
            this.lbTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbTime.AutoSize = true;
            this.lbTime.Font = new System.Drawing.Font("宋体", 12F);
            this.lbTime.Location = new System.Drawing.Point(128, 400);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(56, 16);
            this.lbTime.TabIndex = 51;
            this.lbTime.Text = "时间：";
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMIDeleteRow});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(113, 26);
            // 
            // TSMIDeleteRow
            // 
            this.TSMIDeleteRow.Name = "TSMIDeleteRow";
            this.TSMIDeleteRow.Size = new System.Drawing.Size(112, 22);
            this.TSMIDeleteRow.Text = "删除行";
            this.TSMIDeleteRow.Click += new System.EventHandler(this.TSMIDelete_Click);
            // 
            // FormMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 462);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "FormMaster";
            this.Text = "FormMaster";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormMaster_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMasterData)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btImportMaster;
        private System.Windows.Forms.ComboBox cbSearchContent;
        private System.Windows.Forms.ComboBox cbSearchType;
        private System.Windows.Forms.DataGridView dgvMasterData;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbUser;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lbCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbTime;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem TSMIDeleteRow;
        private System.Windows.Forms.Button btShowAll;
    }
}