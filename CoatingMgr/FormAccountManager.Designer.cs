namespace CoatingMgr
{
    partial class FormAccountManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAccountManager));
            this.dgvAccountData = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lbCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.cbQueryType = new System.Windows.Forms.ComboBox();
            this.tbQueryContent = new System.Windows.Forms.TextBox();
            this.pbQuery = new System.Windows.Forms.PictureBox();
            this.btAdd = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btDelete = new System.Windows.Forms.Button();
            this.btModify = new System.Windows.Forms.Button();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccountData)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbQuery)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvAccountData
            // 
            this.dgvAccountData.AllowUserToOrderColumns = true;
            this.dgvAccountData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAccountData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAccountData.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvAccountData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAccountData.Location = new System.Drawing.Point(2, 2);
            this.dgvAccountData.Name = "dgvAccountData";
            this.dgvAccountData.RowTemplate.Height = 23;
            this.dgvAccountData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAccountData.Size = new System.Drawing.Size(978, 392);
            this.dgvAccountData.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.dgvAccountData);
            this.panel2.Location = new System.Drawing.Point(1, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(982, 422);
            this.panel2.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.lbCount);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Font = new System.Drawing.Font("宋体", 12F);
            this.panel4.Location = new System.Drawing.Point(0, 394);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(983, 28);
            this.panel4.TabIndex = 15;
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
            // cbQueryType
            // 
            this.cbQueryType.Font = new System.Drawing.Font("宋体", 12F);
            this.cbQueryType.FormattingEnabled = true;
            this.cbQueryType.Location = new System.Drawing.Point(4, 9);
            this.cbQueryType.Name = "cbQueryType";
            this.cbQueryType.Size = new System.Drawing.Size(120, 24);
            this.cbQueryType.TabIndex = 0;
            // 
            // tbQueryContent
            // 
            this.tbQueryContent.Font = new System.Drawing.Font("宋体", 12F);
            this.tbQueryContent.Location = new System.Drawing.Point(130, 8);
            this.tbQueryContent.Name = "tbQueryContent";
            this.tbQueryContent.Size = new System.Drawing.Size(159, 26);
            this.tbQueryContent.TabIndex = 2;
            // 
            // pbQuery
            // 
            this.pbQuery.Image = ((System.Drawing.Image)(resources.GetObject("pbQuery.Image")));
            this.pbQuery.Location = new System.Drawing.Point(304, 8);
            this.pbQuery.Name = "pbQuery";
            this.pbQuery.Size = new System.Drawing.Size(28, 27);
            this.pbQuery.TabIndex = 4;
            this.pbQuery.TabStop = false;
            this.pbQuery.Click += new System.EventHandler(this.PbQuery_Click);
            // 
            // btAdd
            // 
            this.btAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btAdd.Font = new System.Drawing.Font("宋体", 12F);
            this.btAdd.Location = new System.Drawing.Point(706, 8);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(80, 27);
            this.btAdd.TabIndex = 5;
            this.btAdd.Text = "添加";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.BtAdd_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.btDelete);
            this.panel1.Controls.Add(this.btModify);
            this.panel1.Controls.Add(this.btAdd);
            this.panel1.Controls.Add(this.pbQuery);
            this.panel1.Controls.Add(this.tbQueryContent);
            this.panel1.Controls.Add(this.cbQueryType);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(983, 36);
            this.panel1.TabIndex = 2;
            // 
            // btDelete
            // 
            this.btDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btDelete.Font = new System.Drawing.Font("宋体", 12F);
            this.btDelete.Location = new System.Drawing.Point(894, 8);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(80, 27);
            this.btDelete.TabIndex = 7;
            this.btDelete.Text = "删除";
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.BtDelete_Click);
            // 
            // btModify
            // 
            this.btModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btModify.Font = new System.Drawing.Font("宋体", 12F);
            this.btModify.Location = new System.Drawing.Point(800, 8);
            this.btModify.Name = "btModify";
            this.btModify.Size = new System.Drawing.Size(80, 27);
            this.btModify.TabIndex = 6;
            this.btModify.Text = "修改";
            this.btModify.UseVisualStyleBackColor = true;
            this.btModify.Click += new System.EventHandler(this.BtModify_Click);
            // 
            // FormAccountManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 462);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FormAccountManager";
            this.Text = "账户管理";
            this.Load += new System.EventHandler(this.FormAccountManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccountData)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbQuery)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAccountData;
        private System.Windows.Forms.Panel panel2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ComboBox cbQueryType;
        private System.Windows.Forms.TextBox tbQueryContent;
        private System.Windows.Forms.PictureBox pbQuery;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.Button btModify;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lbCount;
        private System.Windows.Forms.Label label4;
    }
}