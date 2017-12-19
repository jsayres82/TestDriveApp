using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using TestDriveApp.Models;

namespace TestDriveApp.Dtos
{
    public class VehicleDto
    {
        public int VehicleId { get; set; }
        
        public string OwnerId { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }
        
        public int Year { get; set; }
        
        public string Transmission { get; set; }

        public bool IsAvailable { get; set; }

        public decimal HourlyRate { get; set; }
        //public Image Picture { get; set; }
    }
}