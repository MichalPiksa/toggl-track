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
        private readonly IProjectFacade _projectFacade;
        private readonly IUserFacade _userFacade;
        private readonly BusinessService _businessService;

        public ProjectsController(IProjectFacade projectFacade, IUserFacade userFacade, BusinessService businessService)
        {
            _projectFacade = projectFacade;
            _userFacade = userFacade;
            _businessService = businessService;
        }
        [HttpPost("create", Name = nameof(CreateProjectAsync))]
        public async Task<ProjectDetailModel> CreateProjectAsync(CreateProjectRequest request)
        {
            return await _projectFacade.CreateProjectAsync(request.Name);
        }

        [HttpPost("add-user-to-project", Name = nameof(AddUserToProjectAsync))]
        public async Task AddUserToProjectAsync(AddUserToProjectRequest request)
        {
            await _businessService.AddUserToProjectAsync(request.UserId, request.ProjectId);
        }

        [HttpGet(Name = nameof(GetProjectsAsync))]
        public IEnumerable<ProjectListModel> GetProjectsAsync()
        {
            return _projectFacade.GetAll();
        }

        [HttpGet("{projectId}", Name = nameof(GetProjectByIdAsync))]
        public async Task<ProjectDetailModel?> GetProjectByIdAsync(Guid projectId)
        {
            return await _projectFacade.GetByIdAsync(projectId);
        }

        [HttpGet("{userId}/user-projects", Name = nameof(GetUserProjectsAsync))]
        public async Task<IEnumerable<ProjectListModel>> GetUserProjectsAsync(Guid userId)
        {
            return await _projectFacade.GetUserProjectsAsync(userId);
        }

        [HttpPut("{projectId}", Name = nameof(UpdateOrCreateProjectAsync))]
        public async Task<IActionResult> UpdateOrCreateProjectAsync(Guid projectId, CreateProjectRequest request)
        {
            var updatedProject = new ProjectDetailModel() 
            {
                Id = projectId,
                Name = request.Name
            };
            await _projectFacade.SaveAsync(updatedProject);
            return Ok(updatedProject);
        }

        [HttpDelete("{projectId}", Name = nameof(DeleteProjectAsync))]
        public async Task DeleteProjectAsync(Guid projectId)
        {
            await _projectFacade.DeleteAsync(projectId);
        }
    }
}
