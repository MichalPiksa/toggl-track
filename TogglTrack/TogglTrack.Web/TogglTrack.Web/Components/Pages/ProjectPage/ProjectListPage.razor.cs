using Microsoft.AspNetCore.Components;
using TogglTrack.API.Abstractions;
using TogglTrack.Blazor;
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
        public NavigationManager NavigationManager { get; set; }
        [Parameter]
        public Guid? userId { get; set; }
        public ICollection<ProjectListModel> Projects { get; set; } = new List<ProjectListModel>();

        protected override async Task OnInitializedAsync()
        {
            Projects = await ProjectsClient.GetProjectsAsync();
        }

        public async Task EditProject(Guid projectId)
        {
            //var userGuidId = userId.Value;
            //// Add user to project
            //var request = new AddUserToProjectRequest
            //(
            //    UserId : userId.Value,
            //    ProjectId: projectId
            //);
            //await ProjectsClient.AddUserToProjectAsync(request);

            NavigationManager.NavigateTo($"/users/{userId.Value}/projects/{projectId}/edit");
        }

        public async Task AddProject()
        {
            var newProject = new CreateProjectRequest(
                ProjectName: "Enter project name"
            );
            var createdProject = await ProjectsClient.CreateProjectAsync(newProject);
            NavigationManager.NavigateTo($"/users/{userId.Value}/projects/{createdProject.Id}/edit");
        }

        public void Cancel()
        {
            NavigationManager.NavigateTo($"/users");
        }

        public async Task<ICollection<ProjectListModel>> UserProjectsAsync()
        {
            var user = await UsersClient.GetUserAsync(userId.Value);
            return user.Projects;
        }

        public void UserActivities()
        {
            NavigationManager.NavigateTo($"/users/{userId.Value}/activities");
        }
    }
}
