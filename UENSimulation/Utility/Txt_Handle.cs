using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace UENSimulation.Utility
{
    class Txt_Handle
    {
        //[DllImport("kernel32.dll")]
        //public static extern IntPtr _lopen(string lpPathName, int iReadWrite);

        //[DllImport("kernel32.dll")]
        //public static extern bool CloseHandle(IntPtr hObject);

        public string[] dataRead(string path)
        {
            StreamReader sr = new StreamReader(path);
            string sLine = sr.ReadLine();
            string[] dataReturn = sLine.Split(' ');
            sr.Close();
            return dataReturn;
        }

        public void dataWrite(string path, string[] dataWrite)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入
            for (int i = 0; i < dataWrite.Length; i++)
            {
                if (i != dataWrite.Length - 1)
                {
                    sw.Write(dataWrite[i] + " ");
                }
                else
                {
                    sw.Write(dataWrite[i]);
                }
            }
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
        }

        public bool IsFileInUse(string fileName)
        {
            bool inUse = true;

            FileStream fs = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read,

                FileShare.None);

                inUse = false;
            }
            catch
            {

            }
            finally
            {
                if (fs != null)

                    fs.Close();
            }
            return inUse;//true表示正在使用,false没有使用  
        }
    }
}
