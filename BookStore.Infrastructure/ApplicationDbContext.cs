using BookStore.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<BookEntity> Books => Set<BookEntity>();
        public DbSet<LoanEntity> Loans => Set<LoanEntity>();
        public DbSet<LoanHistoryEntity> LoanHistories => Set<LoanHistoryEntity>();

        public ApplicationDbContext(
                DbContextOptions<ApplicationDbContext> options
            ) : base(options)
        {
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
