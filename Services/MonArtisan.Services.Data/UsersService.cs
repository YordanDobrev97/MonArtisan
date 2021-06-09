using MonArtisan.Common;

namespace MonArtisan.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using MonArtisan.Data;
    using MonArtisan.Data.Common.Repositories;
    using MonArtisan.Data.Models;
    using MonArtisan.Web.ViewModels;

    public class UsersService : IUsersService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext db;

        public UsersService(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext db)
        {
            this.userManager = userManager;
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
                this.UploadGoogledrive(userData.Kbis);
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

        private void UploadGoogledrive(IFormFile pdf)
        {
            // TODO ...
        }
    }
}
