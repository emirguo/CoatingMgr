namespace CoatingMgr
{
    partial class FormStore
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
            this.TSMIDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIModify = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btShowAll = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.btAdd = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbSearchContent = new System.Windows.Forms.ComboBox();
            this.lbCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.contextMenuStrip.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            // btShowAll
            // 
            this.btShowAll.Font = new System.Drawing.Font("宋体", 12F);
            this.btShowAll.Location = new System.Drawing.Point(233, 20);
            this.btShowAll.Name = "btShowAll";
            this.btShowAll.Size = new System.Drawing.Size(122, 30);
            this.btShowAll.TabIndex = 11;
            this.btShowAll.Text = "显示全部仓库";
            this.btShowAll.UseVisualStyleBackColor = true;
            this.btShowAll.Click += new System.EventHandler(this.BtShowAll_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(516, 4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(110, 24);
            this.label10.TabIndex = 78;
            this.label10.Text = "仓库管理";
            // 
            // btAdd
            // 
            this.btAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btAdd.Font = new System.Drawing.Font("宋体", 12F);
            this.btAdd.Location = new System.Drawing.Point(1038, 20);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(116, 30);
            this.btAdd.TabIndex = 5;
            this.btAdd.Text = "添加仓库";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.BtAdd_Click);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.btAdd);
            this.panel3.Controls.Add(this.btShowAll);
            this.panel3.Controls.Add(this.cbSearchContent);
            this.panel3.Location = new System.Drawing.Point(2, 16);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1176, 62);
            this.panel3.TabIndex = 16;
            // 
            // cbSearchContent
            // 
            this.cbSearchContent.Font = new System.Drawing.Font("宋体", 12F);
            this.cbSearchContent.FormattingEnabled = true;
            this.cbSearchContent.Location = new System.Drawing.Point(20, 24);
            this.cbSearchContent.Name = "cbSearchContent";
            this.cbSearchContent.Size = new System.Drawing.Size(191, 24);
            this.cbSearchContent.TabIndex = 0;
            this.cbSearchContent.Text = "请选择仓库";
            this.cbSearchContent.SelectedIndexChanged += new System.EventHandler(this.CbSearchContent_SelectedIndexChanged);
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
            this.panel4.Location = new System.Drawing.Point(202, 450);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(983, 28);
            this.panel4.TabIndex = 15;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.dgvData);
            this.panel2.Location = new System.Drawing.Point(0, 84);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1184, 478);
            this.panel2.TabIndex = 5;
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
            this.dgvData.ReadOnly = true;
            this.dgvData.RowTemplate.Height = 23;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(1180, 448);
            this.dgvData.TabIndex = 0;
            this.dgvData.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DgvData_CellMouseUp);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1184, 84);
            this.panel1.TabIndex = 4;
            // 
            // FormStore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 562);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FormStore";
            this.Text = "FormStore";
            this.SizeChanged += new System.EventHandler(this.FormStore_SizeChanged);
            this.contextMenuStrip.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem TSMIDelete;
        private System.Windows.Forms.ToolStripMenuItem TSMIModify;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.Button btShowAll;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox cbSearchContent;
        private System.Windows.Forms.Label lbCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Panel panel1;
    }
}