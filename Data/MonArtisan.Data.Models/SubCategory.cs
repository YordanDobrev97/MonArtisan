namespace MonArtisan.Data.Models
{
    using System;

    using MonArtisan.Data.Common.Models;

    public class SubCategory : IDeletableEntity
    {

        public SubCategory()
        {
            this.Id = new Guid().ToString();
        }

        public string Id { get; set; }

        public string CategoryId { get; set; }

        public Category Category { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
