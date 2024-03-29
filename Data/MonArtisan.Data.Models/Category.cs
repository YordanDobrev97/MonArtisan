﻿namespace MonArtisan.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MonArtisan.Data.Common.Models;

    public class Category : IDeletableEntity
    {
        public Category()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public string SubCategoryId { get; set; }

        public SubCategory SubCategory { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
