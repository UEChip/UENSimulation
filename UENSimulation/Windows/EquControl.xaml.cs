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
using UENSimulation.Model;
using UENSimulation.Utility;
using System.Net;

namespace UENSimulation.Windows
{
    /// <summary>
    /// EquControl.xaml 的交互逻辑
    /// </summary>
    public partial class EquControl : Window
    {
        public BTClick _btClick;
        public ManualControl _manualControl;

        //点击确定，将改变状态的设备信息传到前台，改变前台设备状态图片
        //客厅：drawing  主卧：master   儿童房：children   书房：study   厨房：kitchen   餐厅：dining   卫生间：bathroom   阳台：balcony
        //温度：tem  照明灯：light_zm   装饰灯：light_zs   湿度：hum  电视：tv   音响：audio
        //private List<string> lsequ = new List<string>();
        //private List<string> lsstate = new List<string>();
        private string[] zstr = { "drawing", "master", "children", "study", "kitchen", "dining", "bathroom", "balcony" };

        string equipmentPath = @"..\..\Local Storage\Equipment.txt";
        string statePath = @"..\..\Local Storage\State.txt";
        string needPath = @"..\..\Local Storage\EnergyNeed.txt";

        public EquControl()
        {
            InitializeComponent();

            SetValueUC._valueChanged += new valueChanged(energyNeedCalculationWhenValueChanged);
            SwitchUC._valueChanged += new valueChanged(energyNeedCalculationWhenValueChanged);
            LeftRightUC._valueChanged += new valueChanged(energyNeedCalculationWhenValueChanged);

            equipmentStateFromFileToControl();
            needReadFromFile(needPath);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<string>[] arrListEquState = new List<string>[2];
            arrListEquState = equipmentState();

            this.Close();

            if (_btClick != null)
            {
                _btClick(arrListEquState[0], arrListEquState[1]);
            }

            if (_manualControl != null)
            {
                _manualControl();
            }
        }

        private List<string>[] equipmentState()
        {
            List<string>[] arrList = new List<string>[2];

            List<string> lsEqu = new List<string>();
            List<string> lsState = new List<string>();

            foreach (string str in zstr)
            {
                StackPanel sp = FindName("stackpanel_" + str) as StackPanel;

                if (sp.Children.Count > 1)
                {
                    StackPanel sp2 = sp.Children[1] as StackPanel;
                    if (sp2.Children.Count > 1)
                    {
                        SetValueUC sv = sp2.Children[0] as SetValueUC;
                        lsEqu.Add(str + "_" + "tem");
                        lsState.Add(sv.GetValue());

                        SwitchUC su = sp2.Children[1] as SwitchUC;
                        lsEqu.Add(str + "_" + "light_zm");
                        if (su.GetUpState())
                        {
                            lsState.Add("on");
                        }
                        else
                        {
                            lsState.Add("off");
                        }
                        lsEqu.Add(str + "_" + "light_zs");
                        if (su.GetDownState())
                        {
                            lsState.Add("on");
                        }
                        else
                        {
                            lsState.Add("off");
                        }
                    }
                    if (!str.Equals("drawing"))
                    {
                        SetValueUC sv2 = sp.Children[2] as SetValueUC;
                        lsEqu.Add(str + "_" + "hum");
                        lsState.Add(sv2.GetValue());
                    }
                    else
                    {
                        StackPanel sp3 = sp.Children[2] as StackPanel;
                        if (sp3.Children.Count > 1)
                        {
                            SetValueUC sv2 = sp3.Children[0] as SetValueUC;
                            lsEqu.Add(str + "_" + "hum");
                            lsState.Add(sv2.GetValue());

                            LeftRightUC lr = sp3.Children[1] as LeftRightUC;
                            lsEqu.Add(str + "_" + "tv");
                            if (lr.Lstate)
                            {
                                lsState.Add("on");
                            }
                            else
                            {
                                lsState.Add("off");
                            }
                            lsEqu.Add(str + "_" + "audio");
                            if (lr.Rstate)
                            {
                                lsState.Add("on");
                            }
                            else
                            {
                                lsState.Add("off");
                            }
                        }
                    }
                }
            }

            arrList[0] = lsEqu;
            arrList[1] = lsState;
            return arrList;
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
            dataToWrite[0] = need_E.Content.ToString();
            dataToWrite[1] = need_H.Content.ToString();
            txt_Handle.dataWrite(needFilePath, dataToWrite);
        }

