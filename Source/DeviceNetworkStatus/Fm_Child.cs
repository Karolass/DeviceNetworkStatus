using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net.NetworkInformation;
//using System.Diagnostics;
using System.Threading;
using DeviceNetworkStatus.Class;
using DeviceNetworkStatus.Modules;

namespace DeviceNetworkStatus
{
    public partial class Fm_Child : Form
    {
        private int _TimeOut = 2000, _TTL = 64, _RETRY = 3;
        private int _ROW_COUNT = 4;

        private Dictionary<string, TabPage> TabGROUPs = new Dictionary<string, TabPage>();
        private Dictionary<string, int> TabGROUPs_SHOW_COUNT = new Dictionary<string, int>();
        private List<Device> Devices = new List<Device>();

        public Fm_Child()
        {
            InitializeComponent();
        }

        #region -----Form Event

        private void Form2_Load(object sender, EventArgs e)
        {
            INI_Config();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            //如果真的是關閉程式 釋放usercontrol資源
            foreach (Device dev in Devices)
            {
                dev.TimerStart = false;
                dev.Dispose();
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
                sw.WriteLine("DEVICE_NAME=ptt");
                sw.WriteLine("DEVICE_IP=ptt.cc");
                sw.WriteLine("ENABLE=Y");
                sw.WriteLine();
                sw.Close();
            }
            else
            {
                using (TINI oTINI = new TINI(fName))
                {
                    int TimeOut, TTL, RETRY;
                    int TOTAL_GROUP_CNT, TOTAL_DEVICE_CNT, ROW_COUNT;

                    if (int.TryParse(oTINI.getKeyValue("DEVICE_CONFIG", "TIMEOUT"), out TimeOut))
                        _TimeOut = TimeOut;
                    else
                        MessageBox.Show("[DEVICE_CONFIG] TIMEOUT未設定", "提示");
                    if (int.TryParse(oTINI.getKeyValue("DEVICE_CONFIG", "TTL"), out TTL))
                        _TTL = TTL;
                    else
                        MessageBox.Show("[DEVICE_CONFIG] TTL未設定", "提示");
                    if (int.TryParse(oTINI.getKeyValue("DEVICE_CONFIG", "RETRY"), out RETRY))
                        _RETRY = RETRY;
                    else
                        MessageBox.Show("[DEVICE_CONFIG] RETRY未設定", "提示");
                    if (int.TryParse(oTINI.getKeyValue("DEVICE_CONFIG", "ROW_COUNT"), out ROW_COUNT))
                        _ROW_COUNT = ROW_COUNT;
                    else
                        MessageBox.Show("[DEVICE_CONFIG] ROW_COUNT未設定", "提示");

                    //重要!!!
                    if (!int.TryParse(oTINI.getKeyValue("TOTAL_GROUP_CNT", "TOTAL"), out TOTAL_GROUP_CNT))
                    {
                        MessageBox.Show("[TOTAL_GROUP_CNT] TOTAL未設定", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    //重要!!!
                    if (!int.TryParse(oTINI.getKeyValue("TOTAL_DEVICE_CNT", "TOTAL"), out TOTAL_DEVICE_CNT))
                    {
                        MessageBox.Show("[TOTAL_DEVICE_CNT] TOTAL未設定", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string GROUP_NO, GROUP_NAME;
                    //開始建Tab GROUP
                    for (int i = 1; i <= TOTAL_GROUP_CNT; i++)
                    {
                        GROUP_NO = string.Format("GROUP_{0:000}", i);

                        GROUP_NAME = oTINI.getKeyValue(GROUP_NO, "GROUP_NAME");
                        //迴圈設定過大取無值就break
                        if (string.IsNullOrEmpty(GROUP_NAME))
                            break;

                        TabPage page = new TabPage(GROUP_NAME);
                        page.AutoScroll = true;
                        page.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
                        this.tabControl_GROUP.TabPages.Add(page);
                        TabGROUPs.Add(GROUP_NAME, page);
                        TabGROUPs_SHOW_COUNT.Add(GROUP_NAME, 0);
                    }

                    string DEVICE_GROUP, DEVICE_NO, DEVICE_NAME, DEVICE_IP;
                    bool DEVICE_ENABLE;
                    //開始建USER CONTROL
                    int SHOW_COUNT = 0; //計數建到第幾個
                    for (int i = 1; i <= TOTAL_DEVICE_CNT; i++)
                    {
                        DEVICE_NO = string.Format("DEVICE_{0:000}", i);

                        DEVICE_GROUP = oTINI.getKeyValue(DEVICE_NO, "DEVICE_GROUP");
                        DEVICE_NAME = oTINI.getKeyValue(DEVICE_NO, "DEVICE_NAME");
                        DEVICE_IP = oTINI.getKeyValue(DEVICE_NO, "DEVICE_IP");
                        //迴圈設定過大取無值就break
                        if (string.IsNullOrEmpty(DEVICE_NAME) && string.IsNullOrEmpty(DEVICE_GROUP))
                            break;

                        DEVICE_ENABLE = (oTINI.getKeyValue(DEVICE_NO, "ENABLE") == "Y") ? true : false;

                        //int x = SHOW_COUNT % _ROW_COUNT;
                        //int y = SHOW_COUNT / _ROW_COUNT;     
                        int x = TabGROUPs_SHOW_COUNT[DEVICE_GROUP] % _ROW_COUNT;
                        int y = TabGROUPs_SHOW_COUNT[DEVICE_GROUP] / _ROW_COUNT;   
                        int X_OFFSET = (x == 0) ? 0 : 12 * x;
                        int Y_OFFSET = (y == 0) ? 0 : 12 * y;
                        
                        if (DEVICE_ENABLE == true)
                        {
                            Device dev = new Device();
                            dev.Location = new System.Drawing.Point(50 + (dev.Width * x) + X_OFFSET, 96 + (dev.Height * (y - 1)) + Y_OFFSET);
                            dev.DeviceName = DEVICE_NAME;
                            dev.DeviceIP = DEVICE_IP;
                            dev.TimerStart = DEVICE_ENABLE;
                            dev.TimeOut = _TimeOut;
                            dev.TimeToLive = _TTL;
                            dev.RETRY = _RETRY;

                            //this.Controls.Add(dev);
                            //this.tabControl_GROUP.TabPages[0].Controls.Add(dev);
                            (TabGROUPs[DEVICE_GROUP] as TabPage).Controls.Add(dev);
                            Devices.Add(dev);
                            //SHOW_COUNT++;
                            TabGROUPs_SHOW_COUNT[DEVICE_GROUP]++;
                        }
                        
                    }
                }
            }
        }

        #endregion
        
        #region -----Event


        #endregion
        
    }
}
