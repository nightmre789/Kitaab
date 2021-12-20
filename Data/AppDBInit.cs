using Kitaab.Models;
using Microsoft.AspNetCore.Builder;
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
    }
}
