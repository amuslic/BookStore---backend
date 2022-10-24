using BookStore.Domain;
using BookStore.Domain.User;
using static BookStore.Domain.IOperationResponse;

namespace BookStore.Application.User
{
    public class UserHandler : IUserHandler
    {
        //layer potentialy not needed since it only calls repository.
        //done for adding additional logics in the future to cleary separate business and infrascture layers ( should have its own models and responses)

        private readonly IUserRepository _userRepository;
        public UserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<IOperationResponse<DomainUser?>> GetUser(int userId, CancellationToken cancellationToken)
        {
            return _userRepository.GetUser(userId, cancellationToken);
        }

        public Task<IOperationResponse<IEnumerable<DomainUser>>> GetUsers(int count, CancellationToken cancellationToken)
        {
            return _userRepository.GetUsers(count, cancellationToken);
        }

        public Task<OperationResult> CreateUser(DomainUser createUser, CancellationToken cancellationToken)
        {
            return _userRepository.CreateUser(createUser, cancellationToken);
        }
        public Task<OperationResult> UpdateUser(CancellationToken cancellationToken)
        {
            return _userRepository.UpdateUser(cancellationToken);
        }

        public Task<OperationResult> DeleteUser(string userId, CancellationToken cancellationToken)
        {
            return _userRepository.DeleteUser(userId, cancellationToken);
        }
    }
}
