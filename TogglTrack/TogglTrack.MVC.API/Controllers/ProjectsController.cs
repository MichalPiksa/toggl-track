using Microsoft.AspNetCore.Mvc;
using TogglTrack.API.Abstractions;
using TogglTrack.BL.Facades.Interfaces;
using TogglTrack.BL.Services;
using TogglTrack.Common.Models.Project;

namespace TogglTrack.MVC.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectFacade facade;
        private readonly BusinessService businessService;

        public ProjectsController(IProjectFacade facade, BusinessService businessService)
        {
            this.facade = facade;
            this.businessService = businessService;
        }
        [HttpPost("create", Name = nameof(CreateProjectAsync))]
        public async Task<ProjectDetailModel> CreateProjectAsync(CreateProjectRequest request)
        {
            return await facade.CreateProjectAsync(request.ProjectName);
        }

        [HttpPost("addusertoproject", Name = nameof(AddUserToProjectAsync))]
        public async Task AddUserToProjectAsync(AddUserToProjectRequest request)
        {
            await businessService.AddUserToProjectAsync(request.UserId, request.ProjectId);
        }

        [HttpGet(Name = nameof(GetProjectsAsync))]
        public IEnumerable<ProjectListModel> GetProjectsAsync()
        {
            return facade.GetAll();
        }

        [HttpGet("{projectId}", Name = nameof(GetProjectByIdAsync))]
        public async Task<ProjectDetailModel?> GetProjectByIdAsync(Guid projectId)
        {
            return await facade.GetByIdAsync(projectId);
        }

        [HttpPut("{projectId}", Name = nameof(UpdateOrCreateProjectAsync))]
        public async Task<IActionResult> UpdateOrCreateProjectAsync(Guid ProjectId, CreateProjectRequest request)
        {
            var updatedProject = new ProjectDetailModel() 
            {
                Id = ProjectId,
                Name = request.ProjectName
            };
            await facade.SaveAsync(updatedProject);
            return Ok(updatedProject);
        }

        [HttpDelete("{projectId}", Name = nameof(DeleteProjectAsync))]
        public async Task DeleteProjectAsync(Guid projectId)
        {
            await facade.DeleteAsync(projectId);
        }
    }
}
