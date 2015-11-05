using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UENSimulation.Model
{
    class EquipmentParameter
    {
        double maxH_HA;//储热——最大储热量
        double etaInH_HA;//储热——充热能量损失
        double etaOutH_HA;//储热——放热能量损失

        double maxE_EA;//储电——最大储电量
        double maxInE_EA;//储电——最大充电速度
        double maxOutE_EA;//储电——最大放电速度
        double etaInE_EA;//储电——充电能量损失
        double etaOutE_EA;//储电——放电能量损失        

        double power_Heat;//光热——设备最大产热能力

        double power_Electricity;//光伏——设备最大产电能力

        double powerE_UE_1;//泛能机——设备产电能力（一档）
        double powerE_UE_2;//泛能机——设备产电能力（二档）
        double powerE_UE_3;//泛能机——设备产电能力（三档）

        double powerH_UE_1;//泛能机——设备产热能力（一档）
        double powerH_UE_2;//泛能机——设备产热能力（二档）
        double powerH_UE_3;//泛能机——设备产热能力（三档）

        double gas_gear_UE_1;//泛能机——设备消耗燃气量（一档） 
        double gas_gear_UE_2;//泛能机——设备消耗燃气量（二档）
        double gas_gear_UE_3;//泛能机——设备消耗燃气量（三档）

        double powerH_gear_Boiler_1;//补燃锅炉——设备产热能力（一档）
        double powerH_gear_Boiler_2;//补燃锅炉——设备产热能力（二档）
        double powerH_gear_Boiler_3;//补燃锅炉——设备产热能力（三档）

        double gas_gear_Boiler_1;//补燃锅炉——设备消耗燃气量（一档）
        double gas_gear_Boiler_2;//补燃锅炉——设备消耗燃气量（二档）
        double gas_gear_Boiler_3;//补燃锅炉——设备消耗燃气量（三档）        

        public double MaxH_HA
        {
            get { return maxH_HA; }
            set { maxH_HA = value; }
        }

        public double EtaInH_HA
        {
            get { return etaInH_HA; }
            set { etaInH_HA = value; }
        }

        public double EtaOutH_HA
        {
            get { return etaOutH_HA; }
            set { etaOutH_HA = value; }
        }

        public double MaxE_EA
        {
            get { return maxE_EA; }
            set { maxE_EA = value; }
        }

        public double MaxInE_EA
        {
            get { return maxInE_EA; }
            set { maxInE_EA = value; }
        }

        public double MaxOutE_EA
        {
            get { return maxOutE_EA; }
            set { maxOutE_EA = value; }
        }

        public double EtaInE_EA
        {
            get { return etaInE_EA; }
            set { etaInE_EA = value; }
        }

        public double EtaOutE_EA
        {
            get { return etaOutE_EA; }
            set { etaOutE_EA = value; }
        }

        public double Power_Heat
        {
            get { return power_Heat; }
            set { power_Heat = value; }
        }

        public double Power_Electricity
        {
            get { return power_Electricity; }
            set { power_Electricity = value; }
        }

        public double PowerE_UE_1
        {
            get { return powerE_UE_1; }
            set { powerE_UE_1 = value; }
        }

        public double PowerE_UE_2
        {
            get { return powerE_UE_2; }
            set { powerE_UE_2 = value; }
        }

        public double PowerE_UE_3
        {
            get { return powerE_UE_3; }
            set { powerE_UE_3 = value; }
        }

        public double PowerH_UE_1
        {
            get { return powerH_UE_1; }
            set { powerH_UE_1 = value; }
        }

        public double PowerH_UE_2
        {
            get { return powerH_UE_2; }
            set { powerH_UE_2 = value; }
        }

        public double PowerH_UE_3
        {
            get { return powerH_UE_3; }
            set { powerH_UE_3 = value; }
        }

        public double Gas_gear_UE_1
        {
            get { return gas_gear_UE_1; }
            set { gas_gear_UE_1 = value; }
        }

        public double Gas_gear_UE_2
        {
            get { return gas_gear_UE_2; }
            set { gas_gear_UE_2 = value; }
        }

        public double Gas_gear_UE_3
        {
            get { return gas_gear_UE_3; }
            set { gas_gear_UE_3 = value; }
        }

        public double PowerH_gear_Boiler_1
        {
            get { return powerH_gear_Boiler_1; }
            set { powerH_gear_Boiler_1 = value; }
        }

        public double PowerH_gear_Boiler_2
        {
            get { return powerH_gear_Boiler_2; }
            set { powerH_gear_Boiler_2 = value; }
        }

        public double PowerH_gear_Boiler_3
        {
            get { return powerH_gear_Boiler_3; }
            set { powerH_gear_Boiler_3 = value; }
        }

        public double Gas_gear_Boiler_1
        {
            get { return gas_gear_Boiler_1; }
            set { gas_gear_Boiler_1 = value; }
        }

        public double Gas_gear_Boiler_2
        {
            get { return gas_gear_Boiler_2; }
            set { gas_gear_Boiler_2 = value; }
        }

        public double Gas_gear_Boiler_3
        {
            get { return gas_gear_Boiler_3; }
            set { gas_gear_Boiler_3 = value; }
        }
    }
}
