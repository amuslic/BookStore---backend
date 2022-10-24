namespace BookStore.Application.BookStoreInformation
{
    public interface IBookStoreInformationHandler
    {
        Task GetBookRentHistory(string bookId, CancellationToken cancellationToken);
        Task GetUsersWithOverdueRents(CancellationToken cancellationToken, int count = 10);
    }
}
