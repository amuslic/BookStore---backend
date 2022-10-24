﻿using System.ComponentModel.DataAnnotations;

namespace BookStore.Contracts.Models
{
    public class UserCreateRequestModel
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public DateTime DateOfBirth { get; set; } = DateTime.MinValue;
        public string? EmailAdress { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
