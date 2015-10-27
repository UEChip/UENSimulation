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
    /// CloudService.xaml 的交互逻辑
    /// </summary>
    public partial class CloudService : Window
    {
        private List<string> strListx = new List<string>() { "电", "热", "气", "水" };
        private List<string> strListyA = new List<string>() { "4", "2", "3", "5" };
        private List<string> strListyB = new List<string>() { "2", "4", "1", "4" };
        private List<string> strListyC = new List<string>() { "3", "3", "4", "3" };
        private List<string> strListyD = new List<string>() { "4", "4", "4", "3" };
        private List<string> strListyE = new List<string>() { "1", "5", "4", "2" };
        Grid grid = new Grid();
        #region 柱状图
        public void CreateChartColumn(string name, List<string> valuex, List<string> valuey)
        {
            //创建一个图标
            Chart chart = new Chart();

            //设置图标的宽度和高度
            chart.Width = 200;
            chart.Height = 200;
            chart.Margin = new Thickness(0, 0, 10, 0);
            //是否启用打印和保持图片
            chart.ToolBarEnabled = false;

            //设置图标的属性
            chart.ScrollingEnabled = false;//是否启用或禁用滚动
            chart.View3D = true;//3D效果显示

            //创建一个标题的对象
            Title title = new Title();

            //设置标题的名称
            title.Text = name;
            title.Padding = new Thickness(0, 10, 5, 0);

            //向图标添加标题
            chart.Titles.Add(title);

            Axis yAxis = new Axis();
            //设置图标中Y轴的最小值永远为0           
            yAxis.AxisMinimum = 0;
            //设置图表中Y轴的后缀          
            yAxis.Suffix = "kW.h";
            chart.AxesY.Add(yAxis);

            // 创建一个新的数据线。               
            DataSeries dataSeries = new DataSeries();

            // 设置数据线的格式
            dataSeries.RenderAs = RenderAs.StackedColumn;//柱状Stacked


            // 设置数据点              
            DataPoint dataPoint;
            for (int i = 0; i < valuex.Count; i++)
            {
                // 创建一个数据点的实例。                   
                dataPoint = new DataPoint();
                // 设置X轴点                    
                dataPoint.AxisXLabel = valuex[i];
                //设置Y轴点                   
                dataPoint.YValue = double.Parse(valuey[i]);
                //添加一个点击事件        
                dataPoint.MouseLeftButtonDown += new MouseButtonEventHandler(dataPoint_MouseLeftButtonDown);
                //添加数据点                   
                dataSeries.DataPoints.Add(dataPoint);
            }

            // 添加数据线到数据序列。                
            chart.Series.Add(dataSeries);

            //将生产的图表增加到Grid，然后通过Grid添加到上层Grid.           
            Grid gr = new Grid();
            gr.Children.Add(chart);
            grid = gr;
            
           
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
        public CloudService()
        {
            InitializeComponent();
            chartA.Children.Clear();
            CreateChartColumn("耗能表", strListx, strListyA);
            chartA.Children.Add(grid);
            chartB.Children.Clear();
            CreateChartColumn("耗能表", strListx,strListyB);
            chartB.Children.Add(grid);
            chartC.Children.Clear();
            CreateChartColumn("耗能表", strListx, strListyC);
            chartC.Children.Add(grid);
            chartD.Children.Clear();
            CreateChartColumn("耗能表", strListx, strListyD);
            chartD.Children.Add(grid);
            chartE.Children.Clear();
            CreateChartColumn("耗能表", strListx, strListyE);
            chartE.Children.Add(grid);
            first_use.Content = "本月用能：128kWh";
            first_save.Content = "节能率：16%";
            first_benefit.Content = "共节约：18.88元";
            second_use.Content = "本月用能：136kWh";
            second_save.Content = "节能率：12%";
            second_benefit.Content = "共节约：15.68元";
            third_use.Content = "本月用能：158kWh";
            third_save.Content = "节能率：9%";
            third_benefit.Content = "共节约：11.88元";
        }

        private void chartA_MouseEnter(object sender, MouseEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 0;
            da.To = 1;
            da.Duration = TimeSpan.FromSeconds(1);
            da.RepeatBehavior = new RepeatBehavior(1);//
            this.chartA.BeginAnimation(TextBlock.OpacityProperty, da);

        }

        private void chartA_MouseLeave(object sender, MouseEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 1;
            da.To = 0;
            da.Duration = TimeSpan.FromSeconds(2);
            da.RepeatBehavior = new RepeatBehavior(1);//
            this.chartA.BeginAnimation(TextBlock.OpacityProperty, da);
        }

        private void chartB_MouseEnter(object sender, MouseEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 0;
            da.To = 1;
            da.Duration = TimeSpan.FromSeconds(1);
            da.RepeatBehavior = new RepeatBehavior(1);//
            this.chartB.BeginAnimation(TextBlock.OpacityProperty, da);
        }

        private void chartB_MouseLeave(object sender, MouseEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 1;
            da.To = 0;
            da.Duration = TimeSpan.FromSeconds(2);
            da.RepeatBehavior = new RepeatBehavior(1);//
            this.chartB.BeginAnimation(TextBlock.OpacityProperty, da);
        }

        private void chartE_MouseEnter(object sender, MouseEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 0;
            da.To = 1;
            da.Duration = TimeSpan.FromSeconds(1);
            da.RepeatBehavior = new RepeatBehavior(1);//
            this.chartE.BeginAnimation(TextBlock.OpacityProperty, da);
        }

        private void chartE_MouseLeave(object sender, MouseEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 1;
            da.To = 0;
            da.Duration = TimeSpan.FromSeconds(2);
            da.RepeatBehavior = new RepeatBehavior(1);//
            this.chartE.BeginAnimation(TextBlock.OpacityProperty, da);
        }

        private void chartD_MouseEnter(object sender, MouseEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 0;
            da.To = 1;
            da.Duration = TimeSpan.FromSeconds(1);
            da.RepeatBehavior = new RepeatBehavior(1);//
            this.chartD.BeginAnimation(TextBlock.OpacityProperty, da);
        }

        private void chartD_MouseLeave(object sender, MouseEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 1;
            da.To = 0;
            da.Duration = TimeSpan.FromSeconds(2);
            da.RepeatBehavior = new RepeatBehavior(1);//
            this.chartD.BeginAnimation(TextBlock.OpacityProperty, da);
        }

        private void chartC_MouseEnter(object sender, MouseEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 0;
            da.To = 1;
            da.Duration = TimeSpan.FromSeconds(1);
            da.RepeatBehavior = new RepeatBehavior(1);//
            this.chartC.BeginAnimation(TextBlock.OpacityProperty, da);
        }

        private void chartC_MouseLeave(object sender, MouseEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 1;
            da.To = 0;
            da.Duration = TimeSpan.FromSeconds(2);
            da.RepeatBehavior = new RepeatBehavior(1);//
            this.chartC.BeginAnimation(TextBlock.OpacityProperty, da);
        }
    }
}
