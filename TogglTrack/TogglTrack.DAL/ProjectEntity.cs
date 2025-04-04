﻿namespace TogglTrack.DAL
{
    public class ProjectEntity : IEntity
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public ICollection<ActivityEntity> Activities { get; set; }
        public ICollection<UserEntity> Users { get; set; }
    }
}