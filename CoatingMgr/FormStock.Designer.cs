namespace CoatingMgr
{
    partial class FormStock
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExport = new System.Windows.Forms.Button();
            this.cbFillWindow = new System.Windows.Forms.CheckBox();
            this.cbSearchContent = new System.Windows.Forms.ComboBox();
            this.cbShowHistogram = new System.Windows.Forms.CheckBox();
            this.btShowAll = new System.Windows.Forms.Button();
            this.cbSearchType = new System.Windows.Forms.ComboBox();
            this.lbUser = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lbTime = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lbCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvStockData = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TSMIDeleteRow = new System.Windows.Forms.ToolStripMenuItem();
            this.chartStock = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockData)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartStock)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.btnExport);
            this.panel1.Controls.Add(this.cbFillWindow);
            this.panel1.Controls.Add(this.cbSearchContent);
            this.panel1.Controls.Add(this.cbShowHistogram);
            this.panel1.Controls.Add(this.btShowAll);
            this.panel1.Controls.Add(this.cbSearchType);
            this.panel1.Location = new System.Drawing.Point(1, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(983, 36);
            this.panel1.TabIndex = 0;
            // 
            // btnExport
            // 
            this.btnExport.Font = new System.Drawing.Font("宋体", 12F);
            this.btnExport.Location = new System.Drawing.Point(497, 6);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(122, 27);
            this.btnExport.TabIndex = 58;
            this.btnExport.Text = "导出Excel表格";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // cbFillWindow
            // 
            this.cbFillWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFillWindow.AutoSize = true;
            this.cbFillWindow.Font = new System.Drawing.Font("宋体", 12F);
            this.cbFillWindow.Location = new System.Drawing.Point(880, 9);
            this.cbFillWindow.Name = "cbFillWindow";
            this.cbFillWindow.Size = new System.Drawing.Size(91, 20);
            this.cbFillWindow.TabIndex = 57;
            this.cbFillWindow.Text = "全屏显示";
            this.cbFillWindow.UseVisualStyleBackColor = true;
            this.cbFillWindow.CheckedChanged += new System.EventHandler(this.CbFillWindow_CheckedChanged);
            // 
            // cbSearchContent
            // 
            this.cbSearchContent.Font = new System.Drawing.Font("宋体", 12F);
            this.cbSearchContent.FormattingEnabled = true;
            this.cbSearchContent.Location = new System.Drawing.Point(181, 8);
            this.cbSearchContent.Name = "cbSearchContent";
            this.cbSearchContent.Size = new System.Drawing.Size(158, 24);
            this.cbSearchContent.TabIndex = 56;
            this.cbSearchContent.Text = "选择过滤内容";
            this.cbSearchContent.SelectedIndexChanged += new System.EventHandler(this.CbSearchContent_SelectedIndexChanged);
            // 
            // cbShowHistogram
            // 
            this.cbShowHistogram.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbShowHistogram.AutoSize = true;
            this.cbShowHistogram.Font = new System.Drawing.Font("宋体", 12F);
            this.cbShowHistogram.Location = new System.Drawing.Point(752, 9);
            this.cbShowHistogram.Name = "cbShowHistogram";
            this.cbShowHistogram.Size = new System.Drawing.Size(107, 20);
            this.cbShowHistogram.TabIndex = 55;
            this.cbShowHistogram.Text = "显示柱状图";
            this.cbShowHistogram.UseVisualStyleBackColor = true;
            this.cbShowHistogram.CheckedChanged += new System.EventHandler(this.CbShowHistogram_CheckedChanged);
            // 
            // btShowAll
            // 
            this.btShowAll.Font = new System.Drawing.Font("宋体", 12F);
            this.btShowAll.Location = new System.Drawing.Point(358, 6);
            this.btShowAll.Name = "btShowAll";
            this.btShowAll.Size = new System.Drawing.Size(122, 27);
            this.btShowAll.TabIndex = 10;
            this.btShowAll.Text = "显示全部库存";
            this.btShowAll.UseVisualStyleBackColor = true;
            this.btShowAll.Click += new System.EventHandler(this.BtShowAll_Click);
            // 
            // cbSearchType
            // 
            this.cbSearchType.Font = new System.Drawing.Font("宋体", 12F);
            this.cbSearchType.FormattingEnabled = true;
            this.cbSearchType.Location = new System.Drawing.Point(5, 7);
            this.cbSearchType.Name = "cbSearchType";
            this.cbSearchType.Size = new System.Drawing.Size(170, 24);
            this.cbSearchType.TabIndex = 1;
            this.cbSearchType.Text = "选择过滤种类";
            this.cbSearchType.SelectedIndexChanged += new System.EventHandler(this.CbSearchType_SelectedIndexChanged);
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
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.lbUser);
            this.panel2.Controls.Add(this.chartStock);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.lbTime);
            this.panel2.Controls.Add(this.dgvStockData);
            this.panel2.Location = new System.Drawing.Point(1, 42);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(982, 422);
            this.panel2.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.lbCount);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Font = new System.Drawing.Font("宋体", 12F);
            this.panel4.Location = new System.Drawing.Point(830, 394);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(153, 28);
            this.panel4.TabIndex = 16;
            // 
            // lbCount
            // 
            this.lbCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCount.AutoSize = true;
            this.lbCount.Font = new System.Drawing.Font("宋体", 12F);
            this.lbCount.Location = new System.Drawing.Point(82, 6);
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
            this.label4.Location = new System.Drawing.Point(22, 6);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "总数量：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvStockData
            // 
            this.dgvStockData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStockData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStockData.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvStockData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStockData.Location = new System.Drawing.Point(2, 2);
            this.dgvStockData.Name = "dgvStockData";
            this.dgvStockData.RowTemplate.Height = 23;
            this.dgvStockData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStockData.Size = new System.Drawing.Size(978, 392);
            this.dgvStockData.TabIndex = 0;
            this.dgvStockData.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DgvStockData_CellMouseUp);
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
            this.TSMIDeleteRow.Click += new System.EventHandler(this.TSMIDeleteRow_Click);
            // 
            // chartStock
            // 
            this.chartStock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chartStock.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartStock.Legends.Add(legend1);
            this.chartStock.Location = new System.Drawing.Point(1, 2);
            this.chartStock.Name = "chartStock";
            this.chartStock.Size = new System.Drawing.Size(980, 392);
            this.chartStock.TabIndex = 2;
            this.chartStock.Text = "chart1";
            this.chartStock.Visible = false;
            // 
            // FormStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 462);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FormStock";
            this.Text = "库存管理";
            this.Load += new System.EventHandler(this.FormStock_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockData)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartStock)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbSearchType;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvStockData;
        private System.Windows.Forms.Label lbUser;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lbTime;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lbCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem TSMIDeleteRow;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartStock;
        private System.Windows.Forms.CheckBox cbShowHistogram;
        private System.Windows.Forms.CheckBox cbFillWindow;
        private System.Windows.Forms.ComboBox cbSearchContent;
        private System.Windows.Forms.Button btShowAll;
        private System.Windows.Forms.Button btnExport;
    }
}