using TogglTrack.DAL;
using TogglTrack.DAL.Repositories;

namespace TogglTrack.BL.Services
{
    public class BusinessService
    {
        private readonly Repository<UserEntity> userRepository;
        private readonly Repository<ProjectEntity> projectRepository;

        public BusinessService(Repository<UserEntity> userRepository, Repository<ProjectEntity> projectRepository)
        {
            this.userRepository = userRepository;
            this.projectRepository = projectRepository;
        }
        public async Task AddUserToProjectAsync(Guid userId, Guid projectId)
        {
            var user = await userRepository.GetByIdAsync(userId);
            var project = await projectRepository.GetByIdAsync(projectId);
            if (user == null || project == null)
            {
                throw new ArgumentException("Item does not exist", user == null ? nameof(user) : nameof(project));
            }
            project.Users.Add(user);
            await projectRepository.UpdateAsync(project);
        }
    }
}
