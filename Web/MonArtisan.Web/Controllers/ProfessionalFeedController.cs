namespace MonArtisan.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using MonArtisan.Services.Data;
    using MonArtisan.Web.ViewModels.Users;

    [Authorize]
    public class ProfessionalFeedController : Controller
    {
        private readonly IUsersService usersService;

        public ProfessionalFeedController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult Index()
        {
            this.ViewData["Title"] = "Professional Feed";

            return this.View();
        }

        [HttpGet]
        [Route("[controller]/SearchClients")]
        public IActionResult SearchClients()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/[controller]/SearchProjects")]
        public async Task<IActionResult> SearchProjects([FromBody] SearchInputModel inputViewModel)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var projects = await this.usersService.Search(userId, inputViewModel.Radius, inputViewModel.Categories);
            return new JsonResult(projects);
        }
    }
}
