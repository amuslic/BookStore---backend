using System.ComponentModel.DataAnnotations;

namespace BookStore.Contracts.User
{
    public class UserUpdateRequestModel
    {
        [Required]
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? EmailAdress { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
