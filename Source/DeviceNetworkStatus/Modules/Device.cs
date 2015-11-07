using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net.NetworkInformation;

namespace DeviceNetworkStatus.Modules
{
    public partial class Device : UserControl
    {
        private string _DeviceName = "";
        private string _DeviceIP = "";
        private IPStatus _Status = IPStatus.Unknown;

        private int _TimeOut = 2000;   //ping timeout時間
        private int _TTL = 64;
        private int _RETRY = 3;
        private int _ERRCount = 0;

        private bool _isSend = false;    //是否送出
        private int _waitPing = 0;

        public Device()
        {
            InitializeComponent();
        }

        #region ---自訂屬性

        [Category("自訂屬性"), Description("設備名")]
        public string DeviceName
        {
            get 
            {
                return _DeviceName;
            }
            set
            {
                _DeviceName = value;
                label_DeviceName.Text = value;
            }
        }

        [Category("自訂屬性"), Description("設備IP")]
        public string DeviceIP
        {
            get
            {
                return _DeviceIP;
            }
            set
            {
                _DeviceIP = value;
                label_DeviceIP.Text = value;
            }
        }

        [Category("自訂屬性"), Description("設備網路狀態")]
        public IPStatus Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;  
            }
        }

        [Category("自訂屬性"), Description("Ping的TimeOut時間")]
        public int TimeOut
        {
            get
            {
                return _TimeOut;
            }
            set
            {
                _TimeOut = value;
            }
        }

        [Category("自訂屬性"), Description("Ping的TTL數")]
        public int TimeToLive
        {
            get
            {
                return _TTL;
            }
            set
            {
                _TTL = value;
            }
        }

        [Category("自訂屬性"), Description("Ping的重試次數")]
        public int RETRY
        {
            get
            {
                return _RETRY;
            }
            set
            {
                _RETRY = value;
            }
        }

        [Category("自訂屬性"), Description("TIMER啟動與否")]
        public bool TimerStart
        {
            get
            {
                return timer1.Enabled;
            }
            set
            {
                timer1.Enabled = value;
                timer2.Enabled = value;

                if (value == true)
                {
                    //pictureBox_Status.Image = global::DeviceNetworkStatus.Properties.Resources.GREEN;

                    pingSender.PingCompleted += new PingCompletedEventHandler(Ping_completed);
                }
                else
                {
                    //pictureBox_Status.Image = global::DeviceNetworkStatus.Properties.Resources.RED;

                    pingSender.PingCompleted -= new PingCompletedEventHandler(Ping_completed);
                }
            }
        }

        #endregion

        #region ---Function

        private void Alarm()
        {
            if (_Status == IPStatus.Success)
                this.BackColor = Color.Lime;
            else
            {
                if (this.BackColor != Color.Red)
                    this.BackColor = Color.Red;
                else
                    this.BackColor = SystemColors.Control;
            }
        }

        private void Write_Log()
        {
            Class.LogHandle.Write_Log(_DeviceName, _DeviceIP);
        }
        
        Ping pingSender = new Ping();
        PingOptions po = new PingOptions();

        private void DevicePing()
        {
            po.Ttl = _TTL;
            po.DontFragment = true;
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            byte[] data = enc.GetBytes("123test");

            pingSender.SendAsync(_DeviceIP, _TimeOut, data, po);
            _isSend = true;
        }

        #endregion

        #region ---Event

        //show alarm
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            Alarm();

            //有時PING卻沒有回completed (如:無聊換IP時會發生)
            //預設10次就重PING
            if (_isSend)
            {
                _waitPing++;
                if (_waitPing >= 10)
                {
                    //pingSender.SendAsyncCancel();
                    //pingSender.PingCompleted -= new PingCompletedEventHandler(Ping_completed);
                    //pingSender.Dispose();
                    (pingSender as IDisposable).Dispose();

                    pingSender = new Ping();
                    pingSender.PingCompleted += new PingCompletedEventHandler(Ping_completed);
                    _isSend = false;
                    _waitPing = 0;
                    timer2.Enabled = true;
                }
            }

            timer1.Enabled = true;
        }

        //ping
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_DeviceIP))
                return;

            timer2.Enabled = false;

            DevicePing();
        }

        public void Ping_completed(object s, PingCompletedEventArgs e)
        {
            if (e.Reply.Status == IPStatus.Success)
            {
                _ERRCount = 0;
                _Status = e.Reply.Status;
            }
            else
            {
                _ERRCount++;
                if (_ERRCount == _RETRY)    //失敗一定次數
                {
                    Write_Log();    //記錄錯誤訊息
                    _Status = e.Reply.Status;   //變更狀態 for 閃爍
                }
            }

            _isSend = false;
            _waitPing = 0;
            timer2.Enabled = true;
        }

        #endregion
    }
}
