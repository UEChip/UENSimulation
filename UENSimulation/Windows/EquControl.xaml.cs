using System;
using System.Collections.Generic;
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
    /// EquControl.xaml 的交互逻辑
    /// </summary>
    public partial class EquControl : Window
    {
        public BTClick _btClick;
        public EquControl()
        {
            InitializeComponent();
        }

        //点击确定，将改变状态的设备信息传到前台，改变前台设备状态图片
        //客厅：drawing  主卧：master   儿童房：children   书房：study   厨房：kitchen   餐厅：dining    卫生间：bathroom 阳台：balcony
        //温度：tem  照明灯：light_zm   装饰灯：light_zs   湿度：hum  电视：tv   音响：audio
        private List<string> lsequ = new List<string>();
        private List<string> lsstate = new List<string>();
        private string[] zstr = { "drawing", "master", "children", "study", "kitchen", "dining", "bathroom", "balcony" };
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (string str in zstr)
            {
                StackPanel sp = FindName("stackpanel_" + str) as StackPanel;

                if (sp.Children.Count > 1)
                {
                    StackPanel sp2 = sp.Children[1] as StackPanel;
                    if (sp2.Children.Count > 1)
                    {
                        SetValueUC sv = sp2.Children[0] as SetValueUC;
                        lsequ.Add(str + "_" + "tem");
                        lsstate.Add(sv.GetValue());

                        SwitchUC su = sp2.Children[1] as SwitchUC;
                        lsequ.Add(str + "_" + "light_zm");
                        if (su.GetUpState())
                        {
                            lsstate.Add("on");
                        }
                        else
                        {
                            lsstate.Add("off");
                        }
                        lsequ.Add(str + "_" + "light_zs");
                        if (su.GetDownState())
                        {
                            lsstate.Add("on");
                        }
                        else
                        {
                            lsstate.Add("off");
                        }
                    }
                    if (!str.Equals("drawing"))
                    {
                        SetValueUC sv2 = sp.Children[2] as SetValueUC;
                        lsequ.Add(str + "_" + "hum");
                        lsstate.Add(sv2.GetValue());
                    }
                    else
                    {
                        StackPanel sp3 = sp.Children[2] as StackPanel;
                        if (sp3.Children.Count > 1)
                        {
                            SetValueUC sv2 = sp3.Children[0] as SetValueUC;
                            lsequ.Add(str + "_" + "hum");
                            lsstate.Add(sv2.GetValue());

                            LeftRightUC lr = sp3.Children[1] as LeftRightUC;
                            lsequ.Add(str + "_" + "tv");
                            if (lr.GetLeftState())
                            {
                                lsstate.Add("on");
                            }
                            else
                            {
                                lsstate.Add("off");
                            }
                            lsequ.Add(str + "_" + "audio");
                            if (lr.GetRightState())
                            {
                                lsstate.Add("on");
                            }
                            else
                            {
                                lsstate.Add("off");
                            }
                        }
                    }
                }

            }

            this.Close();

            if (_btClick != null)
            {
                _btClick(lsequ, lsstate);
            } 
        }
    }
}
