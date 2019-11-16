namespace CoatingMgr
{
    partial class FormIn
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
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbProDescription = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbUser = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lbCount = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lbTime = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panelResult = new System.Windows.Forms.Panel();
            this.lbResult = new System.Windows.Forms.Label();
            this.lbDecide = new System.Windows.Forms.Label();
            this.tbBarCode = new System.Windows.Forms.TextBox();
            this.tbProductionDate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cbSearchStock = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbColor = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbWeight = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbType = new System.Windows.Forms.TextBox();
            this.tbExpiryDate = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbModel = new System.Windows.Forms.TextBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TSMIDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panelResult.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 18F);
            this.label1.Location = new System.Drawing.Point(65, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "入库履历";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(532, 231);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(8, 8);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lbProDescription);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(948, 144);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(231, 377);
            this.panel1.TabIndex = 18;
            // 
            // lbProDescription
            // 
            this.lbProDescription.AutoSize = true;
            this.lbProDescription.Font = new System.Drawing.Font("宋体", 12F);
            this.lbProDescription.Location = new System.Drawing.Point(16, 46);
            this.lbProDescription.Name = "lbProDescription";
            this.lbProDescription.Size = new System.Drawing.Size(0, 16);
            this.lbProDescription.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.lbUser);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.lbTime);
            this.panel2.Controls.Add(this.dgvData);
            this.panel2.Location = new System.Drawing.Point(0, 144);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(944, 416);
            this.panel2.TabIndex = 25;
            // 
            // lbUser
            // 
            this.lbUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbUser.AutoSize = true;
            this.lbUser.Font = new System.Drawing.Font("宋体", 12F);
            this.lbUser.Location = new System.Drawing.Point(65, 393);
            this.lbUser.Name = "lbUser";
            this.lbUser.Size = new System.Drawing.Size(40, 16);
            this.lbUser.TabIndex = 47;
            this.lbUser.Text = "张三";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 12F);
            this.label13.Location = new System.Drawing.Point(3, 393);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 16);
            this.label13.TabIndex = 46;
            this.label13.Text = "操作员：";
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.Controls.Add(this.lbCount);
            this.panel5.Controls.Add(this.label11);
            this.panel5.Location = new System.Drawing.Point(677, 380);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(263, 35);
            this.panel5.TabIndex = 30;
            // 
            // lbCount
            // 
            this.lbCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCount.AutoSize = true;
            this.lbCount.Font = new System.Drawing.Font("宋体", 12F);
            this.lbCount.Location = new System.Drawing.Point(210, 9);
            this.lbCount.Name = "lbCount";
            this.lbCount.Size = new System.Drawing.Size(16, 16);
            this.lbCount.TabIndex = 33;
            this.lbCount.Text = "0";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 12F);
            this.label11.Location = new System.Drawing.Point(134, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(88, 16);
            this.label11.TabIndex = 30;
            this.label11.Text = "合计数量：";
            // 
            // lbTime
            // 
            this.lbTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbTime.AutoSize = true;
            this.lbTime.Font = new System.Drawing.Font("宋体", 12F);
            this.lbTime.Location = new System.Drawing.Point(121, 393);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(56, 16);
            this.lbTime.TabIndex = 5;
            this.lbTime.Text = "时间：";
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvData.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvData.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvData.Location = new System.Drawing.Point(2, 2);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowTemplate.Height = 23;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(936, 374);
            this.dgvData.TabIndex = 29;
            this.dgvData.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DgvData_CellMouseUp);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.label14);
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Font = new System.Drawing.Font("宋体", 12F);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1180, 140);
            this.panel4.TabIndex = 27;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(516, 4);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(110, 24);
            this.label14.TabIndex = 53;
            this.label14.Text = "入库工程";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.panelResult);
            this.panel3.Controls.Add(this.tbBarCode);
            this.panel3.Controls.Add(this.tbProductionDate);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.cbSearchStock);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.tbColor);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.tbName);
            this.panel3.Controls.Add(this.tbWeight);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.tbType);
            this.panel3.Controls.Add(this.tbExpiryDate);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.tbModel);
            this.panel3.Location = new System.Drawing.Point(2, 16);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1176, 120);
            this.panel3.TabIndex = 48;
            // 
            // panelResult
            // 
            this.panelResult.BackColor = System.Drawing.Color.Gainsboro;
            this.panelResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelResult.Controls.Add(this.lbResult);
            this.panelResult.Controls.Add(this.lbDecide);
            this.panelResult.Location = new System.Drawing.Point(980, 16);
            this.panelResult.Name = "panelResult";
            this.panelResult.Size = new System.Drawing.Size(180, 92);
            this.panelResult.TabIndex = 53;
            // 
            // lbResult
            // 
            this.lbResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbResult.AutoSize = true;
            this.lbResult.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbResult.ForeColor = System.Drawing.Color.Black;
            this.lbResult.Location = new System.Drawing.Point(54, 34);
            this.lbResult.Name = "lbResult";
            this.lbResult.Size = new System.Drawing.Size(0, 48);
            this.lbResult.TabIndex = 50;
            // 
            // lbDecide
            // 
            this.lbDecide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbDecide.AutoSize = true;
            this.lbDecide.BackColor = System.Drawing.Color.Transparent;
            this.lbDecide.Font = new System.Drawing.Font("宋体", 12F);
            this.lbDecide.ForeColor = System.Drawing.Color.Black;
            this.lbDecide.Location = new System.Drawing.Point(14, 12);
            this.lbDecide.Name = "lbDecide";
            this.lbDecide.Size = new System.Drawing.Size(88, 16);
            this.lbDecide.TabIndex = 0;
            this.lbDecide.Text = "综合判定：";
            // 
            // tbBarCode
            // 
            this.tbBarCode.Font = new System.Drawing.Font("宋体", 12F);
            this.tbBarCode.Location = new System.Drawing.Point(404, 17);
            this.tbBarCode.Name = "tbBarCode";
            this.tbBarCode.Size = new System.Drawing.Size(558, 26);
            this.tbBarCode.TabIndex = 35;
            this.tbBarCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbBarCode_KeyDown);
            // 
            // tbProductionDate
            // 
            this.tbProductionDate.Font = new System.Drawing.Font("宋体", 12F);
            this.tbProductionDate.Location = new System.Drawing.Point(495, 82);
            this.tbProductionDate.Name = "tbProductionDate";
            this.tbProductionDate.ReadOnly = true;
            this.tbProductionDate.Size = new System.Drawing.Size(128, 26);
            this.tbProductionDate.TabIndex = 52;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F);
            this.label3.Location = new System.Drawing.Point(12, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 32;
            this.label3.Text = "请选择仓库";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 12F);
            this.label12.Location = new System.Drawing.Point(417, 86);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 16);
            this.label12.TabIndex = 51;
            this.label12.Text = "生产日期";
            // 
            // cbSearchStock
            // 
            this.cbSearchStock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSearchStock.Font = new System.Drawing.Font("宋体", 12F);
            this.cbSearchStock.FormattingEnabled = true;
            this.cbSearchStock.Location = new System.Drawing.Point(106, 17);
            this.cbSearchStock.Name = "cbSearchStock";
            this.cbSearchStock.Size = new System.Drawing.Size(229, 24);
            this.cbSearchStock.TabIndex = 33;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 12F);
            this.label10.Location = new System.Drawing.Point(938, 55);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(24, 16);
            this.label10.TabIndex = 50;
            this.label10.Text = "kg";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F);
            this.label4.Location = new System.Drawing.Point(342, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 34;
            this.label4.Text = "条形码";
            // 
            // tbColor
            // 
            this.tbColor.Font = new System.Drawing.Font("宋体", 12F);
            this.tbColor.Location = new System.Drawing.Point(629, 50);
            this.tbColor.Name = "tbColor";
            this.tbColor.ReadOnly = true;
            this.tbColor.Size = new System.Drawing.Size(136, 26);
            this.tbColor.TabIndex = 49;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F);
            this.label5.Location = new System.Drawing.Point(12, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 16);
            this.label5.TabIndex = 36;
            this.label5.Text = "名称";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F);
            this.label2.Location = new System.Drawing.Point(583, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 48;
            this.label2.Text = "颜色";
            // 
            // tbName
            // 
            this.tbName.Font = new System.Drawing.Font("宋体", 12F);
            this.tbName.Location = new System.Drawing.Point(58, 49);
            this.tbName.Name = "tbName";
            this.tbName.ReadOnly = true;
            this.tbName.Size = new System.Drawing.Size(222, 26);
            this.tbName.TabIndex = 37;
            // 
            // tbWeight
            // 
            this.tbWeight.Font = new System.Drawing.Font("宋体", 12F);
            this.tbWeight.Location = new System.Drawing.Point(858, 51);
            this.tbWeight.Name = "tbWeight";
            this.tbWeight.ReadOnly = true;
            this.tbWeight.Size = new System.Drawing.Size(71, 26);
            this.tbWeight.TabIndex = 45;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F);
            this.label6.Location = new System.Drawing.Point(295, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 16);
            this.label6.TabIndex = 38;
            this.label6.Text = "类型";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 12F);
            this.label9.Location = new System.Drawing.Point(780, 55);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 16);
            this.label9.TabIndex = 44;
            this.label9.Text = "标准重量";
            // 
            // tbType
            // 
            this.tbType.Font = new System.Drawing.Font("宋体", 12F);
            this.tbType.Location = new System.Drawing.Point(345, 50);
            this.tbType.Name = "tbType";
            this.tbType.ReadOnly = true;
            this.tbType.Size = new System.Drawing.Size(217, 26);
            this.tbType.TabIndex = 39;
            // 
            // tbExpiryDate
            // 
            this.tbExpiryDate.Font = new System.Drawing.Font("宋体", 12F);
            this.tbExpiryDate.Location = new System.Drawing.Point(715, 82);
            this.tbExpiryDate.Name = "tbExpiryDate";
            this.tbExpiryDate.ReadOnly = true;
            this.tbExpiryDate.Size = new System.Drawing.Size(128, 26);
            this.tbExpiryDate.TabIndex = 43;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 12F);
            this.label7.Location = new System.Drawing.Point(12, 86);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 16);
            this.label7.TabIndex = 40;
            this.label7.Text = "适用机种";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 12F);
            this.label8.Location = new System.Drawing.Point(643, 85);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 16);
            this.label8.TabIndex = 42;
            this.label8.Text = "有效期";
            // 
            // tbModel
            // 
            this.tbModel.Font = new System.Drawing.Font("宋体", 12F);
            this.tbModel.Location = new System.Drawing.Point(90, 82);
            this.tbModel.Name = "tbModel";
            this.tbModel.ReadOnly = true;
            this.tbModel.Size = new System.Drawing.Size(305, 26);
            this.tbModel.TabIndex = 41;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMIDelete});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(101, 26);
            // 
            // TSMIDelete
            // 
            this.TSMIDelete.Name = "TSMIDelete";
            this.TSMIDelete.Size = new System.Drawing.Size(100, 22);
            this.TSMIDelete.Text = "删除";
            this.TSMIDelete.Click += new System.EventHandler(this.TSMIDelete_Click);
            // 
            // FormIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 562);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "FormIn";
            this.Text = "入库工程";
            this.Load += new System.EventHandler(this.FormIn_Load);
            this.SizeChanged += new System.EventHandler(this.FormIn_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panelResult.ResumeLayout(false);
            this.panelResult.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbProDescription;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbTime;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox tbWeight;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbExpiryDate;
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
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lbUser;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lbCount;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem TSMIDelete;
        private System.Windows.Forms.TextBox tbColor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbProductionDate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panelResult;
        private System.Windows.Forms.Label lbResult;
        private System.Windows.Forms.Label lbDecide;
    }
}