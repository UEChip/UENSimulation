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
using Visifire.Charts;
using System.Windows.Media.Animation;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;
using System.Data.OleDb;
using System.Data;
using UENSimulation.Utility;
using System.IO.Ports;
using System.Threading;

namespace UENSimulation
{
    /// <summary>
    /// Gateway.xaml 的交互逻辑
    /// </summary>
    public partial class Gateway : Window
    {
        private List<double> electric = new List<double>() { };
        private List<double> heat = new List<double>() { };
        private List<double> water = new List<double>() { };

        private List<DateTime> modelTime = new List<DateTime>()
            { 
               new DateTime(1,1,1,0,0,0),
               new DateTime(1,1,1,1,0,0),
               new DateTime(1,1,1,2,0,0),
               new DateTime(1,1,1,3,0,0),
               new DateTime(1,1,1,4,0,0),
               new DateTime(1,1,1,5,0,0),
               new DateTime(1,1,1,6,0,0),
               new DateTime(1,1,1,7,0,0),
               new DateTime(1,1,1,8,0,0),
               new DateTime(1,1,1,9,0,0),
               new DateTime(1,1,1,10,0,0),
               new DateTime(1,1,1,11,0,0),
               new DateTime(1,1,1,12,0,0),
               new DateTime(1,1,1,13,0,0),
               new DateTime(1,1,1,14,0,0),
               new DateTime(1,1,1,15,0,0),
               new DateTime(1,1,1,16,0,0),
               new DateTime(1,1,1,17,0,0),
               new DateTime(1,1,1,18,0,0),
               new DateTime(1,1,1,19,0,0),
               new DateTime(1,1,1,20,0,0),
               new DateTime(1,1,1,21,0,0),
               new DateTime(1,1,1,22,0,0),
               new DateTime(1,1,1,23,0,0),
               
            
            };


