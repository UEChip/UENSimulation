using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

        Thread dataCalculationdx = null;
        string filepath = "";
        DataTable dt_Data = null;
        ChartLineYearUC cyu_down = null;
        public YearDayShow()
        {
            InitializeComponent();

            string[] zstr = { "电需求(kW)", "热需求(kW)", "泛能机产电量(kWh)", "泛能机产热量(kWh)", "泛能机消耗燃气量(m³)", "补燃锅炉产热量(kWh)", "补燃锅炉消耗燃气量(m³)", "泛能机电效率(%)", "泛能机热效率(%)", "补燃锅炉热效率(%)", "市电(kW)", "市热(kW)", "光伏消耗(kW)", "光伏存储(kWh)", "光伏产电量(kWh)", "光热消耗(kWh)", "光热存储(kWh)", "光热产热量(kWh)", "储电量(kWh)", "储热量(kWh)" };
            DataTable dt_com = new DataTable();
            dt_com.Columns.Add("id", typeof(string));
            dt_com.Columns.Add("name", typeof(string));
            for (int i = 0; i < zstr.Count(); i++)
            {
                DataRow dr = dt_com.NewRow();
                dr[0] = i + 2;
                dr[1] = zstr[i];
                dt_com.Rows.Add(dr);
            }

            this.mComboBox.SelectedValuePath = "id";
            this.mComboBox.DisplayMemberPath = "name";
            this.mComboBox.ItemsSource = dt_com.DefaultView;
            this.mComboBox.SelectedIndex = 0;

            filepath = @"..\..\Local Storage\YearNeed.txt";
            dataCalculationdx = new Thread(new ThreadStart(ImportTxt));
            dataCalculationdx.Start();
        }

        //导入全部数据到datatable中，用于以后使用
        private void ImportTxt()
        {
            if (File.Exists(filepath))
            {
                dt_Data = new DataTable();
                dt_Data.Columns.Add("id", typeof(string));
                dt_Data.Columns.Add("time", typeof(string));
                dt_Data.Columns.Add("dxq", typeof(string));
                dt_Data.Columns.Add("rxq", typeof(string));
                dt_Data.Columns.Add("fnjcdl", typeof(string));
                dt_Data.Columns.Add("fnjcrl", typeof(string));
                dt_Data.Columns.Add("fnjxhrql", typeof(string));
                dt_Data.Columns.Add("brglcrl", typeof(string));
                dt_Data.Columns.Add("brglxhrql", typeof(string));
                dt_Data.Columns.Add("fnjdxl", typeof(string));
                dt_Data.Columns.Add("fnjrxl", typeof(string));
                dt_Data.Columns.Add("brglrxl", typeof(string));
                dt_Data.Columns.Add("sd", typeof(string));
                dt_Data.Columns.Add("sr", typeof(string));
                dt_Data.Columns.Add("gfxh", typeof(string));
                dt_Data.Columns.Add("gfcc", typeof(string));
                dt_Data.Columns.Add("gfcdl", typeof(string));
                dt_Data.Columns.Add("grxh", typeof(string));
                dt_Data.Columns.Add("grcc", typeof(string));
                dt_Data.Columns.Add("grcrl", typeof(string));
                dt_Data.Columns.Add("cdl", typeof(string));
                dt_Data.Columns.Add("crl", typeof(string));

                var file = File.Open(filepath, FileMode.Open);

                List<string> txt = new List<string>();
                using (var stream = new StreamReader(file, Encoding.GetEncoding("GB2312")))
                {
                    while (!stream.EndOfStream)
                    {
                        string strdata = stream.ReadLine();
                        DataRow dr = dt_Data.NewRow();
                        for (int i = 0; i < strdata.Split('\t').Count(); i++)
                        {
                            dr[i] = strdata.Split('\t')[i];
                        }
                        dt_Data.Rows.Add(dr);
                    }
                }
                file.Close();

                this.Dispatcher.Invoke(new Action(() =>
                            {
                                ChartLineYearUC cyu = new ChartLineYearUC("全年能耗需求");
                                this.grid_xq.Children.Add(cyu);
                                DataTable dt_e = ImportDatatable(0, 2, 1);
                                DataTable dt_h = ImportDatatable(0, 3, 1);
                                if (dt_e != null)
                                {
                                    cyu.AddLineData(dt_e, "电需求(kW)", "#FF0000FF");
                                }
                                if (dt_h != null)
                                {
                                    cyu.AddLineData(dt_h, "热需求(kW)", "#FFFF0000");
                                }

                                int j = Int32.Parse(mComboBox.SelectedValue.ToString().Trim());
                                cyu_down = new ChartLineYearUC(mComboBox.Text + "全年数据");
                                this.grid_fnj.Children.Add(cyu_down);
                                DataTable dt = ImportDatatable(0, j, 1);
                                cyu_down.AddLineData(dt, mComboBox.SelectedItem.ToString(), "#FF0DA4DC");
                            }));
            
            }
        }

        //根据输入列，生成新的数据datatable
        public DataTable ImportDatatable(int id, int value, int time)
        {
            DataTable dt_fnzData = null;
            if (dt_Data != null)
            {
                dt_fnzData = new DataTable();
                dt_fnzData.Columns.Add("id", typeof(string));
                dt_fnzData.Columns.Add("value", typeof(string));
                dt_fnzData.Columns.Add("time", typeof(string));

                for (int i = 1; i < dt_Data.Rows.Count; i++)
                {
                    DataRow dr = dt_fnzData.NewRow();
                    dr[0] = dt_Data.Rows[i][id].ToString();
                    dr[1] = dt_Data.Rows[i][value].ToString();
                    dr[2] = dt_Data.Rows[i][time].ToString();
                    dt_fnzData.Rows.Add(dr);
                }
            }
            return dt_fnzData;
        }

        //点击确定更改下方展示曲线数据源
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int i = Int32.Parse(mComboBox.SelectedValue.ToString().Trim());
            this.grid_fnj.Children.Clear();
            cyu_down = new ChartLineYearUC(mComboBox.Text + "全年数据");
            this.grid_fnj.Children.Add(cyu_down);
            DataTable dt = ImportDatatable(0, i, 1);
            cyu_down.AddLineData(dt, mComboBox.SelectedItem.ToString(), "#FF5B9BD5");
        }
    }
}
