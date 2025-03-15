using System.Text.Json.Serialization;
using TogglTrack.Common.Models.Activity;
using TogglTrack.Common.Models.Project;

namespace TogglTrack.Common.Models.User
{
    public class UserDetailModel : IModel
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? PhotoUrl { get; set; }
        public ICollection<ActivityListModel> Activities { get; set; }
        public ICollection<ProjectListModel> Projects { get; set; }
    }
}
