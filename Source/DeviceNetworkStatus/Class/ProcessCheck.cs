using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace DeviceNetworkStatus.Class
{
    public class ProcessCheck
    {
        public static bool IsProcessing(ProcessCheckRange type, out Mutex mutex)
        {
            //string moduleName = Process.GetCurrentProcess().MainModule.ModuleName;    //process name (會因改檔名而跟著變)
            string moduleName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            string mutexRange = string.Empty;
            bool isCreated = false;

            switch (type)
            {
                case ProcessCheckRange.Global:
                default:
                    mutexRange = "Global\\";    //everyone 只能存在一個process
                    break;
                case ProcessCheckRange.Local:
                    mutexRange = "Local\\";     //只限定同使用者 只能存在一個process
                    break;
            }

            mutex = new System.Threading.Mutex(true, mutexRange + moduleName, out isCreated);

            return isCreated;
        }

        public static bool IsProcessing()
        {
            string moduleName = Process.GetCurrentProcess().MainModule.ModuleName;  //process name (會因改檔名而跟著變)

            Process[] pProcess = Process.GetProcessesByName(moduleName.Substring(0, moduleName.IndexOf('.')));

            if (pProcess.Length >= 2)   //最多只能開一隻程式
                return true;

            return false;
        } 

        public enum ProcessCheckRange
        {
            Global,
            Local
        }

    }
}
