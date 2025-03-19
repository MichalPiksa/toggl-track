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
                FirstName: "EnterName",
                LastName: "EnterSurname",
                PhotoUrl: "https://cdn.vectorstock.com/i/500p/17/61/male-avatar-profile-picture-vector-10211761.jpg"
            );
            var createdUser = await UsersClient.CreateUserAsync(newUser);
            NavigationManager.NavigateTo($"/users/{createdUser.Id}/edit");
        }

        public void EditUser(Guid id)
        {
            NavigationManager.NavigateTo($"/users/{id}/edit");
        }
    }
}
