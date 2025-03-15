namespace TogglTrack.DAL
{
    public class ActivityEntity : IEntity
    {
        public Guid Id { get; set; }
        public DateTime StartTime {  get; set; }
        public DateTime? EndTime { get; set; }
        public required string ActivityType {  get; set; }
        public string? Description { get; set; }
        public Guid UserId { get; set; }
        public UserEntity? User { get; set; }
        public Guid ProjectId { get; set; }
        public ProjectEntity? Project { get; set; }
    }
}
