using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Infrastructure.Entities
{
    public class BookEntity
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string NumberOfCopies { get; set; }
        public string NumberOfRentedCopies { get; set; }
        public IEnumerable<LoanEntity> Loans { get; set; }
        public IEnumerable<LoanHistoryEntity> LoanHistories { get; set; }
    }
}
