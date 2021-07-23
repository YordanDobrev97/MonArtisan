namespace MonArtisan.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using MonArtisan.Services.Data;

    public class ClientsController : BaseController
    {
        private readonly IUsersService usersService;
        private readonly IProjectsService projectsService;

        public ClientsController(IUsersService usersService, IProjectsService projectsService)
        {
            this.usersService = usersService;
            this.projectsService = projectsService;
        }

        public IActionResult Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var projects = this.projectsService.All(userId);
            return this.View(projects);
        }

        [Authorize]
        public async Task<IActionResult> UploadDocument()
        {
            var username = this.User.FindFirstValue(ClaimTypes.Name);

            // TODO ...
            return this.RedirectToAction("Index");
        }
    }
}
