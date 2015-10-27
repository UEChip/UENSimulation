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

namespace UENSimulation
{
    /// <summary>
    /// Gateway.xaml 的交互逻辑
    /// </summary>
    public partial class Gateway : Window
    {
        private List<string> electric = new List<string>(){"0","1","6","7","4","5"};
        private List<string> heat = new List<string>() { "1", "3", "2.5", "2", "2", "0" };
        private List<string> gas = new List<string>() { "0", "1", "3", "4", "2", "3" };
        private List<DateTime> modelTime = new List<DateTime>()
            { 
              
               new DateTime(2015,10,1,0,0,0),
               new DateTime(2015,10,1,4,0,0),
               new DateTime(2015,10,1,8,0,0),
               new DateTime(2015,10,1,12,0,0),
               new DateTime(2015,10,1,16,0,0),
               new DateTime(2015,10,1,20,0,0),
            
            };

        #region 折线图
        public void CreateChartSpline(string name, List<DateTime> lsTime, List<string> a, List<string> b,List<string> c)
        {
            
            Chart chart = new Chart();
            chart.Width = 385;
            chart.Height = 175;
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
            xaxis.Interval = 4;
            //设置X轴的时间显示格式为7-10 11：20           
            xaxis.ValueFormatString = "HH点";
            //给图标添加Axis            
            chart.AxesX.Add(xaxis);

            Axis yAxis = new Axis();
            //设置图标中Y轴的最小值永远为0           
            yAxis.AxisMinimum = 0;
            //设置图表中Y轴的后缀          
            yAxis.Suffix = "kW.h";
            chart.AxesY.Add(yAxis);


            // 创建一个新的数据线。               
            DataSeries dataEle = new DataSeries();
            // 设置数据线的格式。               
            dataEle.LegendText = "电";

            dataEle.RenderAs = RenderAs.Spline;//折线图

            dataEle.XValueType = ChartValueTypes.DateTime;
            dataEle.Color = new SolidColorBrush(Colors.Green);
            // 设置数据点              
            DataPoint dataPoint;
            for (int i = 0; i < lsTime.Count; i++)
            {
                // 创建一个数据点的实例。                   
                dataPoint = new DataPoint();
                // 设置X轴点                    
                dataPoint.XValue = lsTime[i];
                //设置Y轴点                   
                dataPoint.YValue = double.Parse(a[i]);
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
                dataPoint2.YValue = double.Parse(b[i]);
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


            DataSeries dataGas = new DataSeries();
            // 设置数据线的格式。         

            dataGas.LegendText = "气";

            dataGas.RenderAs = RenderAs.Spline;//折线图

            dataGas.XValueType = ChartValueTypes.DateTime;
            dataGas.Color = new SolidColorBrush(Colors.Yellow);
            // 设置数据点              
            DataPoint dataPoint3;
            for (int i = 0; i < lsTime.Count; i++)
            {
                // 创建一个数据点的实例。                   
                dataPoint3 = new DataPoint();
                // 设置X轴点                    
                dataPoint3.XValue = lsTime[i];
                //设置Y轴点                   
                dataPoint3.YValue = double.Parse(c[i]);
                dataPoint3.MarkerSize = 8;
                //dataPoint3.Tag = tableName.Split('(')[0];
                //设置数据点颜色                  
                // dataPoint.Color = new SolidColorBrush(Colors.LightGray);                   
                dataPoint3.MouseLeftButtonDown += new MouseButtonEventHandler(dataPoint_MouseLeftButtonDown);
                //添加数据点                   
                dataGas.DataPoints.Add(dataPoint3);
            }
            // 添加数据线到数据序列。                
            chart.Series.Add(dataGas);

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
        public Gateway()
        {
            InitializeComponent();
            model.Children.Clear();
            CreateChartSpline("用能负荷", modelTime, electric, heat,gas);
        }
    }
}
