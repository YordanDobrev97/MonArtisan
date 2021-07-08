namespace MonArtisan.Data.Models
{
    using MonArtisan.Data.Common.Models;
    using System;

    public class Category : IDeletableEntity
    {
        public Category()
        {
            this.Id = new Guid().ToString();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
