namespace MonArtisan.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MonArtisan.Data.Common.Models;

    public class SubCategoryQuestion : IDeletableEntity
    {
        public SubCategoryQuestion()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public string SubCategoryId { get; set; }

        public SubCategory SubCategory { get; set; }

        public string QuestionId { get; set; }

        public Question Question { get; set; }

        public string AnswerId { get; set; }

        public Answer Answer { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
