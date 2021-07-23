namespace MonArtisan.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MonArtisan.Data.Models;
    using MonArtisan.Web.ViewModels.Projects;

    public interface IProjectsService
    {
        List<GetAllProjectsViewModel> All(string userId);

        Task<bool> Create(string userId, string projectName, string category, string subCategory, Dictionary<string, string> questions);

        Task<bool> SendRequest(string userId, int projectId, decimal price);

        Task<bool> Accept(string userId, int projectId);

        Task FinishProject(string userId, int projectId);

        Task<Project> GetProject(string projectName);

        Task<UserProject> GetUserProject(string userId, int projectId);
    }
}
