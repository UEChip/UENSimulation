using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UENSimulation.UserControls
{
    /// <summary>
    /// MyCheckUC.xaml 的交互逻辑
    /// </summary>
    public partial class MyCheckUC : UserControl
    {
        private bool bstate = false;

        public bool Bstate
        {
            get { return bstate; }
            set 
            { 
                bstate = value;
                if (bstate)
                {
                    this.myimage.Source = new BitmapImage(new Uri("../Resources/Images/yxz.png", UriKind.Relative)); 
                }
                else
                {
                    this.myimage.Source = new BitmapImage(new Uri("../Resources/Images/wxz.png", UriKind.Relative)); 
                }
            }
        }

        public MyCheckUC()
        {
            InitializeComponent();
        }

        private string strid = "";
        public MyCheckUC(string id)
        {
            InitializeComponent();
            strid = id;
            if (strid.Contains('s'))
            {
                Bstate = true;
            }
        }

        private void myimage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string[] ary = File.ReadAllLines("SceneData.txt", Encoding.GetEncoding("GB2312"));
            if (Bstate)
            {
                Bstate = false;
                ary = ary.Select(t => t.Split(';')[0].Split('.')[0].Equals(strid) ? strid.Trim('s') + t.TrimStart(strid.ToCharArray()) : t.Trim()).ToArray();
            }
            else
            {
                Bstate = true;
                ary = ary.Select(t => t.Split(';')[0].Split('.')[0].Equals(strid) ? strid + "s" + t.TrimStart(strid.ToCharArray()) : t.Trim()).ToArray();
            }
            ExportTxt(ary);
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

        private void myimage_MouseEnter(object sender, MouseEventArgs e)
        {
            this.myimage.Opacity = 0.8;
        }

        private void myimage_MouseLeave(object sender, MouseEventArgs e)
        {
            this.myimage.Opacity = 1;
        }
    }
}
