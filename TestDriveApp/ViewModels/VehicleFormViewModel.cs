using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using TestDriveApp.Models;

namespace TestDriveApp.ViewModels
{
    public class VehicleFormViewModel
    {
        public int? VehicleId { get; set; }

        public int OwnerId { get; set; }

        [Required(ErrorMessage = "The name of the Maker is required.")]
        [StringLength(255, ErrorMessage = "The name of the vehicle cannot exceed 255 characters.")]
        public string Make { get; set; }

        [Display(Name = "Model")]
        [Required(ErrorMessage = "The Model is required.")]
        public string Model { get; set; }

        [Display(Name = "Year")]
        [Required(ErrorMessage = "The Year is required.")]
        public int Year { get; set; }

        [Display(Name = "Transmission")]
        [Required(ErrorMessage = "The transmission Type is required.")]
        public string Transmission { get; set; }

        [Display(Name = "Hourly Rate")]
        [Required(ErrorMessage = "The hourly rate is required.")]
        public decimal HourlyRate { get; set; }
        // Dropdown list data
        // public Image Picture { get; set; }
        
        public string Heading
        {
            get { return VehicleId != 0 ? "Edit Vehicle" : "Add Vehicle"; }
        }

        public VehicleFormViewModel()
        {
            VehicleId = 0;
        }

        public VehicleFormViewModel(Vehicle vehicle)
        {
            VehicleId = vehicle.VehicleId;
            OwnerId = vehicle.OwnerId;
            Make = vehicle.Make;
            Model = vehicle.Model;
            Year = vehicle.Year;
            Transmission = vehicle.Transmission;
            HourlyRate = vehicle.HourlyRate;
        }
    }
}