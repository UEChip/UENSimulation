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
    /// EquipmentParameterSet.xaml 的交互逻辑
    /// </summary>
    public partial class EquipmentParameterSet : Window
    {
        EquipmentParameter equipmentParameter = new EquipmentParameter();
        Txt_Handle txt_Handle = new Txt_Handle();

        string path = @"..\..\Local Storage\EquipmentParameter.txt";

        public EquipmentParameterSet()
        {
            InitializeComponent();

            dataReadFromEquipmentParameter(path);
        }

        private void dataReadFromEquipmentParameter(string dataFilePath)
        {
            string[] dataRead = txt_Handle.dataRead(dataFilePath);

            maxH_HA.Value = Convert.ToDecimal(dataRead[0]);
            maxTH_HA.Value = Convert.ToDecimal(dataRead[1]);
            etaInH_HA.Value = Convert.ToDecimal(dataRead[2]);
            etaOutH_HA.Value = Convert.ToDecimal(dataRead[3]);

            maxE_EA.Value = Convert.ToDecimal(dataRead[4]);
            maxInE_EA.Value = Convert.ToDecimal(dataRead[5]);
            maxOutE_EA.Value = Convert.ToDecimal(dataRead[6]);
            etaInE_EA.Value = Convert.ToDecimal(dataRead[7]);
            etaOutE_EA.Value = Convert.ToDecimal(dataRead[8]);

            powerH_gear_Boiler.Value = Convert.ToDecimal(dataRead[9]);
            gas_gear_Boiler.Value = Convert.ToDecimal(dataRead[10]);

            power_Heat.Value = Convert.ToDecimal(dataRead[11]);

            power_Electricity.Value = Convert.ToDecimal(dataRead[12]);

            powerE_UE.Value = Convert.ToDecimal(dataRead[13]);
            powerH_UE.Value = Convert.ToDecimal(dataRead[14]);
            gas_gear_UE.Value = Convert.ToDecimal(dataRead[15]);
            gear_UE.Value = Convert.ToDecimal(dataRead[16]);

            priceE.Value = Convert.ToDecimal(dataRead[17]);
            priceH.Value = Convert.ToDecimal(dataRead[18]);
            priceG.Value = Convert.ToDecimal(dataRead[19]);

            electricity.Value = Convert.ToDecimal(dataRead[20]);
            photoelectricity.Value = Convert.ToDecimal(dataRead[21]);
            optothermal.Value = Convert.ToDecimal(dataRead[22]);
        }

        private void dataWriteToEquipmentParameter(string dataFilePath)
        {
            PropertyInfo[] pis = typeof(EquipmentParameter).GetProperties();
            int length = pis.Length;

            string[] dataWrite = new string[length];

            dataWrite[0] = maxH_HA.Value.ToString();
            dataWrite[1] = maxTH_HA.Value.ToString();
            dataWrite[2] = etaInH_HA.Value.ToString();
            dataWrite[3] = etaOutH_HA.Value.ToString();

            dataWrite[4] = maxE_EA.Value.ToString();
            dataWrite[5] = maxInE_EA.Value.ToString();
            dataWrite[6] = maxOutE_EA.Value.ToString();
            dataWrite[7] = etaInE_EA.Value.ToString();
            dataWrite[8] = etaOutE_EA.Value.ToString();

            dataWrite[9] = powerH_gear_Boiler.Value.ToString();
            dataWrite[10] = gas_gear_Boiler.Value.ToString();

            dataWrite[11] = power_Heat.Value.ToString();

            dataWrite[12] = power_Electricity.Value.ToString();

            dataWrite[13] = powerE_UE.Value.ToString();
            dataWrite[14] = powerH_UE.Value.ToString();
            dataWrite[15] = gas_gear_UE.Value.ToString();
            dataWrite[16] = gear_UE.Value.ToString();

            dataWrite[17] = priceE.Value.ToString();
            dataWrite[18] = priceH.Value.ToString();
            dataWrite[19] = priceG.Value.ToString();

            dataWrite[20] = electricity.Value.ToString();
            dataWrite[21] = photoelectricity.Value.ToString();
            dataWrite[22] = optothermal.Value.ToString();

            txt_Handle.dataWrite(dataFilePath, dataWrite);
        }

        private void btn_OK_Click(object sender, RoutedEventArgs e)
        {
            dataWriteToEquipmentParameter(path);
            this.Close();
        }
    }
}
