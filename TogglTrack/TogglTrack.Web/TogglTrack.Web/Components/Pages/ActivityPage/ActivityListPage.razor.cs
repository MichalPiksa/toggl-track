using Microsoft.AspNetCore.Components;
using TogglTrack.Blazor;
using TogglTrack.Common.Models.Activity;

namespace TogglTrack.Web.Components.Pages.ActivityPage
{
    public partial class ActivityListPage
    {
        [Inject]
        public IActivitiesClient ActivitiesClient { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Parameter]
        public Guid userId { get; set; }
        [Parameter]
        public Guid projectId { get; set; }
        public ICollection<ActivityListModel> Activities { get; set; } = new List<ActivityListModel>();
        protected override async Task OnInitializedAsync()
        {
            Activities = await ActivitiesClient.GetActivitiesAsync();
        }
    }
}
