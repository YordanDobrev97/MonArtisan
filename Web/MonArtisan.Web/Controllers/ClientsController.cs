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

        public ClientsController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [Authorize]
        public async Task<IActionResult> UploadDocument(IFormFile file)
        {
            var username = this.User.FindFirstValue(ClaimTypes.Name);

            await this.usersService.UploadDocumnet(file, $"clientsDocs/{username}");
            return this.RedirectToAction("Index");
        }
    }
}
