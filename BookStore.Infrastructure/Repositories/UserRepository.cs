using BookStore.Application.User;
using BookStore.Domain;
using BookStore.Domain.User;
using BookStore.Infrastructure.Entities;
using Mapster;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static BookStore.Domain.IOperationResponse;

namespace BookStore.Infrastructure
{
    // todo - implement caching 
    // todo - implement retries (polly) on certain status codes
    internal class UserRepository : BaseRepository, IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(ApplicationDbContext context, ILogger<UserRepository> logger) : base(context)

        {
            _logger = logger;
        }

        public async Task<IOperationResponse<UserModel?>> GetUser(int userId, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Fetching user with id {userId}", userId);
            try
            {
                var user = await _context.Users.FindAsync(new object?[] { userId }, cancellationToken);

                if (user == null)
                {
                    _logger.LogInformation("User with id {userId} not Found", userId);
                    return OperationResponse.Error<UserModel>(OperationResult.NotFound);
                }

                return OperationResponse.Success(user.Adapt<UserModel>());
            }

            catch (Exception exception)
            {
                _logger.LogError(exception, "Error while fetching the user");
                return OperationResponse.Error<UserModel>(OperationResult.UnknownError);
            }
        }

        public async Task<IOperationResponse<IEnumerable<UserModel>>?> GetUsers(int count, CancellationToken cancellationToken)
        {
            try
            {
                var users = await _context.Users.Take(count)
                    .ProjectToType<UserModel>()
                    .ToListAsync(cancellationToken: cancellationToken);

                _logger.LogInformation("Returning {Count} users", users.Count);

                return OperationResponse.Success<IReadOnlyList<UserModel>>(users);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error while fetching users");
                return OperationResponse.Error<IReadOnlyList<UserModel>>(OperationResult.UnknownError);
            }
        }

        public async Task<OperationResult> CreateUser(UserModel createUser, CancellationToken cancellationToken)
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
                _logger.LogError(exception, "Error while creating the sure");
                return OperationResult.UnknownError;
            }
        }

        public async Task<OperationResult> UpdateUser(int userId, JsonPatchDocument<UpdateUserModel> patchDocument, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating user with id {userId}", userId);

            try
            {
                var user = await _context.Users.FindAsync(new object?[] { userId }, cancellationToken);

                if (user == null)
                {
                    _logger.LogInformation("User with id {userId} not Found", userId);
                    return OperationResult.NotFound;
                }

                var entityPatchDocument = patchDocument.Adapt<JsonPatchDocument<UserEntity>>();
                entityPatchDocument.ApplyTo(user);
                _context.SaveChanges();

                return OperationResult.Succeeded;
            }

            catch (Exception exception)
            {
                _logger.LogError(exception, "Error while updating the user");
                return OperationResult.UnknownError;
            }
        }

        public async Task<OperationResult> DeleteUser(int userId, CancellationToken cancellationToken)
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
                _context.SaveChanges();

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
