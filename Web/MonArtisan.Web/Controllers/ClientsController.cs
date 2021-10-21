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

    [Authorize(Roles = "Client")]
    public class ClientsController : BaseController
    {
        private readonly IUsersService usersService;
        private readonly IProjectsService projectsService;

        public ClientsController(IUsersService usersService, IProjectsService projectsService)
        {
            this.usersService = usersService;
            this.projectsService = projectsService;
        }

        public async Task<IActionResult> Index(int id = 1)
        {
            int pageToShow = 5;
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var projects = await this.projectsService.All(userId);
            var notApprovedProjects = await this.projectsService.NotApprovedProjects(userId);

            var username = this.User.FindFirstValue(ClaimTypes.Name);
            this.ViewData["Title"] = "Client Feed";
            this.ViewData["Username"] = username;

            var viewModel = new GetAllProjectsViewModel<ClientProjectsViewModel>
            {
                Projects = projects.Skip((id - 1) * pageToShow).Take(pageToShow).ToList(),
                Pages = Math.Ceiling(projects.Count / (decimal)pageToShow),
                ReciveNotifications = notApprovedProjects,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Notifications()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var notifications = await this.usersService.UserNotification(userId);
            return this.View(notifications);
        }

        [HttpPost]
        [Route("api/[controller]/ApproveProject")]
        public async Task<IActionResult> ApproveProject([FromBody] ClientNotificationInputModel input)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await this.usersService.ApproveProject(userId, input.Username, input.ProjectName);
            return new JsonResult(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            this.ViewData["Title"] = "Project Details - Clients Feed";
            var clientProject = await this.projectsService.Details(id);
            return this.View(clientProject);
        }
    }
}
