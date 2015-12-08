using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.IO.Ports;
using System.Threading;


namespace UENSimulation
{
    /// <summary>
    /// Port.xaml 的交互逻辑
    /// </summary>
    public partial class Port : Window
    {
        public Port()
        {
            InitializeComponent();
            string[] portName = System.IO.Ports.SerialPort.GetPortNames();
            for (int i = 0; i < portName.Length; i++)
            {
                COMName.Items.Add(portName[i]);
            }
            stop.IsEnabled = false;
            open.IsEnabled = true;
        }

        public SerialPort port;
        private void Button_Click(object sender, RoutedEventArgs e)//按钮打开事件
        {
            open.IsEnabled = false;
            stop.IsEnabled = true;
            port = new SerialPort();
            port.BaudRate = 115200;
            port.PortName = COMName.Text;
            port.DataBits = 8;
            try
            {
                port.Open();
                port.DiscardInBuffer();
                port.DiscardOutBuffer();
                MessageBox.Show("串口打开成功", "系统提示");
            }
            catch (IOException ex)
            {
                MessageBox.Show("串口打开失败" + ex, "系统提示");
                return;
            }
            _keepReading = true;
            _readThread = new Thread(ReadPort);
            _readThread.Start();
        }

        private Thread _readThread;
        private bool _keepReading;
        public int first = 0;
        public int last = 0;
        private void ReadPort()//接收数据
        {

            while (_keepReading)
            {
                if (port.IsOpen)
                {
                    Thread.Sleep(2000);
                    byte[] readBuffer = new byte[port.ReadBufferSize];
                    try
                    {

                        int count = port.Read(readBuffer, 0, port.ReadBufferSize);
                        String SerialIn = System.Text.Encoding.ASCII.GetString(readBuffer, 0, count);
                        if (count != 0)
                            //byteToHexStr(readBuffer);  
                            //ThreadFunction(byteToHexStr(readBuffer));
                            ThreadFunction(SerialIn);


                    }
                    catch { }
                    Random r = new Random();
                    last = r.Next(0, 3);
                    first = r.Next(0, 3);

                    if (first > 3) first = 0;
                    if (last > 3) last = 0;
                }
                else
                {
                    TimeSpan waitTime = new TimeSpan(0, 0, 0, 0, 50);
                    Thread.Sleep(waitTime);
                }
            }

        }
        private string sendStr;
        private void SendPort()//发送指令，解析显示数据
        {
            try
            {
                sendStr = sendStr.Replace("  ", " ");
                string[] arr = sendStr.Split(' ');
                sendStr = "$" + first + last + "#";
                byte[] sendByte = Encoding.ASCII.GetBytes(sendStr);
                port.Write(sendByte, 0, sendByte.Length);
                double[] dataUE = new double[arr.Length];
                for (int i = 0; i < 9; i++)
                {
                    dataUE[i] = double.Parse(arr[i]);
                    dataUE[i] = dataUE[i] / 100;
                    arr[i] = dataUE[i].ToString();
                }
                t1.Text = arr[0] + "%";
                t2.Text = arr[1] + "%";
                t3.Text = arr[2] + "%";
                t4.Text = arr[3] + "%";
                t5.Text = arr[4] + "%";
                t6.Text = arr[5] + "%";
                t7.Text = arr[6] + "%";
                t8.Text = arr[7] + "%";
                t9.Text = arr[8] + "%";
            }
            catch { }
        }
        private void ThreadFunction(string sRecv)//将接收的字符串显示并进行下一步操作
        {
            //处理数据。。。。额
            Dispatcher.Invoke((Action)delegate
            {

                sendStr = sRecv;
                boxData.AppendText(sendStr);
                SendPort();
            });
        }
        public static string byteToHexStr(byte[] bytes)//字节数组转16进制字符串 
        {
            string returnStr = ""; if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            _keepReading = false;
            port.Close();
            stop.IsEnabled = false;
            open.IsEnabled = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (port != null && port.IsOpen == true)
            {
                _keepReading = false;
                port.Close();
            }
        }
    }
}
