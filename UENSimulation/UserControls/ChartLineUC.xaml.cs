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
    /// ChartLineUC.xaml 的交互逻辑
    /// </summary>
    public partial class ChartLineUC : UserControl
    {
        public ChartLineUC()
        {
            InitializeComponent();
        }

        #region 传递数据，刷新展示曲线，刷新函数在调用页面实现
        public ChartLineUC(List<DataPointCollection> ListDataPoints, string[] zstr, string strtitle)
        {
            InitializeComponent();
            chartC.DataPointWidth = 2;
            Title title = new Title();
            title.Text = strtitle;
            chartC.Titles.Add(title);

            for (int i = 0; i < ListDataPoints.Count(); i++)
            {
                DataPointCollection dpc = ListDataPoints[i];
                chartC.Series[i].DataPoints = dpc;
                chartC.Series[i].LegendText = zstr[i];
                chartC.Series[i].ShowInLegend = true;
                chartC.Series[i].ToolTipText = string.Format("名称：#AxisXLabel {0}数值：#YValue {0}", System.Environment.NewLine);
                chartC.Series[i].AutoFitToPlotArea = true;
                chartC.Series[i].LabelEnabled = false;
                chartC.Series[i].Bevel = false;
                chartC.Series[i].ShadowEnabled = true;
                chartC.Series[i].YValueFormatString = "######.## ";
                chartC.Series[i].LightingEnabled = true;
                chartC.Series[i].LabelText = " #AxisXLabel";
                chartC.Series[i].LabelFontSize = 13;
                chartC.Series[i].IncludePercentageInLegend = true;
            }
        }

        public ChartLineUC(DataPointCollection DataPoints, string str, string strtitle)
        {
            InitializeComponent();
            chartC.DataPointWidth = 2;
            Title title = new Title();
            title.Text = strtitle;
            chartC.Titles.Add(title);

            chartC.Series[0].DataPoints = DataPoints;
            chartC.Series[0].LegendText = str;
            chartC.Series[0].ShowInLegend = true;
            chartC.Series[0].ToolTipText = string.Format("名称：#AxisXLabel {0}数值：#YValue {0}", System.Environment.NewLine);
            chartC.Series[0].AutoFitToPlotArea = true;
            chartC.Series[0].LabelEnabled = false;
            chartC.Series[0].Bevel = false;
            chartC.Series[0].ShadowEnabled = true;
            chartC.Series[0].YValueFormatString = "######.## ";
            chartC.Series[0].LightingEnabled = true;
            chartC.Series[0].LabelText = " #AxisXLabel";
            chartC.Series[0].LabelFontSize = 13;
            chartC.Series[0].IncludePercentageInLegend = true;
        }
        #endregion 
        
        //鼠标移入移出效果
        private void chartC_MouseEnter(object sender, MouseEventArgs e)
        {
            mUserControl.Visibility = System.Windows.Visibility.Visible;
        }

        private void chartC_MouseLeave(object sender, MouseEventArgs e)
        {
            mUserControl.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
