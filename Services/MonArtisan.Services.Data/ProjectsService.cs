namespace MonArtisan.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MonArtisan.Data.Common.Repositories;
    using MonArtisan.Data.Models;
    using MonArtisan.Web.ViewModels.Projects;

    public class ProjectsService : IProjectsService
    {
        private readonly IDeletableEntityRepository<Project> projectRepository;
        private readonly IDeletableEntityRepository<ProjectRequest> projectRequestRepository;
        private readonly IDeletableEntityRepository<UserProject> userProjectRepository;
        private readonly IDeletableEntityRepository<Category> categoryRepository;
        private readonly IDeletableEntityRepository<SubCategory> subCategoryRepository;
        private readonly IDeletableEntityRepository<Question> questionRepository;
        private readonly IDeletableEntityRepository<Answer> answerRepository;
        private readonly IDeletableEntityRepository<SubCategoryQuestion> subCategoryQuestionRepository;

        public ProjectsService(
            IDeletableEntityRepository<Project> projectsRepository,
            IDeletableEntityRepository<ProjectRequest> projectRequestRepository,
            IDeletableEntityRepository<UserProject> userProjectRepository,
            IDeletableEntityRepository<Category> categoryRepository,
            IDeletableEntityRepository<SubCategory> subCategoryRepository,
            IDeletableEntityRepository<Question> questionRepository,
            IDeletableEntityRepository<Answer> answerRepository,
            IDeletableEntityRepository<SubCategoryQuestion> subCategoryQuestionRepository)
        {
            this.projectRepository = projectsRepository;
            this.projectRequestRepository = projectRequestRepository;
            this.userProjectRepository = userProjectRepository;
            this.categoryRepository = categoryRepository;
            this.subCategoryRepository = subCategoryRepository;
            this.questionRepository = questionRepository;
            this.answerRepository = answerRepository;
            this.subCategoryQuestionRepository = subCategoryQuestionRepository;
        }

        public List<ClientProjectsViewModel> All(string userId)
        {
            var projects = this.userProjectRepository.All().Where(x => x.UserId == userId)
                .Select(x => new ClientProjectsViewModel()
                {
                    Id = x.Project.Id,
                    Name = x.Project.Name,
                    Date = x.Project.Date,
                    Status = x.State,
                }).ToList();

            return projects;
        }

        public async Task<bool> Accept(string userId, int projectId)
        {
            var project = await this.userProjectRepository.All()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.ProjectId == projectId);

            if (project != null)
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

        public async Task<bool> FinishProject(string userId, int projectId)
        {
            var project = await this.userProjectRepository.All()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.ProjectId == projectId);

            if (project != null)
            {
                project.State = !project.State;
                await this.userProjectRepository.SaveChangesAsync();
            }

            return project.State;
        }

        public async Task<bool> Create(string userId, string projectName, string category, string subCategory, Dictionary<string, string> questions)
        {
            var newProject = new Project()
            {
                Name = projectName,
                ClientId = userId,
                Date = DateTime.UtcNow,
            };

            await this.projectRepository.AddAsync(newProject);

            await this.userProjectRepository.AddAsync(new UserProject()
            {
                Project = newProject,
                UserId = userId,
            });

            var isExistCategory = await this.categoryRepository.All().AnyAsync(c => c.Name == category);
            Category newCategory = null;
            if (!isExistCategory)
            {
                newCategory = new Category()
                {
                    Name = category,
                };
                await this.categoryRepository.AddAsync(newCategory);
            }
            else
            {
                newCategory = await this.categoryRepository.All().FirstOrDefaultAsync(c => c.Name == category);
            }

            SubCategory newSubCategory = null;
            var isExistSubCategory = await this.subCategoryRepository.All().AnyAsync(s => s.Name == subCategory);
            if (!isExistSubCategory)
            {

                newSubCategory = new SubCategory()
                {
                    Name = subCategory,
                    //Category = newCategory,
                };
                await this.subCategoryRepository.AddAsync(newSubCategory);
            }
            else
            {
                newSubCategory = await this.subCategoryRepository.All().FirstOrDefaultAsync(sc => sc.Name == subCategory);
            }

            foreach (var question in questions.Keys)
            {
                var answer = questions[question];

                var newQuestion = await this.questionRepository.All().FirstOrDefaultAsync(q => q.Content == question);

                if (newQuestion == null)
                {
                    newQuestion = new Question()
                    {
                        Content = question,
                    };
                }

                var newAnswer = await this.answerRepository.All().FirstOrDefaultAsync(a => a.Content == answer);

                if (newAnswer == null)
                {
                    await this.answerRepository.AddAsync(new Answer()
                    {
                        Content = answer,
                    });
                }

                await this.subCategoryQuestionRepository.AddAsync(new SubCategoryQuestion()
                {
                    Question = newQuestion,
                    SubCategory = newSubCategory,
                });
            }

            await this.projectRepository.SaveChangesAsync();
            await this.userProjectRepository.SaveChangesAsync();
            await this.subCategoryQuestionRepository.SaveChangesAsync();
            await this.questionRepository.SaveChangesAsync();
            await this.subCategoryQuestionRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SendRequest(string userId, int projectId, decimal price)
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

        public async Task<Project> GetProject(string projectName) => await this.projectRepository.All().FirstOrDefaultAsync(x => x.Name == projectName);

        public async Task<UserProject> GetUserProject(string userId, int projectId) => await this.userProjectRepository.All().FirstOrDefaultAsync(x => x.ProjectId == projectId && x.UserId == userId);
    }
}
