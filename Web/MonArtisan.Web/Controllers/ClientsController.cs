namespace MonArtisan.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using MonArtisan.Services.Data;
    using MonArtisan.Web.ViewModels.Projects;

    [Authorize]
    public class ClientsController : BaseController
    {
        private readonly IUsersService usersService;
        private readonly IProjectsService projectsService;

        public ClientsController(IUsersService usersService, IProjectsService projectsService)
        {
            this.usersService = usersService;
            this.projectsService = projectsService;
        }

        public IActionResult Index(int pageNumber = 1)
        {
            int pageToShow = 3;
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var projects = this.projectsService.All(userId);

            var viewModel = new GetAllProjectsViewModel
            {
                ClientProjects = projects.Skip((pageNumber - 1) * pageToShow).Take(pageToShow).ToList(),
                Pages = Math.Ceiling(projects.Count / (decimal)pageToShow),
                ReciveNotifications = true,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Notifications()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var notifications = await this.usersService.UserNotification(userId);
            return this.View(notifications);
        }
    }
}
