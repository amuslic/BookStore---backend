using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Infrastructure.Entities
{
    public class UserEntity
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAdress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime UpdatedTime { get; set; } = DateTime.Now;
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public IEnumerable<LoanEntity> Loans { get; set; }
        public IEnumerable<LoanHistoryEntity> LoanHistories { get; set; }
    }
}
