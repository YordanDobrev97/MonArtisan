namespace MonArtisan.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using MonArtisan.Data.Models;
    using MonArtisan.Web.ViewModels;

    public class UsersService : IUsersService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UsersService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<bool> Register(InputRegisterUser userData)
        {
            if (userData.Password != userData.ConfirmPassword)
            {
                return false;
            }

            if (userData.Kbis != null)
            {
                this.UploadGoogledrive(userData.Kbis);
            }

            var user = new ApplicationUser()
            {
                FirstName = userData.FirstName,
                LastName = userData.LastName,
                PhoneNumber = userData.PhoneNumber,
                Address = userData.Address,
                Email = userData.Email,
                Radius = userData.Radius,
                CompanyName = userData.CompanyName,
                CompanyNumber = userData.CompanyNumber,
                UserName = userData.FirstName,
                Profession = userData.Profession,
                ZipCode = userData.ZipCode,
            };

            var result = await this.userManager.CreateAsync(user, userData.Password);
            return result.Succeeded;
        }

        private void UploadGoogledrive(IFormFile pdf)
        {
            // TODO ...
        }
    }
}
