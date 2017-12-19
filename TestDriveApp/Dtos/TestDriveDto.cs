using System;
using System.Collections.Generic;
using TestDriveApp.Models;

namespace TestDriveApp.Dtos
{
    public class NewTestDriveDto
    {
        public int TestDriveId { get; set; }
        public int CustomerId { get; set; }
        public DateTime DateTested { get; set; }
        public int? TestDuration { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

    }
}