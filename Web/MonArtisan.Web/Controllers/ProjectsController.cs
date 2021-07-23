namespace MonArtisan.Web.Controllers
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MonArtisan.Services.Data;
    using MonArtisan.Web.ViewModels.Projects;
    using Newtonsoft.Json;

    public class ProjectsController : BaseController
    {
        private readonly IProjectsService projectService;

        public ProjectsController(IProjectsService projectService)
        {
            this.projectService = projectService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InputCreateProjectModel data)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var qa = JsonConvert.DeserializeObject<Dictionary<string, string>>(data.QuestionAnswers);
            var result = await this.projectService.Create(userId, data.Name, data.Category, data.SubCategory, qa);
            return new JsonResult(result);
        }
    }
}
