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

        //点击确定，将改变状态的设备信息传到前台，改变前台设备状态图片
        //客厅：drawing  主卧：master   儿童房：children   书房：study   厨房：kitchen   餐厅：dining    卫生间：bathroom 阳台：balcony
        //温度：tem  照明灯：light_zm   装饰灯：light_zs   湿度：hum  电视：tv   音响：audio
        private List<string> lsequ = new List<string>();
        private List<string> lsstate = new List<string>();
        private string[] zstr = { "drawing", "master", "children", "study", "kitchen", "dining", "bathroom", "balcony" };

        string equipmentPath = @"..\..\Local Storage\Equipment.txt";
        string statePath = @"..\..\Local Storage\State.txt";
        string needPath = @"..\..\Local Storage\EnergyNeed.txt";

        public EquControl()
        {
            InitializeComponent();

            equipmentStateFromFileToControl();
            needReadFromFile(needPath);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            equipmentState();

            equStateWriteToFile(equipmentPath, lsequ, statePath, lsstate);

            needWriteToFile(needPath);

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
                            if (lr.Lstate)
                            {
                                lsstate.Add("on");
                            }
                            else
                            {
                                lsstate.Add("off");
                            }
                            lsequ.Add(str + "_" + "audio");
                            if (lr.Rstate)
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
                        sv.textblock_value.Text = temp;

                        SwitchUC su = sp2.Children[1] as SwitchUC;

                        //灯光照明赋值
                        string su_name_zm = str + "_" + "light_zm";
                        int index_zm = lsEqu.IndexOf(su_name_zm);
                        string light_zm = lsState[index_zm];
                        if (light_zm.Equals("on"))
                        {
                            su.Bfu = true;
                        }
                        else
                        {
                            su.Bfu = false;
                        }

                        //灯光装饰赋值
                        string su_name_zs = str + "_" + "light_zs";
                        int index_zs = lsEqu.IndexOf(su_name_zs);
                        string light_zs = lsState[index_zs];
                        if (light_zs.Equals("on"))
                        {
                            su.Bfd = true;
                        }
                        else
                        {
                            su.Bfd = false;
                        }
                    }
                    if (!str.Equals("drawing"))
                    {
                        SetValueUC sv2 = sp.Children[2] as SetValueUC;

                        //湿度赋值
                        string sv_hum = str + "_" + "hum";
                        int index_sv2 = lsEqu.IndexOf(sv_hum);
                        string hum = lsState[index_sv2];
                        sv2.textblock_value.Text = hum;
                    }
                    else
                    {
                        StackPanel sp3 = sp.Children[2] as StackPanel;
                        if (sp3.Children.Count > 1)
                        {
                            SetValueUC sv2 = sp3.Children[0] as SetValueUC;

                            //湿度赋值
                            string sv_hum = str + "_" + "hum";
                            int index_sv2 = lsEqu.IndexOf(sv_hum);
                            string hum = lsState[index_sv2];
                            sv2.textblock_value.Text = hum;

                            LeftRightUC lr = sp3.Children[1] as LeftRightUC;

                            //电视赋值
                            string lr_tv = str + "_" + "tv";
                            int index_tv = lsEqu.IndexOf(lr_tv);
                            string tv = lsState[index_tv];
                            if (tv.Equals("on"))
                            {
                                lr.Lstate = true;
                            }
                            else
                            {
                                lr.Lstate = false;
                            }

                            //音响赋值
                            string lr_audio = str + "_" + "audio";
                            int index_audio = lsEqu.IndexOf(lr_audio);
                            string audio = lsState[index_audio];
                            if (audio.Equals("on"))
                            {
                                lr.Rstate = true;
                            }
                            else
                            {
                                lr.Rstate = false;
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

        private void needReadFromFile(string needFilePath)
        {
            Txt_Handle txt_Handle = new Txt_Handle();
            string[] dataRead_EnergyNeed = txt_Handle.dataRead(needFilePath);

            need_E.Content = dataRead_EnergyNeed[0];
            need_H.Content = dataRead_EnergyNeed[1];
        }

        private void needWriteToFile(string needFilePath)
        {
            Txt_Handle txt_Handle = new Txt_Handle();
            string[] dataToWrite = new string[2];
            dataToWrite[0] = need_E.Content.ToString(); ;
            dataToWrite[1] = need_H.Content.ToString();
            txt_Handle.dataWrite(needFilePath, dataToWrite);
        }
    }
}
