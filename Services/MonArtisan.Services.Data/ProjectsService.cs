﻿namespace MonArtisan.Services.Data
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
        private readonly IDeletableEntityRepository<UserProject> userProjectRepository;

        public ProjectsService(
            IDeletableEntityRepository<Project> projectsRepository,
            IDeletableEntityRepository<ProjectRequest> projectRequestRepository,
            IDeletableEntityRepository<UserProject> userProjectRepository)
        {
            this.projectRepository = projectsRepository;
            this.projectRequestRepository = projectRequestRepository;
            this.userProjectRepository = userProjectRepository;
        }

        public async Task<bool> Accept(string userId, string projectId)
        {
            var project = await this.userProjectRepository.All()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.ProjectId == projectId);

            if (project != null || project.State)
            {
                return false;
            }

            await this.userProjectRepository.AddAsync(new UserProject()
            {
                UserId = userId,
                ProjectId = projectId,
                State = false,
            });

            await this.userProjectRepository.SaveChangesAsync();

            return true;
        }

        public async Task FinishProject(string userId, string projectId)
        {
            var project = await this.userProjectRepository.All()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.ProjectId == projectId);

            if (project != null)
            {
                project.State = true;
                await this.userProjectRepository.SaveChangesAsync();
            }
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
            };

            await this.projectRepository.AddAsync(newProject);
            await this.projectRepository.SaveChangesAsync();

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