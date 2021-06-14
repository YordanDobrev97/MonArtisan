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
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProject(IFormFile file)
        {
            var username = this.User.FindFirstValue(ClaimTypes.Name);

            var result = await this.usersService.UploadDocumnet(file, $"proDocs/{username}");
            return this.RedirectToAction("Index");
        }
    }
}
