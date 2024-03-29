﻿namespace MonArtisan.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using MonArtisan.Data.Models;

    using MonArtisan.Web.ViewModels;
    using MonArtisan.Web.ViewModels.Projects;
    using MonArtisan.Web.ViewModels.Users;

    public interface IUsersService
    {
        Task<bool> CraftsmanRegistration(InputRegisterUser userData);

        Task<bool> ClientRegistration(InputRegisterClient input);

        Task<bool> Login(string username, string password);

        Task<bool> UploadDocumnet(IFormFile file, string folder);

        Task<ApplicationUser> FindUser(string username);

        Task<UserProfileViewModel> FindUserById(string id);

        Task<string> FindUserRole(ApplicationUser user);

        Task<List<SearchClientViewModel>> Search(string userId, double radius, string[] categories);

        Task<bool> RequestProject(string userId, int projectId);

        Task<bool> ApproveProject(string userId, string username, string projectName);

        Task<List<UserNotificationViewModel>> UserNotification(string userId);

        Task<List<GetUserProjectsViewModel>> GetUserProjects(string userId);

        Task<List<ChatUserViewModel>> GetChatUsers(string userId);
    }
}
