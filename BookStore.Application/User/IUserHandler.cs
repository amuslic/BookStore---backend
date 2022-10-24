using BookStore.Domain;
using BookStore.Domain.User;
using static BookStore.Domain.IOperationResponse;

namespace BookStore.Application.User
{
    public interface IUserHandler
    {
        Task<IOperationResponse<CreateUser?>> GetUser(string userId, CancellationToken cancellationToken);
        Task<IOperationResponse<IEnumerable<CreateUser>>?> GetUsers(int count, CancellationToken cancellationToken);
        Task<OperationResult> CreateUser(CreateUser createUser, CancellationToken cancellationToken);
        Task<OperationResult> UpdateUser(CancellationToken cancellationToken);
        Task<OperationResult> DeleteUser(string userId, CancellationToken cancellationToken);       
    }
}