        //根据家庭设备控制情况计算能耗需求
        private void energyNeedCalculation()
        {
            //客厅：drawing  主卧：master   儿童房：children   书房：study   厨房：kitchen   餐厅：dining  卫生间：bathroom   阳台：balcony
            double[] light_zm_power = { 0.24, 0.18, 0.12, 0.12, 0.12, 0.9, 0.9, 0.9 };//灯光照明功率
            double[] light_zs_power = { 0.015, 0.09, 0.06, 0.03, 0.03, 0.045, 0.045, 0.03 };//灯光装饰功率
            double tv_power = 0.09;//电视功率
            double audio_power = 0.015;//音响功率
            double[] acreage = { 40, 30, 20, 20, 20, 15, 15, 6 };//房间面积
            double temperature_Standard = 18;
            double[] temperature_Outdoor_Standard = { -6.6, -7.0, -7.6, -8.4, -9.1, -9.6, -9.9, -8.6, -6.8, -5.1, -3.5, -2.2, -1.0, -0.2, 0.3, -0.1, -1.0, -2.1, -3.2, -4.2, -5.0, -5.5, -5.9, -6.3 };//一月份逐时气温
            double[] heat_Need_Standard = { 0.03632, 0.03726, 0.03810, 0.03911, 0.04006, 0.04101, 0.04193, 0.04197, 0.02951, 0.01791, 0.01285, 0.01233, 0.01181, 0.01125, 0.01087, 0.01118, 0.01489, 0.01776, 0.02127, 0.02428, 0.02701, 0.02948, 0.03122 };//冬季典型日热负荷

            double temperature_Outdoor_Now = 5;//当前室外温度

            DateTime now = DateTime.Now;
            int hour_Now = now.Hour;//当前时刻

            List<string>[] arrList = new List<string>[2];
            arrList = equStateReadFromFile(equipmentPath, statePath);

            List<string> lsEqu = arrList[0];
            List<string> lsState = arrList[1];

            //客厅
            Room_Drawing Drawing = (Room_Drawing)value_Room(lsEqu, lsState, "drawing", light_zm_power, light_zs_power, acreage, tv_power, audio_power);
            //电负荷
            Drawing.Electricity_Need = Drawing.Light_zm_power * Convert.ToInt32(Drawing.Light_zm) + Drawing.Light_zs_power * Convert.ToInt32(Drawing.Light_zs) + Drawing.Tv_power * Convert.ToInt32(Drawing.Tv) + Drawing.Audio_power * Convert.ToInt32(Drawing.Audio);
            //热负荷
            Drawing.Heat_Need = ((Drawing.Temperature - temperature_Outdoor_Now) * heat_Need_Standard[hour_Now] / (temperature_Standard - temperature_Outdoor_Standard[hour_Now])) * Drawing.Acreage;

            //主卧
            Room Master = (Room)value_Room(lsEqu, lsState, "master", light_zm_power, light_zs_power, acreage, tv_power, audio_power);
            //电负荷
            Master.Electricity_Need = Master.Light_zm_power * Convert.ToInt32(Master.Light_zm) + Master.Light_zs_power * Convert.ToInt32(Master.Light_zs);
            //热负荷
            Master.Heat_Need = (Master.Temperature - temperature_Outdoor_Now) * heat_Need_Standard[hour_Now] / (temperature_Standard - temperature_Outdoor_Standard[hour_Now]) * Master.Acreage;

            //儿童房
            Room Children = (Room)value_Room(lsEqu, lsState, "children", light_zm_power, light_zs_power, acreage, tv_power, audio_power);
            //电负荷
            Children.Electricity_Need = Children.Light_zm_power * Convert.ToInt32(Children.Light_zm) + Children.Light_zs_power * Convert.ToInt32(Children.Light_zs);
            //热负荷
            Children.Heat_Need = (Children.Temperature - temperature_Outdoor_Now) * heat_Need_Standard[hour_Now] / (temperature_Standard - temperature_Outdoor_Standard[hour_Now]) * Children.Acreage;

            //书房
            Room Study = (Room)value_Room(lsEqu, lsState, "study", light_zm_power, light_zs_power, acreage, tv_power, audio_power);
            //电负荷
            Study.Electricity_Need = Study.Light_zm_power * Convert.ToInt32(Study.Light_zm) + Study.Light_zs_power * Convert.ToInt32(Study.Light_zs);
            //热负荷
            Study.Heat_Need = (Study.Temperature - temperature_Outdoor_Now) * heat_Need_Standard[hour_Now] / (temperature_Standard - temperature_Outdoor_Standard[hour_Now]) * Study.Acreage;

            //厨房
            Room Kitchen = (Room)value_Room(lsEqu, lsState, "kitchen", light_zm_power, light_zs_power, acreage, tv_power, audio_power);
            //电负荷
            Kitchen.Electricity_Need = Kitchen.Light_zm_power * Convert.ToInt32(Kitchen.Light_zm) + Kitchen.Light_zs_power * Convert.ToInt32(Kitchen.Light_zs);
            //热负荷
            Kitchen.Heat_Need = (Kitchen.Temperature - temperature_Outdoor_Now) * heat_Need_Standard[hour_Now] / (temperature_Standard - temperature_Outdoor_Standard[hour_Now]) * Kitchen.Acreage;

            //餐厅
            Room Dining = (Room)value_Room(lsEqu, lsState, "dining", light_zm_power, light_zs_power, acreage, tv_power, audio_power);
            //电负荷
            Dining.Electricity_Need = Dining.Light_zm_power * Convert.ToInt32(Dining.Light_zm) + Dining.Light_zs_power * Convert.ToInt32(Dining.Light_zs);
            //热负荷
            Dining.Heat_Need = (Dining.Temperature - temperature_Outdoor_Now) * heat_Need_Standard[hour_Now] / (temperature_Standard - temperature_Outdoor_Standard[hour_Now]) * Dining.Acreage;

            //卫生间
            Room Bathroom = (Room)value_Room(lsEqu, lsState, "bathroom", light_zm_power, light_zs_power, acreage, tv_power, audio_power);
            //电负荷
            Bathroom.Electricity_Need = Bathroom.Light_zm_power * Convert.ToInt32(Bathroom.Light_zm) + Bathroom.Light_zs_power * Convert.ToInt32(Bathroom.Light_zs);
            //热负荷
            Bathroom.Heat_Need = (Bathroom.Temperature - temperature_Outdoor_Now) * heat_Need_Standard[hour_Now] / (temperature_Standard - temperature_Outdoor_Standard[hour_Now]) * Bathroom.Acreage;

            //阳台
            Room Balcony = (Room)value_Room(lsEqu, lsState, "balcony", light_zm_power, light_zs_power, acreage, tv_power, audio_power);
            //电负荷
            Balcony.Electricity_Need = Balcony.Light_zm_power * Convert.ToInt32(Balcony.Light_zm) + Balcony.Light_zs_power * Convert.ToInt32(Balcony.Light_zs);
            //热负荷
            Balcony.Heat_Need = (Balcony.Temperature - temperature_Outdoor_Now) * heat_Need_Standard[hour_Now] / (temperature_Standard - temperature_Outdoor_Standard[hour_Now]) * Balcony.Acreage;

            double electricity_Need = Drawing.Electricity_Need + Master.Electricity_Need + Children.Electricity_Need + Study.Electricity_Need + Kitchen.Electricity_Need + Dining.Electricity_Need + Bathroom.Electricity_Need + Balcony.Electricity_Need;
            need_E.Content = electricity_Need.ToString("0.00");

            double heat_Need = Drawing.Heat_Need + Master.Heat_Need + Children.Heat_Need + Study.Heat_Need + Kitchen.Heat_Need + Dining.Heat_Need + Bathroom.Heat_Need + Balcony.Heat_Need;
            need_H.Content = heat_Need.ToString("0.00");
        }

