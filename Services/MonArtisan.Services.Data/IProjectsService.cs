namespace MonArtisan.Services.Data
{
    using System.Threading.Tasks;
    using MonArtisan.Data.Models;
    using MonArtisan.Web.ViewModels.Projects;

    public interface IProjectsService
    {
        Task<bool> Create(InputCreateProjectModel inputModel);

        Task<bool> SendRequest(string userId, string projectId, decimal price);

        Task<bool> Accept(string userId, string projectId);

        Task FinishProject(string userId, string projectId);

        Task<Project> GetProject(string projectName);

        Task<UserProject> GetUserProject(string userId, string projectId);
    }
}
