namespace BookStore.Contracts.User
{
    //todo - add email validation
    //todo - add max length for strings

    public class UpdateUserRequestModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? EmailAdress { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
