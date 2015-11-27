using System;
using System.Threading;
using System.Collections;
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

            Thread dataCalculation = new Thread(new ThreadStart(dataFromEnergyCalculation));
            dataCalculation.Start();
        }

        private void dataFromEnergyCalculation()
        {
            string filePath_EquipmentParameter = @"..\..\Local Storage\EquipmentParameter.txt";
            string filePath_SimulatedData = @"..\..\Local Storage\SimulatedData.txt";
            string filePath_EnergyNeed = @"..\..\Local Storage\EnergyNeed.txt";
            string filePath_Mode = @"..\..\Local Storage\Mode.txt";

            EnergyCalculation energyCalculation = new EnergyCalculation(filePath_EquipmentParameter, filePath_SimulatedData, filePath_EnergyNeed, filePath_Mode);

            //光伏
            double output_PT;
            output_PT = energyCalculation.pt();

            //光热
            double output_PV;
            output_PV = energyCalculation.pv();

            //储热
            double[] output_SaveH = new double[2];
            output_SaveH = energyCalculation.saveH();

            //储电
            double output_SaveE;
            output_SaveE = energyCalculation.saveE();

            //泛能机
            double[] output_UEMachine = new double[3];
            output_UEMachine = energyCalculation.ueMachine();

            //补燃锅炉
            double[] output_GasBoiler = new double[2];
            output_GasBoiler = energyCalculation.gasBoiler();

            //优化
            ArrayList output_Ctrlopt = new ArrayList();
            output_Ctrlopt = energyCalculation.ctrlopt_Array();

            //设备效率
            double[] outPut_Etacal = new double[3];
            outPut_Etacal = energyCalculation.etacal();

            this.Dispatcher.Invoke(new Action(() =>
            {
                //控件赋值
                //光伏
                consume_Pv.Content = ((double)output_Ctrlopt[6]).ToString("0.00");
                save_Pv.Content = ((double)output_Ctrlopt[7]).ToString("0.00");

                //光热
                consume_Pt.Content = ((double)output_Ctrlopt[8]).ToString("0.00");
                save_Pt.Content = ((double)output_Ctrlopt[9]).ToString("0.00");

                //储热
                if (Convert.ToInt32(output_Ctrlopt[3]) == 1)
                {
                    charge_HA.Content = "充热";
                }
                else if (Convert.ToInt32(output_Ctrlopt[3]) == -1)
                {
                    charge_HA.Content = "放热";
                }
                saveH_HA.Content = ((double)output_Ctrlopt[4]).ToString("0.0000");
                deltaH_HA.Content = ((double)output_Ctrlopt[5]).ToString("0.0000");

                //储电        
                if (Convert.ToInt32(output_Ctrlopt[0]) == 1)
                {
                    charge_EA.Content = "充电";
                }
                else if (Convert.ToInt32(output_Ctrlopt[0]) == -1)
                {
                    charge_EA.Content = "放电";
                }
                saveE_EA.Content = ((double)output_Ctrlopt[1]).ToString("0.0000");
                deltaE_EA.Content = ((double)output_Ctrlopt[2]).ToString("0.0000");

                //泛能机
                prdctE_UE.Content = ((double)output_UEMachine[0]).ToString("0.00");
                prdctH_UE.Content = ((double)output_UEMachine[1]).ToString("0.00");
                consumeG_UE.Content = ((double)output_UEMachine[2]).ToString("0.00");
                gear_UE.Content = output_Ctrlopt[12].ToString();

                //补燃锅炉
                prdctH_Boiler.Content = ((double)output_GasBoiler[0]).ToString("0.00");
                consumeG_Boiler.Content = ((double)output_GasBoiler[1]).ToString("0.00");
                gasboiler_Gear.Content = output_Ctrlopt[13].ToString();

                //额外的电和热
                outsideE.Content = ((double)output_Ctrlopt[10]).ToString("0.00");
                outsideH.Content = ((double)output_Ctrlopt[11]).ToString("0.00");

                //泛能机系统输出的电和热
                needE.Content = energyCalculation.EnergyNeed.Electricity_Need.ToString("0.00");
                needH.Content = energyCalculation.EnergyNeed.Heat_Need.ToString("0.00");
            }));
        }
    }
}
