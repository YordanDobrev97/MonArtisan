namespace MonArtisan.Services.Data
{
    using System.Threading.Tasks;

    using MonArtisan.Web.ViewModels;

    public interface IUsersService
    {
        Task<bool> Register(InputRegisterUser userData);
    }
}
