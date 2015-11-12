using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UENSimulation.Model;
using UENSimulation.WeatherServer;

namespace UENSimulation.Utility
{
    class WeatherGet
    {
        static WeatherServer.WeatherWebServiceSoapClient w = new WeatherServer.WeatherWebServiceSoapClient("WeatherWebServiceSoap");

        //获得天气状况
        public Weather GetWeather(string city)
        {
            string[] weatherMsg = w.getWeatherbyCityName(city);
            if (weatherMsg == null || weatherMsg.Length == 0)
                return null;
            Weather weather = new Weather();
            weather.Temperature = weatherMsg[5];
            weather.WeatherCondition = weatherMsg[6];
            weather.WindDirection = weatherMsg[7];
            //weather.pictToday = Application.StartupPath + "\\Resource\\a_" + weatherMsg[8];

            weather.tmTemperature = weatherMsg[12];
            weather.tmWeather = weatherMsg[13];
            //weather.pictTomorrow = Application.StartupPath + "\\Resource\\a_" + weatherMsg[15];
            return weather;
        }

        public string GetWeather()
        {
            string getWeatherUrl = "http://m.weather.com.cn/data/101230101.html";

            WebRequest webReq = WebRequest.Create(getWeatherUrl);
            WebResponse webResp = webReq.GetResponse();
            Stream stream = webResp.GetResponseStream();

            StreamReader sr = new StreamReader(stream, Encoding.UTF8);
            string html = sr.ReadToEnd();

            sr.Close();
            stream.Close();

            return html;
        }

        public string WeatherInfoGet()
        {
            string json = GetWeather();

            WeatherInfo weatherIofo = JsonConvert.DeserializeObject<WeatherInfo>(json);
            string line = "";

            foreach (KeyValuePair<string, string> kv in weatherIofo.Weatherinfo)
            {
                line += string.Format("{0}-->{1}\r\n", kv.Key, kv.Value);
            }

            return line;
        }

        [Serializable]
        class WeatherInfo
        {
            [JsonProperty("weatherinfo")]
            public Dictionary<string, string> Weatherinfo { get; set; }
        }
    }
}
