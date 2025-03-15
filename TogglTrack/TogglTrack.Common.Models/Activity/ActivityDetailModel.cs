using TogglTrack.Common.Models.Project;
using TogglTrack.Common.Models.User;

namespace TogglTrack.Common.Models.Activity
{
    public class ActivityDetailModel : IModel
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public required string ActivityType { get; set; }
        public string? Description { get; set; }
        public UserListModel? User {  get; set; }
        public ProjectListModel? Project { get; set; }
    }
}
