using BookStore.Domain;
using BookStore.Domain.User;
using static BookStore.Domain.IOperationResponse;

namespace BookStore.Application.User
{
    public interface IUserHandler
    {
        Task<IOperationResponse<UserModel?>> GetUser(int userId, CancellationToken cancellationToken);
        Task<IOperationResponse<IEnumerable<UserModel>>?> GetUsers(int count, CancellationToken cancellationToken);
        Task<OperationResult> CreateUser(UserModel createUser, CancellationToken cancellationToken);
        Task<OperationResult> UpdateUser(UpdateUser updateUser, CancellationToken cancellationToken);
        Task<OperationResult> DeleteUser(string userId, CancellationToken cancellationToken);       
    }
}
