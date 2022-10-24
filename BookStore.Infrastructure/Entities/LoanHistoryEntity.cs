using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Infrastructure.Entities
{
    public class LoanHistoryEntity
    {
        [Key]
        public int LoanHistoryId { get; set; }
        
        [ForeignKey("BookId")]
        public int BookId { get; set; }
        public BookEntity Book  { get; set; }
      
        public int UserId { get; set; }
        public UserEntity User { get; set; }
      
        public DateTime RentedTime { get; set; }
        public DateTime ReturnedTime { get; set; }
        public bool WasOverdue
        {
            get
            {
                return ReturnedTime > RentedTime;
            }
        }
    }
}
