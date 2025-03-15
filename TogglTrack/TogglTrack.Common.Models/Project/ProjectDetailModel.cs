using TogglTrack.Common.Models.Activity;
using TogglTrack.Common.Models.User;

namespace TogglTrack.Common.Models.Project
{
    public class ProjectDetailModel : IModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public ICollection<ActivityListModel?> Activities { get; set; }
        public ICollection<UserListModel?> Users { get; set; }
    }
}
