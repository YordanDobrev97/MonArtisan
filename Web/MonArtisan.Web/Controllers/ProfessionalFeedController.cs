namespace MonArtisan.Web.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using MonArtisan.Services.Data;
    using System.Threading.Tasks;

    public class ProfessionalFeedController : Controller
    {
        private readonly IProfessionalService professionalService;

        public ProfessionalFeedController(IProfessionalService professionalService)
        {
            this.professionalService = professionalService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProject(IFormFile file)
        {
            var result = await this.professionalService.UploadDocument(file);
            return this.RedirectToAction("Index", "ProfessionalFeed");
        }
    }
}
