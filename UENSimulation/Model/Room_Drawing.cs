using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UENSimulation.Model
{
    class Room_Drawing : Room
    {
        bool tv;//电视        
        bool audio;//音响
        double tv_power;//电视功率
        double audio_power;//音响功率

        public Room_Drawing(double _temperature, bool _light_zm, bool _light_zs, double _light_zm_power, double _light_zs_power, double _acreage, bool _tv, bool _audio, double _tv_power, double _audio_power)
            : base(_temperature, _light_zm, _light_zs, _light_zm_power, _light_zs_power, _acreage)
        {
            Tv = _tv;
            Audio = _audio;
            Tv_power = _tv_power;
            Audio_power = _audio_power;
        }

        public bool Tv
        {
            get { return tv; }
            set { tv = value; }
        }

        public bool Audio
        {
            get { return audio; }
            set { audio = value; }
        }

        public double Tv_power
        {
            get { return tv_power; }
            set { tv_power = value; }
        }

        public double Audio_power
        {
            get { return audio_power; }
            set { audio_power = value; }
        }
    }
}
