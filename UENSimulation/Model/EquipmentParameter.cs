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
        double maxTH_HA;//储热——最大储热水温
        double etaInH_HA;//储热——充热能量损失
        double etaOutH_HA;//储热——放热能量损失

        double maxE_EA;//储电——最大储电量
        double maxInE_EA;//储电——最大充电速度
        double maxOutE_EA;//储电——最大放电速度
        double etaInE_EA;//储电——充电能量损失
        double etaOutE_EA;//储电——放电能量损失

        double powerH_gear_Boiler;//补燃锅炉——设备产热能力
        double gas_gear_Boiler;//补燃锅炉——设备消耗燃气量

        double power_Heat;//光热——设备最大产热能力

        double power_Electricity;//光伏——设备最大产电能力

        double powerE_UE;//泛能机——设备产电能力
        double powerH_UE;//泛能机——设备产热能力
        double gas_gear_UE;//泛能机——设备消耗燃气量  
        double gear_UE;//泛能机——泛能机档位

        double priceE;//能源价格——电价
        double priceH;//能源价格——热价
        double priceG;//能源价格——气价

        double electricity;//能源限额——电
        double photoelectricity;//能源限额——光电
        double optothermal;//能源限额——光伏

        public double MaxH_HA
        {
            get { return maxH_HA; }
            set { maxH_HA = value; }
        }

        public double MaxTH_HA
        {
            get { return maxTH_HA; }
            set { maxTH_HA = value; }
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

        public double PowerH_gear_Boiler
        {
            get { return powerH_gear_Boiler; }
            set { powerH_gear_Boiler = value; }
        }

        public double Gas_gear_Boiler
        {
            get { return gas_gear_Boiler; }
            set { gas_gear_Boiler = value; }
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

        public double PowerE_UE
        {
            get { return powerE_UE; }
            set { powerE_UE = value; }
        }

        public double PowerH_UE
        {
            get { return powerH_UE; }
            set { powerH_UE = value; }
        }

        public double Gas_gear_UE
        {
            get { return gas_gear_UE; }
            set { gas_gear_UE = value; }
        }

        public double Gear_UE
        {
            get { return gear_UE; }
            set { gear_UE = value; }
        }

        public double PriceE
        {
            get { return priceE; }
            set { priceE = value; }
        }

        public double PriceH
        {
            get { return priceH; }
            set { priceH = value; }
        }

        public double PriceG
        {
            get { return priceG; }
            set { priceG = value; }
        }

        public double Electricity
        {
            get { return electricity; }
            set { electricity = value; }
        }

        public double Photoelectricity
        {
            get { return photoelectricity; }
            set { photoelectricity = value; }
        }

        public double Optothermal
        {
            get { return optothermal; }
            set { optothermal = value; }
        }
    }
}
