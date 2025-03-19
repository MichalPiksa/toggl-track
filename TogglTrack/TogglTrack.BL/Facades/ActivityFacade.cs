using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TogglTrack.BL.Facades.Interfaces;
using TogglTrack.BL.Services;
using TogglTrack.Common.Models.Activity;
using TogglTrack.Common.Models.Project;
using TogglTrack.DAL;
using TogglTrack.DAL.Repositories;

namespace TogglTrack.BL.Facades
{
    public class ActivityFacade : FacadeBase<ActivityEntity, ActivityListModel, ActivityDetailModel>, IActivityFacade
    {
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IRepository<ProjectEntity> _projectRepository;
        private readonly BusinessService _businessService;
        public ActivityFacade(IMapper mapper, IRepository<ActivityEntity> repository, IRepository<UserEntity> userRepository, IRepository<ProjectEntity> projectRepository, BusinessService businessService)
            : base(mapper, repository)
        {
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _businessService = businessService;
        }
        public async Task StartActivityAsync(Guid userId, Guid projectId, string activityType, string description)
        {
            var isUserExist = await _userRepository.ExistsAsync(userId);
            var isProjectExist = await _projectRepository.ExistsAsync(projectId);
            if (!isUserExist)
            {
                throw new ArgumentException("User does not exist", nameof(userId));
            }
            if (!isProjectExist)
            {
                throw new ArgumentException("Project does not exist", nameof(projectId));
            }
            var project = await _projectRepository.GetByIdAsync(projectId);
            if (!project.Users.Any(x => x.Id == userId))
            {
                await _businessService.AddUserToProjectAsync(userId, projectId);
            }
            await StopActivityAsync(userId);

            await repository.InsertAsync(new ActivityEntity()
            {
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now,
                ActivityType = activityType,
                Description = description,
                UserId = userId,
                ProjectId = projectId
            });
        }
        public async Task StopActivityAsync(Guid userId)
        {
            // Give me active Activity with empty EndDate
            var activeActivity = await repository
                .GetAll()
                .Where(activity => activity.UserId == userId && activity.EndTime == null)
                .FirstOrDefaultAsync();
            if (activeActivity == null)
            {
                return;
            }
            activeActivity.EndTime = DateTime.Now;
            await repository.UpdateAsync(activeActivity);
        }
        public async Task<IEnumerable<ActivityListModel>> GetUserActivitiesByFilterAsync(Guid userId, string? filter)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user is null)
            {
                throw new ArgumentException($"User with id {userId} does not exist", nameof(userId));
            }
            if (filter == "lastWeek")
            {
                var lastWeekActivities = await repository.GetAll()
                    .Where(activity => activity.UserId == userId && activity.StartTime >= DateTime.Now.AddDays(-7))
                    .OrderBy(activity => activity.StartTime)
                    .ToListAsync();
                return mapper.Map<IEnumerable<ActivityListModel>>(lastWeekActivities);
            }
            else if (filter == "lastMonth")
            {
                var lastMonthActivities = await repository.GetAll()
                    .Where(activity => activity.UserId == userId && activity.StartTime >= DateTime.Now.AddMonths(-1))
                    .OrderBy(activity => activity.StartTime)
                    .ToListAsync();
                return mapper.Map<IEnumerable<ActivityListModel>>(lastMonthActivities);
            }
            else if (filter == "lastYear")
            {
                var lastYearActivities = await repository.GetAll()
                    .Where(activity => activity.UserId == userId && activity.StartTime >= DateTime.Now.AddYears(-1))
                    .OrderBy(activity => activity.StartTime)
                    .ToListAsync();
                return mapper.Map<IEnumerable<ActivityListModel>>(lastYearActivities);
            }
            var allUserActivities = await repository.GetAll()
                .Where(activity => activity.UserId == userId)
                .OrderBy(activity => activity.StartTime)
                .ToListAsync();
            return mapper.Map<IEnumerable<ActivityListModel>>(allUserActivities);
        }

        public async Task<ActivityDetailModel?> GetActiveUserActivityAsync(Guid userId)
        {
            var activeActivity = await repository.GetAll()
                .Where(a => a.UserId == userId && a.EndTime == null)
                .FirstOrDefaultAsync();
            return activeActivity is null ? null : mapper.Map<ActivityDetailModel>(activeActivity);
        }
    }
}
