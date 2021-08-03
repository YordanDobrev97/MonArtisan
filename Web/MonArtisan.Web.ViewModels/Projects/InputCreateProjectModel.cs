namespace MonArtisan.Web.ViewModels.Projects
{
    using Microsoft.AspNetCore.Http;

    public class InputCreateProjectModel
    {
        public string Name { get; set; }

        public string Category { get; set; }

        public string SubCategory { get; set; }

        public string QuestionAnswers { get; set; }

        public string[] Images { get; set; }
    }
}
