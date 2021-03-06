﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UENSimulation.Model
{
    class SimulatedData
    {
        double charge_HA;//储热——充放热状态(+1/-1)
        double h_HA;//储热——储存或释放的热量（正值）
        double savedH_HA;//储热——当前存储热量

        double charge_EA;//储电——充放电状态
        double duration_EA;//储电——运行时间
        double speed_EA;//储电——速度       
        double savedE_EA;//储电——当前存储电量

        double gear_Boiler;//补燃锅炉——补燃锅炉负荷
        double duration_Boiler;//补燃锅炉——运行时间

        double envrmtdata_lout_Heat;//光热——阳光强度参数
        double duration_Heat;//光热——运行时间

        double envrmtdata_lout_Electricity;//光伏——阳光强度参数       
        double duration_Electricity;//光伏——运行时间

        double gear_UE;//泛能机——泛能机负荷
        double duration_UE;//泛能机——运行时间  

        double price_E;//电价
        double price_H;//热价
        double price_G;//气价       

        public double Charge_HA
        {
            get { return charge_HA; }
            set { charge_HA = value; }
        }

        public double H_HA
        {
            get { return h_HA; }
            set { h_HA = value; }
        }

        public double SavedH_HA
        {
            get { return savedH_HA; }
            set { savedH_HA = value; }
        }

        public double Charge_EA
        {
            get { return charge_EA; }
            set { charge_EA = value; }
        }

        public double Duration_EA
        {
            get { return duration_EA; }
            set { duration_EA = value; }
        }

        public double Speed_EA
        {
            get { return speed_EA; }
            set { speed_EA = value; }
        }

        public double SavedE_EA
        {
            get { return savedE_EA; }
            set { savedE_EA = value; }
        }

        public double Gear_Boiler
        {
            get { return gear_Boiler; }
            set { gear_Boiler = value; }
        }

        public double Duration_Boiler
        {
            get { return duration_Boiler; }
            set { duration_Boiler = value; }
        }

        public double Envrmtdata_lout_Heat
        {
            get { return envrmtdata_lout_Heat; }
            set { envrmtdata_lout_Heat = value; }
        }

        public double Duration_Heat
        {
            get { return duration_Heat; }
            set { duration_Heat = value; }
        }

        public double Envrmtdata_lout_Electricity
        {
            get { return envrmtdata_lout_Electricity; }
            set { envrmtdata_lout_Electricity = value; }
        }

        public double Duration_Electricity
        {
            get { return duration_Electricity; }
            set { duration_Electricity = value; }
        }

        public double Gear_UE
        {
            get { return gear_UE; }
            set { gear_UE = value; }
        }

        public double Duration_UE
        {
            get { return duration_UE; }
            set { duration_UE = value; }
        }

        public double Price_E
        {
            get { return price_E; }
            set { price_E = value; }
        }

        public double Price_H
        {
            get { return price_H; }
            set { price_H = value; }
        }

        public double Price_G
        {
            get { return price_G; }
            set { price_G = value; }
        }
    }
}
