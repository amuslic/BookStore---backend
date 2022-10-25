using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.User
{
    public class UserModel
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
