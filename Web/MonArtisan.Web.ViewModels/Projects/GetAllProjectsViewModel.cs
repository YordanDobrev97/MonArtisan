namespace MonArtisan.Web.ViewModels.Projects
{
    using System;
    using System.Collections.Generic;

    public class GetAllProjectsViewModel<T>
    {
        public List<T> Projects { get; set; }

        public decimal Pages { get; set; }

        public bool ReciveNotifications { get; set; }
    }
}
