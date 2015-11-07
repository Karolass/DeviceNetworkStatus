namespace DeviceNetworkStatus
{
    partial class Fm_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Fm_Main));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Minimize = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Visible = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_MenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_notify = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_OPEN = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.timer_Log = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip_notify.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(1194, 28);
            this.menuStrip1.TabIndex = 0;
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Minimize,
            this.toolStripMenuItem_Visible,
            this.toolStripSeparator1,
            this.ToolStripMenuItem_MenuExit});
            this.toolStripMenuItem1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(71, 24);
            this.toolStripMenuItem1.Text = "程式(&F)";
            // 
            // toolStripMenuItem_Minimize
            // 
            this.toolStripMenuItem_Minimize.Name = "toolStripMenuItem_Minimize";
            this.toolStripMenuItem_Minimize.Size = new System.Drawing.Size(167, 24);
            this.toolStripMenuItem_Minimize.Text = "視窗縮小(&M)";
            this.toolStripMenuItem_Minimize.Click += new System.EventHandler(this.toolStripMenuItem_Minimize_Click);
            // 
            // toolStripMenuItem_Visible
            // 
            this.toolStripMenuItem_Visible.Name = "toolStripMenuItem_Visible";
            this.toolStripMenuItem_Visible.Size = new System.Drawing.Size(167, 24);
            this.toolStripMenuItem_Visible.Text = "視窗隱藏(&V)";
            this.toolStripMenuItem_Visible.Click += new System.EventHandler(this.toolStripMenuItem_Visible_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(164, 6);
            // 
            // ToolStripMenuItem_MenuExit
            // 
            this.ToolStripMenuItem_MenuExit.Name = "ToolStripMenuItem_MenuExit";
            this.ToolStripMenuItem_MenuExit.Size = new System.Drawing.Size(167, 24);
            this.ToolStripMenuItem_MenuExit.Text = "結束程式(&X)";
            this.ToolStripMenuItem_MenuExit.Click += new System.EventHandler(this.ToolStripMenuItem_Exit_Click);
            // 
            // contextMenuStrip_notify
            // 
            this.contextMenuStrip_notify.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_OPEN,
            this.ToolStripMenuItem_Exit});
            this.contextMenuStrip_notify.Name = "contextMenuStrip_notify";
            this.contextMenuStrip_notify.Size = new System.Drawing.Size(123, 48);
            // 
            // ToolStripMenuItem_OPEN
            // 
            this.ToolStripMenuItem_OPEN.Name = "ToolStripMenuItem_OPEN";
            this.ToolStripMenuItem_OPEN.Size = new System.Drawing.Size(122, 22);
            this.ToolStripMenuItem_OPEN.Text = "視窗還原";
            this.ToolStripMenuItem_OPEN.Click += new System.EventHandler(this.ToolStripMenuItem_OPEN_Click);
            // 
            // ToolStripMenuItem_Exit
            // 
            this.ToolStripMenuItem_Exit.Name = "ToolStripMenuItem_Exit";
            this.ToolStripMenuItem_Exit.Size = new System.Drawing.Size(122, 22);
            this.ToolStripMenuItem_Exit.Text = "結束程式";
            this.ToolStripMenuItem_Exit.Click += new System.EventHandler(this.ToolStripMenuItem_Exit_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip_notify;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "網路設備狀態監控";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // timer_Log
            // 
            this.timer_Log.Interval = 3600000;
            this.timer_Log.Tick += new System.EventHandler(this.timer_Log_Tick);
            // 
            // Fm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1194, 884);
            this.ControlBox = false;
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1200, 900);
            this.Name = "Fm_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Fm_Main_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Fm_Main_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip_notify.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_MenuExit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_notify;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_OPEN;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Exit;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Timer timer_Log;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Minimize;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Visible;
    }
}