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
using System.Reflection;
using UENSimulation.Model;
using UENSimulation.Utility;

namespace UENSimulation
{
    /// <summary>
    /// SimulatedDataSet.xaml 的交互逻辑
    /// </summary>
    public partial class SimulatedDataSet : Window
    {
        string path_Simulated = @"..\..\Local Storage\SimulatedData.txt";
        string path_Mode = @"..\..\Local Storage\Mode.txt";

        string path_mode_Season = @"..\..\Local Storage\mode_Season.txt";

        string path_NeedE_Manual = @"..\..\Local Storage\needE_Manual.txt";
        string path_NeedH_Manual = @"..\..\Local Storage\needH_Manual.txt";

        string path_NeedE_Winter = @"..\..\Local Storage\needE_W.txt";
        string path_NeedH_Winter = @"..\..\Local Storage\needH_W.txt";

        string path_NeedE_Summer = @"..\..\Local Storage\needE_S.txt";
        string path_NeedH_Summer = @"..\..\Local Storage\needH_S.txt";

        string path_NeedE_SA = @"..\..\Local Storage\needE_SA.txt";
        string path_NeedH_SA = @"..\..\Local Storage\needH_SA.txt";

        public SimulatedDataSet()
        {
            InitializeComponent();

            dataReadFromSimulatedData(path_Simulated, path_Mode);
            RB_Winter.IsChecked = true;
        }

        private void dataReadFromSimulatedData(string dataFilePath_Simulated, string dataFilePath_Mode)
        {
            Txt_Handle txt_Handle = new Txt_Handle();

            string[] dataRead_Simulated = txt_Handle.dataRead(dataFilePath_Simulated);
            string[] dataRead_Mode = txt_Handle.dataRead(dataFilePath_Mode);

            charge_HA.Text = dataRead_Simulated[0];

            double h_HA_Double = Convert.ToDouble(dataRead_Simulated[1]);
            h_HA.Value = Convert.ToDecimal(System.Math.Abs(h_HA_Double));
            savedH_HA.Value = Convert.ToDecimal(dataRead_Simulated[2]);

            charge_EA.Text = dataRead_Simulated[3];
            duration_EA.Value = Convert.ToDecimal(dataRead_Simulated[4]);
            speed_EA.Value = Convert.ToDecimal(dataRead_Simulated[5]);
            savedE_EA.Value = Convert.ToDecimal(dataRead_Simulated[6]);

            gear_Boiler.Value = Convert.ToDecimal(dataRead_Simulated[7]);
            duration_Boiler.Value = Convert.ToDecimal(dataRead_Simulated[8]);

            envrmtdata_lout_Heat.Value = Convert.ToDecimal(dataRead_Simulated[9]);
            duration_Heat.Value = Convert.ToDecimal(dataRead_Simulated[10]);

            envrmtdata_lout_Electricity.Value = Convert.ToDecimal(dataRead_Simulated[11]);
            duration_Electricity.Value = Convert.ToDecimal(dataRead_Simulated[12]);

            gear_UE.Value = Convert.ToDecimal(dataRead_Simulated[13]);
            duration_UE.Value = Convert.ToDecimal(dataRead_Simulated[14]);

            price_E.Value = Convert.ToDecimal(dataRead_Simulated[15]);
            price_H.Value = Convert.ToDecimal(dataRead_Simulated[16]);
            price_G.Value = Convert.ToDecimal(dataRead_Simulated[17]);

            switch (dataRead_Mode[0])
            {
                case "1":
                    Mode.Text = "经济";
                    break;
                case "2":
                    Mode.Text = "节能";
                    break;
                case "4":
                    Mode.Text = "经济+节能";
                    break;
            }
        }

        private void dataWriteToSimulatedData(string dataFilePath_Simulated, string dataFilePath_Mode)
        {
            Txt_Handle txt_Handle = new Txt_Handle();

            PropertyInfo[] pis = typeof(SimulatedData).GetProperties();
            int length = pis.Length;

            string[] dataWrite_Simulated = new string[length];

            dataWrite_Simulated[0] = charge_HA.Text.ToString();
            dataWrite_Simulated[1] = h_HA.Value.ToString();
            dataWrite_Simulated[2] = savedH_HA.Value.ToString();

            dataWrite_Simulated[3] = charge_EA.Text.ToString();
            dataWrite_Simulated[4] = duration_EA.Value.ToString();
            dataWrite_Simulated[5] = speed_EA.Value.ToString();
            dataWrite_Simulated[6] = savedE_EA.Value.ToString();

            dataWrite_Simulated[7] = gear_Boiler.Value.ToString();
            dataWrite_Simulated[8] = duration_Boiler.Value.ToString();

            dataWrite_Simulated[9] = envrmtdata_lout_Heat.Value.ToString();
            dataWrite_Simulated[10] = duration_Heat.Value.ToString();

            dataWrite_Simulated[11] = envrmtdata_lout_Electricity.Value.ToString();
            dataWrite_Simulated[12] = duration_Electricity.Value.ToString();

            dataWrite_Simulated[13] = gear_UE.Value.ToString();
            dataWrite_Simulated[14] = duration_UE.Value.ToString();

            dataWrite_Simulated[15] = price_E.Value.ToString();
            dataWrite_Simulated[16] = price_H.Value.ToString();
            dataWrite_Simulated[17] = price_G.Value.ToString();

            txt_Handle.dataWrite(dataFilePath_Simulated, dataWrite_Simulated);

            string[] dataWrite_Mode = new string[1];

            switch (Mode.Text)
            {
                case "经济":
                    dataWrite_Mode[0] = "1";
                    break;
                case "节能":
                    dataWrite_Mode[0] = "2";
                    break;
                case "经济+节能":
                    dataWrite_Mode[0] = "4";
                    break;
            }

            txt_Handle.dataWrite(dataFilePath_Mode, dataWrite_Mode);
        }

