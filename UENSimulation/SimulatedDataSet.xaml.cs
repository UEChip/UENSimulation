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
        SimulatedData simulatedData = new SimulatedData();
        Txt_Handle txt_Handle = new Txt_Handle();

        string path = @"..\..\Local Storage\SimulatedData.txt";

        public SimulatedDataSet()
        {
            InitializeComponent();

            dataReadFromSimulatedData(path);
        }

        private void dataReadFromSimulatedData(string dataFilePath)
        {
            string[] dataRead = txt_Handle.dataRead(dataFilePath);

            charge_HA.Value = Convert.ToDecimal(dataRead[0]);
            duration_HA.Value = Convert.ToDecimal(dataRead[1]);
            savedH_HA.Value = Convert.ToDecimal(dataRead[2]);
            saveT_HA.Value = Convert.ToDecimal(dataRead[3]);

            charge_EA.Value = Convert.ToDecimal(dataRead[4]);
            duration_EA.Value = Convert.ToDecimal(dataRead[5]);
            savedE_EA.Value = Convert.ToDecimal(dataRead[6]);

            gear_Boiler.Value = Convert.ToDecimal(dataRead[7]);
            duration_Boiler.Value = Convert.ToDecimal(dataRead[8]);

            envrmtdata_lout_Heat.Value = Convert.ToDecimal(dataRead[9]);
            duration_Heat.Value = Convert.ToDecimal(dataRead[10]);

            envrmtdata_lout_Electricity.Value = Convert.ToDecimal(dataRead[11]);
            duration_Electricity.Value = Convert.ToDecimal(dataRead[12]);

            gear_UE.Value = Convert.ToDecimal(dataRead[13]);
            duration_UE.Value = Convert.ToDecimal(dataRead[14]);
        }

        private void dataWriteToSimulatedData(string dataFilePath)
        {
            PropertyInfo[] pis = typeof(SimulatedData).GetProperties();
            int length = pis.Length;

            string[] dataWrite = new string[length];

            dataWrite[0] = charge_HA.Value.ToString();
            dataWrite[1] = duration_HA.Value.ToString();
            dataWrite[2] = savedH_HA.Value.ToString();
            dataWrite[3] = saveT_HA.Value.ToString();

            dataWrite[4] = charge_EA.Value.ToString();
            dataWrite[5] = duration_EA.Value.ToString();
            dataWrite[6] = savedE_EA.Value.ToString();

            dataWrite[7] = gear_Boiler.Value.ToString();
            dataWrite[8] = duration_Boiler.Value.ToString();

            dataWrite[9] = envrmtdata_lout_Heat.Value.ToString();
            dataWrite[10] = duration_Heat.Value.ToString();

            dataWrite[11] = envrmtdata_lout_Electricity.Value.ToString();
            dataWrite[12] = duration_Electricity.Value.ToString();

            dataWrite[13] = gear_UE.Value.ToString();
            dataWrite[14] = duration_UE.Value.ToString();

            txt_Handle.dataWrite(dataFilePath, dataWrite);
        }

        private void btn_OK_Click(object sender, RoutedEventArgs e)
        {
            dataWriteToSimulatedData(path);
            this.Close();
        }
    }
}
