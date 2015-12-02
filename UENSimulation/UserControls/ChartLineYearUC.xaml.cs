using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Visifire.Charts;

namespace UENSimulation.UserControls
{
    /// <summary>
    /// ChartLineYearUC.xaml 的交互逻辑
    /// </summary>
    public partial class ChartLineYearUC : UserControl
    {
        public ChartLineYearUC()
        {
            InitializeComponent();
        }

        public ChartLineYearUC(string strtitle)
        {
            InitializeComponent();

            chartC.ZoomingEnabled = true;
            chartC.ZoomOutText = "返回";
            chartC.ShowAllText = "退出";
            Title title = new Title();
            title.Text = strtitle;
            chartC.Titles.Add(title);
        }

        public void AddLineData(DataTable dt,string strname, string linecolor){
            SetChartData(dt, strname, linecolor);
        }

        private void SetChartData(DataTable dt,string strname, string linecolor)
        {
            Visifire.Charts.DataSeries dataSeries = new Visifire.Charts.DataSeries();
            AddDataSeries(strname, dataSeries, LineStyles.Solid, new SolidColorBrush((Color)ColorConverter.ConvertFromString(linecolor)), Visifire.Charts.RenderAs.QuickLine);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Visifire.Charts.DataPoint dataPoint = new DataPoint();
                dataPoint.AxisXLabel = dt.Rows[i]["time"].ToString();
                dataPoint.Color = new SolidColorBrush(Colors.Blue);
                dataPoint.YValue = Convert.ToDouble(dt.Rows[i]["value"]);
                dataPoint.Tag = " ";
                dataSeries.DataPoints.Add(dataPoint);
            }
            this.chartC.Series.Add(dataSeries);
        }

        private void AddDataSeries(string legendText, Visifire.Charts.DataSeries dataSeries, LineStyles style, SolidColorBrush brush, Visifire.Charts.RenderAs renderAs)
        {
            //　设置数据线的格式。 　　　　　　　　
            dataSeries.RenderAs = renderAs;
            dataSeries.LegendText = legendText;
            dataSeries.ShowInLegend = true;
            dataSeries.Color = brush;
        }
    }
}
