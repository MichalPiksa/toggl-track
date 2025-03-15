using TogglTrack.Common.Models.Activity;

namespace TogglTrack.BL.Facades.Interfaces
{
    public interface IActivityFacade : IFacade<ActivityListModel, ActivityDetailModel>
    {
        Task StartActivityAsync(Guid userId, Guid projectId, string activityType, string description);
        Task StopActivityAsync(Guid userId);
        Task<IEnumerable<ActivityListModel>> GetUserActivitiesByFilterAsync(Guid userId, string? filter);
    }
}
