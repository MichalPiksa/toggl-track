using FluentValidation;
using FluentValidation.AspNetCore;
using TogglTrack.API.Abstractions.Validations;
using TogglTrack.Blazor;
using TogglTrack.Web.Components;
using TogglTrack.Web.MapperProfiles;

namespace TogglTrack.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var apiBaseUrl = builder.Configuration.GetValue<string>("ApiBaseUrl");

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();

            builder.Services.AddHttpClient();
            builder.Services.AddScoped(serviceProvider =>
            {
                var httpClient = serviceProvider.GetService<IHttpClientFactory>().CreateClient();
                httpClient.BaseAddress = new Uri(apiBaseUrl);
                return httpClient;
            });

            builder.Services.AddAutoMapper(typeof(UserMapperWebProfile));
            builder.Services.AddAutoMapper(typeof(ProjectMapperWebProfile));

            builder.Services.AddTransient<IUsersClient, UsersClient>();
            builder.Services.AddTransient<IProjectsClient, ProjectsClient>();
            builder.Services.AddTransient<IActivitiesClient, ActivitiesClient>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

            app.Run();
        }
    }
}
