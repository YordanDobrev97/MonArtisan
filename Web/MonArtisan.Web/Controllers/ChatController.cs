namespace MonArtisan.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ChatController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
