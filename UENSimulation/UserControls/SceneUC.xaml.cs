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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UENSimulation.UserControls
{
    /// <summary>
    /// SceneUC.xaml 的交互逻辑
    /// </summary>
    public partial class SceneUC : UserControl
    {
        public SceneUC()
        {
            InitializeComponent();
        }

        //后景色
        string bcolor = "#FFDEEBF7";
        //前景色
        string fcolor = "#FFBDD7EE";
        public void SetBcolor(string sbcolor)
        {
            bcolor = sbcolor;
            this.border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(bcolor));
        }

        public void SetFcolor(string sfcolor)
        {
            fcolor = sfcolor;
            if (this.stackpanel.Children.Count > 1)
            {
                StackPanel sp = stackpanel.Children[1] as StackPanel;
                if (sp.Children.Count > 1)
                {
                    for (int i = 1; i < sp.Children.Count; i ++ )
                    {
                        Border b = sp.Children[i] as Border;
                        b.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(fcolor));
                    }
                }
            }
        }

        public SceneUC(string zid, string zname, string zstrdata)
        {
            InitializeComponent();
            Label myl = new Label();
            myl.Content = zname;
            myl.FontSize = 20;
            myl.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            myl.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;

            StackPanel mysp = new StackPanel();
            mysp.Orientation = Orientation.Horizontal;
            MyCheckUC mc = new MyCheckUC(zid);
            mc.Margin = new Thickness(20, -10, 0, 0);
            mysp.Children.Add(mc);

            string[] strdata = zstrdata.Split(',');
            int k = 0;
            for (int n = 0; n < strdata.Count() / 3; n++)
            {
                Border b = new Border();
                b.Width = 280;
                b.Height = 100;
                b.Margin = new Thickness(20, 0, 0, 20);
                b.CornerRadius = new CornerRadius(10);
                b.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(fcolor));

                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Vertical;

                for (int m = 0; m < 3 && (n * 3 + m) < strdata.Count(); m++)
                {
                    Label l = new Label();
                    l.Content = strdata[n * 3 + m];
                    sp.Children.Add(l);
                    k += 1;
                }
                b.Child = sp;
                mysp.Children.Add(b);
            }
            if (k < strdata.Count())
            {
                Border b = new Border();
                b.Width = 280;
                b.Height = 100;
                b.Margin = new Thickness(20, 0, 0, 20);
                b.CornerRadius = new CornerRadius(10);
                b.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(fcolor));

                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Vertical;
                for (; k < strdata.Count(); k++)
                {
                    Label l = new Label();
                    l.Content = strdata[k];
                    sp.Children.Add(l);
                }
                b.Child = sp;
                mysp.Children.Add(b);
            }

            this.stackpanel.Children.Add(myl);
            this.stackpanel.Children.Add(mysp);
        }
    }
}
