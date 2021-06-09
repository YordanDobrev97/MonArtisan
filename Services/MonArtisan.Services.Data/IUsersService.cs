namespace MonArtisan.Services.Data
{
    using System.Threading.Tasks;

    using MonArtisan.Web.ViewModels;

    public interface IUsersService
    {
        Task<bool> CraftsmanRegistration(InputRegisterUser userData);

        Task<bool> ClientRegistration(InputRegisterClient input);
    }
}
