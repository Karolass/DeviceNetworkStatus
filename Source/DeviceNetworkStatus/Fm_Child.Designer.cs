namespace DeviceNetworkStatus
{
    partial class Fm_Child
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl_GROUP = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // tabControl_GROUP
            // 
            this.tabControl_GROUP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_GROUP.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Bold);
            this.tabControl_GROUP.Location = new System.Drawing.Point(0, 0);
            this.tabControl_GROUP.Name = "tabControl_GROUP";
            this.tabControl_GROUP.SelectedIndex = 0;
            this.tabControl_GROUP.Size = new System.Drawing.Size(386, 362);
            this.tabControl_GROUP.TabIndex = 0;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(386, 362);
            this.Controls.Add(this.tabControl_GROUP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form2";
            this.Text = "網路設備狀態監控";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl_GROUP;



    }
}

