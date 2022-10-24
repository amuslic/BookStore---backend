using BookStore.Domain;
using BookStore.Domain.User;
using static BookStore.Domain.IOperationResponse;

namespace BookStore.Application.User
{
    public interface IUserHandler
    {
        Task<IOperationResponse<DomainUser?>> GetUser(int userId, CancellationToken cancellationToken);
        Task<IOperationResponse<IEnumerable<DomainUser>>?> GetUsers(int count, CancellationToken cancellationToken);
        Task<OperationResult> CreateUser(DomainUser createUser, CancellationToken cancellationToken);
        Task<OperationResult> UpdateUser(CancellationToken cancellationToken);
        Task<OperationResult> DeleteUser(string userId, CancellationToken cancellationToken);       
    }
}
