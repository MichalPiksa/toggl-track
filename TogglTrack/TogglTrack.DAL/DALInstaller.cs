using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TogglTrack.DAL.Repositories;

namespace TogglTrack.DAL
{
    public static class DALInstaller
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContextFactory<TogglTrackDbContext, TogglTrackDbContextFactory>();
            serviceCollection.AddScoped<TogglTrackDbContext>(x =>
            x.GetRequiredService<IDbContextFactory<TogglTrackDbContext>>().CreateDbContext());

            serviceCollection.AddTransient(typeof(Repository<>));
            serviceCollection.AddTransient<Repository<UserEntity>, UserRepository>();
            serviceCollection.AddTransient<Repository<ProjectEntity>, ProjectRepository>();
            serviceCollection.AddTransient<Repository<ActivityEntity>, ActivityRepository>();

            return serviceCollection;
        }
    }
}