        #region 折线图
        public void CreateChartSpline(string name, List<DateTime> lsTime, List<double> a, List<double> b, List<double> c)
        {

            Chart chart = new Chart();
            chart.Width = 580;
            chart.Height = 230;
            chart.Margin = new Thickness(0, 0, 10, 0);
            //是否启用打印和保持图片
            chart.ToolBarEnabled = false;

            chart.ScrollingEnabled = false;//是否启用或禁用滚动
            chart.View3D = false;//3D效果显示

            //创建一个标题的对象
            Title title = new Title();

            //设置标题的名称
            title.Text = name;
            title.Padding = new Thickness(0, 10, 5, 0);
            chart.Titles.Add(title);

            //初始化一个新的Axis
            Axis xaxis = new Axis();
            //设置Axis的属性
            //图表的X轴坐标按什么来分类，如时分秒
            xaxis.IntervalType = IntervalTypes.Hours;
            //图表的X轴坐标间隔如2,3,20等，单位为xAxis.IntervalType设置的时分秒。
            xaxis.Interval = 2;
            //设置X轴的时间显示格式为7-10 11：20           
            xaxis.ValueFormatString = "HH点";
            //给图标添加Axis            
            chart.AxesX.Add(xaxis);

            Axis yAxis = new Axis();
            //设置图标中Y轴的最小值永远为0           
            yAxis.AxisMinimum = 0;
            //设置图表中Y轴的后缀          
            yAxis.Suffix = "kW";
            chart.AxesY.Add(yAxis);


            // 创建一个新的数据线。               
            DataSeries dataEle = new DataSeries();
            // 设置数据线的格式。               
            dataEle.LegendText = "电";

            dataEle.RenderAs = RenderAs.Spline;//折线图

            dataEle.XValueType = ChartValueTypes.DateTime;
            dataEle.Color = new SolidColorBrush(Colors.Blue);
            // 设置数据点              
            DataPoint dataPoint;
            for (int i = 0; i < lsTime.Count; i++)
            {
                // 创建一个数据点的实例。                   
                dataPoint = new DataPoint();
                // 设置X轴点                    
                dataPoint.XValue = lsTime[i];
                //设置Y轴点                   
                dataPoint.YValue = a[i];
                dataPoint.MarkerSize = 8;
                //dataPoint.Tag = tableName.Split('(')[0];
                //设置数据点颜色                  
                //dataPoint.Color = new SolidColorBrush(Colors.Green);                   
                dataPoint.MouseLeftButtonDown += new MouseButtonEventHandler(dataPoint_MouseLeftButtonDown);
                //添加数据点                   
                dataEle.DataPoints.Add(dataPoint);
            }

            // 添加数据线到数据序列。                
            chart.Series.Add(dataEle);


            // 创建一个新的数据线。               
            DataSeries dataSeriesHeat = new DataSeries();
            // 设置数据线的格式。         

            dataSeriesHeat.LegendText = "热";

            dataSeriesHeat.RenderAs = RenderAs.Spline;//折线图

            dataSeriesHeat.XValueType = ChartValueTypes.DateTime;
            dataSeriesHeat.Color = new SolidColorBrush(Colors.Red);
            // 设置数据点              

            DataPoint dataPoint2;
            for (int i = 0; i < lsTime.Count; i++)
            {
                // 创建一个数据点的实例。                   
                dataPoint2 = new DataPoint();
                // 设置X轴点                    
                dataPoint2.XValue = lsTime[i];
                //设置Y轴点                   
                dataPoint2.YValue = b[i];
                dataPoint2.MarkerSize = 8;
                //dataPoint2.Tag = tableName.Split('(')[0];
                //设置数据点颜色                  
                // dataPoint.Color = new SolidColorBrush(Colors.LightGray);                   
                dataPoint2.MouseLeftButtonDown += new MouseButtonEventHandler(dataPoint_MouseLeftButtonDown);
                //添加数据点                   
                dataSeriesHeat.DataPoints.Add(dataPoint2);
            }
            // 添加数据线到数据序列。                
            chart.Series.Add(dataSeriesHeat);



            // 创建一个新的数据线。               
            DataSeries dataSeriesWater = new DataSeries();
            // 设置数据线的格式。         

            dataSeriesWater.LegendText = "生活热水";

            dataSeriesWater.RenderAs = RenderAs.Spline;//折线图

            dataSeriesWater.XValueType = ChartValueTypes.DateTime;
            dataSeriesWater.Color = new SolidColorBrush(Colors.Yellow);
            // 设置数据点              

            DataPoint dataPoint3;
            for (int i = 0; i < lsTime.Count; i++)
            {
                // 创建一个数据点的实例。                   
                dataPoint3 = new DataPoint();
                // 设置X轴点                    
                dataPoint3.XValue = lsTime[i];
                //设置Y轴点                   
                dataPoint3.YValue = c[i];
                dataPoint3.MarkerSize = 8;
                //dataPoint2.Tag = tableName.Split('(')[0];
                //设置数据点颜色                  
                // dataPoint.Color = new SolidColorBrush(Colors.LightGray);                   
                dataPoint3.MouseLeftButtonDown += new MouseButtonEventHandler(dataPoint_MouseLeftButtonDown);
                //添加数据点                   
                dataSeriesWater.DataPoints.Add(dataPoint3);
            }
            // 添加数据线到数据序列。                
            chart.Series.Add(dataSeriesWater);

            //将生产的图表增加到Grid，然后通过Grid添加到上层Grid.           
            Grid gr = new Grid();
            gr.Children.Add(chart);

            model.Children.Add(gr);
        }
        #endregion

