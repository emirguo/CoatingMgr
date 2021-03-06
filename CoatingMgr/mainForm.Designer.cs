﻿namespace CoatingMgr
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
            this.master文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIMaster = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIImportMaster = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMItemAccountManager = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIManagerAccount = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIAddAccount = new System.Windows.Forms.ToolStripMenuItem();
            this.告警ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIWarning = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMISetWarning = new System.Windows.Forms.ToolStripMenuItem();
            this.日志ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMILog = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIExportLog = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnStock = new System.Windows.Forms.Button();
            this.btnStir = new System.Windows.Forms.Button();
            this.btnOut = new System.Windows.Forms.Button();
            this.btnIn = new System.Windows.Forms.Button();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
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
            this.master文件ToolStripMenuItem,
            this.TSMItemAccountManager,
            this.告警ToolStripMenuItem,
            this.日志ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // master文件ToolStripMenuItem
            // 
            this.master文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMIMaster,
            this.TSMIImportMaster});
            this.master文件ToolStripMenuItem.Name = "master文件ToolStripMenuItem";
            this.master文件ToolStripMenuItem.Size = new System.Drawing.Size(85, 21);
            this.master文件ToolStripMenuItem.Text = "Master文件";
            // 
            // TSMIMaster
            // 
            this.TSMIMaster.Name = "TSMIMaster";
            this.TSMIMaster.Size = new System.Drawing.Size(165, 22);
            this.TSMIMaster.Text = "查看Master文件";
            this.TSMIMaster.Click += new System.EventHandler(this.TSMIMaster_Click);
            // 
            // TSMIImportMaster
            // 
            this.TSMIImportMaster.Name = "TSMIImportMaster";
            this.TSMIImportMaster.Size = new System.Drawing.Size(165, 22);
            this.TSMIImportMaster.Text = "导入Master文件";
            this.TSMIImportMaster.Click += new System.EventHandler(this.TSMIImportMaster_Click);
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
            this.TSMIManagerAccount.Size = new System.Drawing.Size(124, 22);
            this.TSMIManagerAccount.Text = "管理账户";
            this.TSMIManagerAccount.Click += new System.EventHandler(this.TSMIManagerAccount_Click);
            // 
            // TSMIAddAccount
            // 
            this.TSMIAddAccount.Name = "TSMIAddAccount";
            this.TSMIAddAccount.Size = new System.Drawing.Size(124, 22);
            this.TSMIAddAccount.Text = "添加账户";
            this.TSMIAddAccount.Click += new System.EventHandler(this.TSMIAddAccount_Click);
            // 
            // 告警ToolStripMenuItem
            // 
            this.告警ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMIWarning,
            this.TSMISetWarning});
            this.告警ToolStripMenuItem.Name = "告警ToolStripMenuItem";
            this.告警ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.告警ToolStripMenuItem.Text = "告警";
            // 
            // TSMIWarning
            // 
            this.TSMIWarning.Name = "TSMIWarning";
            this.TSMIWarning.Size = new System.Drawing.Size(148, 22);
            this.TSMIWarning.Text = "查看告警规则";
            this.TSMIWarning.Click += new System.EventHandler(this.TSMIWarning_Click);
            // 
            // TSMISetWarning
            // 
            this.TSMISetWarning.Name = "TSMISetWarning";
            this.TSMISetWarning.Size = new System.Drawing.Size(148, 22);
            this.TSMISetWarning.Text = "设置告警规则";
            this.TSMISetWarning.Click += new System.EventHandler(this.TSMISetWarning_Click);
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
            this.TSMILog.Size = new System.Drawing.Size(124, 22);
            this.TSMILog.Text = "查看日志";
            this.TSMILog.Click += new System.EventHandler(this.TSMILog_Click);
            // 
            // TSMIExportLog
            // 
            this.TSMIExportLog.Name = "TSMIExportLog";
            this.TSMIExportLog.Size = new System.Drawing.Size(124, 22);
            this.TSMIExportLog.Text = "导出日志";
            this.TSMIExportLog.Click += new System.EventHandler(this.TSMIExportLog_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMIAbout});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // TSMIAbout
            // 
            this.TSMIAbout.Name = "TSMIAbout";
            this.TSMIAbout.Size = new System.Drawing.Size(100, 22);
            this.TSMIAbout.Text = "关于";
            this.TSMIAbout.Click += new System.EventHandler(this.TSMIAbout_Click);
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
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
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
            this.Text = "武汉高木--涂料管理系统";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
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
        private System.Windows.Forms.ToolStripMenuItem TSMIAbout;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Button btnStock;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolStripMenuItem 日志ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TSMILog;
        private System.Windows.Forms.ToolStripMenuItem TSMIExportLog;
        private System.Windows.Forms.ToolStripMenuItem master文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TSMIMaster;
        private System.Windows.Forms.ToolStripMenuItem TSMIImportMaster;
        private System.Windows.Forms.ToolStripMenuItem 告警ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TSMIWarning;
        private System.Windows.Forms.ToolStripMenuItem TSMISetWarning;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
    }
}