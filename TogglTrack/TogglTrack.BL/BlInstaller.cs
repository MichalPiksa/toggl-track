using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using TogglTrack.BL.Facades;
using TogglTrack.BL.Facades.Interfaces;
using TogglTrack.BL.MapperProfiles;
using TogglTrack.BL.Services;
using TogglTrack.DAL;
using TogglTrack.DAL.Repositories;

namespace TogglTrack.BL
{
    public static class BlInstaller
    {
        public static IServiceCollection AddBusinessLayer(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IRepository<UserEntity>,UserRepository>();
            serviceCollection.AddTransient<IRepository<ProjectEntity>, ProjectRepository>();
            serviceCollection.AddTransient<IRepository<ActivityEntity>, ActivityRepository>();

            serviceCollection.AddTransient<BusinessService>();

            serviceCollection.AddTransient<IProjectFacade, ProjectFacade>();
            serviceCollection.AddTransient<IActivityFacade, ActivityFacade>();
            serviceCollection.AddTransient<IUserFacade, UserFacade>();
            serviceCollection.AddAutoMapper(expression =>
            {
                expression.AddProfile(new ProjectMapperProfile());
                expression.AddProfile(new ActivityMapperProfile());
                expression.AddProfile(new UserMapperProfile());
            });
            return serviceCollection;
        }
    }
}
