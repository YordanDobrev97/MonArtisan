namespace MonArtisan.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MonArtisan.Services.Data;
    using MonArtisan.Web.ViewModels;

    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService _usersService)
        {
            this._usersService = _usersService;
        }

        [HttpPost]
        public async Task<IActionResult> CraftsmanRegistration(InputRegisterUser userData)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("CraftsmanRegistration");
            }

            var result = await this._usersService.CraftsmanRegistration(userData);

            if (result)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.Redirect("Register");
        }

        [HttpPost]
        public async Task<IActionResult> ClientRegistration(InputRegisterClient userData)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToPage("/Account/ClientRegister", new { area = "Identity" });
            }

            var result = await this._usersService.ClientRegistration(userData);

            if (result)
            {
                return this.RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            return this.RedirectToPage("/Account/ClientRegister", new { area = "Identity" });
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            return new JsonResult("Logged in");
        }
    }
}
