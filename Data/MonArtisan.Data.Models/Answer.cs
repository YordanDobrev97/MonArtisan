namespace MonArtisan.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MonArtisan.Data.Common.Models;

    public class Answer : IDeletableEntity
    {
        public Answer()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public string Content { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
