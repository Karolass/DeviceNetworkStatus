using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DeviceNetworkStatus.Class;

namespace DeviceNetworkStatus
{
    public partial class Fm_Main : Form
    {
        private int _CYCLE_TIME = 3600;
        private int _LOG_KEEP_DAYS = 90;

        public Fm_Main()
        {
            InitializeComponent();
        }

        #region -----Form Event

        private void Fm_Main_Load(object sender, EventArgs e)
        {
            INI_Config();
            timer_Log.Interval = _CYCLE_TIME * 1000;
            timer_Log.Enabled = true;

            Fm_Child fm = new Fm_Child();
            fm.MdiParent = this;
            fm.FormBorderStyle = FormBorderStyle.None;
            fm.Dock = DockStyle.Fill;
            fm.Show();
        }

        private void Fm_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            //按X或alt+F4強關
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
                notifyIcon1.ShowBalloonTip(2000, "網路設備監控程式", "程式仍在執行中!", ToolTipIcon.Info);
                return;
            }
        }

        #endregion

        #region -----Function

        //讀INI資料
        private void INI_Config()
        {
            System.IO.FileStream fs;
            string fDir = Application.StartupPath;
            string fName = System.IO.Path.Combine(fDir, "config.ini");

            //if (!System.IO.Directory.Exists(fDir))
            //    System.IO.Directory.CreateDirectory(fDir);
            if (!System.IO.File.Exists(fName))
            {
                fs = new System.IO.FileStream(fName, System.IO.FileMode.Create);

                System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, System.Text.Encoding.UTF8);
                sw = new System.IO.StreamWriter(fs);
                //sw.WriteLine("[SETUP]" + Environment.NewLine +
                //             "TimeOut=2000" + Environment.NewLine +
                //             "TTL=64" + Environment.NewLine +
                //             "AUTOSTART=Y");
                sw.WriteLine("[LOG_CONFIG]");
                sw.WriteLine("CYCLE_TIME=3600");
                sw.WriteLine("LOG_KEEP_DAYS=90");
                sw.WriteLine();
                sw.WriteLine("[DEVICE_CONFIG]");
                sw.WriteLine("TIMEOUT=2000");
                sw.WriteLine("TTL=64");
                sw.WriteLine("RETRY=3");
                sw.WriteLine("ROW_COUNT=4");
                sw.WriteLine();
                sw.WriteLine("[TOTAL_DEVICE_CNT]");
                sw.WriteLine("TOTAL=1");
                sw.WriteLine();
                sw.WriteLine("[DEVICE_001]");
                sw.WriteLine("DEVICE_NAME=ADAM-6024-H2O2-F1");
                sw.WriteLine("DEVICE_IP=192.168.100.191");
                sw.WriteLine("ENABLE=Y");
                sw.WriteLine();
                sw.Close();
            }
            else
            {
                using (TINI oTINI = new TINI(fName))
                {
                    int CYCLE_TIME, LOG_KEEP_DAYS;

                    if (int.TryParse(oTINI.getKeyValue("LOG_CONFIG", "CYCLE_TIME"), out CYCLE_TIME))
                        _CYCLE_TIME = CYCLE_TIME;
                    else
                        MessageBox.Show("[LOG_CONFIG] CYCLE_TIME未設定", "提示");
                    if (int.TryParse(oTINI.getKeyValue("LOG_CONFIG", "LOG_KEEP_DAYS"), out LOG_KEEP_DAYS))
                        _LOG_KEEP_DAYS = LOG_KEEP_DAYS;
                    else
                        MessageBox.Show("[LOG_CONFIG] LOG_KEEP_DAYS未設定", "提示");
                }
            }
        }

        #endregion

        #region -----Event

        //刪log用
        private void timer_Log_Tick(object sender, EventArgs e)
        {
            timer_Log.Enabled = false;

            LogHandle.Delete_Old_Log(_LOG_KEEP_DAYS);

            timer_Log.Enabled = true;
        }

        //雙擊叫出
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        //視窗縮小
        private void toolStripMenuItem_Minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        //視窗隱藏
        private void toolStripMenuItem_Visible_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
            notifyIcon1.ShowBalloonTip(2000, "網路設備監控程式", "程式仍在執行中!", ToolTipIcon.Info);
        }
        //視窗還原
        private void ToolStripMenuItem_OPEN_Click(object sender, EventArgs e)
        {
            notifyIcon1_MouseDoubleClick(null, null);
        }
        //結束程式
        private void ToolStripMenuItem_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
            //Application.ExitThread();
        }

        #endregion



    }
}
