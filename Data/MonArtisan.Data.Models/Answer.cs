namespace MonArtisan.Data.Models
{
    using System;

    using MonArtisan.Data.Common.Models;

    public class Answer : IDeletableEntity
    {
        public Answer()
        {
            this.Id = new Guid().ToString();
        }

        public string Id { get; set; }

        public string Content { get; set; }

        public string QuestionId { get; set; }

        public Question Question { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
