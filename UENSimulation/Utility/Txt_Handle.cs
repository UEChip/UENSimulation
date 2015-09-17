using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UENSimulation.Utility
{
    class Txt_Handle
    {
        public string[] dataRead(string path)
        {
            StreamReader sr = new StreamReader(path);
            string sLine = sr.ReadLine();
            string[] dataReturn = sLine.Split(' ');
            sr.Close();
            return dataReturn;
        }

        public void dataWrite(string path, int[] dataInt)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入
            for (int i = 0; i < dataInt.Length; i++)
            {
                sw.Write(dataInt[i].ToString());
            }
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
        }
    }
}
