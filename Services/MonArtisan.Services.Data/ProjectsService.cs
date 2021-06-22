namespace MonArtisan.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MonArtisan.Data.Common.Repositories;
    using MonArtisan.Data.Models;
    using MonArtisan.Data.Repositories;

    public class ProjectsService : IProjectsService
    {
        private readonly IDeletableEntityRepository<Project> projectRepository;
        private readonly IDeletableEntityRepository<ProjectRequest> projectRequestRepository;

        public ProjectsService(
            IDeletableEntityRepository<Project> projectsRepository,
            IDeletableEntityRepository<ProjectRequest> projectRequestRepository)
        {
            this.projectRepository = projectsRepository;
            this.projectRequestRepository = projectRequestRepository;
        }

        public async Task<bool> Create(string name)
        {
            var project = await this.projectRepository.All()
                .FirstOrDefaultAsync(x => x.Name == name);

            if (project != null)
            {
                return false;
            }

            var newProject = new Project()
            {
                Name = name,
                Date = DateTime.UtcNow,
                State = false,
            };

            await this.projectRepository.AddAsync(newProject);

            return true;
        }

        public async Task<bool> SendRequest(string userId, string projectId, decimal price)
        {
            var projectRequest = await this.projectRequestRepository.All()
                .FirstOrDefaultAsync(x => x.ProjectId == projectId && x.UserId == userId);

            if (projectRequest != null)
            {
                return false;
            }

            await this.projectRequestRepository.AddAsync(new ProjectRequest()
            {
                UserId = userId,
                ProjectId = projectId,
                Price = price,
            });

            await this.projectRequestRepository.SaveChangesAsync();

            return true;
        }
    }
}
