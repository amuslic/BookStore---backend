using BookStore.Application.User;
using BookStore.Contracts.Models;
using BookStore.Domain;
using BookStore.Domain.User;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Net;
using static BookStoreApi.Models.ErrorResponse;

namespace BookStoreApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [OpenApiTag("BookStore User API")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserHandler _userHandler;

        public UserController(
            ILogger<UserController> logger,
            IUserHandler userHandler)
        {
            _logger = logger;
            _userHandler = userHandler;
        }

        /// <summary>
        /// Retrieve users
        /// </summary>
        /// <param name="count">Number of users to return, default is 100</param>
        /// <param name="cancellationToken"></param>
        /// <returns>List of user</returns>
        [HttpGet]
        [Route("bookstore/api/v{version:apiVersion}/users")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(UserModel), Description = "The user")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(ErrorResponseModel), Description = "Unknown error while trying to retrieve users")]
        public async Task<ActionResult> GetUsers(
            CancellationToken cancellationToken,
            int count = 100)
        {
            _logger.LogInformation("Retrieving {count} users", count);

            var users = await _userHandler.GetUsers(count, cancellationToken);

            switch (users.OperationResult)
            {
                case OperationResult.Succeeded:
                    {
                        var userModels = users.Adapt<IReadOnlyList<UserModel>>();
                        return Ok(userModels);
                    }
                default:
                    {
                        return Problem("Unknown error while trying to retrieve users", statusCode: (int?)HttpStatusCode.InternalServerError);
                    }

            }
        }

        /// <summary>
        /// Retrieve user by provided id
        /// </summary>
        /// <param name="userId">Id of the user we want to retrieve</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User</returns>
        [HttpGet]
        [Route("bookstore/api/v{version:apiVersion}/users/{userId}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(UserModel), Description = "The user")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(ErrorResponseModel), Description = "User with provided id was not found")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(ErrorResponseModel), Description = "Uknown error while trying to fetch users")]
        public async Task<ActionResult> GetUser(
            int userId,
            CancellationToken cancellationToken)
        {

            var user = await _userHandler.GetUser(userId, cancellationToken);

            switch (user.OperationResult)
            {
                case OperationResult.Succeeded:
                    {
                        var userModel = user.Response.Adapt<UserModel>();
                        return Ok(userModel);
                    }
                case OperationResult.NotFound:
                    {
                        return NotFound("User with provided id was not found");
                    }
                default:
                    {
                        return Problem("Unknown error while trying to retrieve users", statusCode: (int?)HttpStatusCode.InternalServerError);
                    }

            }
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="createUserRequestModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("bookstore/api/v{version:apiVersion}/users")]
        [SwaggerResponse(HttpStatusCode.Created, typeof(UserModel), Description = "Created user")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(ErrorResponseModel), Description = "Site name is invalid")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(ErrorResponseModel), Description = "Unknow error while trying to create user")]
        public async Task<ActionResult> CreateUser(
            UserCreateRequestModel createUserRequestModel,
            CancellationToken cancellationToken)
        {
            //add fluent validation 
            _logger.LogInformation("Creating user");

            var user = createUserRequestModel.Adapt<DomainUser>();

            var createResponse = await _userHandler.CreateUser(user, cancellationToken);
            switch (createResponse)
            {
                case OperationResult.Succeeded:
                    {
                        var userModel = user.Adapt<UserModel>();
                        return Ok(userModel);
                    }
                case OperationResult.NotFound:
                    {
                        return BadRequest("User with provided id was not found");
                    }
                default:
                    {
                        return Problem("Unknown error while trying to retrieve users", statusCode: (int?)HttpStatusCode.InternalServerError);
                    }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("bookstore/api/v{version:apiVersion}/users/{userId}")]
        //[SwaggerResponse(HttpStatusCode.Created, typeof(ShippingAdvanceShippingNoticeResponseModel), Description = "The advance shipping notice was created.")]
        //[SwaggerResponse(HttpStatusCode.BadRequest, typeof(ErrorResponseModel), Description = "Site name is invalid")]
        public async Task<ActionResult> UpdateUser(
        string userId,
        CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating user");

            await _userHandler.DeleteUser(userId, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("bookstore/api/v{version:apiVersion}/users/{userId}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(UserModel), Description = "The advance shipping notice was created.")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(ErrorResponseModel), Description = "User with provided id not found")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(ErrorResponseModel), Description = "Unknown error while trying to delete user")]
        public async Task<ActionResult> DeleteUser(
            string userId,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating user");

            var deleteResponse = await _userHandler.DeleteUser(userId, cancellationToken);
            switch (deleteResponse)
            {
                case OperationResult.Succeeded:
                    {
                        return Ok();
                    }
                case OperationResult.NotFound:
                    {
                        return BadRequest("User with provided id was not found");
                    }
                default:
                    {
                        return Problem("Unknown error while trying to delete user", statusCode: (int?)HttpStatusCode.InternalServerError);
                    }

            }
        }
    }
}