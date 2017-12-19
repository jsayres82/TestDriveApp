using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestDriveApp.Models;

namespace TestDriveApp.Dtos
{
    public class CustomerDto
    {
        public int NumericId { get; set; }

        [Required(ErrorMessage = "Please enter customer's name.")]
        [StringLength(255, ErrorMessage = "The customer name cannot exceed 255 characters.")]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

//        [Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }

        [Required(ErrorMessage = "The membership type is required.")]
        public byte MembershipTypeId { get; set; }
        public string ApplicationUserId { get; set; }

        public MembershipTypeDto MembershipType { get; set; }

        public List<Vehicle> Vehicles { get; set; }        
    }
}