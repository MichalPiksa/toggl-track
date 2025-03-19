using Microsoft.AspNetCore.Components;
using TogglTrack.API.Abstractions;
using TogglTrack.Blazor;
using TogglTrack.Common.Models.Activity;
using TogglTrack.Common.Models.Project;
using TogglTrack.Common.Models.User;

namespace TogglTrack.Web.Components.Pages.ProjectPage
{
    public partial class ProjectListPage
    {
        [Inject]
        public IProjectsClient ProjectsClient { get; set; }
        [Inject]
        public IUsersClient UsersClient { get; set; }
        [Inject]
        public IActivitiesClient ActivitiesClient { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Parameter]
        public Guid? userId { get; set; }
        public bool isButtonVisible = true;
        public UserDetailModel UserProfile { get; set; } = new UserDetailModel
        {
            FirstName = string.Empty,
            LastName = string.Empty,
            PhotoUrl = string.Empty
        };
        public ICollection<ProjectListModel> UserProjects { get; set; } = new List<ProjectListModel>();
        public ActivityDetailModel? InProgressActivity { get; set; } = new ActivityDetailModel
        {
            ActivityType = string.Empty
        };

        protected override async Task OnInitializedAsync()
        {
            UserProjects = await ProjectsClient.GetUserProjectsAsync(userId.Value);
            UserProfile = await UsersClient.GetUserAsync(userId.Value);
            InProgressActivity = await ActivitiesClient.GetUserActiveActivityAsync(userId.Value);
        }

        public async Task EditProject(Guid projectId)
        {
            NavigationManager.NavigateTo($"/users/{userId.Value}/projects/{projectId}/edit");
        }

        public async Task AddProject()
        {
            var newProject = new CreateProjectRequest
            {
                Name = "Enter project name"
            };
            var createdProject = await ProjectsClient.CreateProjectAsync(newProject);
            await ProjectsClient.AddUserToProjectAsync(new AddUserToProjectRequest
            (
                UserId: userId.Value,
                ProjectId: createdProject.Id
            ));
            NavigationManager.NavigateTo($"/users/{userId.Value}/projects/{createdProject.Id}/edit");
        }

        public void Cancel()
        {
            NavigationManager.NavigateTo($"/users");
        }

        public void UserActivities()
        {
            NavigationManager.NavigateTo($"/users/{userId.Value}/activities");
        }

        public async Task StopActivity()
        {
            var stopActivityRequest = new StopActivityRequest
            (
                UserId: userId.Value
            );
            await ActivitiesClient.StopAsync(stopActivityRequest);
            isButtonVisible = false;
        }
    }
}
