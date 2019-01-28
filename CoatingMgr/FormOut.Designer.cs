namespace CoatingMgr
{
    partial class FormOut
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
            this.dgvOutStockData = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lbNumCount = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.lbUser = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lbTime = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.lbProDescription = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btModify = new System.Windows.Forms.Button();
            this.tbWeight = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbDate = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbModel = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbType = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbBarCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbSearchStock = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutStockData)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvOutStockData
            // 
            this.dgvOutStockData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOutStockData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOutStockData.Location = new System.Drawing.Point(3, 3);
            this.dgvOutStockData.Name = "dgvOutStockData";
            this.dgvOutStockData.RowTemplate.Height = 23;
            this.dgvOutStockData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOutStockData.Size = new System.Drawing.Size(738, 350);
            this.dgvOutStockData.TabIndex = 29;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.dgvOutStockData);
            this.panel2.Controls.Add(this.lbUser);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.lbTime);
            this.panel2.Location = new System.Drawing.Point(0, 68);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(744, 392);
            this.panel2.TabIndex = 30;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.Controls.Add(this.lbNumCount);
            this.panel5.Controls.Add(this.label11);
            this.panel5.Controls.Add(this.btnOk);
            this.panel5.Location = new System.Drawing.Point(477, 356);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(263, 35);
            this.panel5.TabIndex = 36;
            // 
            // lbNumCount
            // 
            this.lbNumCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbNumCount.AutoSize = true;
            this.lbNumCount.Font = new System.Drawing.Font("宋体", 12F);
            this.lbNumCount.Location = new System.Drawing.Point(94, 9);
            this.lbNumCount.Name = "lbNumCount";
            this.lbNumCount.Size = new System.Drawing.Size(16, 16);
            this.lbNumCount.TabIndex = 33;
            this.lbNumCount.Text = "0";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 12F);
            this.label11.Location = new System.Drawing.Point(18, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(88, 16);
            this.label11.TabIndex = 30;
            this.label11.Text = "合计数量：";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Font = new System.Drawing.Font("宋体", 12F);
            this.btnOk.Location = new System.Drawing.Point(177, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(83, 29);
            this.btnOk.TabIndex = 29;
            this.btnOk.Text = "确认出库";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // lbUser
            // 
            this.lbUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbUser.AutoSize = true;
            this.lbUser.Font = new System.Drawing.Font("宋体", 12F);
            this.lbUser.Location = new System.Drawing.Point(65, 369);
            this.lbUser.Name = "lbUser";
            this.lbUser.Size = new System.Drawing.Size(40, 16);
            this.lbUser.TabIndex = 49;
            this.lbUser.Text = "张三";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 12F);
            this.label13.Location = new System.Drawing.Point(3, 369);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 16);
            this.label13.TabIndex = 48;
            this.label13.Text = "操作员：";
            // 
            // lbTime
            // 
            this.lbTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbTime.AutoSize = true;
            this.lbTime.Font = new System.Drawing.Font("宋体", 12F);
            this.lbTime.Location = new System.Drawing.Point(121, 369);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(56, 16);
            this.lbTime.TabIndex = 5;
            this.lbTime.Text = "时间：";
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Font = new System.Drawing.Font("宋体", 12F);
            this.btnDelete.Location = new System.Drawing.Point(894, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(83, 29);
            this.btnDelete.TabIndex = 35;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // lbProDescription
            // 
            this.lbProDescription.AutoSize = true;
            this.lbProDescription.Font = new System.Drawing.Font("宋体", 12F);
            this.lbProDescription.Location = new System.Drawing.Point(16, 46);
            this.lbProDescription.Name = "lbProDescription";
            this.lbProDescription.Size = new System.Drawing.Size(72, 16);
            this.lbProDescription.TabIndex = 0;
            this.lbProDescription.Text = "出库履历";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.lbProDescription);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(748, 68);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(231, 393);
            this.panel1.TabIndex = 29;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 18F);
            this.label1.Location = new System.Drawing.Point(14, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "出库履历";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(532, 233);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(8, 8);
            this.flowLayoutPanel1.TabIndex = 28;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.btModify);
            this.panel4.Controls.Add(this.btnDelete);
            this.panel4.Controls.Add(this.tbWeight);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.tbDate);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.tbModel);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.tbType);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.tbName);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.tbBarCode);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.cbSearchStock);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Font = new System.Drawing.Font("宋体", 12F);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(980, 64);
            this.panel4.TabIndex = 32;
            // 
            // btModify
            // 
            this.btModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btModify.Font = new System.Drawing.Font("宋体", 12F);
            this.btModify.Location = new System.Drawing.Point(800, 2);
            this.btModify.Name = "btModify";
            this.btModify.Size = new System.Drawing.Size(80, 27);
            this.btModify.TabIndex = 51;
            this.btModify.Text = "修改";
            this.btModify.UseVisualStyleBackColor = true;
            this.btModify.Click += new System.EventHandler(this.BtModify_Click);
            // 
            // tbWeight
            // 
            this.tbWeight.Font = new System.Drawing.Font("宋体", 12F);
            this.tbWeight.Location = new System.Drawing.Point(477, 36);
            this.tbWeight.Name = "tbWeight";
            this.tbWeight.Size = new System.Drawing.Size(90, 26);
            this.tbWeight.TabIndex = 45;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 12F);
            this.label9.Location = new System.Drawing.Point(409, 41);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 16);
            this.label9.TabIndex = 44;
            this.label9.Text = "标准重量";
            // 
            // tbDate
            // 
            this.tbDate.Font = new System.Drawing.Font("宋体", 12F);
            this.tbDate.Location = new System.Drawing.Point(849, 35);
            this.tbDate.Name = "tbDate";
            this.tbDate.Size = new System.Drawing.Size(118, 26);
            this.tbDate.TabIndex = 43;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 12F);
            this.label8.Location = new System.Drawing.Point(771, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 16);
            this.label8.TabIndex = 42;
            this.label8.Text = "使用期限";
            // 
            // tbModel
            // 
            this.tbModel.Font = new System.Drawing.Font("宋体", 12F);
            this.tbModel.Location = new System.Drawing.Point(644, 37);
            this.tbModel.Name = "tbModel";
            this.tbModel.Size = new System.Drawing.Size(121, 26);
            this.tbModel.TabIndex = 41;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 12F);
            this.label7.Location = new System.Drawing.Point(573, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 16);
            this.label7.TabIndex = 40;
            this.label7.Text = "适用机种";
            // 
            // tbType
            // 
            this.tbType.Font = new System.Drawing.Font("宋体", 12F);
            this.tbType.Location = new System.Drawing.Point(307, 37);
            this.tbType.Name = "tbType";
            this.tbType.Size = new System.Drawing.Size(96, 26);
            this.tbType.TabIndex = 39;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F);
            this.label6.Location = new System.Drawing.Point(229, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 16);
            this.label6.TabIndex = 38;
            this.label6.Text = "涂料种类";
            // 
            // tbName
            // 
            this.tbName.Font = new System.Drawing.Font("宋体", 12F);
            this.tbName.Location = new System.Drawing.Point(83, 36);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(140, 26);
            this.tbName.TabIndex = 37;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F);
            this.label5.Location = new System.Drawing.Point(5, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 16);
            this.label5.TabIndex = 36;
            this.label5.Text = "涂料名称";
            // 
            // tbBarCode
            // 
            this.tbBarCode.Font = new System.Drawing.Font("宋体", 12F);
            this.tbBarCode.Location = new System.Drawing.Point(391, 3);
            this.tbBarCode.Name = "tbBarCode";
            this.tbBarCode.Size = new System.Drawing.Size(205, 26);
            this.tbBarCode.TabIndex = 35;
            this.tbBarCode.TextChanged += new System.EventHandler(this.TbBarCode_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F);
            this.label4.Location = new System.Drawing.Point(329, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 34;
            this.label4.Text = "条形码";
            // 
            // cbSearchStock
            // 
            this.cbSearchStock.Font = new System.Drawing.Font("宋体", 12F);
            this.cbSearchStock.FormattingEnabled = true;
            this.cbSearchStock.Location = new System.Drawing.Point(95, 3);
            this.cbSearchStock.Name = "cbSearchStock";
            this.cbSearchStock.Size = new System.Drawing.Size(204, 24);
            this.cbSearchStock.TabIndex = 33;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F);
            this.label3.Location = new System.Drawing.Point(5, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 32;
            this.label3.Text = "请选择仓库";
            // 
            // FormOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 462);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "FormOut";
            this.Text = "出库工程";
            this.Load += new System.EventHandler(this.FormOut_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutStockData)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvOutStockData;
        private System.Windows.Forms.Panel panel2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label lbProDescription;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lbTime;
        private System.Windows.Forms.TextBox tbWeight;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbDate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbModel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbBarCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbSearchStock;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbUser;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lbNumCount;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btModify;
    }
}