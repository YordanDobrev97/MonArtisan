﻿namespace MonArtisan.Data.Models
{
    using System;

    using MonArtisan.Data.Common.Models;

    public class Image : IDeletableEntity
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
