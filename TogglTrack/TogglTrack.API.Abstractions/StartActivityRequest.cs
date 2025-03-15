namespace TogglTrack.API.Abstractions
{
    public class StartActivityRequest
    {
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
        public required string ActivityType { get; set; }
        public string? Description { get; set; }
    }
}
