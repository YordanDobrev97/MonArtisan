namespace MonArtisan.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class ClientsController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            return this.View();
        }
    }
}
