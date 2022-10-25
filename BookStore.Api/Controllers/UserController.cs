using BookStore.Application.User;
using BookStore.Contracts.Models;
using BookStore.Contracts.User;
using BookStore.Domain;
using BookStore.Domain.User;
using Mapster;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStoreApi.Controllers
{
    //todo - add fluent validation
    //todo - add exception controller


    /// <summary>
    /// 
    /// </summary>
    [Route("bookstore/api/")]
    [ApiController]
    [ApiVersion("1.0")]
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
        [Route("v{version:apiVersion}/users")]
        [ProducesResponseType(200, Type = typeof(UserModel))]
        [ProducesResponseType(500)]
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
                        var userModels = users.Response.Adapt<IReadOnlyList<UserResponseModel>>();
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
        [Route("v{version:apiVersion}/users/{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserModel>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetUser(
            int userId,
            CancellationToken cancellationToken)
        {

            var user = await _userHandler.GetUser(userId, cancellationToken);

            switch (user.OperationResult)
            {
                case OperationResult.Succeeded:
                    {
                        var userModel = user.Response.Adapt<UserResponseModel>();
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
        /// Create a new user
        /// </summary>
        /// <param name="createUserRequestModel">User properties of the user we want to create</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("v{version:apiVersion}/users")]
        [ProducesResponseType(200, Type = typeof(UserModel))]
        [ProducesResponseType(500)]
        public async Task<ActionResult> CreateUser(
            UserCreateRequestModel createUserRequestModel,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating user");

            var user = createUserRequestModel.Adapt<UserModel>();

            var createResponse = await _userHandler.CreateUser(user, cancellationToken);
            switch (createResponse)
            {
                case OperationResult.Succeeded:
                    {
                        var userModel = user.Adapt<UserResponseModel>();
                        return Ok(userModel);
                    }
                default:
                    {
                        return Problem("Unknown error while trying to retrieve users", statusCode: (int?)HttpStatusCode.InternalServerError);
                    }

            }
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="userId">Id of the user we want to retrieve</param>
        /// <param name="patchDocument">Patch operations for fields we want to update</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH 
        ///       {
        ///          "path": "/firstName",
        ///          "op": "replace",
        ///          "value": "updated first Name"
        ///       }
        /// </remarks>
        [HttpPatch]
        [Route("v{version:apiVersion}/users/{userId}")]
        [ProducesResponseType(200, Type = typeof(UserModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateUser(
            int userId,
            [FromBody] JsonPatchDocument<UserUpdateRequestModel> patchDocument,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating User");

            if (!patchDocument.Operations.Any())
            {
                return BadRequest("Patch operations array cannot be empty");
            }

            var updateUserModel = patchDocument.Adapt<JsonPatchDocument<UpdateUserModel>>();

            var updateResponse = await _userHandler.UpdateUser(userId, updateUserModel, cancellationToken);

            switch (updateResponse)
            {
                case OperationResult.Succeeded:
                    {

                        return Ok();
                    }
                case OperationResult.NotFound:
                    {
                        return NotFound("User with provided id was not found");
                    }
                default:
                    {
                        return Problem("Unknown error while trying to update user", statusCode: (int?)HttpStatusCode.InternalServerError);
                    }

            }
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="userId">Id of the user we want to retrieve</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("v{version:apiVersion}/users/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteUser(
            int userId,
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
                        return NotFound("User with provided id was not found");
                    }
                default:
                    {
                        return Problem("Unknown error while trying to delete user", statusCode: (int?)HttpStatusCode.InternalServerError);
                    }

            }
        }
    }
}