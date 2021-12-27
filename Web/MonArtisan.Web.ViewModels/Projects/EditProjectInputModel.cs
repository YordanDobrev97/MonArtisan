namespace MonArtisan.Web.ViewModels.Projects
{
    using Microsoft.AspNetCore.Http;

    public class EditProjectInputModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }

        public IFormFile[] Images { get; set; }
    }
}
