using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;

namespace UI
{
    class AppLogger
    {
        public static void Error(string msg)
        {
            //try
            //{
            //    StackFrame stackFrame = new StackFrame(1, true);
            //    msg = DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss") + " " + getFileName(stackFrame.GetFileName()) + " : " + stackFrame.GetFileLineNumber().ToString() + " " + getHashCode() + " " + msg;

            //    string filePath = System.Windows.Forms.Application.StartupPath + @"\" + DateTime.Now.Date.ToString("yyyyMMdd") + "_ERR.txt";

            //    using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, true))
            //    {
            //        file.WriteLine(msg);
            //    }

            //}
            //catch
            //{ }

            FileStream fs = new FileStream(@"E:\ServiceLog.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.WriteLine(msg);
            sw.Flush();
            sw.Close();
        }

        public static void Info(string msg)
        {
            try
            {
                StackFrame stackFrame = new StackFrame(1, true);
                msg = DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss") + " " + getFileName(stackFrame.GetFileName()) + " : " + stackFrame.GetFileLineNumber().ToString() + " " + getHashCode() + " " + msg;

                string filePath = System.Windows.Forms.Application.StartupPath + @"\" + DateTime.Now.Date.ToString("yyyyMMdd") + "_INFO.txt";

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, true))
                {
                    file.WriteLine(msg);
                }

            }
            catch
            { }
        }

        public static void Debug(string msg)
        {
            try
            {

                return;
                StackFrame stackFrame = new StackFrame(1, true);
                msg = DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss") + " " + getFileName(stackFrame.GetFileName()) + " : " + stackFrame.GetFileLineNumber().ToString() + " " + getHashCode() + " " + msg;

                string filePath = System.Windows.Forms.Application.StartupPath + @"\" + DateTime.Now.Date.ToString("yyyyMMdd") + "_DEBUG.txt";

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, true))
                {
                    file.WriteLine(msg);
                }

            }
            catch
            { }
        }

        private static String getHashCode()
        {
            String hashCode = "";
            int hcode = System.Threading.Thread.CurrentThread.GetHashCode();
            hcode = (hcode < 0 ? hcode * -1 : hcode);
            hashCode = "[" + hcode + "] - ";
            return hashCode;
        }

        public static string getFileName(string strFilePath)
        {
            string[] strArr;

            if (string.IsNullOrEmpty(strFilePath))
                return "";

            strArr = strFilePath.Split('\\');
            if (strArr != null)
            {
                if (strArr.Length > 0)
                    return strArr[strArr.Length - 1];
                else
                    return "";
            }
            else
                return "";
        }
    }
}