        #region 点击事件
        //点击事件
        void dataPoint_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //DataPoint dp = sender as DataPoint;
            //MessageBox.Show(dp.YValue.ToString());
        }
        #endregion
        private string city;
        private void GetCity()
        {
            string[] dataRead = new string[29];
            bool file = File.Exists(@"..\..\Local Storage\familyInformation.txt");
            if (file == true)
            {
                StreamReader sr = new StreamReader(@"..\..\Local Storage\familyInformation.txt");
                int i = 0;
                for (i = 0; i < 29; i++)
                {
                    dataRead[i] = sr.ReadLine();
                }
                sr.Close();
                city = dataRead[28];
            }
        }
        private DataTable GetXlsx()
        {
            string cityNick;
            switch (city)
            {
                case "北京": cityNick = "BEIJ";
                    break;
                case "上海": cityNick = "SHANGH";
                    break;
                case "广州": cityNick = "GUANGZ";
                    break;
                case "沈阳": cityNick = "SHENY";
                    break;
                case "武汉": cityNick = "WUH";
                    break;
                case "重庆": cityNick = "CHONGQ";
                    break;
                default: cityNick = "BEIJ";//缺参默认北京
                    break;
            }
            string filePath = @"..\..\Local Storage\database\" + cityNick + ".xlsx";
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=False;IMEX=1'"; //固定句不更改
            OleDbConnection OleConn = new OleDbConnection(strConn);
            OleConn.Open();
            String sql = "select * from [Sheet1$]";
            OleDbCommand select = new OleDbCommand(sql, OleConn);
            OleDbDataAdapter objAdapter1 = new OleDbDataAdapter();
            objAdapter1.SelectCommand = select;
            DataSet OleDsExcle = new DataSet();
            objAdapter1.Fill(OleDsExcle, "Sheet1");
            OleConn.Close();

            return OleDsExcle.Tables["Sheet1"];
        }
        private void GetData(string season)
        {
            DataTable dt;
            dt = GetXlsx();
            if (season == "winter")
            {
                electric.Add(Convert.ToDouble(dt.Rows[23][4]));
                heat.Add(Convert.ToDouble(dt.Rows[23][2]));
                for (int h = 1; h < 24; h++)
                {
                    electric.Add(Convert.ToDouble(dt.Rows[h - 1][4]));
                    heat.Add(Convert.ToDouble(dt.Rows[h - 1][2]));
                }
            }
            if (season == "spring")
            {
                electric.Add(Convert.ToDouble(dt.Rows[23][12]));
                heat.Add(0);
                for (int h = 1; h < 24; h++)
                {
                    electric.Add(Convert.ToDouble(dt.Rows[h - 1][12]));
                    heat.Add(0);
                }
            }
            if (season == "autumn")
            {
                heat.Add(0);
                electric.Add(Convert.ToDouble(dt.Rows[23][17]));
                for (int h = 1; h < 24; h++)
                {
                    electric.Add(Convert.ToDouble(dt.Rows[h - 1][4]));
                    heat.Add(0);
                }
            }
            if (season == "summer")
            {
                heat.Add(0);
                electric.Add(Convert.ToDouble(dt.Rows[23][9]));

                for (int h = 1; h < 24; h++)
                {
                    electric.Add(Convert.ToDouble(dt.Rows[h - 1][9]));
                    heat.Add(0);
                }
            }
        }

        private DataTable GetXlsxPath(string filePath)
        {
            //string filePath = @"..\..\Local Storage\database\WATER.xlsx";
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=False;IMEX=1'";
            OleDbConnection OleConn = new OleDbConnection(strConn);
            OleConn.Open();
            String sql = "select * from [Sheet1$]";
            OleDbCommand select = new OleDbCommand(sql, OleConn);
            OleDbDataAdapter objAdapter1 = new OleDbDataAdapter();
            objAdapter1.SelectCommand = select;
            DataSet OleDsExcle = new DataSet();
            objAdapter1.Fill(OleDsExcle, "Sheet1");
            OleConn.Close();
            return OleDsExcle.Tables["Sheet1"];
        }
        private void GetWaterData()
        {
            DataTable dataTable;
            dataTable = GetXlsxPath(@"..\..\Local Storage\database\WATER.xlsx");
            water.Add(Convert.ToDouble(dataTable.Rows[23][1]));
            for (int h = 1; h < 24; h++)
            {
                water.Add(Convert.ToDouble(dataTable.Rows[h - 1][1]));
            }
        }



