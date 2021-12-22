using Kitaab.Data.Static;
using Kitaab.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitaab.Data
{
    public class AppDBInit
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDBContext>();
                context.Database.EnsureCreated();
                if (!context.Authors.Any())
                {
                    context.Authors.AddRange(new List<Author>()
                    {
                       new Author()
                       {
                           FullName = "Orscon Scott Card",
                       },
                       new Author()
                       {
                           FullName = "Jane Austen",
                       }
                    });
                    context.SaveChanges();
                }
                if (!context.Books.Any())
                {
                    context.Books.AddRange(new List<Book>()
                    {
                        new Book()
                        {
                            Title = "Ender's Game",
                            Cover = "Test",
                            Description = "Cool Book!",
                            Category = "Sci-Fi",
                            Quantity = 20,
                            Price = 4.99f,
                            AuthorId = 1,
                        },
                        new Book()
                        {
                            Title = "Pride and Prejudice",
                            Cover = "Test",
                            Description = "Very Cool Book!",
                            Category = "Romance",
                            Quantity = 10,
                            Price = 11.99f,
                            AuthorId = 2
                        }
                    });
                    context.SaveChanges();
                }
                
            }
        }
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@kitaab.com";
                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Aneeb Asif",
                        UserName = "admin",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Secret123@");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }
                
                string userEmail = "user@kitaab.com";
                var user = await userManager.FindByEmailAsync(userEmail);
                if (user == null)
                {
                    var newUser = new ApplicationUser()
                    {
                        FullName = "Hasan Burney",
                        UserName = "burney",
                        Email = userEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newUser, "Secret123@");
                    await userManager.AddToRoleAsync(newUser, UserRoles.User);
                }
            }
        }
    }
}
