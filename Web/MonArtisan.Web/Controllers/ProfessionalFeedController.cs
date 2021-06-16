namespace MonArtisan.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using MonArtisan.Services.Data;

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

        [HttpPost]
        public async Task<IActionResult> AddProject()
        {
            var username = this.User.FindFirstValue(ClaimTypes.Name);

            // TODO ...
            return this.RedirectToAction("Index");
        }

        public IActionResult SearchClients()
        {
            return this.View();
        }
    }
}
