namespace MonArtisan.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MonArtisan.Common;
    using MonArtisan.Services.Data;
    using MonArtisan.Web.ViewModels;
    using MonArtisan.Web.ViewModels.Projects;

    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpPost]
        [Route("api/[controller]/CraftsmanRegistration")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CraftsmanRegistration([FromBody] InputRegisterUser userData)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToPage("/Account/CraftsmanRegister", new { area = "Identity" });
            }

            var result = await this.usersService.CraftsmanRegistration(userData);

            if (result)
            {
                return new JsonResult(new { result = "Redirect", url = "/Identity/Account/Login" });
            }

            return this.Redirect("Register");
        }

        [HttpPost]
        [Route("api/[controller]/ClientRegistration")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClientRegistration([FromBody] InputRegisterClient userData)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToPage("/Account/ClientRegister", new { area = "Identity" });
            }

            var result = await this.usersService.ClientRegistration(userData);

            if (result)
            {
                return new JsonResult(new { result = "Redirect", url = "/Identity/Account/Login" });
            }

            return this.RedirectToPage("/Account/ClientRegister", new { area = "Identity" });
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
               return this.RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var result = await this.usersService.Login(username, password);

            if (result)
            {
                var user = await this.usersService.FindUser(username);
                var userRole = await this.usersService.FindUserRole(user);

                if (userRole == GlobalConstants.Client)
                {
                    return this.RedirectToAction("Index", "Clients");
                }
                else if (userRole == GlobalConstants.Craftsman)
                {
                    return this.RedirectToAction("Index", "ProfessionalFeed");
                }
            }

            return this.RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        [HttpPost]
        [Route("api/[controller]/RequestProject")]
        public async Task<IActionResult> RequestProject([FromBody] RequestProjectInputModel inputModel)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await this.usersService.RequestProject(userId, inputModel.ProjectId);
            return new JsonResult(result);
        }
    }
}
