namespace TogglTrack.DAL
{
    public class UserEntity : IEntity
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? PhotoUrl { get; set; }
        public ICollection<ActivityEntity> Activities { get; set; }
        public ICollection<ProjectEntity> Projects { get; set; }
    }
}
