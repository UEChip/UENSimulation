﻿using System;
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
        public ChartLineUC(List<DataPointCollection> ListDataPoints, string[] zstr)
        {
            InitializeComponent();
            chartC.DataPointWidth = 2;

            for (int i = 0; i < ListDataPoints.Count(); i++)
            {
                DataPointCollection dpc = ListDataPoints[i];
                chartC.Series[i].DataPoints = dpc;
                chartC.Series[i].LegendText = zstr[i];
                chartC.Series[i].ShowInLegend = true;
                chartC.Series[i].ToolTipText = string.Format("名称：#AxisXLabel {0}数值：#YValue {0}", System.Environment.NewLine);
            }
        }

        public ChartLineUC(DataPointCollection DataPoints, string str)
        {
            InitializeComponent();
            chartC.DataPointWidth = 2;

            chartC.Series[0].DataPoints = DataPoints;
            chartC.Series[0].LegendText = str;
            chartC.Series[0].ShowInLegend = true;
            chartC.Series[0].ToolTipText = string.Format("名称：#AxisXLabel {0}数值：#YValue {0}", System.Environment.NewLine);
        }

        private void AddDataSeries(string legendText, Visifire.Charts.DataSeries dataSeries, LineStyles style, SolidColorBrush brush, Visifire.Charts.RenderAs renderAs)
        {
            //　设置数据线的格式。 　　　　　　　　
            dataSeries.RenderAs = renderAs;
            dataSeries.LegendText = legendText;
            dataSeries.ShowInLegend = true;
            dataSeries.Color = brush;
            //dataSeries.AutoFitToPlotArea = true;
            //dataSeries.LineStyle = style;
            //dataSeries.LabelEnabled = false;
            //dataSeries.Bevel = false;
            //dataSeries.ShadowEnabled = true;
            //dataSeries.YValueFormatString = "######.## ";
            //dataSeries.LightingEnabled = true;
            //dataSeries.LabelText = " #AxisXLabel";
            //dataSeries.LabelFontSize = 13;
            //dataSeries.IncludePercentageInLegend = true;
            //dataSeries.ToolTipText = string.Format("名称：#AxisXLabel {0}数量：#YValue {0}占比：#Percentage%", System.Environment.NewLine);
        }
        #endregion 
    }
}