        public Gateway()
        {
            InitializeComponent();

            string[] portName = cmm.GetPortNames();
            for (int i = 0; i < portName.Length; i++)
            {
                COMName.Items.Add(portName[i]);
            }
            stop.IsEnabled = false;
            open.IsEnabled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            model.Children.Clear();
            electric.Clear();
            heat.Clear();
            water.Clear();
            GetCity();
            GetData("summer");
            GetWaterData();
            string nameCS = city + "用能负荷折线图";
            CreateChartSpline(nameCS, modelTime, electric, heat, water);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            model.Children.Clear();
            electric.Clear();
            heat.Clear();
            water.Clear();
            GetCity();
            GetXlsx();
            GetData("winter");
            GetWaterData();
            string nameCS = city + "用能负荷折线图";
            CreateChartSpline(nameCS, modelTime, electric, heat, water);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            model.Children.Clear();
            electric.Clear();
            heat.Clear();
            water.Clear();
            GetCity();
            GetXlsx();
            GetData("spring");
            GetWaterData();
            string nameCS = city + "用能负荷折线图";
            CreateChartSpline(nameCS, modelTime, electric, heat, water);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            model.Children.Clear();
            electric.Clear();
            heat.Clear();
            water.Clear();
            GetCity();
            GetXlsx();
            GetData("autumn");
            GetWaterData();
            string nameCS = city + "用能负荷折线图";
            CreateChartSpline(nameCS, modelTime, electric, heat, water);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btn_Winter.Focus();
            Button_Click_1(sender, e);
        }

        #region com口获取感应结果和模拟表计值

        //com口操作函数
        COMManage cmm = new COMManage();
        public SerialPort port = null;
        private Thread _readThread;
        private bool _keepReading;
        //打开串口
        private void open_Click(object sender, RoutedEventArgs e)
        {
            if (COMName.Text.ToString().Trim().Length > 0)
            {
                open.IsEnabled = false;
                stop.IsEnabled = true;
                port = new SerialPort();
                port.BaudRate = 115200;
                port.PortName = COMName.Text;
                port.DataBits = 8;
                cmm.OpenPort(port);
                _keepReading = true;
                _readThread = new Thread(ReadPort);
                _readThread.Start();
            }
            else
            {
                MessageBox.Show("请选择端口");
            }
        }

        //关闭串口
        private void stop_Click(object sender, RoutedEventArgs e)
        {
            _keepReading = false;
            port.Close();
            stop.IsEnabled = false;
            open.IsEnabled = true;
        }

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
                            ThreadFunction(SerialIn);
                    }
                    catch { }
                }
                else
                {
                    TimeSpan waitTime = new TimeSpan(0, 0, 0, 0, 50);
                    Thread.Sleep(waitTime);
                }
            }

        }
        private void ThreadFunction(string sRecv)//将接收的字符串显示并进行下一步操作
        {
            //处理数据
            Dispatcher.Invoke((Action)delegate
            {
                string[] zstr = sRecv.Split('&');
                foreach (string str in zstr)
                {
                    if (str.Contains("@temhumposition"))
                    {
                        this.temtexblock.Text = str.Split(':')[1].Split(',')[0] + "℃";
                        this.humtextblock.Text = str.Split(':')[1].Split(',')[1] + "%";
                    }
                    else if (str.Contains("@lightposition"))
                    {
                        if (str.Split(':')[1].Equals("light"))
                        {
                            this.lighttextblock.Text = "有光";
                        }
                        else if (str.Split(':')[1].Equals("dark"))
                        {
                            this.lighttextblock.Text = "无光";
                        }
                    }
                    else if (str.Contains("@smokeposition"))
                    {
                        if (str.Split(':')[1].Equals("smoke"))
                        {
                            this.smoketextblock.Text = "有烟";
                        }
                        else if (str.Split(':')[1].Equals("nosmoke"))
                        {
                            this.smoketextblock.Text = "无烟";
                        }
                    }
                    else if (str.Contains("@bodyposition"))
                    {
                        if (str.Split(':')[1].Equals("somebody"))
                        {
                            this.bodytextblock.Text = "有人";
                        }
                        else if (str.Split(':')[1].Equals("nobody"))
                        {
                            this.bodytextblock.Text = "无人";
                        }
                    }
                    else if (str.Contains("@gasmeter"))
                    {
                        this.gastextblock.Text = str.Split(':')[1];
                    }
                }
            });
        }
        #endregion
    }
}
