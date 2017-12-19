using System;
using System.ComponentModel.DataAnnotations;

namespace TestDriveApp.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;

            if (customer.MembershipType == MembershipType.Unknown || 
                customer.MembershipType == MembershipType.PayAsYouGo)
            {
                return ValidationResult.Success;
            }

            if (customer.Birthdate == null)
            {
                return new ValidationResult("Birthdate is required.");
            }

            var age = DateTime.Today.Year - customer.Birthdate.Value.Year;

            return (age >= 18 
                ? ValidationResult.Success 
                : new ValidationResult("Customer should be atleast 18 years old to go on a membership."));
        }
    }
}