        private object value_Room(List<string> _lsEqu, List<string> _lsState, string _name_Room, double[] _light_zm_power, double[] _light_zs_power, double[] _acreage, double _tv_power, double _audio_power)
        {
            //string[] zstr = { "drawing", "master", "children", "study", "kitchen", "dining", "bathroom", "balcony" };
            List<string> ls_Name_Room = zstr.ToList<string>();

            //温度
            int index_tem = _lsEqu.IndexOf(_name_Room + "_" + "tem");
            double tem = Convert.ToDouble(_lsState[index_tem]);
            //灯光照明
            int index_light_zm = _lsEqu.IndexOf(_name_Room + "_" + "light_zm");
            bool light_zm;
            if (_lsState[index_light_zm].Equals("on"))
            {
                light_zm = true;
            }
            else
            {
                light_zm = false;
            }
            //灯光装饰
            int index_light_zs = _lsEqu.IndexOf(_name_Room + "_" + "light_zs");
            bool light_zs;
            if (_lsState[index_light_zs].Equals("on"))
            {
                light_zs = true;
            }
            else
            {
                light_zs = false;
            }

            int index_Name_Room = ls_Name_Room.IndexOf(_name_Room);
            //照明功率
            double light_zm_power = _light_zm_power[index_Name_Room];
            //装饰功率
            double light_zs_power = _light_zs_power[index_Name_Room];
            //房间面积
            double acreage = _acreage[index_Name_Room];

            if (_name_Room.Equals("drawing"))
            {
                //电视
                int index_tv = _lsEqu.IndexOf(_name_Room + "_" + "tv");
                bool tv;
                if (_lsState[index_tv].Equals("on"))
                {
                    tv = true;
                }
                else
                {
                    tv = false;
                }
                //音响
                int index_audio = _lsEqu.IndexOf(_name_Room + "_" + "audio");
                bool audio;
                if (_lsState[index_audio].Equals("on"))
                {
                    audio = true;
                }
                else
                {
                    audio = false;
                }

                double tv_power = _tv_power;
                double audio_power = _audio_power;

                Room_Drawing room_Drawing = new Room_Drawing(tem, light_zm, light_zs, light_zm_power, light_zs_power, acreage, tv, audio, tv_power, audio_power);
                return room_Drawing;
            }
            else
            {
                Room room = new Room(tem, light_zm, light_zs, light_zm_power, light_zs_power, acreage);
                return room;
            }
        }

        private void energyNeedCalculationWhenValueChanged()
        {
            List<string>[] arrListEquState = new List<string>[2];
            arrListEquState = equipmentState();

            equStateWriteToFile(equipmentPath, arrListEquState[0], statePath, arrListEquState[1]);

            energyNeedCalculation();

            needWriteToFile(needPath);

            needReadFromFile(needPath);
        }
    }
}
