namespace BookStore.Application.BookStoreInformation
{
    public class BookStoreInformationHandler : IBookStoreInformationHandler
    {
        public Task GetBookRentHistory(string bookId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task GetUsersWithOverdueRents(int count = 10, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task GetUsersWithOverdueRents(CancellationToken cancellationToken, int count = 10)
        {
            throw new NotImplementedException();
        }
    }
}
