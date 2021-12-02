using GMap.NET.MapProviders;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace NeXT_1._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            map.MapProvider = GMapProviders.GoogleMap;
            map.Position = new PointLatLng(0, 0);
            map.Zoom = 10;
            TimePicker.Format = DateTimePickerFormat.Time;
            TimePicker.ShowUpDown = true;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string detime = DatePicker.Value.ToString("yyyy-MM-dd") +" "+TimePicker.Value.ToString("HH:mm:ss");
            WhereISS iss = new WhereISS();
            WhereISS.Info theOne = iss.RunOne(detime);
            List<WhereISS.Info> plots = iss.Run(detime);
            List<string> utc = iss.createDatesUTC(detime);
            textBox1.Clear();
            for (int i = 0; i < utc.Count; i++)
            {
                textBox1.AppendText($"{i+1})  {utc[i]}{Environment.NewLine}");
            }
            //foreach (var time in utc)
            //{
            //    textBox1.AppendText(time+Environment.NewLine);
            //}

            map.DragButton = MouseButtons.Left;
            map.MapProvider = GMapProviders.GoogleMap;
            //double lat = Convert.ToDouble(theOne.latitude);
            //double lon = Convert.ToDouble(theOne.longitude);
            map.Position = new PointLatLng(theOne.latitude, theOne.longitude);
            map.MinZoom = 1;
            map.MaxZoom = 100;
            map.Zoom = 5;

            GMapRoute line_layer;
            GMapOverlay line_overlay = new GMapOverlay();
            line_layer = new GMapRoute("single_line");
            line_layer.Stroke = new Pen(Brushes.Red, 2);

            line_overlay.Routes.Add(line_layer);
            map.Overlays.Add(line_overlay);
            foreach (var plot in plots)
            {
                line_layer.Points.Add(new PointLatLng(plot.latitude,plot.longitude));
                var marker = new GMarkerGoogle(new PointLatLng(plot.latitude, plot.longitude), GMarkerGoogleType.blue_small);
                marker.IsVisible = true;
                line_overlay.Markers.Add(marker);
            }
            map.UpdateRouteLocalPosition(line_layer);
            //map.Position = new PointLatLng(plots.FirstOrDefault().latitude, plots.FirstOrDefault().longitude);

            //GMapMarker marker = new GMarkerCross(new PointLatLng(10,10));
            //line_overlay.Markers.Add(marker);
        }

    }
}
