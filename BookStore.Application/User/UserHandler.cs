using BookStore.Domain;
using BookStore.Domain.User;
using Microsoft.AspNetCore.JsonPatch;
using static BookStore.Domain.IOperationResponse;

namespace BookStore.Application.User
{
    public class UserHandler : IUserHandler
    {
        //layer potentialy not needed since it only calls repository.
        //done for adding additional logics in the future to cleary separate business and infrascture layers ( should have its own models and responses)

        private readonly IUserRepository _userRepository;
        private readonly IJsonPatchDocumentValidator _jsonPatchDocumentValidator;

        public UserHandler(IUserRepository userRepository, IJsonPatchDocumentValidator jsonPatchDocumentValidator)
        {
            _userRepository = userRepository;
            _jsonPatchDocumentValidator = jsonPatchDocumentValidator;
        }
        public Task<IOperationResponse<UserModel?>> GetUser(int userId, CancellationToken cancellationToken)
        {
            return _userRepository.GetUser(userId, cancellationToken);
        }

        public Task<IOperationResponse<IEnumerable<UserModel>>> GetUsers(int count, CancellationToken cancellationToken)
        {
            return _userRepository.GetUsers(count, cancellationToken);
        }

        public Task<OperationResult> CreateUser(UserModel createUser, CancellationToken cancellationToken)
        {
            return _userRepository.CreateUser(createUser, cancellationToken);
        }
        public Task<OperationResult> UpdateUser(int userId, JsonPatchDocument<UpdateUserModel> patchDocument, CancellationToken cancellationToken)
        {
            var isValid = _jsonPatchDocumentValidator.ValidateJsonPatchDocument(patchDocument);
            if (!isValid)
            {
                return Task.FromResult(OperationResult.ValidationError);
            }
            return _userRepository.UpdateUser(userId, patchDocument, cancellationToken);
        }

        public Task<OperationResult> DeleteUser(int userId, CancellationToken cancellationToken)
        {
            return _userRepository.DeleteUser(userId, cancellationToken);
        }
    }
}
