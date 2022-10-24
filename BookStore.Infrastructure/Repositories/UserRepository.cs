using BookStore.Application.User;
using BookStore.Domain;
using BookStore.Domain.User;
using BookStore.Infrastructure.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static BookStore.Domain.IOperationResponse;

namespace BookStore.Infrastructure
{
    // todo - implement caching 
    internal class UserRepository : BaseRepository, IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(ApplicationDbContext context, ILogger<UserRepository> logger) : base(context)

        {
            _logger = logger;
        }

        public async Task<IOperationResponse<CreateUser?>> GetUser(int userId, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Fetching user with id {userId}", userId);
            try
            {
                var user = await _context.Users.FindAsync(new object?[] { userId }, cancellationToken);

                if (user == null)
                {
                    _logger.LogInformation("User with id {userId} not Found", userId);
                    return OperationResponse.Error<CreateUser>(OperationResult.NotFound);
                }

                return OperationResponse.Success(user.Adapt<CreateUser>());
            }

            catch (Exception exception)
            {
                _logger.LogError(exception, "Error while fetching the user");
                return OperationResponse.Error<CreateUser>(OperationResult.UnknownError);
            }
        }

        public async Task<IOperationResponse<IEnumerable<CreateUser>>?> GetUsers(int count, CancellationToken cancellationToken)
        {
            try
            {
                var users = await _context.Users.Take(count)
                    .ProjectToType<CreateUser>()
                    .ToListAsync(cancellationToken: cancellationToken);

                _logger.LogInformation("Returning {Count} users", users.Count);

                return OperationResponse.Success<IReadOnlyList<CreateUser>>(users);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error on permission listing");
                return OperationResponse.Error<IReadOnlyList<CreateUser>>(OperationResult.UnknownError);
            }
        }

        public async Task<OperationResult> CreateUser(CreateUser createUser, CancellationToken cancellationToken)
        {
            try
            {
                var userEntity = createUser.Adapt<UserEntity>();
                await _context.AddAsync(userEntity, cancellationToken);
                _context.SaveChanges();
                return OperationResult.Succeeded;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error on permission listing");
                return OperationResult.UnknownError;
            }
        }

        public Task<OperationResult> UpdateUser(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> DeleteUser(string userId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting user with id {userId}", userId);
            try
            {
                var user = await _context.Users.FindAsync(new object?[] { userId }, cancellationToken);

                if (user == null)
                {
                    _logger.LogInformation("User with id {userId} not Found", userId);
                    return OperationResult.NotFound;
                }

                _context.Users.Remove(user);

                return OperationResult.Succeeded;
            }

            catch (Exception exception)
            {
                _logger.LogError(exception, "Error while fetching the user");
                return OperationResult.UnknownError;
            }
        }
    }
}
