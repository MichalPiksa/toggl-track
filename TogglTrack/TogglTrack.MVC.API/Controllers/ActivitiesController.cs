using Microsoft.AspNetCore.Mvc;
using TogglTrack.API.Abstractions;
using TogglTrack.BL.Facades;
using TogglTrack.BL.Facades.Interfaces;
using TogglTrack.Common.Models.Activity;

namespace TogglTrack.MVC.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivityFacade _activityFacade;
        private readonly IUserFacade _userFacade;

        public ActivitiesController(IActivityFacade activityFacade, IUserFacade userFacade)
        {
            _activityFacade = activityFacade;
            _userFacade = userFacade;
        }

        [HttpPost("start", Name = nameof(StartAsync))]
        public async Task StartAsync(StartActivityRequest request)
        {
            await _activityFacade.StartActivityAsync(request.UserId, request.ProjectId,request.ActivityType, request.Description);
        }

        [HttpPost("stop", Name = nameof(StopAsync))]
        public async Task StopAsync(StopActivityRequest request)
        {
            await _activityFacade.StopActivityAsync(request.UserId);
        }

        [HttpGet(Name = nameof(GetActivities))]
        public IEnumerable<ActivityListModel> GetActivities()
        {
            return _activityFacade.GetAll();
        }

        [HttpGet("{activityId}", Name = nameof(GetActivityAsync))]
        public async Task<ActivityDetailModel?> GetActivityAsync(Guid activityId)
        {
            return await _activityFacade.GetByIdAsync(activityId);
        }

        [HttpGet("{userId}/useractivities", Name = nameof(GetUserActivitiesAsync))]
        public async Task<ActionResult<IEnumerable<ActivityListModel>>> GetUserActivitiesAsync(Guid userId, string? filter)
        {
            try
            {
                var searchedUser = await _userFacade.GetByIdAsync(userId);
                if (searchedUser is not null)
                {
                    if (searchedUser.Activities.Any())
                    {
                        return Ok(await _activityFacade.GetUserActivitiesByFilterAsync(userId, filter));
                    }
                    else
                    {
                        return NotFound("No activity found.");
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

        [HttpDelete("{activityId}", Name = nameof(DeleteActivityAsync))]
        public async Task DeleteActivityAsync(Guid activityId)
        {
            await _activityFacade.DeleteAsync(activityId);
        }
    }
}
