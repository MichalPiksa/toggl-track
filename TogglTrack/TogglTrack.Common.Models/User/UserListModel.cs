using System.Text.Json.Serialization;

namespace TogglTrack.Common.Models.User
{
    public class UserListModel : IModel
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}
