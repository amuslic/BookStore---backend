namespace BookStore.Contracts.Models
{
    //todo - add email validation
    public class UserResponseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? EmailAdress { get; set; }
        public string? PhoneNumber { get; set; }

    }
}
