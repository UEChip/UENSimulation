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
        public EquipmentParameter equipmentParameter = new EquipmentParameter();
        public SimulatedData simulatedData = new SimulatedData();

        public EnergyCalculation(string filePath_EquipmentParameter, string filePath_SimulatedData)
        {
            dataReadFromEquipmentParameter(filePath_EquipmentParameter);
            dataReadFromSimulatedData(filePath_SimulatedData);
        }

        //读取设备数据
        public void dataReadFromEquipmentParameter(string dataFilePath)
        {
            string[] dataRead = txt_Handle.dataRead(dataFilePath);

            equipmentParameter.MaxH_HA = Convert.ToDouble(dataRead[0]);
            equipmentParameter.EtaInH_HA = Convert.ToDouble(dataRead[1]);
            equipmentParameter.EtaOutH_HA = Convert.ToDouble(dataRead[2]);

            equipmentParameter.MaxE_EA = Convert.ToDouble(dataRead[3]);
            equipmentParameter.MaxInE_EA = Convert.ToDouble(dataRead[4]);
            equipmentParameter.MaxOutE_EA = Convert.ToDouble(dataRead[5]);
            equipmentParameter.EtaInE_EA = Convert.ToDouble(dataRead[6]);
            equipmentParameter.EtaOutE_EA = Convert.ToDouble(dataRead[7]);

            equipmentParameter.PowerH_gear_Boiler = Convert.ToDouble(dataRead[8]);
            equipmentParameter.Gas_gear_Boiler = Convert.ToDouble(dataRead[9]);

            equipmentParameter.Power_Heat = Convert.ToDouble(dataRead[10]);

            equipmentParameter.Electricity = Convert.ToDouble(dataRead[11]);

            equipmentParameter.PowerE_UE = Convert.ToDouble(dataRead[12]);
            equipmentParameter.PowerH_UE = Convert.ToDouble(dataRead[13]);
            equipmentParameter.Gas_gear_UE = Convert.ToDouble(dataRead[14]);
            equipmentParameter.Gear_UE = Convert.ToDouble(dataRead[15]);

            equipmentParameter.PriceE = Convert.ToDouble(dataRead[16]);
            equipmentParameter.PriceH = Convert.ToDouble(dataRead[17]);
            equipmentParameter.PriceG = Convert.ToDouble(dataRead[18]);

            equipmentParameter.Electricity = Convert.ToDouble(dataRead[19]);
            equipmentParameter.Photoelectricity = Convert.ToDouble(dataRead[20]);
            equipmentParameter.Optothermal = Convert.ToDouble(dataRead[21]);
        }

        //读取模拟输入数据
        public void dataReadFromSimulatedData(string dataFilePath)
        {
            string[] dataRead = txt_Handle.dataRead(dataFilePath);

            simulatedData.Charge_HA = Convert.ToDouble(dataRead[0]);
            simulatedData.H_HA = Convert.ToDouble(dataRead[1]);
            simulatedData.SavedH_HA = Convert.ToDouble(dataRead[2]);

            simulatedData.Charge_EA = Convert.ToDouble(dataRead[3]);
            simulatedData.Duration_EA = Convert.ToDouble(dataRead[4]);
            simulatedData.Speed_EA = Convert.ToDouble(dataRead[5]);
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

        //泛能机
        public double[] ueMachine(EquipmentParameter equipmentParameter, SimulatedData simulatedData)
        {
            //输入变量，结构数组
            string[] variableIn = new string[2];
            variableIn[0] = "gear";
            variableIn[1] = "duration";
            MWStructArray variableInStruct = new MWStructArray(1, 1, variableIn);
            variableInStruct.SetField(variableIn[0], simulatedData.Gear_UE);
            variableInStruct.SetField(variableIn[1], simulatedData.Duration_UE);

            //设备参数，嵌套结构数组
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

            //设备参数，结构数组
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
            string[] fieldName_Consume = consume.FieldNames;
            MWNumericArray consumeG = (MWNumericArray)consume.GetField("G");
            double[,] consumeG_Output = (double[,])consumeG.ToArray(MWArrayComponent.Real);
            //prdct数组
            MWStructArray prdct = (MWStructArray)dataOutStruct.GetField("prdct");
            string[] fieldName_prdct = prdct.FieldNames;
            MWNumericArray prdctE = (MWNumericArray)prdct.GetField("E");
            MWNumericArray prdctH = (MWNumericArray)prdct.GetField("H");
            double[,] prdctE_Output = (double[,])prdctE.ToArray(MWArrayComponent.Real);
            double[,] prdctH_Output = (double[,])prdctH.ToArray(MWArrayComponent.Real);

            //返回数据
            double[] ueMachine_Output = new double[3];
            ueMachine_Output[0] = consumeG_Output[0, 0];
            ueMachine_Output[1] = prdctE_Output[0, 0];
            ueMachine_Output[2] = prdctH_Output[0, 0];

            return ueMachine_Output;
        }

        //储热
        public double[] saveH(EquipmentParameter equipmentParameter, SimulatedData simulatedData)
        {
            //输入变量，结构数组
            string[] variableIn = new string[3];
            variableIn[0] = "charge";
            variableIn[1] = "H";
            variableIn[2] = "savedH";
            MWStructArray variableInStruct = new MWStructArray(1, 1, variableIn);
            variableInStruct.SetField(variableIn[0], simulatedData.Charge_HA);
            variableInStruct.SetField(variableIn[1], simulatedData.H_HA);
            variableInStruct.SetField(variableIn[2], simulatedData.SavedH_HA);

            //设备参数，结构数组
            string[] equipmentParameterIn = new string[3];
            equipmentParameterIn[0] = "maxH";
            equipmentParameterIn[1] = "etaIn";
            equipmentParameterIn[2] = "etaOut";
            MWStructArray equipmentParameterInStruct = new MWStructArray(1, 1, equipmentParameterIn);
            equipmentParameterInStruct.SetField(equipmentParameterIn[0], equipmentParameter.MaxH_HA);
            equipmentParameterInStruct.SetField(equipmentParameterIn[1], equipmentParameter.EtaInH_HA);
            equipmentParameterInStruct.SetField(equipmentParameterIn[2], equipmentParameter.EtaOutH_HA);

            //saveH函数调用
            object[] dataOut = uec.saveh(1, variableInStruct, equipmentParameterInStruct);
            MWStructArray dataOutStruct = (MWStructArray)dataOut[0];
            string[] fieldName = dataOutStruct.FieldNames;
            //将输出由MWStructArray类型转化为MWNumericArray类型
            MWNumericArray savedH = (MWNumericArray)dataOutStruct.GetField("savedH");
            MWNumericArray deltaH = (MWNumericArray)dataOutStruct.GetField("deltaH");
            //将输出由MWNumericArray类型转化为double类型(中间需要转化为Array类型)
            double[,] savedH_Output = (double[,])savedH.ToArray(MWArrayComponent.Real);
            double[,] deltaH_Output = (double[,])deltaH.ToArray(MWArrayComponent.Real);

            //返回数据
            double[] saveH_Output = new double[2];
            saveH_Output[0] = savedH_Output[0, 0];
            saveH_Output[1] = deltaH_Output[0, 0];

            return saveH_Output;
        }

        //储电
        public double saveE(EquipmentParameter equipmentParameter, SimulatedData simulatedData)
        {
            //输入变量，结构数组
            string[] variableIn = new string[4];
            variableIn[0] = "charge";
            variableIn[1] = "duration";
            variableIn[2] = "speed";
            variableIn[3] = "savedE";
            MWStructArray variableInStruct = new MWStructArray(1, 1, variableIn);
            variableInStruct.SetField(variableIn[0], simulatedData.Charge_EA);
            variableInStruct.SetField(variableIn[1], simulatedData.Duration_EA);
            variableInStruct.SetField(variableIn[2], simulatedData.Speed_EA);
            variableInStruct.SetField(variableIn[3], simulatedData.SavedE_EA);

            //设备参数，结构数组
            string[] equipmentParameterIn = new string[5];
            equipmentParameterIn[0] = "maxE";
            equipmentParameterIn[1] = "maxInE";
            equipmentParameterIn[2] = "maxOutE";
            equipmentParameterIn[3] = "etaIn";
            equipmentParameterIn[4] = "etaOut";
            MWStructArray equipmentParameterInStruct = new MWStructArray(1, 1, equipmentParameterIn);
            equipmentParameterInStruct.SetField(equipmentParameterIn[0], equipmentParameter.MaxE_EA);
            equipmentParameterInStruct.SetField(equipmentParameterIn[1], equipmentParameter.MaxInE_EA);
            equipmentParameterInStruct.SetField(equipmentParameterIn[2], equipmentParameter.MaxOutE_EA);
            equipmentParameterInStruct.SetField(equipmentParameterIn[3], equipmentParameter.EtaInE_EA);
            equipmentParameterInStruct.SetField(equipmentParameterIn[4], equipmentParameter.EtaOutE_EA);

            //saveE函数调用
            object[] dataOut = uec.savee(1, variableInStruct, equipmentParameterInStruct);
            MWStructArray dataOutStruct = (MWStructArray)dataOut[0];
            string[] fieldName = dataOutStruct.FieldNames;
            //将输出由MWStructArray类型转化为MWNumericArray类型
            MWNumericArray savedE = (MWNumericArray)dataOutStruct.GetField("savedE");
            double[,] savedE_Output = (double[,])savedE.ToArray(MWArrayComponent.Real);

            double saveE_Output = savedE_Output[0, 0];

            return saveE_Output;
        }

        //光伏
        public double pv(EquipmentParameter equipmentParameter, SimulatedData simulatedData)
        {
            //输入变量，结构数组
            string[] envrmtdata = new string[1];
            envrmtdata[0] = "lout";
            MWStructArray envrmtdataStruct = new MWStructArray(1, 1, envrmtdata);
            envrmtdataStruct.SetField(envrmtdata[0], simulatedData.Envrmtdata_lout_Electricity);

            string[] variableIn = new string[2];
            variableIn[0] = "envrmtdata";
            variableIn[1] = "duration";
            MWStructArray variableInStruct = new MWStructArray(1, 1, variableIn);
            variableInStruct.SetField(variableIn[0], envrmtdataStruct);
            variableInStruct.SetField(variableIn[1], simulatedData.Duration_Electricity);

            //设备参数，结构数组
            string[] equipmentParameterIn = new string[1];
            equipmentParameterIn[0] = "power";
            MWStructArray equipmentParameterInStruct = new MWStructArray(1, 1, equipmentParameterIn);
            equipmentParameterInStruct.SetField(equipmentParameterIn[0], equipmentParameter.Power_Electricity);

            //pv函数调用
            object[] dataOut = uec.pv(1, variableInStruct, equipmentParameterInStruct);
            MWStructArray dataOutStruct = (MWStructArray)dataOut[0];
            MWStructArray prdct = (MWStructArray)dataOutStruct.GetField("prdct");//////////
            string[] fieldName = prdct.FieldNames;

            //将输出由MWStructArray类型转化为MWNumericArray类型
            MWNumericArray e = (MWNumericArray)prdct.GetField("E");
            double[,] e_Output = (double[,])e.ToArray(MWArrayComponent.Real);

            double pv_Output = e_Output[0, 0];

            return pv_Output;
        }

        //光热
        public double pt(EquipmentParameter equipmentParameter, SimulatedData simulatedData)
        {
            //输入变量，结构数组
            string[] envrmtdata = new string[1];
            envrmtdata[0] = "lout";
            MWStructArray envrmtdataStruct = new MWStructArray(1, 1, envrmtdata);
            envrmtdataStruct.SetField(envrmtdata[0], simulatedData.Envrmtdata_lout_Heat);

            string[] variableIn = new string[2];
            variableIn[0] = "envrmtdata";
            variableIn[1] = "duration";
            MWStructArray variableInStruct = new MWStructArray(1, 1, variableIn);
            variableInStruct.SetField(variableIn[0], envrmtdataStruct);
            variableInStruct.SetField(variableIn[1], simulatedData.Duration_Heat);

            //设备参数，结构数组
            string[] equipmentParameterIn = new string[1];
            equipmentParameterIn[0] = "power";
            MWStructArray equipmentParameterInStruct = new MWStructArray(1, 1, equipmentParameterIn);
            equipmentParameterInStruct.SetField(equipmentParameterIn[0], equipmentParameter.Power_Heat);

            //pt函数调用
            object[] dataOut = uec.pt(1, variableInStruct, equipmentParameterInStruct);
            //外层结构数组
            MWStructArray dataOutStruct = (MWStructArray)dataOut[0];
            string[] fieldName = dataOutStruct.FieldNames;
            //内层结构数组
            MWStructArray prdct = (MWStructArray)dataOutStruct.GetField("prdct");
            string[] fieldName_prdct = prdct.FieldNames;

            //将输出由MWStructArray类型转化为MWNumericArray类型
            MWNumericArray h = (MWNumericArray)prdct.GetField("H");
            double[,] h_Output = (double[,])h.ToArray(MWArrayComponent.Real);

            double pt_Output = h_Output[0, 0];

            return pt_Output;
        }

        //补燃锅炉
        public double[] gasBoiler(EquipmentParameter equipmentParameter, SimulatedData simulatedData)
        {
            //输入变量，结构数组
            string[] variableIn = new string[2];
            variableIn[0] = "gear";
            variableIn[1] = "duration";
            MWStructArray variableInStruct = new MWStructArray(1, 1, variableIn);
            variableInStruct.SetField(variableIn[0], simulatedData.Gear_Boiler);
            variableInStruct.SetField(variableIn[1], simulatedData.Duration_Boiler);

            //设备参数，嵌套结构数组
            //内层结构数组powerH定义
            string[] equipmentParameter_PowerH = new string[1];
            equipmentParameter_PowerH[0] = "gear";
            MWStructArray powerH = new MWStructArray(1, 1, equipmentParameter_PowerH);
            powerH.SetField(equipmentParameter_PowerH[0], equipmentParameter.PowerH_gear_Boiler);

            //内层结构数组gas定义
            string[] equipmentParameter_Gas = new string[1];
            equipmentParameter_Gas[0] = "gear";
            MWStructArray gas = new MWStructArray(1, 1, equipmentParameter_Gas);
            gas.SetField(equipmentParameter_Gas[0], equipmentParameter.Gas_gear_Boiler);

            //设备参数，结构数组
            string[] equipmentParamaterIn = new string[2];
            equipmentParamaterIn[0] = "powerH";
            equipmentParamaterIn[1] = "gas";
            MWStructArray equipmentParamaterInStruct = new MWStructArray(1, 1, equipmentParamaterIn);
            equipmentParamaterInStruct.SetField(equipmentParamaterIn[0], powerH);
            equipmentParamaterInStruct.SetField(equipmentParamaterIn[1], gas);

            //函数调用
            object[] dataOut = uec.gasboiler(1, variableInStruct, equipmentParamaterInStruct);
            MWStructArray dataOutStruct = (MWStructArray)dataOut[0];
            string[] fieldName = dataOutStruct.FieldNames;
            //将输出由MWStructArray类型转化为MWNumericArray类型
            MWStructArray prdct = (MWStructArray)dataOutStruct.GetField("prdct");
            MWStructArray consume = (MWStructArray)dataOutStruct.GetField("consume");
            string[] prdctFieldName = prdct.FieldNames;
            string[] consumeFieldName = consume.FieldNames;

            //将输出由MWNumericArray类型转化为double类型(中间需要转化为Array类型)
            MWNumericArray prdctH = (MWNumericArray)prdct.GetField("H");
            MWNumericArray consumeG = (MWNumericArray)consume.GetField("G");

            double[,] prdctH_Output = (double[,])prdctH.ToArray(MWArrayComponent.Real);
            double[,] consumeG_Output = (double[,])consumeG.ToArray(MWArrayComponent.Real);

            //返回数据
            double[] gasBoiler_Output = new double[2];
            gasBoiler_Output[0] = prdctH_Output[0, 0];
            gasBoiler_Output[1] = consumeG_Output[0, 0];

            return gasBoiler_Output;
        }
    }
}
