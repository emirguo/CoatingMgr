namespace CoatingMgr
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.TSMItemAccountManager = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIManagerAccount = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIAddAccount = new System.Windows.Forms.ToolStripMenuItem();
            this.日志ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMILog = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIExportLog = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnWarn = new System.Windows.Forms.Button();
            this.btnStock = new System.Windows.Forms.Button();
            this.btnStir = new System.Windows.Forms.Button();
            this.btnOut = new System.Windows.Forms.Button();
            this.btnIn = new System.Windows.Forms.Button();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.master文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIImportMaster = new System.Windows.Forms.ToolStripMenuItem();
            this.查看Master文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Highlight;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMItemAccountManager,
            this.master文件ToolStripMenuItem,
            this.日志ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // TSMItemAccountManager
            // 
            this.TSMItemAccountManager.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMIManagerAccount,
            this.TSMIAddAccount});
            this.TSMItemAccountManager.Name = "TSMItemAccountManager";
            this.TSMItemAccountManager.Size = new System.Drawing.Size(44, 21);
            this.TSMItemAccountManager.Text = "账户";
            // 
            // TSMIManagerAccount
            // 
            this.TSMIManagerAccount.Name = "TSMIManagerAccount";
            this.TSMIManagerAccount.Size = new System.Drawing.Size(180, 22);
            this.TSMIManagerAccount.Text = "管理账户";
            this.TSMIManagerAccount.Click += new System.EventHandler(this.TSMIManagerAccount_Click);
            // 
            // TSMIAddAccount
            // 
            this.TSMIAddAccount.Name = "TSMIAddAccount";
            this.TSMIAddAccount.Size = new System.Drawing.Size(180, 22);
            this.TSMIAddAccount.Text = "添加账户";
            this.TSMIAddAccount.Click += new System.EventHandler(this.TSMIAddAccount_Click);
            // 
            // 日志ToolStripMenuItem
            // 
            this.日志ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMILog,
            this.TSMIExportLog});
            this.日志ToolStripMenuItem.Name = "日志ToolStripMenuItem";
            this.日志ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.日志ToolStripMenuItem.Text = "日志";
            // 
            // TSMILog
            // 
            this.TSMILog.Name = "TSMILog";
            this.TSMILog.Size = new System.Drawing.Size(180, 22);
            this.TSMILog.Text = "查看日志";
            this.TSMILog.Click += new System.EventHandler(this.TSMILog_Click);
            // 
            // TSMIExportLog
            // 
            this.TSMIExportLog.Name = "TSMIExportLog";
            this.TSMIExportLog.Size = new System.Drawing.Size(180, 22);
            this.TSMIExportLog.Text = "导出日志";
            this.TSMIExportLog.Click += new System.EventHandler(this.TSMIExportLog_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关于ToolStripMenuItem});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.关于ToolStripMenuItem.Text = "关于";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.splitContainer1.Panel1.Controls.Add(this.btnWarn);
            this.splitContainer1.Panel1.Controls.Add(this.btnStock);
            this.splitContainer1.Panel1.Controls.Add(this.btnStir);
            this.splitContainer1.Panel1.Controls.Add(this.btnOut);
            this.splitContainer1.Panel1.Controls.Add(this.btnIn);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.mainPanel);
            this.splitContainer1.Size = new System.Drawing.Size(800, 425);
            this.splitContainer1.SplitterDistance = 99;
            this.splitContainer1.TabIndex = 2;
            // 
            // btnWarn
            // 
            this.btnWarn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWarn.Location = new System.Drawing.Point(12, 180);
            this.btnWarn.Name = "btnWarn";
            this.btnWarn.Size = new System.Drawing.Size(75, 36);
            this.btnWarn.TabIndex = 7;
            this.btnWarn.Text = "库存告警";
            this.btnWarn.UseVisualStyleBackColor = true;
            this.btnWarn.Click += new System.EventHandler(this.BtnWarn_Click);
            // 
            // btnStock
            // 
            this.btnStock.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.btnStock.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStock.Location = new System.Drawing.Point(12, 12);
            this.btnStock.Name = "btnStock";
            this.btnStock.Size = new System.Drawing.Size(75, 36);
            this.btnStock.TabIndex = 0;
            this.btnStock.Text = "库存管理";
            this.btnStock.UseVisualStyleBackColor = true;
            this.btnStock.Click += new System.EventHandler(this.BtnStock_Click);
            // 
            // btnStir
            // 
            this.btnStir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStir.Location = new System.Drawing.Point(12, 138);
            this.btnStir.Name = "btnStir";
            this.btnStir.Size = new System.Drawing.Size(75, 36);
            this.btnStir.TabIndex = 6;
            this.btnStir.Text = "涂料调和";
            this.btnStir.UseVisualStyleBackColor = true;
            this.btnStir.Click += new System.EventHandler(this.BtnStir_Click);
            // 
            // btnOut
            // 
            this.btnOut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOut.Location = new System.Drawing.Point(12, 96);
            this.btnOut.Name = "btnOut";
            this.btnOut.Size = new System.Drawing.Size(75, 36);
            this.btnOut.TabIndex = 5;
            this.btnOut.Text = "涂料出库";
            this.btnOut.UseVisualStyleBackColor = true;
            this.btnOut.Click += new System.EventHandler(this.BtnOut_Click);
            // 
            // btnIn
            // 
            this.btnIn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIn.Location = new System.Drawing.Point(12, 54);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(75, 36);
            this.btnIn.TabIndex = 4;
            this.btnIn.Text = "涂料入库";
            this.btnIn.UseVisualStyleBackColor = true;
            this.btnIn.Click += new System.EventHandler(this.BtnIn_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainPanel.Location = new System.Drawing.Point(4, 4);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(681, 411);
            this.mainPanel.TabIndex = 0;
            // 
            // master文件ToolStripMenuItem
            // 
            this.master文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查看Master文件ToolStripMenuItem,
            this.TSMIImportMaster});
            this.master文件ToolStripMenuItem.Name = "master文件ToolStripMenuItem";
            this.master文件ToolStripMenuItem.Size = new System.Drawing.Size(85, 21);
            this.master文件ToolStripMenuItem.Text = "Master文件";
            // 
            // TSMIImportMaster
            // 
            this.TSMIImportMaster.Name = "TSMIImportMaster";
            this.TSMIImportMaster.Size = new System.Drawing.Size(180, 22);
            this.TSMIImportMaster.Text = "导入Master文件";
            this.TSMIImportMaster.Click += new System.EventHandler(this.TSMIImportMaster_Click);
            // 
            // 查看Master文件ToolStripMenuItem
            // 
            this.查看Master文件ToolStripMenuItem.Name = "查看Master文件ToolStripMenuItem";
            this.查看Master文件ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.查看Master文件ToolStripMenuItem.Text = "查看Master文件";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "main";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnIn;
        private System.Windows.Forms.Button btnOut;
        private System.Windows.Forms.Button btnStir;
        private System.Windows.Forms.ToolStripMenuItem TSMItemAccountManager;
        private System.Windows.Forms.ToolStripMenuItem TSMIAddAccount;
        private System.Windows.Forms.ToolStripMenuItem TSMIManagerAccount;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Button btnStock;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btnWarn;
        private System.Windows.Forms.ToolStripMenuItem 日志ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TSMILog;
        private System.Windows.Forms.ToolStripMenuItem TSMIExportLog;
        private System.Windows.Forms.ToolStripMenuItem master文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查看Master文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TSMIImportMaster;
    }
}