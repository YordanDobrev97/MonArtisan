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

    [Authorize]
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

        [HttpPost]
        [Route("api/[controller]/Create")]
        public async Task<IActionResult> Create([FromBody] InputCreateProjectModel data)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var qa = JsonConvert.DeserializeObject<Dictionary<string, string>>(data.QuestionAnswers);
            var result = await this.projectService.Create(userId, data.Name, data.Category, data.SubCategory, qa, data.Images);
            return new JsonResult(result);
        }

        [HttpPost]
        [Route("api/[controller]/finishProject")]
        public async Task<IActionResult> FinishProject([FromBody] FinishProjectViewModel projectInput)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await this.projectService.FinishProject(userId, projectInput.ProjectId);
            return new JsonResult(result);
        }
    }
}
