using Microsoft.AspNetCore.Components;
using TogglTrack.API.Abstractions;
using TogglTrack.Blazor;
using TogglTrack.Common.Models.Activity;
using TogglTrack.Common.Models.User;

namespace TogglTrack.Web.Components.Pages.ActivityPage
{
    public partial class ActivityDetailPage
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IActivitiesClient ActivitiesClient { get; set; }
        [Inject]
        public IUsersClient UsersClient { get; set; }
        [Parameter]
        public Guid? userId { get; set; }
        [Parameter]
        public Guid? projectId { get; set; }
        public ActivityDetailModel ActivityData { get; set; } = new ActivityDetailModel
        {
            Id = Guid.NewGuid(),
            ActivityType = "Enter activity type",
            Description = "Enter description"
        };
        public UserDetailModel? UserProfile { get; set; } = new UserDetailModel
        {
            FirstName = string.Empty,
            LastName = string.Empty,
            PhotoUrl = string.Empty
        };

        protected override async Task OnInitializedAsync()
        {
            //ActivityData = await ActivitiesClient.GetActivityAsync(userId.Value);
            UserProfile = await UsersClient.GetUserAsync(userId.Value);
        }

        public async Task StartActivity()
        {
            var startActivityRequest = new StartActivityRequest
            {
                UserId = userId.Value,
                ProjectId = projectId.Value,
                ActivityType = ActivityData.ActivityType,
                Description = ActivityData.Description
            };
            await ActivitiesClient.StartAsync(startActivityRequest);
            NavigationManager.NavigateTo($"/users/{userId}/projects");
        }

        public async Task StopActivity()
        {
            var stopActivityRequest = new StopActivityRequest
            (
                UserId: userId.Value
            );
            await ActivitiesClient.StopAsync(stopActivityRequest);
        }

        public void Cancel()
        {
            NavigationManager.NavigateTo($"/users/{userId}/projects");
        }
    }
}
