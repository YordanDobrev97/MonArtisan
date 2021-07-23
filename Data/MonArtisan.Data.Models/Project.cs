
namespace MonArtisan.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MonArtisan.Data.Common.Models;

    public class Project : IDeletableEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string ClientId { get; set; }

        public ApplicationUser Client { get; set; }

        public string CategoryId { get; set; }

        public Category Category { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set ; }
    }
}
