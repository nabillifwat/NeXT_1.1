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

namespace NeXT_1._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            TimePicker.Format = DateTimePickerFormat.Time;
            TimePicker.ShowUpDown = true;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            map.MapProvider = GMapProviders.GoogleMap;
            double lat = Convert.ToDouble(12);
            double lon = Convert.ToDouble(12);
            map.Position = new GMap.NET.PointLatLng(lat,lon);
            map.MinZoom = 5;
            map.MaxZoom = 100;
            map.Zoom = 10;
        }

    }
}
