using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UENSimulation.Model
{
    class Weather
    {
        private string temperature;
        private string weatherCondition;
        private string windDirection;

        public string pictToday;
        public string pictTomorrow;

        public string tmTemperature;
        public string tmWeather;


        public string Temperature
        {
            get { return this.temperature; }
            set { this.temperature = value; }
        }

        public string WeatherCondition
        {
            get { return this.weatherCondition; }
            set { this.weatherCondition = value; }
        }

        public string WindDirection
        {
            get { return this.windDirection; }
            set { this.windDirection = value; }
        }
    }
}
