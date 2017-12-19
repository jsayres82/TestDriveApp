using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestDriveApp.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Please enter customer's name.")]
        [StringLength(255, ErrorMessage = "The customer name cannot exceed 255 characters.")]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        [Display(Name = "Date of Birth")]
        [Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }

        [Display(Name = "Membership Type")]
        [Required(ErrorMessage = "The membership type is required.")]
        public byte MembershipTypeId { get; set; }

        public string ApplicationUserId { get; set; }
        
        // Navigation Properties
        public MembershipType MembershipType { get; set; }

        public List<Vehicle> Vehicles { get; set; }

        public List<TestDrive> TestDrives { get; set; }
    }
}