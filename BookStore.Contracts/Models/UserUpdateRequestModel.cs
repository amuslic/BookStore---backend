﻿namespace BookStore.Contracts.User
{
    public class UserUpdateRequestModel
    {
        public string? UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAdress { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
