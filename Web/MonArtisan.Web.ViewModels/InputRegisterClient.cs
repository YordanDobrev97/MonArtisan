namespace MonArtisan.Web.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class InputRegisterClient
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
        public decimal Longitude { get; set; }

        [Required]
        public decimal Latitude { get; set; }
    }
}