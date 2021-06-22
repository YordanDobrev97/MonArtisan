namespace MonArtisan.Services.Data
{
    using System.Threading.Tasks;

    public interface IProjectsService
    {
        Task<bool> Create(string name);

        Task<bool> SendRequest(string userId, string projectId, decimal price);

        Task<bool> Accept(string userId, string projectId);

        Task FinishProject(string userId, string projectId);
    }
}
