using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.User
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } = DateTime.MinValue;
        public string? EmailAdress { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
