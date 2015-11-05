using System;
using System.Collections;
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
        private EquipmentParameter equipmentParameter = new EquipmentParameter();
        private SimulatedData simulatedData = new SimulatedData();
        private EnergyNeed energyNeed = new EnergyNeed();

        public EquipmentParameter EquipmentParameter
        {
            get { return equipmentParameter; }
            set { equipmentParameter = value; }
        }

        public SimulatedData SimulatedData
        {
            get { return simulatedData; }
            set { simulatedData = value; }
        }

        public EnergyNeed EnergyNeed
        {
            get { return energyNeed; }
            set { energyNeed = value; }
        }

        public EnergyCalculation(string filePath_EquipmentParameter, string filePath_SimulatedData, string dataFilePath_EnergyNeed, string dataFilePath_Mode)
        {
            dataReadFromEquipmentParameter(filePath_EquipmentParameter);
            dataReadFromSimulatedData(filePath_SimulatedData);
            dataReadFromEnergyNeed(dataFilePath_EnergyNeed, dataFilePath_Mode);
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

            equipmentParameter.Power_Heat = Convert.ToDouble(dataRead[8]);

            equipmentParameter.Power_Electricity = Convert.ToDouble(dataRead[9]);

            equipmentParameter.PowerE_UE_1 = Convert.ToDouble(dataRead[10]);
            equipmentParameter.PowerE_UE_2 = Convert.ToDouble(dataRead[11]);
            equipmentParameter.PowerE_UE_3 = Convert.ToDouble(dataRead[12]);

            equipmentParameter.PowerH_UE_1 = Convert.ToDouble(dataRead[13]);
            equipmentParameter.PowerH_UE_2 = Convert.ToDouble(dataRead[14]);
            equipmentParameter.PowerH_UE_3 = Convert.ToDouble(dataRead[15]);

            equipmentParameter.Gas_gear_UE_1 = Convert.ToDouble(dataRead[16]);
            equipmentParameter.Gas_gear_UE_2 = Convert.ToDouble(dataRead[17]);
            equipmentParameter.Gas_gear_UE_3 = Convert.ToDouble(dataRead[18]);

            equipmentParameter.PowerH_gear_Boiler_1 = Convert.ToDouble(dataRead[19]);
            equipmentParameter.PowerH_gear_Boiler_2 = Convert.ToDouble(dataRead[20]);
            equipmentParameter.PowerH_gear_Boiler_3 = Convert.ToDouble(dataRead[21]);

            equipmentParameter.Gas_gear_Boiler_1 = Convert.ToDouble(dataRead[22]);
            equipmentParameter.Gas_gear_Boiler_2 = Convert.ToDouble(dataRead[23]);
            equipmentParameter.Gas_gear_Boiler_3 = Convert.ToDouble(dataRead[24]);
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

        //读取能源需求
        public void dataReadFromEnergyNeed(string dataFilePath_EnergyNeed, string dataFilePath_Mode)
        {
            string[] dataRead_EnergyNeed = txt_Handle.dataRead(dataFilePath_EnergyNeed);
            string[] dataRead_Mode = txt_Handle.dataRead(dataFilePath_Mode);

            energyNeed.Electricity_Need = Convert.ToDouble(dataRead_EnergyNeed[0]);
            energyNeed.Heat_Need = Convert.ToDouble(dataRead_EnergyNeed[1]);
            energyNeed.Mode = Convert.ToDouble(dataRead_Mode[0]);
        }

        //泛能机
        public double[] ueMachine()
        {
            MWStructArray[] data_ueMachine_pack = ueMachine_pack();
            MWStructArray variableInStruct = (MWStructArray)data_ueMachine_pack[0].GetField("uemachine");
            MWStructArray equipmentParameterInStruct = (MWStructArray)data_ueMachine_pack[1].GetField("uemachine");

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
        public double[] saveH()
        {
            MWStructArray[] data_saveH_pack = saveH_pack();
            MWStructArray variableInStruct = (MWStructArray)data_saveH_pack[0].GetField("saveh");
            MWStructArray equipmentParameterInStruct = (MWStructArray)data_saveH_pack[1].GetField("saveh");

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
        public double saveE()
        {
            MWStructArray[] data_saveE_pack = saveE_pack();
            MWStructArray variableInStruct = (MWStructArray)data_saveE_pack[0].GetField("savee");
            MWStructArray equipmentParameterInStruct = (MWStructArray)data_saveE_pack[1].GetField("savee");

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
        public double pv()
        {
            MWStructArray[] data_pv_pack = pv_pack();
            MWStructArray variableInStruct = (MWStructArray)data_pv_pack[0].GetField("pv");
            MWStructArray equipmentParameterInStruct = (MWStructArray)data_pv_pack[1].GetField("pv");

            //pv函数调用
            object[] dataOut = uec.pv(1, variableInStruct, equipmentParameterInStruct);
            MWStructArray dataOutStruct = (MWStructArray)dataOut[0];
            MWStructArray prdct = (MWStructArray)dataOutStruct.GetField("prdct");
            string[] fieldName = prdct.FieldNames;

            //将输出由MWStructArray类型转化为MWNumericArray类型
            MWNumericArray e = (MWNumericArray)prdct.GetField("E");
            double[,] e_Output = (double[,])e.ToArray(MWArrayComponent.Real);

            double pv_Output = e_Output[0, 0];

            return pv_Output;
        }

        //光热
        public double pt()
        {
            MWStructArray[] data_pt_pack = pt_pack();
            MWStructArray variableInStruct = (MWStructArray)data_pt_pack[0].GetField("pt");
            MWStructArray equipmentParameterInStruct = (MWStructArray)data_pt_pack[1].GetField("pt");

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
        public double[] gasBoiler()
        {
            MWStructArray[] data_gasBoiler_pack = gasBoiler_pack();
            MWStructArray variableInStruct = (MWStructArray)data_gasBoiler_pack[0].GetField("gasboiler");
            MWStructArray equipmentParameterInStruct = (MWStructArray)data_gasBoiler_pack[1].GetField("gasboiler");

            //函数调用
            object[] dataOut = uec.gasboiler(1, variableInStruct, equipmentParameterInStruct);
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

        public ArrayList ctrlopt()
        {
            //能源需求参数
            string[] need = new string[3];
            need[0] = "E";
            need[1] = "H";
            need[2] = "mode";
            MWStructArray needStruct = new MWStructArray(1, 1, need);
            needStruct.SetField(need[0], energyNeed.Electricity_Need);
            needStruct.SetField(need[1], energyNeed.Heat_Need);
            needStruct.SetField(need[2], energyNeed.Mode);

            //参数封装格式定义
            MWStructArray[] data_pv_pack = pv_pack();
            MWStructArray[] data_pt_pack = pt_pack();
            MWStructArray[] data_saveE_pack = saveE_pack();
            MWStructArray[] data_saveH_pack = saveH_pack();
            MWStructArray[] data_ueMachine = ueMachine_pack();
            MWStructArray[] data_gasBoiler = gasBoiler_pack();

            string[] variable = new string[6];
            variable[0] = "pv";
            variable[1] = "pt";
            variable[2] = "savee";
            variable[3] = "saveh";
            variable[4] = "uemachine";
            variable[5] = "gasboiler";

            //输入变量提取            
            MWStructArray variableInStruct = new MWStructArray(1, 1, variable);
            variableInStruct.SetField(variable[0], data_pv_pack[0]);
            variableInStruct.SetField(variable[1], data_pt_pack[0]);
            variableInStruct.SetField(variable[2], data_saveE_pack[0]);
            variableInStruct.SetField(variable[3], data_saveH_pack[0]);
            variableInStruct.SetField(variable[4], data_ueMachine[0]);
            variableInStruct.SetField(variable[5], data_gasBoiler[0]);

            //设备参数提取
            MWStructArray equipmentParameterInStruct = new MWStructArray(1, 1, variable);
            equipmentParameterInStruct.SetField(variable[0], data_pv_pack[1]);
            equipmentParameterInStruct.SetField(variable[1], data_pt_pack[1]);
            equipmentParameterInStruct.SetField(variable[2], data_saveE_pack[1]);
            equipmentParameterInStruct.SetField(variable[3], data_saveH_pack[1]);
            equipmentParameterInStruct.SetField(variable[4], data_ueMachine[1]);
            equipmentParameterInStruct.SetField(variable[5], data_gasBoiler[1]);

            //函数调用
            object[] dataOut = uec.ctrlopt(1, needStruct, variableInStruct, equipmentParameterInStruct);
            //输出结果为嵌套结构数组，类型转换
            MWStructArray dataOutStruct = (MWStructArray)dataOut[0];
            string[] fieldName = dataOutStruct.FieldNames;

            //输出结构体的内层结构体
            MWStructArray savee = (MWStructArray)dataOutStruct.GetField("Savee");
            MWStructArray saveh = (MWStructArray)dataOutStruct.GetField("Saveh");
            MWStructArray pv = (MWStructArray)dataOutStruct.GetField("Pv");
            MWStructArray pt = (MWStructArray)dataOutStruct.GetField("Pt");
            //输出结构体的其他非嵌套成员
            MWStructArray outsideE_dataOut = (MWStructArray)dataOutStruct.GetField("outsideE");
            MWStructArray outsideH_dataOut = (MWStructArray)dataOutStruct.GetField("outsideH");
            MWStructArray uemachine_gear_dataOut = (MWStructArray)dataOutStruct.GetField("uemachine_gear");
            MWStructArray gasboiler_gear_dataOut = (MWStructArray)dataOutStruct.GetField("gasboiler_gear");

            string[] fieldName_savee = savee.FieldNames;
            string[] fieldName_saveh = saveh.FieldNames;
            string[] fieldName_pv = pv.FieldNames;
            string[] fieldName_pt = pt.FieldNames;

            string[] fieldName_outsideE = outsideE_dataOut.FieldNames;
            string[] fieldName_outsideH = outsideH_dataOut.FieldNames;
            string[] fieldName_uemachine_gear = uemachine_gear_dataOut.FieldNames;
            string[] fieldName_gasboiler_gear = gasboiler_gear_dataOut.FieldNames;

            //内层结构体Savee
            MWNumericArray charge_Savee = (MWNumericArray)savee.GetField("charge");
            MWNumericArray savedE_Savee = (MWNumericArray)savee.GetField("savedE");
            MWNumericArray deltaE_Savee = (MWNumericArray)savee.GetField("deltaE");
            //内层结构体Saveh
            MWNumericArray charge_Saveh = (MWNumericArray)saveh.GetField("charge");
            MWNumericArray savedH_Saveh = (MWNumericArray)saveh.GetField("savedH");
            MWNumericArray deltaH_Saveh = (MWNumericArray)saveh.GetField("deltaH");
            //内层结构体Pv
            MWNumericArray consume_Pv = (MWNumericArray)pv.GetField("consume");
            MWNumericArray save_Pv = (MWNumericArray)pv.GetField("save");
            //内层结构体Pt
            MWNumericArray consume_Pt = (MWNumericArray)pt.GetField("consume");
            MWNumericArray save_Pt = (MWNumericArray)pt.GetField("save");
            //非嵌套部分
            MWNumericArray outsideE = (MWNumericArray)outsideE_dataOut.GetField("outsideE");
            MWNumericArray outsideH = (MWNumericArray)outsideH_dataOut.GetField("outsideH");
            MWNumericArray uemachine_gear = (MWNumericArray)uemachine_gear_dataOut.GetField("uemachine_gear");
            MWNumericArray gasboiler_gear = (MWNumericArray)gasboiler_gear_dataOut.GetField("gasboiler_gear");

            //数据类型转换
            int[,] charge_Savee_Output = (int[,])charge_Savee.ToArray(MWArrayComponent.Real);
            double[,] savedE_Savee_Output = (double[,])savedE_Savee.ToArray(MWArrayComponent.Real);
            double[,] deltaE_Savee_Output = (double[,])deltaE_Savee.ToArray(MWArrayComponent.Real);

            int[,] charge_Saveh_Output = (int[,])charge_Saveh.ToArray(MWArrayComponent.Real);
            double[,] savedH_Saveh_Output = (double[,])savedH_Saveh.ToArray(MWArrayComponent.Real);
            double[,] deltaH_Saveh_Output = (double[,])deltaH_Saveh.ToArray(MWArrayComponent.Real);

            double[,] consume_Pv_Output = (double[,])consume_Pv.ToArray(MWArrayComponent.Real);
            double[,] save_Pv_Output = (double[,])save_Pv.ToArray(MWArrayComponent.Real);

            double[,] consume_Pt_Output = (double[,])consume_Pt.ToArray(MWArrayComponent.Real);
            double[,] save_Pt_Output = (double[,])save_Pt.ToArray(MWArrayComponent.Real);

            double[,] outsideE_Output = (double[,])outsideE.ToArray(MWArrayComponent.Real);
            double[,] outsideH_Output = (double[,])outsideH.ToArray(MWArrayComponent.Real);
            double[,] uemachine_gear_Output = (double[,])uemachine_gear.ToArray(MWArrayComponent.Real);
            double[,] gasboiler_gear_Output = (double[,])gasboiler_gear.ToArray(MWArrayComponent.Real);

            //返回数据
            ArrayList ctrlopt_Output = new ArrayList();
            ctrlopt_Output[0] = charge_Savee_Output[0, 0];//储电控制参数
            ctrlopt_Output[1] = savedE_Savee_Output[0, 0];//储电值
            ctrlopt_Output[2] = deltaE_Savee_Output[0, 0];//存储电量的改变值

            ctrlopt_Output[3] = charge_Saveh_Output[0, 0];//储热控制参数
            ctrlopt_Output[4] = savedH_Saveh_Output[0, 0];//储热值
            ctrlopt_Output[5] = deltaH_Saveh_Output[0, 0];//存储热量的改变值

            ctrlopt_Output[6] = consume_Pv_Output[0, 0];//光伏产电量中被用掉了多少
            ctrlopt_Output[7] = save_Pv_Output[0, 0];//光伏产电量中被存储了多少

            ctrlopt_Output[8] = consume_Pt_Output[0, 0];//光热产热量中被用掉了多少
            ctrlopt_Output[9] = save_Pt_Output[0, 0];//光热产热量中被存储了多少

            ctrlopt_Output[10] = outsideE_Output[0, 0];//除光伏、储电、泛能机之外还需要多少电
            ctrlopt_Output[11] = outsideH_Output[0, 0];//除光热、储热、泛能机之外还需要多少热
            ctrlopt_Output[12] = uemachine_gear_Output[0, 0];//泛能机档位设置
            ctrlopt_Output[13] = gasboiler_gear_Output[0, 0];//补燃锅炉档位设置

            return ctrlopt_Output;
        }

        //封装光伏数据
        public MWStructArray[] pv_pack()
        {
            //输入变量，嵌套结构数组
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

            //封装后的pv输入变量
            string[] variableIn_pack = new string[1];
            variableIn_pack[0] = "pv";
            MWStructArray simulatedData_pv = new MWStructArray(1, 1, variableIn_pack);
            simulatedData_pv.SetField(variableIn_pack[0], variableInStruct);

            //设备参数，嵌套结构数组
            string[] equipmentParameterIn = new string[1];
            equipmentParameterIn[0] = "power";
            MWStructArray equipmentParameterInStruct = new MWStructArray(1, 1, equipmentParameterIn);
            equipmentParameterInStruct.SetField(equipmentParameterIn[0], equipmentParameter.Power_Electricity);

            //封装后的pv设备参数
            string[] equipmentParameterIn_pv_pack = new string[1];
            equipmentParameterIn_pv_pack[0] = "pv";
            MWStructArray equipmentParameter_pv = new MWStructArray(1, 1, equipmentParameterIn_pv_pack);
            equipmentParameter_pv.SetField(equipmentParameterIn_pv_pack[0], equipmentParameterInStruct);

            //返回数据
            MWStructArray[] structArray_Return = new MWStructArray[2];
            structArray_Return[0] = simulatedData_pv;
            structArray_Return[1] = equipmentParameter_pv;

            return structArray_Return;
        }

        //封装光热数据
        public MWStructArray[] pt_pack()
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

            //封装后的pt输入变量
            string[] variableIn_pack = new string[1];
            variableIn_pack[0] = "pt";
            MWStructArray simulatedData_pt = new MWStructArray(1, 1, variableIn_pack);
            simulatedData_pt.SetField(variableIn_pack[0], variableInStruct);

            //设备参数，结构数组
            string[] equipmentParameterIn = new string[1];
            equipmentParameterIn[0] = "power";
            MWStructArray equipmentParameterInStruct = new MWStructArray(1, 1, equipmentParameterIn);
            equipmentParameterInStruct.SetField(equipmentParameterIn[0], equipmentParameter.Power_Heat);

            //封装后的pt设备参数
            string[] equipmentParameterIn_pack = new string[1];
            equipmentParameterIn_pack[0] = "pt";
            MWStructArray equipmentParameter_pt = new MWStructArray(1, 1, equipmentParameterIn_pack);
            equipmentParameter_pt.SetField(equipmentParameterIn_pack[0], equipmentParameterInStruct);

            //返回数据
            MWStructArray[] structArray_Return = new MWStructArray[2];
            structArray_Return[0] = simulatedData_pt;
            structArray_Return[1] = equipmentParameter_pt;

            return structArray_Return;
        }

        //封装储电数据
        public MWStructArray[] saveE_pack()
        {
            //输入变量，结构数组
            string[] variableIn = new string[4];
            variableIn[0] = "charge";
            variableIn[1] = "duration";
            variableIn[2] = "speed";
            variableIn[3] = "savedE";
            MWStructArray variableInStruct_saveE = new MWStructArray(1, 1, variableIn);
            variableInStruct_saveE.SetField(variableIn[0], simulatedData.Charge_EA);
            variableInStruct_saveE.SetField(variableIn[1], simulatedData.Duration_EA);
            variableInStruct_saveE.SetField(variableIn[2], simulatedData.Speed_EA);
            variableInStruct_saveE.SetField(variableIn[3], simulatedData.SavedE_EA);

            //封装后的saveE输入变量
            string[] variableIn_pack = new string[1];
            variableIn_pack[0] = "savee";
            MWStructArray simulatedData_saveE = new MWStructArray(1, 1, variableIn_pack);
            simulatedData_saveE.SetField(variableIn_pack[0], variableInStruct_saveE);

            //设备参数，结构数组
            string[] equipmentParameterIn = new string[5];
            equipmentParameterIn[0] = "maxE";
            equipmentParameterIn[1] = "maxInE";
            equipmentParameterIn[2] = "maxOutE";
            equipmentParameterIn[3] = "etaIn";
            equipmentParameterIn[4] = "etaOut";
            MWStructArray equipmentParameterInStruct_saveE = new MWStructArray(1, 1, equipmentParameterIn);
            equipmentParameterInStruct_saveE.SetField(equipmentParameterIn[0], equipmentParameter.MaxE_EA);
            equipmentParameterInStruct_saveE.SetField(equipmentParameterIn[1], equipmentParameter.MaxInE_EA);
            equipmentParameterInStruct_saveE.SetField(equipmentParameterIn[2], equipmentParameter.MaxOutE_EA);
            equipmentParameterInStruct_saveE.SetField(equipmentParameterIn[3], equipmentParameter.EtaInE_EA);
            equipmentParameterInStruct_saveE.SetField(equipmentParameterIn[4], equipmentParameter.EtaOutE_EA);

            //封装后的saveE设备参数
            string[] equipmentParameterIn_pack = new string[1];
            equipmentParameterIn_pack[0] = "savee";
            MWStructArray equipmentParameter_saveE = new MWStructArray(1, 1, equipmentParameterIn_pack);
            equipmentParameter_saveE.SetField(equipmentParameterIn_pack[0], equipmentParameterInStruct_saveE);

            //返回数据
            MWStructArray[] structArray_Return = new MWStructArray[2];
            structArray_Return[0] = simulatedData_saveE;
            structArray_Return[1] = equipmentParameter_saveE;

            return structArray_Return;
        }

        //封装储热数据
        public MWStructArray[] saveH_pack()
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

            //封装后的saveH输入变量
            string[] variableIn_pack = new string[1];
            variableIn_pack[0] = "saveh";
            MWStructArray simulatedData_saveH = new MWStructArray(1, 1, variableIn_pack);
            simulatedData_saveH.SetField(variableIn_pack[0], variableInStruct);

            //设备参数，结构数组
            string[] equipmentParameterIn = new string[3];
            equipmentParameterIn[0] = "maxH";
            equipmentParameterIn[1] = "etaIn";
            equipmentParameterIn[2] = "etaOut";
            MWStructArray equipmentParameterInStruct = new MWStructArray(1, 1, equipmentParameterIn);
            equipmentParameterInStruct.SetField(equipmentParameterIn[0], equipmentParameter.MaxH_HA);
            equipmentParameterInStruct.SetField(equipmentParameterIn[1], equipmentParameter.EtaInH_HA);
            equipmentParameterInStruct.SetField(equipmentParameterIn[2], equipmentParameter.EtaOutH_HA);

            //封装后的saveH设备参数
            string[] equipmentParameterIn_pack = new string[1];
            equipmentParameterIn_pack[0] = "saveh";
            MWStructArray equipmentParameter_saveH = new MWStructArray(1, 1, equipmentParameterIn_pack);
            equipmentParameter_saveH.SetField(equipmentParameterIn_pack[0], equipmentParameterInStruct);

            //返回数据
            MWStructArray[] structArray_Return = new MWStructArray[2];
            structArray_Return[0] = simulatedData_saveH;
            structArray_Return[1] = equipmentParameter_saveH;

            return structArray_Return;
        }

        //封装泛能机数据
        public MWStructArray[] ueMachine_pack()
        {
            //输入变量，结构数组
            string[] variableIn = new string[2];
            variableIn[0] = "gear";
            variableIn[1] = "duration";
            MWStructArray variableInStruct = new MWStructArray(1, 1, variableIn);
            variableInStruct.SetField(variableIn[0], simulatedData.Gear_UE);
            variableInStruct.SetField(variableIn[1], simulatedData.Duration_UE);

            //封装后的ueMachine输入变量
            string[] variableIn_pack = new string[1];
            variableIn_pack[0] = "uemachine";
            MWStructArray simulatedData_ueMachine = new MWStructArray(1, 1, variableIn_pack);
            simulatedData_ueMachine.SetField(variableIn_pack[0], variableInStruct);

            //设备参数，嵌套结构数组
            //内层结构数组powerE定义 与档位对应的数组
            double[] powerE_UE = new double[3];
            powerE_UE[0] = equipmentParameter.PowerE_UE_1;
            powerE_UE[1] = equipmentParameter.PowerE_UE_2;
            powerE_UE[2] = equipmentParameter.PowerE_UE_3;

            string[] equipmentParameter_powerE = new string[1];
            equipmentParameter_powerE[0] = "gear";
            MWStructArray powerE = new MWStructArray(1, 1, equipmentParameter_powerE);
            powerE.SetField(equipmentParameter_powerE[0], (MWNumericArray)powerE_UE);

            //内层结构数组powerH定义 与档位对应的数组
            double[] powerH_UE = new double[3];
            powerH_UE[0] = equipmentParameter.PowerH_UE_1;
            powerH_UE[1] = equipmentParameter.PowerH_UE_2;
            powerH_UE[2] = equipmentParameter.PowerH_UE_3;

            string[] equipmentParameter_PowerH = new string[1];
            equipmentParameter_PowerH[0] = "gear";
            MWStructArray powerH = new MWStructArray(1, 1, equipmentParameter_PowerH);
            powerH.SetField(equipmentParameter_PowerH[0], (MWNumericArray)powerH_UE);

            //内层结构数组gas定义 与档位对应的数组
            double[] gas_gear_UE = new double[3];
            gas_gear_UE[0] = equipmentParameter.Gas_gear_UE_1;
            gas_gear_UE[1] = equipmentParameter.Gas_gear_UE_2;
            gas_gear_UE[2] = equipmentParameter.Gas_gear_UE_3;

            string[] equipmentParameter_Gas = new string[1];
            equipmentParameter_Gas[0] = "gear";
            MWStructArray gas = new MWStructArray(1, 1, equipmentParameter_Gas);
            gas.SetField(equipmentParameter_Gas[0], (MWNumericArray)gas_gear_UE);

            //设备参数，结构数组
            string[] equipmentParameterIn = new string[3];
            equipmentParameterIn[0] = "powerE";
            equipmentParameterIn[1] = "powerH";
            equipmentParameterIn[2] = "gas";
            MWStructArray equipmentParameterInStruct = new MWStructArray(1, 1, equipmentParameterIn);
            equipmentParameterInStruct.SetField(equipmentParameterIn[0], powerE);
            equipmentParameterInStruct.SetField(equipmentParameterIn[1], powerH);
            equipmentParameterInStruct.SetField(equipmentParameterIn[2], gas);

            //封装后的ueMachine设备参数
            string[] equipmentParameterIn_pack = new string[1];
            equipmentParameterIn_pack[0] = "uemachine";
            MWStructArray equipmentParameter_ueMachine = new MWStructArray(1, 1, equipmentParameterIn_pack);
            equipmentParameter_ueMachine.SetField(equipmentParameterIn_pack[0], equipmentParameterInStruct);

            //返回数据
            MWStructArray[] structArray_Return = new MWStructArray[2];
            structArray_Return[0] = simulatedData_ueMachine;
            structArray_Return[1] = equipmentParameter_ueMachine;

            return structArray_Return;
        }

        //封装补燃锅炉数据
        public MWStructArray[] gasBoiler_pack()
        {
            //输入变量，结构数组
            string[] variableIn = new string[2];
            variableIn[0] = "gear";
            variableIn[1] = "duration";
            MWStructArray variableInStruct = new MWStructArray(1, 1, variableIn);
            variableInStruct.SetField(variableIn[0], simulatedData.Gear_Boiler);
            variableInStruct.SetField(variableIn[1], simulatedData.Duration_Boiler);

            //封装后的gasBoiler输入变量
            string[] variableIn_pack = new string[1];
            variableIn_pack[0] = "gasboiler";
            MWStructArray simulatedData_gasBoiler = new MWStructArray(1, 1, variableIn_pack);
            simulatedData_gasBoiler.SetField(variableIn_pack[0], variableInStruct);

            //设备参数，嵌套结构数组
            //内层结构数组powerH定义 与档位对应的数组
            double[] powerH_gear_Boiler = new double[3];
            powerH_gear_Boiler[0] = equipmentParameter.PowerH_gear_Boiler_1;
            powerH_gear_Boiler[1] = equipmentParameter.PowerH_gear_Boiler_2;
            powerH_gear_Boiler[2] = equipmentParameter.PowerH_gear_Boiler_3;

            string[] equipmentParameter_PowerH = new string[1];
            equipmentParameter_PowerH[0] = "gear";
            MWStructArray powerH = new MWStructArray(1, 1, equipmentParameter_PowerH);
            powerH.SetField(equipmentParameter_PowerH[0], (MWNumericArray)powerH_gear_Boiler);

            //内层结构数组gas定义 与档位对应的数组
            double[] gas_gear_Boiler = new double[3];
            gas_gear_Boiler[0] = equipmentParameter.Gas_gear_Boiler_1;
            gas_gear_Boiler[1] = equipmentParameter.Gas_gear_Boiler_2;
            gas_gear_Boiler[2] = equipmentParameter.Gas_gear_Boiler_3;

            string[] equipmentParameter_Gas = new string[1];
            equipmentParameter_Gas[0] = "gear";
            MWStructArray gas = new MWStructArray(1, 1, equipmentParameter_Gas);
            gas.SetField(equipmentParameter_Gas[0], (MWNumericArray)gas_gear_Boiler);

            //设备参数，结构数组
            string[] equipmentParamaterIn = new string[2];
            equipmentParamaterIn[0] = "powerH";
            equipmentParamaterIn[1] = "gas";
            MWStructArray equipmentParameterInStruct = new MWStructArray(1, 1, equipmentParamaterIn);
            equipmentParameterInStruct.SetField(equipmentParamaterIn[0], powerH);
            equipmentParameterInStruct.SetField(equipmentParamaterIn[1], gas);

            //封装后的ueMachine设备参数
            string[] equipmentParameterIn_pack = new string[1];
            equipmentParameterIn_pack[0] = "gasboiler";
            MWStructArray equipmentParameter_gasboiler = new MWStructArray(1, 1, equipmentParameterIn_pack);
            equipmentParameter_gasboiler.SetField(equipmentParameterIn_pack[0], equipmentParameterInStruct);

            //返回数据
            MWStructArray[] structArray_Return = new MWStructArray[2];
            structArray_Return[0] = simulatedData_gasBoiler;
            structArray_Return[1] = equipmentParameter_gasboiler;

            return structArray_Return;
        }
    }
}
