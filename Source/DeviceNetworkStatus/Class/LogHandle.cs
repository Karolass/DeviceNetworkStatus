using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DeviceNetworkStatus.Class
{
    public class LogHandle
    {
        //寫log
        public static void Write_Log(string DeviceName, string DeviceIP)
        {
            try
            {
                System.IO.FileStream fs;
                string fDate = DateTime.Now.ToString("yyyyMMdd");
                string fDir = System.IO.Path.Combine(Application.StartupPath, "LOG\\");
                string fName = System.IO.Path.Combine(fDir, fDate + ".txt");//(fDir + "\\" + fDate + ".txt");

                if (!System.IO.Directory.Exists(fDir))
                    System.IO.Directory.CreateDirectory(fDir);
                if (!System.IO.File.Exists(fName))
                    fs = new System.IO.FileStream(fName, System.IO.FileMode.Create);
                else
                    fs = new System.IO.FileStream(fName, System.IO.FileMode.Append);

                System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, System.Text.Encoding.UTF8);
                sw = new System.IO.StreamWriter(fs);
                sw.WriteLine("[" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "] " +
                             string.Format("設備名:{0}, IP:{1}, 斷線", DeviceName, DeviceIP));
                sw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //刪除太久的LOG (預設90天)
        public static void Delete_Old_Log(int LOG_KEEP_DAYS)
        {
            try
            {
                string fDir = System.IO.Path.Combine(Application.StartupPath, "LOG\\");

                if (System.IO.Directory.Exists(fDir) && LOG_KEEP_DAYS != 0)
                {
                    foreach (string file in System.IO.Directory.GetFiles(fDir))
                    {
                        DateTime d1 = DateTime.Now.AddDays(-LOG_KEEP_DAYS); //時間 n天前
                        if (System.IO.File.GetCreationTime(file) <= d1)
                            System.IO.File.Delete(file);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
