namespace TogglTrack.Common.Models.Activity
{
    public class ActivityListModel : IModel
    {
        public Guid Id { get; set; }
        public required string ActivityType { get; set; }
    }
}
