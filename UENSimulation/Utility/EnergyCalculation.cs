using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UENSimulation.Model;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;

namespace UENSimulation.Utility
{
    class EnergyCalculation
    {
        UECM.UEC uec = new UECM.UEC();
        Txt_Handle txt_Handle = new Txt_Handle();
        EquipmentParameter equipmentParameter = new EquipmentParameter();
        SimulatedData simulatedData = new SimulatedData();

        public void dataReadFromEquipmentParameter(string dataFilePath)
        {
            string[] dataRead = txt_Handle.dataRead(dataFilePath);

            equipmentParameter.MaxH_HA = Convert.ToDouble(dataRead[0]);
            equipmentParameter.MaxTH_HA = Convert.ToDouble(dataRead[1]);
            equipmentParameter.EtaInH_HA = Convert.ToDouble(dataRead[2]);
            equipmentParameter.EtaOutH_HA = Convert.ToDouble(dataRead[3]);

            equipmentParameter.MaxE_EA = Convert.ToDouble(dataRead[4]);
            equipmentParameter.MaxInE_EA = Convert.ToDouble(dataRead[5]);
            equipmentParameter.MaxOutE_EA = Convert.ToDouble(dataRead[6]);
            equipmentParameter.EtaInE_EA = Convert.ToDouble(dataRead[7]);
            equipmentParameter.EtaOutE_EA = Convert.ToDouble(dataRead[8]);

            equipmentParameter.PowerH_gear_Boiler = Convert.ToDouble(dataRead[9]);
            equipmentParameter.Gas_gear_Boiler = Convert.ToDouble(dataRead[10]);

            equipmentParameter.Power_Heat = Convert.ToDouble(dataRead[11]);

            equipmentParameter.Electricity = Convert.ToDouble(dataRead[12]);

            equipmentParameter.PowerE_UE = Convert.ToDouble(dataRead[13]);
            equipmentParameter.PowerH_UE = Convert.ToDouble(dataRead[14]);
            equipmentParameter.Gas_gear_UE = Convert.ToDouble(dataRead[15]);
            equipmentParameter.Gear_UE = Convert.ToDouble(dataRead[16]);

            equipmentParameter.PriceE = Convert.ToDouble(dataRead[17]);
            equipmentParameter.PriceH = Convert.ToDouble(dataRead[18]);
            equipmentParameter.PriceG = Convert.ToDouble(dataRead[19]);

            equipmentParameter.Electricity = Convert.ToDouble(dataRead[20]);
            equipmentParameter.Photoelectricity = Convert.ToDouble(dataRead[21]);
            equipmentParameter.Optothermal = Convert.ToDouble(dataRead[22]);
        }

        public void dataReadFromSimulatedData(string dataFilePath)
        {
            string[] dataRead = txt_Handle.dataRead(dataFilePath);

            simulatedData.Charge_HA = Convert.ToDouble(dataRead[0]);
            simulatedData.Duration_HA = Convert.ToDouble(dataRead[1]);
            simulatedData.SavedH_HA = Convert.ToDouble(dataRead[2]);
            simulatedData.SaveT_HA = Convert.ToDouble(dataRead[3]);

            simulatedData.Charge_EA = Convert.ToDouble(dataRead[4]);
            simulatedData.Duration_EA = Convert.ToDouble(dataRead[5]);
            simulatedData.SavedE_EA = Convert.ToDouble(dataRead[6]);

            simulatedData.Gear_Boiler = Convert.ToDouble(dataRead[7]);
            simulatedData.Duration_Boiler = Convert.ToDouble(dataRead[8]);

            simulatedData.Envrmtdata_lout_Heat = Convert.ToDouble(dataRead[9]);
            simulatedData.Duration_Heat = Convert.ToDouble(dataRead[10]);

            simulatedData.Envrmtdata_lout_Electricity = Convert.ToDouble(dataRead[11]);
            simulatedData.Duration_Electricity = Convert.ToDouble(dataRead[12]);

            simulatedData.Gear_UE = Convert.ToDouble(dataRead[13]);
            simulatedData.Duration_UE = Convert.ToDouble(dataRead[14]);
        }

        public void ueMachine(EquipmentParameter equipmentParameter, SimulatedData simulatedData)
        {
            //输入变量,结构数组
            string[] variableIn = new string[2];
            variableIn[0] = "gear";
            variableIn[1] = "duration";
            MWStructArray variableInStruct = new MWStructArray(1, 1, variableIn);
            variableInStruct.SetField(variableIn[0], simulatedData.Gear_Boiler);
            variableInStruct.SetField(variableIn[1], simulatedData.Duration_Boiler);

            //输入变量，嵌套结构数组
            //内层结构数组powerE定义
            string[] equipmentParameter_powerE = new string[1];
            equipmentParameter_powerE[0] = "gear";
            MWStructArray powerE = new MWStructArray(1, 1, equipmentParameter_powerE);
            powerE.SetField(equipmentParameter_powerE[0], equipmentParameter.PowerE_UE);

            //内层结构数组powerH定义
            string[] equipmentParameter_PowerH = new string[1];
            equipmentParameter_PowerH[0] = "gear";
            MWStructArray powerH = new MWStructArray(1, 1, equipmentParameter_PowerH);
            powerH.SetField(equipmentParameter_PowerH[0], equipmentParameter.PowerH_UE);

            //内层结构数组gas定义
            string[] equipmentParameter_Gas = new string[1];
            equipmentParameter_Gas[0] = "gear";
            MWStructArray gas = new MWStructArray(1, 1, equipmentParameter_Gas);
            gas.SetField(equipmentParameter_Gas[0], equipmentParameter.Gas_gear_UE);

            //输入变量，设备参数，结构数组
            string[] equipmentParameterIn = new string[3];
            equipmentParameterIn[0] = "powerE";
            equipmentParameterIn[1] = "powerH";
            equipmentParameterIn[2] = "gas";
            MWStructArray equipmentParameterInStruct = new MWStructArray(1, 1, equipmentParameterIn);
            equipmentParameterInStruct.SetField(equipmentParameterIn[0], powerE);
            equipmentParameterInStruct.SetField(equipmentParameterIn[1], powerH);
            equipmentParameterInStruct.SetField(equipmentParameterIn[2], gas);

            //uemachine函数调用
            object[] dataOut = uec.uemachine(1, variableInStruct, equipmentParameterInStruct);
            MWStructArray dataOutStruct = (MWStructArray)dataOut[0];
            //consume数组
            MWStructArray consume = (MWStructArray)dataOutStruct.GetField("consume");
            string[] f2CO = consume.FieldNames;
            MWNumericArray consumeGas = (MWNumericArray)consume.GetField("G");
            double[,]consumeG1 = (double[,])consumeGas.ToArray(MWArrayComponent.Real);
            //prdct数组
            MWStructArray prdct = (MWStructArray)dataOutStruct.GetField("prdct");
            string[] f1PR = prdct.FieldNames;
            MWNumericArray prdctE11 = (MWNumericArray)prdct.GetField("E");
            MWNumericArray prdctH11 = (MWNumericArray)prdct.GetField("H");
            double[,] prdctE1 = (double[,])prdctE11.ToArray(MWArrayComponent.Real);
            double[,] prdctH1 = (double[,])prdctH11.ToArray(MWArrayComponent.Real);
        }
    }
}
