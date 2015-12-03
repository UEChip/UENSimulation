using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UENSimulation.Utility
{
    //com口（串口）管理类
    class COMManage
    {
        //获取所有端口名
        public string[] GetPortNames()
        {
            return System.IO.Ports.SerialPort.GetPortNames();
        }

        //打开端口
        public bool OpenPort(SerialPort port)
        {
            bool bflag = false;
            try
            {
                port.Open();
                port.DiscardInBuffer();
                port.DiscardOutBuffer();
                bflag = true;
            }
            catch (IOException ex)
            {
                bflag = false;
            }
            return bflag;
        }

        //读取数据

        //下发数据
    }
}
