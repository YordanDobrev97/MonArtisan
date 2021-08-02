using MonArtisan.Common;

namespace MonArtisan.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using GeoCoordinatePortable;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using MonArtisan.Data;
    using MonArtisan.Data.Common.Repositories;
    using MonArtisan.Data.Models;
    using MonArtisan.Web.ViewModels;
    using MonArtisan.Web.ViewModels.Users;

    public class UsersService : IUsersService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationDbContext db;
        private readonly Cloudinary cloudinary;

        public UsersService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            Cloudinary cloudinary,
            ApplicationDbContext db)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.cloudinary = cloudinary;
            this.db = db;
        }

        public async Task<bool> CraftsmanRegistration(InputRegisterUser userData)
        {
            if (userData.Password != userData.ConfirmPassword)
            {
                return false;
            }

            if (userData.Kbis != null)
            {
                await this.UploadDocumnet(userData.Kbis, "KbisDocs");
            }

            var user = new ApplicationUser()
            {
                FirstName = userData.FirstName,
                LastName = userData.LastName,
                PhoneNumber = userData.PhoneNumber,
                Address = userData.Address,
                Email = userData.Email,
                Radius = userData.Radius,
                CompanyName = userData.CompanyName,
                CompanyNumber = userData.CompanyNumber,
                UserName = userData.FirstName,
                Profession = userData.Profession,
                ZipCode = userData.ZipCode,
            };

            var result = await this.userManager.CreateAsync(user, userData.Password);

            if (result.Succeeded)
            {
                // Add user to role
                var craftsman = await this.db.Roles
                    .FirstOrDefaultAsync(x => x.Name == GlobalConstants.Craftsman);

                var addedUserRole = this.db.UserRoles
                    .Any(x => x.UserId == user.Id
                           && x.RoleId == craftsman.Id);

                if (craftsman != null && !addedUserRole)
                {
                    await this.db.UserRoles
                        .AddAsync(new IdentityUserRole<string>()
                    {
                        RoleId = craftsman.Id,
                        UserId = user.Id,
                    });

                    await this.db.SaveChangesAsync();
                }
            }

            return result.Succeeded;
        }

        public async Task<bool> ClientRegistration(InputRegisterClient input)
        {
            var user = new ApplicationUser()
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                UserName = input.FirstName,
                Email = input.Email,
                ZipCode = input.ZipCode,
                PhoneNumber = input.PhoneNumber,
            };

            var result = await this.userManager.CreateAsync(user, input.Password);

            if (result.Succeeded)
            {
                // Add user to role
                var client = await this.db.Roles
                    .FirstOrDefaultAsync(x => x.Name == GlobalConstants.Client);

                var addedUserRole = this.db.UserRoles
                    .Any(x => x.UserId == user.Id
                              && x.RoleId == client.Id);

                if (client != null && !addedUserRole)
                {
                    await this.db.UserRoles
                        .AddAsync(new IdentityUserRole<string>()
                        {
                            RoleId = client.Id,
                            UserId = user.Id,
                        });

                    await this.db.SaveChangesAsync();
                }
            }

            return result.Succeeded;
        }

        public async Task<bool> Login(string username, string password)
        {
            var result = await this.signInManager.PasswordSignInAsync(username, password, true, false);
            return result.Succeeded;
        }

        public async Task<ApplicationUser> FindUser(string username)
        {
            var user = await this.userManager.Users.FirstOrDefaultAsync(x => x.UserName == username);
            return user;
        }

        public async Task<string> FindUserRole(ApplicationUser user)
        {
            var findUser = (await this.userManager.GetRolesAsync(user)).FirstOrDefault();
            return findUser;
        }

        public async Task<List<SearchClientViewModel>> Search(string userId, double radius, string[] categories)
        {
            var countryCode = "FR";

            var craftsmanUser = await this.db.Users.FirstOrDefaultAsync(x => x.Id == userId);
            var firstLocation = await ZipToLocation.ConvertZipCodeToLocation(craftsmanUser.ZipCode, countryCode);

            var role = await this.db.Roles.FirstOrDefaultAsync(x => x.Name == GlobalConstants.Client);
            var clients = this.db.Users.Where(x => x.Roles.Any(x => x.RoleId == role.Id)).ToList();

            var resultClients = new List<SearchClientViewModel>();

            foreach (var user in clients)
            {
                var secondLocation = await ZipToLocation.ConvertZipCodeToLocation(user.ZipCode, countryCode);
                var clientKm = this.Distance(firstLocation, secondLocation);

                if (clientKm <= radius)
                {
                    var projects = this.db.UserProjects.Where(x => x.UserId == user.Id).Select(x => new SearchClientViewModel
                    {
                        ProjectName = x.Project.Name,
                        Date = x.Project.Date,
                    }).ToList();

                    resultClients.AddRange(projects);
                }
            }

            return resultClients;
        }

        public async Task<bool> UploadDocumnet(IFormFile file, string folder)
        {
            byte[] fileBytes;

            using var stream = new MemoryStream();
            file.CopyTo(stream);
            fileBytes = stream.ToArray();

            var destination = new MemoryStream(fileBytes);
            var fileName = $"{Guid.NewGuid().ToString()}";
            var uploadParameters = new ImageUploadParams()
            {
                File = new FileDescription(fileName, destination),
                PublicId = file.FileName,
                Folder = folder,
            };

            var result = await this.cloudinary.UploadAsync(uploadParameters);
            return true;
        }

        private double Distance(Location craftsmanLocation, Location clientLocation)
        {
            var sCoord = new GeoCoordinate(craftsmanLocation.Latitude, craftsmanLocation.Longitude);
            var eCoord = new GeoCoordinate(clientLocation.Latitude, clientLocation.Longitude);

            var miles = sCoord.GetDistanceTo(eCoord);

            return this.ConvertMilesToKilometers(miles);
        }

        private double ConvertMilesToKilometers(double miles)
        {
            return miles * 1.609344;
        }
    }
}
