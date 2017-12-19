using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace TestDriveApp.Models
{
    public class VehicleCalendar
    {
        public int VehicleCalendarId { get; set; }

        public int VehicleId { get; set; }

        public bool Sunday { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }


    }
}