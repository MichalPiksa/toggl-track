using Microsoft.AspNetCore.Components;
using TogglTrack.Blazor;
using TogglTrack.Common.Models.Activity;
using TogglTrack.Common.Models.User;

namespace TogglTrack.Web.Components.Pages.ActivityPage
{
    public partial class ActivityListPage
    {
        [Inject]
        public IActivitiesClient ActivitiesClient { get; set; }
        [Inject]
        public IUsersClient UsersClient { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Parameter]
        public Guid UserId { get; set; }
        [Parameter]
        public Guid ProjectId { get; set; }
        public ICollection<ActivityListModel> AllUserActivities { get; set; } = new List<ActivityListModel>();
        public ICollection<ActivityListModel> LastWeekUserActivities { get; set; } = new List<ActivityListModel>();
        public ICollection<ActivityListModel> LastMonthUserActivities { get; set; } = new List<ActivityListModel>();
        public ICollection<ActivityListModel> LastYearUserActivities { get; set; } = new List<ActivityListModel>();
        private bool showAllActivities = false;
        private bool showLastWeekActivities = false;
        private bool showLastMonthActivities = false;
        private bool showLastYearActivities = false;
        private ActivityDetailModel? selectedActivity;
        public ActivityDetailModel? InProgressActivity { get; set; } = new ActivityDetailModel
        {
            ActivityType = string.Empty
        };
        public UserDetailModel? UserProfile { get; set; } = new UserDetailModel
        {
            FirstName = string.Empty,
            LastName = string.Empty,
            PhotoUrl = string.Empty
        };

        protected override async Task OnInitializedAsync()
        {
            AllUserActivities = await ActivitiesClient.GetUserActivitiesAsync(UserId, "");
            LastWeekUserActivities = await ActivitiesClient.GetUserActivitiesAsync(UserId, "lastWeek");
            LastMonthUserActivities = await ActivitiesClient.GetUserActivitiesAsync(UserId, "lastMonth");
            LastYearUserActivities = await ActivitiesClient.GetUserActivitiesAsync(UserId, "lastYear");
            UserProfile = await UsersClient.GetUserAsync(UserId);
            InProgressActivity = await ActivitiesClient.GetUserActiveActivityAsync(UserId);
        }

        private void ShowAllActivities()
        {
            ResetFlags();
            showAllActivities = true;
            selectedActivity = null;
        }

        private void ShowLastWeekActivities()
        {
            ResetFlags();
            showLastWeekActivities = true;
            selectedActivity = null;
        }

        private void ShowLastMonthActivities()
        {
            ResetFlags();
            showLastMonthActivities = true;
            selectedActivity = null;
        }

        private void ShowLastYearActivities()
        {
            ResetFlags();
            showLastYearActivities = true;
            selectedActivity = null;
        }

        private void ResetFlags()
        {
            showAllActivities = false;
            showLastWeekActivities = false;
            showLastMonthActivities = false;
            showLastYearActivities = false;
        }

        private async Task ActivityDetail(Guid activityId)
        {
            selectedActivity = await ActivitiesClient.GetActivityAsync(activityId);
        }

        public void Cancel()
        {
            NavigationManager.NavigateTo($"/users/{UserId}/projects");
        }
    }
}
