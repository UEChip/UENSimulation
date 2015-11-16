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
    /// SceneSet.xaml 的交互逻辑
    /// </summary>
    public partial class SceneSet : Window
    {
        public SceneSet()
        {
            InitializeComponent();
           // SetSceneUC();
        }

        //点击配置
        private void mybutton_Click(object sender, RoutedEventArgs e)
        {
            SceneConfig sc = new SceneConfig();
            sc.Show();
        }

        //从场景配置文件中读取数据，加载各场景配置情况
        private void SetSceneUC()
        {
            if (File.Exists(@"SceneData.txt"))
            {
                DataTable dt_fnzData = new DataTable();
                dt_fnzData.Columns.Add("no", typeof(string));
                dt_fnzData.Columns.Add("name", typeof(string));
                dt_fnzData.Columns.Add("info", typeof(string));

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

                        dt_fnzData.Rows.Add(dr);
                    }
                }
                file.Close();

                for (int i = 0; i < dt_fnzData.Rows.Count; i++)
                {
                    SceneUC suc = new SceneUC(dt_fnzData.Rows[i]["no"].ToString(), dt_fnzData.Rows[i]["name"].ToString(), dt_fnzData.Rows[i]["info"].ToString());
                    suc.SetBcolor(backcolor[i % (backcolor.GetLength(0)), 0]);
                    suc.SetFcolor(backcolor[i % (backcolor.GetLength(0)), 1]);
                    this.mystackpanel.Children.Add(suc);
                }

            }
        }

        private string[,] backcolor = new String[,] { { "#FFDEEBF7", "#FFBDD7EE" }, { "#FFEDEDED", "#FFDBDBDB" }, { "#FFFBE5D6", "#FFF8CBAD" } };
    }
}
