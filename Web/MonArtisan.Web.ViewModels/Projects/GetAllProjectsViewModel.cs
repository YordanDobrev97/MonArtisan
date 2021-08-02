namespace MonArtisan.Web.ViewModels.Projects
{
    using System;
    using System.Collections.Generic;

    public class GetAllProjectsViewModel
    {
        public List<ClientProjectsViewModel> ClientProjects { get; set; }

        public decimal Pages { get; set; }
    }
}
