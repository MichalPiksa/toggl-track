using Microsoft.AspNetCore.Mvc;
using TogglTrack.API.Abstractions;
using TogglTrack.BL.Facades.Interfaces;
using TogglTrack.Common.Models.Activity;
using TogglTrack.Common.Models.Project;
using TogglTrack.Common.Models.User;

namespace TogglTrack.MVC.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserFacade _userFacade;

        public UsersController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        [HttpGet(Name = nameof(GetUsers))]
        public IEnumerable<UserListModel> GetUsers()
        {
            return _userFacade.GetAll();
        }

        [HttpGet("{userId}", Name = nameof(GetUserAsync))]
        public async Task<UserDetailModel?> GetUserAsync(Guid userId)
        {
            return await _userFacade.GetByIdAsync(userId);
        }

        [HttpGet("{userId}/projects", Name = nameof(GetUserProjectsAsync))]
        public async Task<ActionResult<IEnumerable<ProjectListModel>>> GetUserProjectsAsync(Guid userId)
        {
            try
            {
                var searchedUser = await _userFacade.GetByIdAsync(userId);
                if (searchedUser is not null)
                {
                    if (searchedUser.Projects.Any())
                    {
                        return Ok(searchedUser.Projects);
                    }
                    else
                    {
                        return NotFound("No project found.");
                    }
                }
                else
                {
                    return NotFound("User not found.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create", Name = nameof(CreateUserAsync))]
        public async Task<UserDetailModel> CreateUserAsync(CreateUserRequest request)
        {
            return await _userFacade.CreateUserAsync(request.FirstName, request.LastName, request.PhotoUrl);
        }

        [HttpPut("{userId}", Name = nameof(UpdateOrCreateUserAsync))]
        public async Task<IActionResult> UpdateOrCreateUserAsync(Guid userId, CreateUserRequest request)
        {
            var updatedUser = new UserDetailModel
            {
                Id = userId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhotoUrl = request.PhotoUrl,
            };
            await _userFacade.SaveAsync(updatedUser);
            return Ok(updatedUser);
        }

        [HttpDelete("{userId}", Name = nameof(DeleteUserAsync))]
        public async Task DeleteUserAsync(Guid userId)
        {
            await _userFacade.DeleteAsync(userId);
        }
    }
}
