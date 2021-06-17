namespace MonArtisan.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MonArtisan.Common;
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
                return this.RedirectToPage("/Account/CraftsmanRegister", new { area = "Identity" });
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
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
               return this.RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var result = await this._usersService.Login(username, password);

            if (result)
            {
                var user = await this._usersService.FindUser(username);
                var userRole = await this._usersService.FindUserRole(user);

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
    }
}
