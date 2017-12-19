using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestDriveApp.Models;

namespace TestDriveApp.ViewModels
{
    public class CustomerFormViewModel
    {
        public int? NumericId { get; set; }

        [Required(ErrorMessage = "Please enter customer's name.")]
        [StringLength(255, ErrorMessage = "The customer name cannot exceed 255 characters.")]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        [Display(Name = "Date of Birth")]
        [Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }

        [Display(Name = "Membership Type")]
        [Required(ErrorMessage = "The membership type is required.")]
        public byte? MembershipTypeId { get; set; }

        // Data for membersghip types dropdown list
        public IEnumerable<MembershipType> MembershipTypes { get; set; }

        public string ApplicationUserId { get; set; }
        
        public string Heading
        {
            get { return NumericId != 0 ? "Edit Customer" : "Add Customer"; }
        }

        public CustomerFormViewModel(string userId)
        {
            ApplicationUserId = userId;
            NumericId = 0;
        }

        public CustomerFormViewModel(Customer customer)
        {
            if (NumericId != 0)
                NumericId = customer.NumericId;
            ApplicationUserId = customer.ApplicationUserId;
            Name = customer.Name;
            IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            Birthdate = customer.Birthdate;
            MembershipTypeId = customer.MembershipType;
        }
    }
}