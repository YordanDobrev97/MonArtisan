
namespace MonArtisan.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Project
    {
        [Key]
        public string Id { get; set; }

        public int CustomID { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Craftsman { get; set; }

        public bool State { get; set; }
    }
}
