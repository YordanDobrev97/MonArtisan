namespace MonArtisan.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
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
        private readonly IDeletableEntityRepository<Image> imageRepository;
        private readonly IDeletableEntityRepository<ProjectImage> projectImageRepository;
        private readonly Cloudinary cloudinary;

        public ProjectsService(
            IDeletableEntityRepository<Project> projectsRepository,
            IDeletableEntityRepository<ProjectRequest> projectRequestRepository,
            IDeletableEntityRepository<UserProject> userProjectRepository,
            IDeletableEntityRepository<Category> categoryRepository,
            IDeletableEntityRepository<SubCategory> subCategoryRepository,
            IDeletableEntityRepository<Question> questionRepository,
            IDeletableEntityRepository<Answer> answerRepository,
            IDeletableEntityRepository<SubCategoryQuestion> subCategoryQuestionRepository,
            IDeletableEntityRepository<Image> imageRepository,
            IDeletableEntityRepository<ProjectImage> projectImageRepository,
            Cloudinary cloudinary)
        {
            this.projectRepository = projectsRepository;
            this.projectRequestRepository = projectRequestRepository;
            this.userProjectRepository = userProjectRepository;
            this.categoryRepository = categoryRepository;
            this.subCategoryRepository = subCategoryRepository;
            this.questionRepository = questionRepository;
            this.answerRepository = answerRepository;
            this.subCategoryQuestionRepository = subCategoryQuestionRepository;
            this.imageRepository = imageRepository;
            this.projectImageRepository = projectImageRepository;
            this.cloudinary = cloudinary;
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

        public async Task<bool> Create(
            string userId,
            string projectName,
            string category,
            string subCategory,
            Dictionary<string, string> questions,
            string[] images)
        {
            SubCategory newSubCategory = new SubCategory()
            {
                Name = subCategory,
            };

            Category newCategory = new Category()
            {
                Name = category,
                SubCategory = newSubCategory,
            };

            await this.categoryRepository.AddAsync(newCategory);

            var newProject = new Project()
            {
                Name = projectName,
                ClientId = userId,
                Category = newCategory,
                Date = DateTime.UtcNow,
            };

            await this.projectRepository.AddAsync(newProject);

            foreach (var img in images)
            {
                string url = this.AddImage(img, $"{projectName} {Guid.NewGuid()}");

                var newImage = new Image()
                {
                    Url = url,
                };

                var newProjectImage = new ProjectImage()
                {
                    Image = newImage,
                    Project = newProject,
                };

                await this.imageRepository.AddAsync(newImage);
                await this.projectImageRepository.AddAsync(newProjectImage);

            }

            await this.userProjectRepository.AddAsync(new UserProject()
            {
                Project = newProject,
                UserId = userId,
            });

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
                    newAnswer = new Answer()
                    {
                        Content = answer,
                    };
                }

                var subCategoryQuestion = new SubCategoryQuestion()
                {
                    SubCategory = newSubCategory,
                    Question = newQuestion,
                    Answer = newAnswer,
                };

                await this.subCategoryQuestionRepository.AddAsync(subCategoryQuestion);
            }

            await this.projectRepository.SaveChangesAsync();
            await this.userProjectRepository.SaveChangesAsync();
            await this.subCategoryQuestionRepository.SaveChangesAsync();
            await this.questionRepository.SaveChangesAsync();
            await this.subCategoryQuestionRepository.SaveChangesAsync();

            return true;
        }

        public async Task<ProjectDetailsViewModel> Details(int id)
        {
            var images = this.projectImageRepository.All()
                .Where(x => x.ProjectId == id)
                .Select(x => x.Image.Url)
                .ToArray();

            var project = await this.projectRepository.All()
                .Where(x => x.Id == id)
                .Select(x => new ProjectDetailsViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    QuestionAnswerPairs = x.Category.SubCategory.Questions.Select(q => new QuestionAnswerPair()
                    {
                        Answer = q.Answer.Content,
                        Question = q.Question.Content,
                    }).ToArray(),
                    Images = images,
                }).FirstOrDefaultAsync();

            return project;
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

        private string AddImage(string image, string imageName)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(imageName, image),
                Folder = "/Projects",
            };

            var result = this.cloudinary.Upload(uploadParams);
            return result.Url.OriginalString;
        }
    }
}
