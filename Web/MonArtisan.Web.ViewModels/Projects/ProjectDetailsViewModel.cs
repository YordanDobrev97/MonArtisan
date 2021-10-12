namespace MonArtisan.Web.ViewModels.Projects
{
    using System;

    public class ProjectDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public DateTime Date { get; set; }

        public decimal Price { get; set; }

        public QuestionAnswerPair[] QuestionAnswerPairs { get; set; }

        public string[] Images { get; set; }

        public string Description { get; set; }
    }
}
