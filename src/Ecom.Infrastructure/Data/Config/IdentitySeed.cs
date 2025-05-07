using Ecom.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Data.Config
{
    public class IdentitySeed
    {
        public static async Task SeedUserAsync(UserManager<User> userManager)
        {
            if(!userManager.Users.Any())
            {
                //Create New User
                var user = new User
                {
                    DisplayName = "ahmets",
                    Email="ahmets@gmail.com",
                    UserName = "ahmets@gmail.com",
                    Address = new Address
                    {
                        FirstName = "Ahmet",
                        LastName = "Sukkar",
                        Street = "2005 Sokak",
                        City = "Ankara",
                        State = "Ankara",
                        ZipCode = "06300"
                    }
                };

                await userManager.CreateAsync(user,"P@ssw0rd");
            }
        }
    }
}
