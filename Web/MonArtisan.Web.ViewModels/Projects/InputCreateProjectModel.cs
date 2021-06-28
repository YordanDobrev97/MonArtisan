namespace MonArtisan.Web.ViewModels.Projects
{
    using MonArtisan.Data.Models;

    public class InputCreateProjectModel
    {
        public string Name { get; set; }

        public ApplicationUser Client { get; set; }
    }
}
