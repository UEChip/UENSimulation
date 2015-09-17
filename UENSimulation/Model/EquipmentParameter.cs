using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UENSimulation.Model
{
    class EquipmentParameter
    {
        int maxH_HA;//储热——最大储热量
        int maxTH_HA;//储热——最大储热水温
        int etaInH_HA;//储热——充热能量损失
        int etaOutH_HA;//储热——放热能量损失

        int maxE_EA;//储电——最大储电量
        int maxInE_EA;//储电——最大充电速度
        int maxOutE_EA;//储电——最大放电速度
        int etaInE_EA;//储电——充电能量损失
        int etaOutE_EA;//储电——放电能量损失

        int powerH_gear_Boiler;//补燃锅炉——设备产热能力
        int gas_gear_Boiler;//补燃锅炉——设备消耗燃气量

        int power_Heat;//光热——设备最大产热能力

        int power_Electricity;//光伏——设备最大产电能力

        int powerE_UE;//泛能机——设备产电能力
        int powerH_UE;//泛能机——设备产热能力
        int gas_gear_UE;//泛能机——设备消耗燃气量  
        int gear_UE;//泛能机——泛能机档位

        public int MaxH_HA
        {
            get { return maxH_HA; }
            set { maxH_HA = value; }
        }

        public int MaxTH_HA
        {
            get { return maxTH_HA; }
            set { maxTH_HA = value; }
        }

        public int EtaInH_HA
        {
            get { return etaInH_HA; }
            set { etaInH_HA = value; }
        }

        public int EtaOutH_HA
        {
            get { return etaOutH_HA; }
            set { etaOutH_HA = value; }
        }

        public int MaxE_EA
        {
            get { return maxE_EA; }
            set { maxE_EA = value; }
        }

        public int MaxInE_EA
        {
            get { return maxInE_EA; }
            set { maxInE_EA = value; }
        }

        public int MaxOutE_EA
        {
            get { return maxOutE_EA; }
            set { maxOutE_EA = value; }
        }

        public int EtaInE_EA
        {
            get { return etaInE_EA; }
            set { etaInE_EA = value; }
        }

        public int EtaOutE_EA
        {
            get { return etaOutE_EA; }
            set { etaOutE_EA = value; }
        }

        public int PowerH_gear_Boiler
        {
            get { return powerH_gear_Boiler; }
            set { powerH_gear_Boiler = value; }
        }

        public int Gas_gear_Boiler
        {
            get { return gas_gear_Boiler; }
            set { gas_gear_Boiler = value; }
        }

        public int Power_Heat
        {
            get { return power_Heat; }
            set { power_Heat = value; }
        }

        public int Power_Electricity
        {
            get { return power_Electricity; }
            set { power_Electricity = value; }
        }

        public int PowerE_UE
        {
            get { return powerE_UE; }
            set { powerE_UE = value; }
        }

        public int PowerH_UE
        {
            get { return powerH_UE; }
            set { powerH_UE = value; }
        }

        public int Gas_gear_UE
        {
            get { return gas_gear_UE; }
            set { gas_gear_UE = value; }
        }

        public int Gear_UE
        {
            get { return gear_UE; }
            set { gear_UE = value; }
        }
    }
}
