namespace MonArtisan.Data.Models
{
    using System;

    using MonArtisan.Data.Common.Models;

    public class ProjectRequest : IDeletableEntity
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string ProjectId { get; set; }

        public Project Project { get; set; }

        public decimal Price { get; set; }

        public bool Approved { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
