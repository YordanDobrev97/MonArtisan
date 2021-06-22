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

        public ProjectsService(IDeletableEntityRepository<Project> projectsRepository = null)
        {
            this.projectRepository = projectsRepository;
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
    }
}
