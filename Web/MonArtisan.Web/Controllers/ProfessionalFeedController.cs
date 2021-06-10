namespace MonArtisan.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ProfessionalFeedController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
