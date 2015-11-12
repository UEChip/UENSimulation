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

namespace UENSimulation.Windows
{
    /// <summary>
    /// SceneConfig.xaml 的交互逻辑
    /// </summary>
    public partial class SceneConfig : Window
    {
        public SceneConfig()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ImportTxt();
            SetReadOnly();
        }

        //导入
        public void ImportTxt()
        {
            if (File.Exists(@"SceneData.txt"))
            {
                int nu = 1;
                DataTable dt_fnzData = new DataTable();
                dt_fnzData.Columns.Add("no", typeof(string));
                dt_fnzData.Columns.Add("name", typeof(string));
                dt_fnzData.Columns.Add("info", typeof(string));
                dt_fnzData.Columns.Add("id", typeof(string));

                var file = File.Open("SceneData.txt", FileMode.Open);

                List<string> txt = new List<string>();
                using (var stream = new StreamReader(file, Encoding.GetEncoding("GB2312")))
                {
                    while (!stream.EndOfStream)
                    {
                        string strdata = stream.ReadLine();
                        string[] str1 = strdata.Split(';');
                        DataRow dr = dt_fnzData.NewRow();
                        dr[0] = str1[0].Split('.')[0];
                        dr[1] = str1[0].Split('.')[1];
                        dr[2] = str1[1];
                        dr[3] = nu;
                        nu += 1;

                        dt_fnzData.Rows.Add(dr);
                    }
                }
                file.Close();

                this.mydatagrid.ItemsSource = dt_fnzData.DefaultView;
            }
        }

