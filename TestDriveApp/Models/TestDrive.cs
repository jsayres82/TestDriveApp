using System;
using System.ComponentModel.DataAnnotations;

namespace TestDriveApp.Models
{
    public class TestDrive
    {
        public int TestDriveId { get; set; }

        public int CustomerId { get; set; }

        public DateTime DateTested { get; set; }

        public int? TestDuration { get; set; }
        
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}