namespace TogglTrack.Common.Models.Project
{
    public class ProjectListModel : IModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }
}
