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
using UENSimulation.Utility;

namespace UENSimulation
{
    /// <summary>
    /// UEMSystem.xaml 的交互逻辑
    /// </summary>
    public partial class UEMSystem : Window
    {
        public UEMSystem()
        {
            InitializeComponent();

            dataFromEnergyCalculation();
        }

        private void dataFromEnergyCalculation()
        {
            string filePath_EquipmentParameter = @"..\..\Local Storage\EquipmentParameter.txt";
            string filePath_SimulatedData = @"..\..\Local Storage\SimulatedData.txt";
            string filePath_EnergyNeed = @"..\..\Local Storage\SimulatedData.txt";
            string filePath_Mode = @"..\..\Local Storage\Mode.txt";

            EnergyCalculation energyCalculation = new EnergyCalculation(filePath_EquipmentParameter, filePath_SimulatedData, filePath_EnergyNeed, filePath_Mode);

            //储热
            double[] output_SaveH = new double[2];
            output_SaveH = energyCalculation.saveH(energyCalculation.equipmentParameter, energyCalculation.simulatedData);

            //储电
            double output_SaveE;
            output_SaveE = energyCalculation.saveE(energyCalculation.equipmentParameter, energyCalculation.simulatedData);

            //补燃锅炉
            double[] output_GasBoiler = new double[2];
            output_GasBoiler = energyCalculation.gasBoiler(energyCalculation.equipmentParameter, energyCalculation.simulatedData);

            //光热
            double output_PV;
            output_PV = energyCalculation.pv(energyCalculation.equipmentParameter, energyCalculation.simulatedData);

            //光伏
            double output_PT;
            output_PT = energyCalculation.pt(energyCalculation.equipmentParameter, energyCalculation.simulatedData);

            //泛能机
            double[] output_UEMachine = new double[3];
            output_UEMachine = energyCalculation.ueMachine(energyCalculation.equipmentParameter, energyCalculation.simulatedData);

            //控件赋值
            //储热
            savedH_HA.Content = output_SaveH[0].ToString();
            saveT_HA.Content = output_SaveH[1].ToString();

            //储电
            savedE_EA.Content = output_SaveE.ToString();

            //补燃锅炉
            prdctH_Boiler.Content = output_GasBoiler[0].ToString();
            consumeG_Boiler.Content = output_GasBoiler[1].ToString();

            //光热
            prdctH_Heat.Content = output_PV.ToString();

            //光伏
            prdctE_Electricity.Content = output_PT.ToString();

            //泛能机
            prdctE_Gear.Content = output_UEMachine[0].ToString();
            prdctH_Gear.Content = output_UEMachine[1].ToString();
            consumeG_Gear.Content = output_UEMachine[2].ToString();
        }
    }
}
