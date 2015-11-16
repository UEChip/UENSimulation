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

            #region 设置datatable显示数据
            Random rd = new Random();
            //datatable(id, time, value, type) 分别代表id，0-23时，数值，类型（电需求E，热需求H）
            DataTable dt_e = new DataTable();
            dt_e.Columns.Add("id", typeof(string));
            dt_e.Columns.Add("time", typeof(string));
            dt_e.Columns.Add("value", typeof(string));
            dt_e.Columns.Add("type", typeof(string));

            for (int i = 0; i < 24; i++)
            {
                DataRow dr = dt_e.NewRow();
                dr[0] = i + 1;
                dr[1] = i;
                dr[2] = rd.Next(0, 45);
                dr[3] = "E";
                dt_e.Rows.Add(dr);
            }

            DataTable dt_h = new DataTable();
            dt_h.Columns.Add("id", typeof(string));
            dt_h.Columns.Add("time", typeof(string));
            dt_h.Columns.Add("value", typeof(string));
            dt_h.Columns.Add("type", typeof(string));

            for (int i = 0; i < 24; i++)
            {
                DataRow dr = dt_h.NewRow();
                dr[0] = i + 1;
                dr[1] = i;
                dr[2] = rd.Next(1, 50);
                dr[3] = "H";
                dt_h.Rows.Add(dr);
            }
            #endregion

                chartC.DataPointWidth = 2;
            Visifire.Charts.DataSeries dataSeries_e = new Visifire.Charts.DataSeries();
            AddDataSeries("电需求", dataSeries_e, LineStyles.Solid, new SolidColorBrush(Colors.Red), Visifire.Charts.RenderAs.Line);
            for (int i = 0; i < dt_e.Rows.Count; i++)
            {
                Visifire.Charts.DataPoint dataPoint = new DataPoint();
                dataPoint.AxisXLabel = dt_e.Rows[i]["time"].ToString() + "时";
                dataPoint.Color = new SolidColorBrush(Colors.Blue);
                dataPoint.YValue = Convert.ToDouble(dt_e.Rows[i]["value"]);
                dataPoint.Tag = " ";
                dataSeries_e.DataPoints.Add(dataPoint);
            }
            Visifire.Charts.DataSeries dataSeries_h = new Visifire.Charts.DataSeries();
            AddDataSeries("热需求", dataSeries_h, LineStyles.Solid, new SolidColorBrush(Colors.Yellow), Visifire.Charts.RenderAs.Line);
            for (int i = 0; i < dt_h.Rows.Count; i++)
            {
                Visifire.Charts.DataPoint dataPoint = new DataPoint();
                dataPoint.AxisXLabel = dt_h.Rows[i]["time"].ToString() + "时";
                dataPoint.Color = new SolidColorBrush(Colors.Blue);
                dataPoint.YValue = Convert.ToDouble(dt_h.Rows[i]["value"]);
                dataPoint.Tag = " ";
                dataSeries_h.DataPoints.Add(dataPoint);
            }
            this.chartC.Series.Add(dataSeries_e);
            this.chartC.Series.Add(dataSeries_h);
        }

        public void AddDataSeries(string legendText, Visifire.Charts.DataSeries dataSeries, LineStyles style, SolidColorBrush brush, Visifire.Charts.RenderAs renderAs)
        {
            //　设置数据线的格式。 　　　　　　　　
            dataSeries.RenderAs = renderAs;
            dataSeries.LegendText = legendText;
            dataSeries.ShowInLegend = true;
            dataSeries.AutoFitToPlotArea = true;
            dataSeries.LineStyle = style;
            dataSeries.Color = brush;
            dataSeries.LabelEnabled = false;
            dataSeries.Bevel = false;
            dataSeries.ShadowEnabled = true;
            dataSeries.YValueFormatString = "######.## ";
            dataSeries.LightingEnabled = true;
            dataSeries.LabelText = " #AxisXLabel";
            dataSeries.LabelFontSize = 13;
            dataSeries.IncludePercentageInLegend = true;

            dataSeries.ToolTipText = string.Format("名称：#AxisXLabel {0}数量：#YValue {0}占比：#Percentage%", System.Environment.NewLine);
        }
    }
}
