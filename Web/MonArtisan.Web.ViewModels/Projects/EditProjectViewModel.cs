namespace MonArtisan.Web.ViewModels.Projects
{
    public class EditProjectViewModel
    {
        public string ProjectName { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string[] Categories { get; set; }

        public QuestionAnswerPair[] QuestionAnswerPairs { get; set; }

        public string[] Images { get; set; }
    }
}
