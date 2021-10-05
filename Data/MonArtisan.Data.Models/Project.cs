namespace MonArtisan.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MonArtisan.Data.Common.Models;

    public class Project : IDeletableEntity
    {
        public Project()
        {
            this.ProjectImages = new HashSet<ProjectImage>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public DateTime Date { get; set; }

        public string ClientId { get; set; }

        public ApplicationUser Client { get; set; }

        public string CategoryId { get; set; }

        public Category Category { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public HashSet<ProjectImage> ProjectImages { get; set; }
    }
}
