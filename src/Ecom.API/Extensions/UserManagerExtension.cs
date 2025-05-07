using Ecom.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Ecom.API.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<User> FindUserByClaimPrincipalWithAddress(this UserManager<User> userManager, ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            if (email == null) return null;
            var currentUser = await userManager.Users
                        .Include(x => x.Address)
                        .SingleOrDefaultAsync(x => x.Email == email);
            return currentUser;
        }

        public static async Task<User> FindEmailByClaimPrincipal(this UserManager<User> userManager, ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            if (email == null) return null;
            return await userManager.Users
                        .SingleOrDefaultAsync(x => x.Email == email);
        }

        public static string GetUserEmail(this ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            return email;
        }
    }
}
