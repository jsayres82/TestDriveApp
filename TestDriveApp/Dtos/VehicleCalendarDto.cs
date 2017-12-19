using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using TestDriveApp.Models;


namespace TestDriveApp.Dtos
{
    public class VehicleCalendarDto
    {
        public int CalenderId { get; set; }
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