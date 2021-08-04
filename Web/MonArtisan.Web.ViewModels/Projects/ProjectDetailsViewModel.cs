namespace MonArtisan.Web.ViewModels.Projects
{
    public class ProjectDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public QuestionAnswerPair[] QuestionAnswerPairs { get; set; }

        public string[] Images { get; set; }

        public string Description { get; set; }
    }
}
