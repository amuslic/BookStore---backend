using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Entities
{
    public class LoanEntity
    {
        [Key]
        public int LoanId { get; set; }

        public int BookId { get; set; }
        public BookEntity Book { get; set; }

        public int UserId { get; set; }
        public UserEntity User { get; set; }

        public DateTime RentedTime { get; set; }
      
        public DateTime DueDate { get; set; }
    }
}
