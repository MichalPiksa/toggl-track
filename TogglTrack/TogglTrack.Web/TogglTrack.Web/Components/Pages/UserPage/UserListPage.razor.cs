using AutoMapper;
using Microsoft.AspNetCore.Components;
using TogglTrack.API.Abstractions;
using TogglTrack.Blazor;
using TogglTrack.Common.Models.User;

namespace TogglTrack.Web.Components.Pages.UserPage
{
    public partial class UserListPage
    {
        private readonly IMapper mapper;
        public UserListPage(IMapper mapper)
        {
            this.mapper = mapper;
        }
        [Inject]
        public IUsersClient UsersClient { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public ICollection<UserListModel> Users { get; set; } = new List<UserListModel>();

        protected override async Task OnInitializedAsync()
        {
            Users = await UsersClient.GetUsersAsync();
        }

        public async Task AddUser()
        {
            var newUser = new CreateUserRequest(
                FirstName: "Enter name",
                LastName: "Enter surname",
                PhotoUrl: "Enter photo url"
            );
            var createdUser = await UsersClient.CreateUserAsync(newUser);
            NavigationManager.NavigateTo($"/users/{createdUser.Id}");
        }

        public void SelectUser(Guid id)
        {
            NavigationManager.NavigateTo($"/users/{id}/projects");
        }

        public async Task DeleteUser(Guid id)
        {
            await UsersClient.DeleteUserAsync(id);
            Users = await UsersClient.GetUsersAsync();
        }
    }
}
