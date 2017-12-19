using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace TestDriveApp.Models
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public string OwnerId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Transmission { get; set; }
        public bool IsAvailable { get; set; }
        public decimal HourlyRate { get; set; }
        public VehicleCalendar Calender { get; set; }
        //public Image Picture { get; set; }

    }
}