using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UENSimulation.Model
{
    class EnergyNeed
    {
        double electricity_Need;//电需求
        double heat_Need;//热需求
        double mode;//模式       

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

        public double Mode
        {
            get { return mode; }
            set { mode = value; }
        }
    }
}
