namespace MonArtisan.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MonArtisan.Data.Common.Models;

    public class SubCategory : IDeletableEntity
    {
        public SubCategory()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Questions = new HashSet<SubCategoryQuestion>();
        }

        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public HashSet<SubCategoryQuestion> Questions { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
