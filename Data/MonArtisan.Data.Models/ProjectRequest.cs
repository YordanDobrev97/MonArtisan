﻿namespace MonArtisan.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MonArtisan.Data.Common.Models;

    public class ProjectRequest : IDeletableEntity
    {
        public ProjectRequest()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public string ReceiverId { get; set; }

        public ApplicationUser Receiver { get; set; }

        public string SenderId { get; set; }

        public ApplicationUser Sender { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }

        public decimal Price { get; set; }

        public bool Approved { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
