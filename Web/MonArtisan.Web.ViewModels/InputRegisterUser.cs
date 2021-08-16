namespace MonArtisan.Web.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class InputRegisterUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string CompanyNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public decimal Longitude { get; set; }

        [Required]
        public decimal Latitude { get; set; }

        [Required]
        public string Profession { get; set; }

        [Required]
        public int Radius { get; set; }

        public IFormFile Kbis { get; set; }
    }
}
