using GMap.NET;
using GMap.NET.WindowsForms;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeXT_1._1
{
    class WhereISS
    {
        public List<Info> Run(string detime)
        {
            //List<Info> one = getPosition(detime);
            //var theFirst = one.FirstOrDefault();
            //
            string ticks = createDates(detime);
            List<Info> values = getPositions(ticks);
            return values;
        }
        public Info RunOne(string detime)
        {
            var time =  DateTime.Parse(detime);
            Int32 unixTimestamp = (Int32)(time.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            Info one = getPosition(unixTimestamp.ToString());
            var theOne = one;
            return theOne;
        }
        public string createDates(string dTime)
        {
            DateTime curTime = DateTime.Parse(dTime);
            DateTime startTime = curTime.AddHours(-1);
            DateTime endTime = curTime.AddHours(1);
            string ticks = "";
            for (DateTime time = startTime; time <= endTime; time = time.AddMinutes(10))
            {
                Int32 unixTimestamp = (Int32)(time.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                if (time == startTime)
                {
                    
                    ticks += unixTimestamp;
                }
                else
                {
                    ticks += "," + unixTimestamp;
                }

            }
            return ticks;
        }
        public List<string> createDatesUTC(string dTime)
        {
            DateTime curTime = DateTime.Parse(dTime);
            DateTime startTime = curTime.AddHours(-1);
            DateTime endTime = curTime.AddHours(1);
            List<string> ticks = new List<string>();
            for (DateTime time = startTime; time <= endTime; time = time.AddMinutes(10))
            {
                ticks.Add(time.ToUniversalTime().ToString());
            }
            return ticks;
        }
        private List<Info> getPositions(string ticks)
        {
            //string position = "https://api.wheretheiss.at/v1/satellites/25544/positions?timestamps=1436029892,1436029902&units=km";
            string position = $"https://api.wheretheiss.at/v1/satellites/25544/positions?timestamps={ticks}";
            var client = new RestClient(position);
            var response = client.Execute(new RestRequest());
            List<Info> items = JsonConvert.DeserializeObject<List<Info>>(response.Content);
            return items;
        }
        private Info getPosition(string ticks)
        {
            //string position = "https://api.wheretheiss.at/v1/satellites/25544/positions?timestamps=1436029892,1436029902&units=km";
            string position = $"https://api.wheretheiss.at/v1/satellites/25544/positions?timestamps={ticks}";
            var client = new RestClient(position);
            var response = client.Execute(new RestRequest());
            List<Info> items = JsonConvert.DeserializeObject<List<Info>>(response.Content);
            var item = items.FirstOrDefault();
            return item;
        }

        private string getMapping(float lat, float lon)
        {
            //string position = "https://api.wheretheiss.at/v1/satellites/25544/positions?timestamps=1436029892,1436029902&units=km";
            string map = $"https://api.wheretheiss.at/v1/coordinates/37.795517,-122.393693";
            var client = new RestClient(map);
            var response = client.Execute(new RestRequest());
            return response.Content;
        }

        public void GMAP()
        {
            //GMapOverlay polyOverlay = new GMapOverlay("polygons");
            //IList<PointLatLng> points = new List<PointLatLng>();
            //points.Add(new PointLatLng(-25.969562, 32.585789));
            //points.Add(new PointLatLng(-25.966205, 32.588171));
            //GMapPolygon polygon = new GMapPolygon(points, "mypolygon");
            //polygon.Fill = new SolidBrush(Color.FromArgb(50, Color.Red));
            //polygon.Stroke = new Pen(Color.Red, 1);
            //polyOverlay.Polygons.Add(polygon);
            //gmap.Overlays.Add(polyOverlay);
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
