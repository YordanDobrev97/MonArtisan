namespace MonArtisan.Data.Models
{
    using System;

    using MonArtisan.Data.Common.Models;

    public class Question : IDeletableEntity
    {
        public Question()
        {
            this.Id = new Guid().ToString();
        }

        public string Id { get; set; }

        public string Content { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
