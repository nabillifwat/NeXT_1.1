using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeXT_1._1
{
    class WhereISS
    {
        public void testRun()
        {

            string ticks = createDates(DateTime.Now.ToString());
            string dis = getPosition(ticks);
            List<Info> asd = JsonConvert.DeserializeObject<List<Info>>(dis);
            asd.FirstOrDefault();
            var value = getMapping(asd.FirstOrDefault().latitude, asd.FirstOrDefault().longitude);
        }

        private string createDates(string dTime)
        {
            DateTime curTime = DateTime.Parse(dTime);
            DateTime startTime = curTime.AddHours(-1);
            DateTime endTime = curTime.AddHours(1);
            string ticks = "";
            //List<DateTime> detime = new List<DateTime>();
            //DateTime time = startTime;
            //while (time <= endTime)
            //{
            //    detime.Add(time);
            //    time.AddMinutes(30);
            //}
            for (DateTime time = startTime; time <= endTime; time = time.AddMinutes(30))
            {
                if (time == startTime)
                {
                    ticks += (time.Ticks / 100000000);
                }
                else
                {
                    ticks += "," + (time.Ticks / 100000000);
                }

            }
            //var displayTime = detime.Select(x => x.Ticks.ToString());
            return ticks;
        }
        private string createDates2()
        {
            DateTime curTime = DateTime.Parse(DateTime.Now.ToString());
            DateTime startTime = curTime.AddHours(-1);
            DateTime endTime = curTime.AddHours(1);
            string ticks = "";
            for (DateTime time = startTime; time <= endTime; time = time.AddMinutes(30))
            {
                if (time == startTime)
                {
                    ticks += (time / 100000000);
                }
                else
                {
                    ticks += "," + (time.Ticks / 100000000);
                }

            }
            //var displayTime = detime.Select(x => x.Ticks.ToString());
            return ticks;
        }
        private string getPosition(string ticks)
        {
            //string position = "https://api.wheretheiss.at/v1/satellites/25544/positions?timestamps=1436029892,1436029902&units=km";
            string position = $"https://api.wheretheiss.at/v1/satellites/25544/positions?timestamps={ticks}";
            var client = new RestClient(position);
            var response = client.Execute(new RestRequest());
            return response.Content;
        }

        private string getMapping(float lat, float lon)
        {
            //string position = "https://api.wheretheiss.at/v1/satellites/25544/positions?timestamps=1436029892,1436029902&units=km";
            string map = $"https://api.wheretheiss.at/v1/coordinates/37.795517,-122.393693";
            var client = new RestClient(map);
            var response = client.Execute(new RestRequest());
            return response.Content;
        }

        public class Info
        {
            public string name { get; set; }
            public int id { get; set; }
            public float latitude { get; set; }
            public float longitude { get; set; }
            public float altitude { get; set; }
            public float velocity { get; set; }
            public string visibility { get; set; }
            public float footprint { get; set; }
            public float timestamp { get; set; }
            public float daynum { get; set; }
            public float solar_lat { get; set; }
            public float solar_lon { get; set; }
            public string units { get; set; }

        }
    }
}
