using Microsoft.AspNetCore.Mvc;
using TogglTrack.API.Abstractions;
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

        [HttpGet("{userId}/user-activities", Name = nameof(GetUserActivitiesAsync))]
        public async Task<IEnumerable<ActivityListModel>> GetUserActivitiesAsync(Guid userId, string? filter)
        {
            return await _activityFacade.GetUserActivitiesByFilterAsync(userId, filter);
        }

        [HttpGet("{userId}/user-active-activity", Name = nameof(GetUserActiveActivityAsync))]
        public async Task<ActivityDetailModel> GetUserActiveActivityAsync(Guid userId)
        {
            var activeActivity = await _activityFacade.GetActiveUserActivityAsync(userId);
            if (activeActivity == null)
            {
                return new ActivityDetailModel { ActivityType = null };
            }
            return activeActivity;
        }

        [HttpDelete("{activityId}", Name = nameof(DeleteActivityAsync))]
        public async Task DeleteActivityAsync(Guid activityId)
        {
            await _activityFacade.DeleteAsync(activityId);
        }
    }
}
