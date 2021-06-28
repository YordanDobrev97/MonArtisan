namespace MonArtisan.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MonArtisan.Data.Common.Models;

    public class UserProject : IDeletableEntity
    {
        public UserProject()
        {
            this.Id = new Guid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string ProjectId { get; set; }

        public Project Project { get; set; }

        public bool State { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
