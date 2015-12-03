using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
using UENSimulation.UserControls;

namespace UENSimulation.Windows
{
    /// <summary>
    /// YearDayShow.xaml 的交互逻辑
    /// </summary>
    public partial class YearDayShow : Window
    {
        public YearDayShow()
        {
            InitializeComponent();

            ChartLineYearUC cyu = new ChartLineYearUC("全年能耗需求");
            this.grid_xq.Children.Add(cyu);
            DataTable dt_e = ImportTxt(@"..\..\Local Storage\YearNeed_E.txt");
            DataTable dt_h = ImportTxt(@"..\..\Local Storage\YearNeed_H.txt");
            if (dt_e != null)
            {
                cyu.AddLineData(dt_e, "电需求(kW)", "#FF5B9BD5");
            }
            if (dt_h != null)
            {
                cyu.AddLineData(dt_h, "热需求(kW)", "#FFADB9CA");
            }
        }

        //导入txt数据
        public DataTable ImportTxt(string filepath)
        {
            DataTable dt_fnzData = null;
            if (File.Exists(filepath))
            {
                dt_fnzData = new DataTable();
                dt_fnzData.Columns.Add("id", typeof(string));
                dt_fnzData.Columns.Add("value", typeof(string));
                dt_fnzData.Columns.Add("time", typeof(string));

                var file = File.Open(filepath, FileMode.Open);

                List<string> txt = new List<string>();
                using (var stream = new StreamReader(file, Encoding.GetEncoding("GB2312")))
                {
                    while (!stream.EndOfStream)
                    {
                        string strdata = stream.ReadLine();
                        DataRow dr = dt_fnzData.NewRow();
                        dr[0] = strdata.Split('\t')[0];
                        dr[1] = strdata.Split('\t')[1];
                        dr[2] = strdata.Split('\t')[2];

                        dt_fnzData.Rows.Add(dr);
                    }
                }
                file.Close();
            }
            return dt_fnzData;
        }
    }
}
