using Microsoft.AspNetCore.Components;
using TogglTrack.API.Abstractions;
using TogglTrack.Blazor;
using TogglTrack.Common.Models.User;
using AutoMapper;
using FluentValidation;

namespace TogglTrack.Web.Components.Pages.UserPage
{
    public partial class UserEditPage
    {
        private readonly IMapper mapper;

        public UserEditPage(IMapper mapper)
        {
            this.mapper = mapper;
        }
        [Inject]
        public IUsersClient UsersClient { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IValidator<CreateUserRequest> _createUserValidator { get; set; }
        [Parameter]
        public Guid? Id { get; set; }

        public UserDetailModel Data { get; set; } = new UserDetailModel
        {
            Id = Guid.NewGuid(),
            FirstName = string.Empty,
            LastName = string.Empty
        };
        protected override async Task OnInitializedAsync()
        {
            if (Id.HasValue)
            {
                var user = await UsersClient.GetUserAsync(Id.Value);
                Data = mapper.Map<UserDetailModel>(user);
            }
        }

        public async Task Save()
        {
            var createUserRequest = mapper.Map<CreateUserRequest>(Data);
            await UsersClient.UpdateOrCreateUserAsync(Id.Value, createUserRequest);
            NavigationManager.NavigateTo("/users");
        }
        public void Cancel()
        {
            NavigationManager.NavigateTo("/users");
        }

        public async Task DeleteUser(Guid id)
        {
            await UsersClient.DeleteUserAsync(id);
            NavigationManager.NavigateTo("/users");
        }

        public void HandleInvalidSubmit()
        {
            var errorMessage = "Validation error.";
        }
    }
}
