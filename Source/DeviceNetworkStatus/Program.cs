using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
//using System.Diagnostics;

namespace DeviceNetworkStatus
{
    static class Program
    {
        private static System.Threading.Mutex _mutex;    //宣告全域變數mutex 避免GC回收
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //確認程式是否已執行
            //if (Class.ProcessCheck.IsProcessing())   //最多只能開一隻程式
            if (!Class.ProcessCheck.IsProcessing(Class.ProcessCheck.ProcessCheckRange.Global, out _mutex))
                Application.Exit();
            else
            {
                _mutex.WaitOne();    //hold住mutex 不讓GC釋放

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Fm_Main());

                _mutex.ReleaseMutex();
            }
        }
    }
}
