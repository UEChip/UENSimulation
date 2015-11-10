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
using UENSimulation.Utility;

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

        string equipmentPath = @"..\..\Local Storage\Equipment.txt";
        string statePath = @"..\..\Local Storage\State.txt";

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            equipmentState();

            equStateWriteToFile(equipmentPath, lsequ, statePath, lsstate);

            this.Close();

            if (_btClick != null)
            {
                _btClick(lsequ, lsstate);
            }
        }

        private void equipmentState()
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
        }

        private void equipmentStateFromFileToControl()
        {
            List<string>[] arrList = new List<string>[2];
            arrList = equStateReadFromFile(equipmentPath, statePath);

            List<string> lsEqu = arrList[0];
            List<string> lsState = arrList[1];

            foreach (string str in zstr)
            {
                StackPanel sp = FindName("stackpanel_" + str) as StackPanel;

                if (sp.Children.Count > 1)
                {
                    StackPanel sp2 = sp.Children[1] as StackPanel;
                    if (sp2.Children.Count > 1)
                    {
                        SetValueUC sv = sp2.Children[0] as SetValueUC;

                        //温度赋值
                        string sv_name = str + "_" + "tem";
                        int index_sv = lsEqu.IndexOf(sv_name);
                        string temp = lsState[index_sv];
                        sv.ProTitle = temp;

                        //lsequ.Add(str + "_" + "tem");
                        //lsstate.Add(sv.GetValue());

                        SwitchUC su = sp2.Children[1] as SwitchUC;

                        //灯光赋值
                        string su_name = str + "_" + "light_zm";
                        int index_su = lsEqu.IndexOf(su_name);
                        string light_zm = lsState[index_su];

                        //待整体同步后修改

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
        }

        private void equStateWriteToFile(string equipmentFilePath, List<string> lsEqu, string stateFilePath, List<string> lsState)
        {
            Txt_Handle txt_Handle = new Txt_Handle();

            string[] strEqu = lsEqu.ToArray();
            string[] strState = lsState.ToArray();

            txt_Handle.dataWrite(equipmentFilePath, strEqu);
            txt_Handle.dataWrite(stateFilePath, strState);
        }

        private List<string>[] equStateReadFromFile(string equipmentFilePath, string stateFilePath)
        {
            Txt_Handle txt_Handle = new Txt_Handle();

            string[] strEqu = txt_Handle.dataRead(equipmentFilePath);
            string[] strState = txt_Handle.dataRead(stateFilePath);

            List<string> lsEqu = new List<string>(strEqu);
            List<string> lsState = new List<string>(strState);

            List<string>[] arrList = new List<string>[2];
            arrList[0] = lsEqu;
            arrList[1] = lsState;
            return arrList;
        }
    }
}
