namespace MonArtisan.Services.Data
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using MonArtisan.Data.Models;

    using MonArtisan.Web.ViewModels;

    public interface IUsersService
    {
        Task<bool> CraftsmanRegistration(InputRegisterUser userData);

        Task<bool> ClientRegistration(InputRegisterClient input);

        Task<bool> Login(string username, string password);

        Task<bool> UploadDocumnet(IFormFile file, string folder);

        Task<ApplicationUser> FindUser(string username);

        Task<string> FindUserRole(ApplicationUser user);
    }
}
