﻿using System.ComponentModel.DataAnnotations;

namespace TestDriveApp.Models
{
    public class MembershipType
    {
        public byte Id { get; set; }

        [Required(ErrorMessage = "The membership type name is required.")]
        public string Name { get; set; }

        public short SignUpFee { get; set; }

        public byte DurationInMonths { get; set; }

        public byte DiscountRate { get; set; }


        // static variables for Min18YearsIfAMember validation
        public static readonly byte Unknown = 0;
        public static readonly byte PayAsYouGo = 1;
    }
}