        private void btn_OK_Click(object sender, RoutedEventArgs e)
        {
            dataWriteToSimulatedData(path_Simulated, path_Mode);
            if (RB_Manual.IsChecked == true)
            {
                dataWriteToNeedE(path_NeedE_Manual);
                dataWriteToNeedH(path_NeedH_Manual);
            }
            this.Close();
        }

        private void dataReadFromNeedE(string dataFilePath)
        {
            Txt_Handle txt_Handle = new Txt_Handle();

            string[] dataRead = txt_Handle.dataRead(dataFilePath);

            for (int i = 0; i < 24; i++)
            {
                TAlex.WPF.Controls.NumericUpDown numbericUpDown = need.FindName("needE_" + i.ToString()) as TAlex.WPF.Controls.NumericUpDown;
                numbericUpDown.Value = Convert.ToDecimal(dataRead[i]);
            }
        }

        private void dataWriteToNeedE(string dataFilePath)
        {
            Txt_Handle txt_Handle = new Txt_Handle();

            string[] dataWrite = new string[24];

            for (int i = 0; i < 24; i++)
            {
                TAlex.WPF.Controls.NumericUpDown numbericUpDown = need.FindName("needE_" + i.ToString()) as TAlex.WPF.Controls.NumericUpDown;

                dataWrite[i] = numbericUpDown.Value.ToString();
            }

            txt_Handle.dataWrite(dataFilePath, dataWrite);
        }

        private void dataReadFromNeedH(string dataFilePath)
        {
            Txt_Handle txt_Handle = new Txt_Handle();

            string[] dataRead = txt_Handle.dataRead(dataFilePath);

            for (int i = 0; i < 24; i++)
            {
                TAlex.WPF.Controls.NumericUpDown numbericUpDown = need.FindName("needH_" + i.ToString()) as TAlex.WPF.Controls.NumericUpDown;
                numbericUpDown.Value = Convert.ToDecimal(dataRead[i]);
            }
        }

        private void dataWriteToNeedH(string dataFilePath)
        {
            Txt_Handle txt_Handle = new Txt_Handle();

            string[] dataWrite = new string[24];

            for (int i = 0; i < 24; i++)
            {
                TAlex.WPF.Controls.NumericUpDown numbericUpDown = need.FindName("needH_" + i.ToString()) as TAlex.WPF.Controls.NumericUpDown;

                dataWrite[i] = numbericUpDown.Value.ToString();
            }

            txt_Handle.dataWrite(dataFilePath, dataWrite);
        }

        private void RB_Winter_Checked(object sender, RoutedEventArgs e)
        {
            Txt_Handle txt_Handle = new Txt_Handle();

            dataReadFromNeedE(path_NeedE_Winter);
            dataReadFromNeedH(path_NeedH_Winter);

            string[] mode_Season = new string[1];
            mode_Season[0] = "W";
            txt_Handle.dataWrite(path_mode_Season, mode_Season);
        }

        private void RB_Summer_Checked(object sender, RoutedEventArgs e)
        {
            Txt_Handle txt_Handle = new Txt_Handle();

            dataReadFromNeedE(path_NeedE_Summer);
            dataReadFromNeedH(path_NeedH_Summer);

            string[] mode_Season = new string[1];
            mode_Season[0] = "S";
            txt_Handle.dataWrite(path_mode_Season, mode_Season);
        }

        private void RB_SA_Checked(object sender, RoutedEventArgs e)
        {
            Txt_Handle txt_Handle = new Txt_Handle();

            dataReadFromNeedE(path_NeedE_SA);
            dataReadFromNeedH(path_NeedH_SA);

            string[] mode_Season = new string[1];
            mode_Season[0] = "SA";
            txt_Handle.dataWrite(path_mode_Season, mode_Season);
        }

        private void RB_Manual_Checked(object sender, RoutedEventArgs e)
        {
            Txt_Handle txt_Handle = new Txt_Handle();

            dataReadFromNeedE(path_NeedH_Manual);
            dataReadFromNeedH(path_NeedH_Manual);

            string[] mode_Season = new string[1];
            mode_Season[0] = "M";
            txt_Handle.dataWrite(path_mode_Season, mode_Season);
        }
    }
}
