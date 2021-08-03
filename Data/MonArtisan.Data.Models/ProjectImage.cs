namespace MonArtisan.Data.Models
{
    using System;

    using MonArtisan.Data.Common.Models;

    public class ProjectImage : IDeletableEntity
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }

        public int ImageId { get; set; }

        public Image Image { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
