namespace CoatingMgr
{
    partial class FormChartFillWindow
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.LegendItem legendItem5 = new System.Windows.Forms.DataVisualization.Charting.LegendItem();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.LegendItem legendItem6 = new System.Windows.Forms.DataVisualization.Charting.LegendItem();
            System.Windows.Forms.DataVisualization.Charting.LegendItem legendItem7 = new System.Windows.Forms.DataVisualization.Charting.LegendItem();
            System.Windows.Forms.DataVisualization.Charting.LegendItem legendItem8 = new System.Windows.Forms.DataVisualization.Charting.LegendItem();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.chartStock = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cbShowWarn = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.chartStock)).BeginInit();
            this.SuspendLayout();
            // 
            // chartStock
            // 
            this.chartStock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea2.Name = "ChartArea1";
            this.chartStock.ChartAreas.Add(chartArea2);
            legend3.Alignment = System.Drawing.StringAlignment.Center;
            legendItem5.Color = System.Drawing.Color.Green;
            legendItem5.Name = "库存重量";
            legend3.CustomItems.Add(legendItem5);
            legend3.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend3.Name = "LegendWeight";
            legend4.Alignment = System.Drawing.StringAlignment.Center;
            legendItem6.Color = System.Drawing.Color.Yellow;
            legendItem6.Name = "库存上限";
            legendItem7.Color = System.Drawing.Color.Green;
            legendItem7.Name = "库存重量";
            legendItem8.Color = System.Drawing.Color.DarkRed;
            legendItem8.Name = "库存下限";
            legend4.CustomItems.Add(legendItem6);
            legend4.CustomItems.Add(legendItem7);
            legend4.CustomItems.Add(legendItem8);
            legend4.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend4.Enabled = false;
            legend4.Name = "LegendMulti";
            this.chartStock.Legends.Add(legend3);
            this.chartStock.Legends.Add(legend4);
            this.chartStock.Location = new System.Drawing.Point(1, 1);
            this.chartStock.Name = "chartStock";
            this.chartStock.Size = new System.Drawing.Size(799, 448);
            this.chartStock.TabIndex = 0;
            this.chartStock.Text = "chart1";
            title2.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title2.Name = "库存统计";
            title2.Text = "库存统计";
            this.chartStock.Titles.Add(title2);
            // 
            // cbShowWarn
            // 
            this.cbShowWarn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbShowWarn.AutoSize = true;
            this.cbShowWarn.BackColor = System.Drawing.Color.White;
            this.cbShowWarn.Font = new System.Drawing.Font("宋体", 12F);
            this.cbShowWarn.Location = new System.Drawing.Point(709, 1);
            this.cbShowWarn.Name = "cbShowWarn";
            this.cbShowWarn.Size = new System.Drawing.Size(91, 20);
            this.cbShowWarn.TabIndex = 59;
            this.cbShowWarn.Text = "显示告警";
            this.cbShowWarn.UseVisualStyleBackColor = false;
            this.cbShowWarn.CheckedChanged += new System.EventHandler(this.CbShowWarn_CheckedChanged);
            // 
            // FormChartFillWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cbShowWarn);
            this.Controls.Add(this.chartStock);
            this.Name = "FormChartFillWindow";
            this.Text = "武汉高木--涂料管理系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormChartFillWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartStock)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartStock;
        private System.Windows.Forms.CheckBox cbShowWarn;
    }
}