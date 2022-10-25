using BookStore.Domain;
using BookStore.Domain.User;
using Microsoft.AspNetCore.JsonPatch;
using static BookStore.Domain.IOperationResponse;

namespace BookStore.Application.User
{
    public interface IUserRepository
    {
        Task<IOperationResponse<UserModel?>> GetUser(int userId, CancellationToken cancellationToken);
        Task<IOperationResponse<IEnumerable<UserModel>>> GetUsers(int count, CancellationToken cancellationToken);
        Task<OperationResult> CreateUser(UserModel createUser, CancellationToken cancellationToken);
        Task<OperationResult> UpdateUser(string userId, JsonPatchDocument<UpdateUserModel> patchModel, CancellationToken cancellationToken);
        Task<OperationResult> DeleteUser(string userId, CancellationToken cancellationToken);
    }
}
