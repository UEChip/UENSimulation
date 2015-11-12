using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UENSimulation.Model
{
    class Room
    {
        double temperature;//温度
        bool light_zm;//灯光照明
        bool light_zs;//灯光装饰 
        double light_zm_power;//照明功率
        double light_zs_power;//装饰功率
        double acreage;//房间面积
        double electricity_Need;//电负荷
        double heat_Need;//热负荷        

        public Room(double _temperature, bool _light_zm, bool _light_zs, double _light_zm_power, double _light_zs_power, double _acreage)
        {
            Temperature = _temperature;
            Light_zm = _light_zm;
            Light_zs = _light_zs;
            Light_zm_power = _light_zm_power;
            Light_zs_power = _light_zs_power;
            Acreage = _acreage;
        }

        public double Temperature
        {
            get { return temperature; }
            set { temperature = value; }
        }

        public bool Light_zm
        {
            get { return light_zm; }
            set { light_zm = value; }
        }

        public bool Light_zs
        {
            get { return light_zs; }
            set { light_zs = value; }
        }

        public double Light_zm_power
        {
            get { return light_zm_power; }
            set { light_zm_power = value; }
        }

        public double Light_zs_power
        {
            get { return light_zs_power; }
            set { light_zs_power = value; }
        }

        public double Acreage
        {
            get { return acreage; }
            set { acreage = value; }
        }

        public double Electricity_Need
        {
            get { return electricity_Need; }
            set { electricity_Need = value; }
        }

        public double Heat_Need
        {
            get { return heat_Need; }
            set { heat_Need = value; }
        }
    }
}
