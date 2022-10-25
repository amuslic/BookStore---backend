using BookStore.Domain;
using BookStore.Domain.User;
using Microsoft.AspNetCore.JsonPatch;
using static BookStore.Domain.IOperationResponse;

namespace BookStore.Application.User
{
    public interface IUserHandler
    {
        Task<IOperationResponse<UserModel?>> GetUser(int userId, CancellationToken cancellationToken);
        Task<IOperationResponse<IEnumerable<UserModel>>?> GetUsers(int count, CancellationToken cancellationToken);
        Task<OperationResult> CreateUser(UserModel createUser, CancellationToken cancellationToken);
        Task<OperationResult> UpdateUser(int userId, JsonPatchDocument<UpdateUserModel> patchDocument, CancellationToken cancellationToken);
        Task<OperationResult> DeleteUser(int userId, CancellationToken cancellationToken);       
    }
}
