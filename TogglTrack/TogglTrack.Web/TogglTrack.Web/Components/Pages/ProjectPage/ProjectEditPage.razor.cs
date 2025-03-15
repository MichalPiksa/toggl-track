using AutoMapper;
using Microsoft.AspNetCore.Components;
using TogglTrack.API.Abstractions;
using TogglTrack.Blazor;
using TogglTrack.Common.Models.Project;
using TogglTrack.Common.Models.User;

namespace TogglTrack.Web.Components.Pages.ProjectPage
{
    public partial class ProjectEditPage
    {
        private readonly IMapper mapper;

        public ProjectEditPage(IMapper mapper)
        {
            this.mapper = mapper;
        }
        [Inject]
        public IProjectsClient ProjectsClient { get; set; }
        [Inject]
        public IUsersClient UsersClient { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Parameter]
        public Guid? userId { get; set; }
        [Parameter]
        public Guid? projectId { get; set; }
        public ProjectDetailModel Data { get; set; } = new ProjectDetailModel
        {
            Id = Guid.NewGuid(),
            Name = string.Empty
        };

        protected override async Task OnInitializedAsync()
        {
            Data = await ProjectsClient.GetProjectByIdAsync(projectId.Value);
            //Data = mapper.Map<UserDetailModel>(user);
        }

        public async void SaveProject()
        {
            var createProjectRequest = mapper.Map<CreateProjectRequest>(Data);
            await ProjectsClient.UpdateOrCreateProjectAsync(projectId.Value, createProjectRequest);
            NavigationManager.NavigateTo($"/users/{userId.Value}/projects");
        }
        public void Cancel()
        {
            NavigationManager.NavigateTo($"/users/{userId.Value}/projects");
        }
        public void DeleteProject(Guid id)
        {
            ProjectsClient.DeleteProjectAsync(id);
            NavigationManager.NavigateTo($"/users/{userId}/projects");
        }
    }
}