        //点击确定，保存数据
        //判断新增还是修改
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.textbox_cj.Text.Trim().Length > 0)
            {
                if (System.Windows.MessageBox.Show("确定保存？", "警告", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    string[] ary = File.ReadAllLines("SceneData.txt", Encoding.GetEncoding("GB2312"));
                    string str = "";
                    if (textbox_cj.Tag.ToString().Length > 0)
                    {
                        str += this.textbox_cj.Tag.ToString() + ".";
                    }
                    else
                    {
                        int no = 0;
                        //求ary中no最大的，获取新增的数据no
                        foreach (string strno in ary)
                        {
                            if (Int32.Parse(strno.Split(';')[0].ToString().Split('.')[0].TrimEnd('s')) > no)
                            {
                                no = Int32.Parse(strno.Split(';')[0].ToString().Split('.')[0].TrimEnd('s'));
                            }
                        }
                        str += no + 1 + ".";
                    }
                    str += this.textbox_cj.Text + ";";
                    if (this.checkbox_wd.IsChecked == true)
                    {
                        str += "温度:" + this.textbox_wd1.Text + "-" + this.textbox_wd2.Text + "℃" + ",";
                    }
                    if (this.checkbox_sd.IsChecked == true)
                    {
                        str += "湿度:" + this.textbox_sd1.Text + "%" + ",";
                    }
                    if (this.checkbox_fs.IsChecked == true)
                    {
                        str += "风管风速:" + this.textbox_fs1.Text + "m/s" + ",";
                    }
                    if (this.checkbox_zmd.IsChecked == true)
                    {
                        str += "照明灯:";
                        if (this.radiobutton_zmd1.IsChecked == true)
                        {
                            str += "开,";
                        }
                        else
                        {
                            str += "关,";
                        }
                    }
                    if (this.checkbox_gsd.IsChecked == true)
                    {
                        str += "观赏灯:";
                        if (this.radiobutton_gsd1.IsChecked == true)
                        {
                            str += "开,";
                        }
                        else
                        {
                            str += "关,";
                        }
                    }
                    if (this.checkbox_ds.IsChecked == true)
                    {
                        str += "电视:";
                        if (this.radiobutton_ds1.IsChecked == true)
                        {
                            str += "开,";
                        }
                        else
                        {
                            str += "关,";
                        }
                    }
                    if (this.checkbox_yx.IsChecked == true)
                    {
                        str += "音响:";
                        if (this.radiobutton_yx1.IsChecked == true)
                        {
                            str += "开,";
                        }
                        else
                        {
                            str += "关,";
                        }
                    }

                    str = str.TrimEnd(',');

                    if (textbox_cj.Tag.ToString().Length > 0)
                    {
                        ary = ary.Select(t => t.Split(';')[0].Split('.')[0].Equals(str.Split(';')[0].ToString().Split('.')[0]) ? str : t.Trim()).ToArray();
                    }
                    else
                    {
                        List<string> rary = ary.ToList();
                        rary.Add(str);
                        ary = rary.ToArray();
                    }
                    //选择要修改id的那条，改变其数据
                    ExportTxt(ary);
                }
                ImportTxt();
                SetReadOnly();
            }
            else
            {
                MessageBox.Show("场景名不能为空！");
            }
        }

        //导出
        public void ExportTxt(string[] ary)
        {
            StreamWriter sw = new StreamWriter("SceneData.txt", false, Encoding.GetEncoding("GB2312"));
            foreach (string str in ary)
            {
                sw.Write(str + "\r\n");
            }
            sw.Close();
        }

        //初始化状态
        private void InitialData()
        {
            this.textbox_cj.Text = "";
            this.textbox_cj.Tag = "";
            this.checkbox_wd.IsChecked = false;
            this.textbox_wd1.Text = "";
            this.textbox_wd2.Text = "";
            this.checkbox_sd.IsChecked = false;
            this.textbox_sd1.Text = "";
            this.checkbox_fs.IsChecked = false;
            this.textbox_fs1.Text = "";
            this.checkbox_zmd.IsChecked = false;
            this.radiobutton_zmd1.IsChecked = false;
            this.radiobutton_zmd2.IsChecked = false;
            this.checkbox_gsd.IsChecked = false;
            this.radiobutton_gsd1.IsChecked = false;
            this.radiobutton_gsd2.IsChecked = false;
            this.checkbox_ds.IsChecked = false;
            this.radiobutton_ds1.IsChecked = false;
            this.radiobutton_ds2.IsChecked = false;
            this.checkbox_yx.IsChecked = false;
            this.radiobutton_yx1.IsChecked = false;
            this.radiobutton_yx2.IsChecked = false;
        }

        //将状态置为只读
        private void SetReadOnly()
        {
            this.textbox_cj.IsEnabled = false;
            this.checkbox_wd.IsEnabled = false;
            this.textbox_wd1.IsEnabled = false;
            this.textbox_wd2.IsEnabled = false;
            this.checkbox_sd.IsEnabled = false;
            this.textbox_sd1.IsEnabled = false;
            this.checkbox_fs.IsEnabled = false;
            this.textbox_fs1.IsEnabled = false;
            this.checkbox_zmd.IsEnabled = false;
            this.radiobutton_zmd1.IsEnabled = false;
            this.radiobutton_zmd2.IsEnabled = false;
            this.checkbox_gsd.IsEnabled = false;
            this.radiobutton_gsd1.IsEnabled = false;
            this.radiobutton_gsd2.IsEnabled = false;
            this.checkbox_ds.IsEnabled = false;
            this.radiobutton_ds1.IsEnabled = false;
            this.radiobutton_ds2.IsEnabled = false;
            this.checkbox_yx.IsEnabled = false;
            this.radiobutton_yx1.IsEnabled = false;
            this.radiobutton_yx2.IsEnabled = false;
        }

        //将状态置为可写
        private void SetWrite()
        {
            this.textbox_cj.IsEnabled = true;
            this.checkbox_wd.IsEnabled = true;
            this.textbox_wd1.IsEnabled = true;
            this.textbox_wd2.IsEnabled = true;
            this.checkbox_sd.IsEnabled = true;
            this.textbox_sd1.IsEnabled = true;
            this.checkbox_fs.IsEnabled = true;
            this.textbox_fs1.IsEnabled = true;
            this.checkbox_zmd.IsEnabled = true;
            this.radiobutton_zmd1.IsEnabled = true;
            this.radiobutton_zmd2.IsEnabled = true;
            this.checkbox_gsd.IsEnabled = true;
            this.radiobutton_gsd1.IsEnabled = true;
            this.radiobutton_gsd2.IsEnabled = true;
            this.checkbox_ds.IsEnabled = true;
            this.radiobutton_ds1.IsEnabled = true;
            this.radiobutton_ds2.IsEnabled = true;
            this.checkbox_yx.IsEnabled = true;
            this.radiobutton_yx1.IsEnabled = true;
            this.radiobutton_yx2.IsEnabled = true;
        }

        //点击“新增”按钮
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            InitialData();
            SetWrite();
        }

        //点击“修改”按钮
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (this.mydatagrid.SelectedItems.Count > 0)
            {
                SetWrite();
                InitialData();

                DataRow dr = ((DataRowView)this.mydatagrid.SelectedItem).Row;
                this.textbox_cj.Tag = dr["no"].ToString();
                this.textbox_cj.Text = dr["name"].ToString();

                string[] strdata = dr["info"].ToString().Split(',');
                foreach (string str in strdata)
                {
                    if (str.Contains("照明灯"))
                    {
                        this.checkbox_zmd.IsChecked = true;
                        if (str.Split(':')[1] == "开")
                        {
                            this.radiobutton_zmd1.IsChecked = true;
                        }
                        else if (str.Split(':')[1] == "关")
                        {
                            this.radiobutton_zmd2.IsChecked = true;
                        }
                    }
                    else if (str.Contains("观赏灯"))
                    {
                        this.checkbox_gsd.IsChecked = true;
                        if (str.Split(':')[1] == "开")
                        {
                            this.radiobutton_gsd1.IsChecked = true;
                        }
                        else if (str.Split(':')[1] == "关")
                        {
                            this.radiobutton_gsd2.IsChecked = true;
                        }
                    }
                    else if (str.Contains("温度"))
                    {
                        this.checkbox_wd.IsChecked = true;
                        this.textbox_wd1.Text = str.Split(':')[1].Split('-')[0];
                        this.textbox_wd2.Text = str.Split(':')[1].Split('-')[1].TrimEnd('℃');
                    }
                    else if (str.Contains("湿度"))
                    {
                        this.checkbox_sd.IsChecked = true;
                        this.textbox_sd1.Text = str.Split(':')[1].TrimEnd('%');
                    }
                    else if (str.Contains("电视"))
                    {
                        this.checkbox_ds.IsChecked = true;
                        if (str.Split(':')[1] == "开")
                        {
                            this.radiobutton_ds1.IsChecked = true;
                        }
                        else if (str.Split(':')[1] == "关")
                        {
                            this.radiobutton_ds2.IsChecked = true;
                        }
                    }
                    else if (str.Contains("音响"))
                    {
                        this.checkbox_yx.IsChecked = true;
                        if (str.Split(':')[1] == "开")
                        {
                            this.radiobutton_yx1.IsChecked = true;
                        }
                        else if (str.Split(':')[1] == "关")
                        {
                            this.radiobutton_yx2.IsChecked = true;
                        }
                    }
                    else if (str.Contains("风管风速"))
                    {
                        this.checkbox_fs.IsChecked = true;
                        this.textbox_fs1.Text = str.Split(':')[1].TrimEnd('s').TrimEnd('/').TrimEnd('m');
                    }
                }
            }
            else
            {
                MessageBox.Show("须先选中修改项！");
            }
        }

        //点击“删除”按钮
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (this.mydatagrid.SelectedItems.Count > 0)
            {
                DataRow dr = ((DataRowView)this.mydatagrid.SelectedItem).Row;
                string[] ary = File.ReadAllLines("SceneData.txt", Encoding.GetEncoding("GB2312"));
                //string[] ary = File.ReadAllLines("SceneData.txt", Encoding.GetEncoding("GB2312"));
                foreach (string str in ary)
                {
                    if (str.Split(';')[0].Split('.')[0].Equals(dr["no"].ToString()))
                    {
                        List<string> rary = ary.ToList();
                        rary.Remove(str);
                        ary = rary.ToArray();
                        break;
                    }
                }
                ExportTxt(ary);
                ImportTxt();
                SetReadOnly();
            }
            else
            {
                MessageBox.Show("需先选中删除项！");
            }
        }
    }
}
