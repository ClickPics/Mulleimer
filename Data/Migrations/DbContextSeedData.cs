using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZenithWebsite.Data;
using ZenithWebsite.Models;

namespace ZenithWebsite.Data.Migrations
{
    public static class DbContextSeedData
    {
        public static void Seed(IServiceProvider service)
        {

            //get instance of DbContext
            //if you are having problems seeding the data delete the db in the root directory and update the schema using 
            //(dotnet ef database update) then run the application to seed the data
            //NOTE: data gets seeded when the application is run/started.
            using(var context = service.GetRequiredService<ApplicationDbContext>())
            {
                if (context.ActivityCategories.Any())
                    return;

                SeedRoles(service);
                context.SaveChanges();
                SeedUsers(service);

                var categories = GetActivityCategories();
                context.ActivityCategories.AddRange(categories);
                context.SaveChanges();

                var events = GetEvents(context);
                context.Events.AddRange(events);

                context.SaveChanges();
                context.Dispose();
            }
            
        }
        private static async void SeedRoles(IServiceProvider services)
        {
            using (var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>())
            {
                
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                if (!await roleManager.RoleExistsAsync("Member"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Member"));

                } 
            }
        }
        private static async void SeedUsers(IServiceProvider services)
        {
            using (var userManager = services.GetRequiredService<UserManager<ApplicationUser>>())
            {
                
                if (await userManager.FindByEmailAsync("a@a.a") == null)
                {
                    var user = new ApplicationUser
                    {
                        Email = "a@a.a",
                        UserName = "a",
                    };
                    var result = await userManager.CreateAsync(user, "P@$$w0rd");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(await userManager.FindByEmailAsync(user.Email), "Admin");
                    }
                }

                if (await userManager.FindByEmailAsync("g@g.g") == null)
                {
                    var user = new ApplicationUser
                    {
                        Email = "m@m.m",
                        UserName = "m",
                    };
                    var result = await userManager.CreateAsync(user, "P@$$w0rd");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(await userManager.FindByEmailAsync(user.Email), "Member");
                    }
                }
            }
        }
        private static List<ActivityCategory> GetActivityCategories()
        {

            List<ActivityCategory> categories = new List<ActivityCategory>()
            {
                new ActivityCategory {
                    ActivityDescription = "Senior's Golf Tournament",
                    CreationDate = DateTime.Now},
                new ActivityCategory {
                    ActivityDescription = "Leadership General Assembly Meeting",
                    CreationDate = DateTime.Now},
                new ActivityCategory {
                    ActivityDescription = "Youth Bowling Tournament",
                    CreationDate = DateTime.Now},
                new ActivityCategory {
                    ActivityDescription = "Young Ladies Cooking Lessons",
                    CreationDate = DateTime.Now},
                new ActivityCategory {
                    ActivityDescription = "Youth Craft Lessons",
                    CreationDate = DateTime.Now},
                new ActivityCategory {
                    ActivityDescription = "Youth Choir Practice",
                    CreationDate = DateTime.Now},
                new ActivityCategory {
                    ActivityDescription = "Lunch",
                    CreationDate = DateTime.Now},
                new ActivityCategory {
                    ActivityDescription = "Pancake Breakfast",
                    CreationDate = DateTime.Now},
                new ActivityCategory {
                    ActivityDescription = "Swimming Lessons for the Youth",
                    CreationDate = DateTime.Now},
                new ActivityCategory {
                    ActivityDescription = "Swimming Exercise for Parents",
                    CreationDate = DateTime.Now},
                new ActivityCategory {
                    ActivityDescription = "Bingo Tournament",
                    CreationDate = DateTime.Now},
                new ActivityCategory {
                    ActivityDescription = "BBQ Lunch",
                    CreationDate = DateTime.Now},
                new ActivityCategory {
                    ActivityDescription = "Garage Sale",
                    CreationDate = DateTime.Now},
            };
            return categories;
        }
        private static List<Event> GetEvents(ApplicationDbContext context)
        {
            List<Event> events = new List<Event>()
            {
                new Event() { StartDateTime= new DateTime(2017, 10, 10, 8, 30, 00),
                    EndDateTime = new DateTime(2017, 10, 10, 10, 30, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 1).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 10, 11, 8, 30, 00),
                    EndDateTime = new DateTime(2017, 10, 11, 10, 30, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 2).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 10, 13, 17, 30, 00),
                    EndDateTime = new DateTime(2017, 10, 13, 19, 15, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 3).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 10, 13, 19, 00, 00),
                    EndDateTime = new DateTime(2017, 10, 13, 20, 00, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 4).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 10, 14, 8, 30, 00),
                    EndDateTime = new DateTime(2017, 10, 14, 10, 30, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 5).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 10, 14, 10, 30, 00),
                    EndDateTime = new DateTime(2017, 10, 14, 12, 00, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 6).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 10, 14, 12, 00, 00),
                    EndDateTime = new DateTime(2017, 10, 14, 13, 30, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 7).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 10, 15, 7, 30, 00),
                    EndDateTime = new DateTime(2017, 10, 15, 8, 30, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 8).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 10, 15, 8, 30, 00),
                    EndDateTime = new DateTime(2017, 10, 15, 10, 30, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 9).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 10, 15, 8, 30, 00),
                    EndDateTime = new DateTime(2017, 10, 15, 10, 30, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 10).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 10, 15, 10, 30, 00),
                    EndDateTime = new DateTime(2017, 10, 15, 12, 00, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 11).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 10, 15, 12, 00, 00),
                    EndDateTime = new DateTime(2017, 10, 15, 13, 00, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 12).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 10, 15, 13, 00, 00),
                    EndDateTime = new DateTime(2017, 10, 15, 18, 00, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 13).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 10, 17, 8, 30, 00),
                    EndDateTime = new DateTime(2017, 10, 17, 10, 30, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 1).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 10, 18, 8, 30, 00),
                    EndDateTime = new DateTime(2017, 10, 18, 10, 30, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 2).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 10, 20, 17, 30, 00),
                    EndDateTime = new DateTime(2017, 10, 20, 19, 15, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 3).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 10, 20, 19, 00, 00),
                    EndDateTime = new DateTime(2017, 10, 20, 20, 00, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 4).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 10, 21, 8, 30, 00),
                    EndDateTime = new DateTime(2017, 10, 21, 10, 30, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 5).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 10, 21, 10, 30, 00),
                    EndDateTime = new DateTime(2017, 10, 21, 12, 00, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 6).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 10, 21, 12, 00, 00),
                    EndDateTime = new DateTime(2017, 10, 21, 13, 30, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 7).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 10, 22, 7, 30, 00),
                    EndDateTime = new DateTime(2017, 10, 22, 8, 30, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 8).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 10, 22, 8, 30, 00),
                    EndDateTime = new DateTime(2017, 10, 22, 10, 30, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 9).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 10, 22, 8, 30, 00),
                    EndDateTime = new DateTime(2017, 10, 22, 10, 30, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 10).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 10, 22, 10, 30, 00),
                    EndDateTime = new DateTime(2017, 10, 22, 12, 00, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 11).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 10, 22, 12, 00, 00),
                    EndDateTime = new DateTime(2017, 10, 22, 13, 00, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 12).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 10, 22, 13, 00, 00),
                    EndDateTime = new DateTime(2017, 10, 22, 18, 00, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 13).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 11, 29, 12, 00, 00),
                    EndDateTime = new DateTime(2017, 10, 22, 15, 00, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 7).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 11, 30, 15, 00, 00),
                    EndDateTime = new DateTime(2017, 10, 22, 18, 00, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 8).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 12, 2, 8, 00, 00),
                    EndDateTime = new DateTime(2017, 10, 22, 15, 00, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 3).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 12, 3, 10, 00, 00),
                    EndDateTime = new DateTime(2017, 10, 22, 18, 00, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 4).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 12, 5, 8, 00, 00),
                    EndDateTime = new DateTime(2017, 12, 5, 11, 00, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 1).ActivityCategoryId},
                
                new Event() { StartDateTime= new DateTime(2017, 12, 5, 16, 00, 00),
                    EndDateTime = new DateTime(2017, 12, 5, 19, 00, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 4).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 12, 6, 14, 00, 00),
                    EndDateTime = new DateTime(2017, 12, 6, 16, 00, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 9).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 12, 6, 17, 00, 00),
                    EndDateTime = new DateTime(2017, 12, 6, 18, 30, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 5).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 12, 6, 8, 00, 00),
                    EndDateTime = new DateTime(2017, 12, 6, 10, 00, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 10).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 12, 6, 21, 00, 00),
                    EndDateTime = new DateTime(2017, 12, 6, 22, 30, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 8).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 12, 7, 13, 00, 00),
                    EndDateTime = new DateTime(2017, 12, 7, 15, 00, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 3).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 12, 8, 9, 00, 00),
                    EndDateTime = new DateTime(2017, 12, 8, 10, 30, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 4).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 12, 10, 9, 00, 00),
                    EndDateTime = new DateTime(2017, 12, 10, 11, 30, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 2).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 12, 10, 17, 00, 00),
                    EndDateTime = new DateTime(2017, 12, 10, 18, 30, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 7).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 12, 10, 22, 00, 00),
                    EndDateTime = new DateTime(2017, 12, 10, 23, 30, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 13).ActivityCategoryId},



                new Event() { StartDateTime= new DateTime(2017, 12, 12, 9, 00, 00),
                    EndDateTime = new DateTime(2017, 12, 12, 10, 30, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 4).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 12, 12, 13, 00, 00),
                    EndDateTime = new DateTime(2017, 12, 12, 14, 00, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 2).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 12, 12, 17, 00, 00),
                    EndDateTime = new DateTime(2017, 12, 10, 18, 30, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 7).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 12, 14, 22, 00, 00),
                    EndDateTime = new DateTime(2017, 12, 14, 23, 30, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 13).ActivityCategoryId},



                new Event() { StartDateTime= new DateTime(2017, 12, 13, 9, 00, 00),
                    EndDateTime = new DateTime(2017, 12, 13, 10, 30, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 10).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 12, 13, 13, 00, 00),
                    EndDateTime = new DateTime(2017, 12, 13, 14, 00, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 8).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 12, 13, 17, 00, 00),
                    EndDateTime = new DateTime(2017, 12, 13, 18, 30, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 7).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 12, 13, 22, 00, 00),
                    EndDateTime = new DateTime(2017, 12, 13, 23, 30, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 2).ActivityCategoryId},


                new Event() { StartDateTime= new DateTime(2017, 12, 18, 17, 00, 00),
                    EndDateTime = new DateTime(2017, 12, 18, 18, 30, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 11).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 12, 18, 22, 00, 00),
                    EndDateTime = new DateTime(2017, 12, 18, 23, 30, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 12).ActivityCategoryId},


                new Event() { StartDateTime= new DateTime(2017, 12, 19, 7, 00, 00),
                    EndDateTime = new DateTime(2017, 12, 19, 8, 30, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 1).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 12, 19, 9, 00, 00),
                    EndDateTime = new DateTime(2017, 12, 19, 11, 30, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 2).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 12, 19, 14, 00, 00),
                    EndDateTime = new DateTime(2017, 12, 19, 16, 30, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 3).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 12, 19, 18, 00, 00),
                    EndDateTime = new DateTime(2017, 12, 19, 20, 00, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 4).ActivityCategoryId},

                new Event() { StartDateTime= new DateTime(2017, 12, 20, 7, 00, 00),
                    EndDateTime = new DateTime(2017, 12, 20, 8, 30, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 6).ActivityCategoryId},

                new Event() { StartDateTime= new DateTime(2017, 12, 21, 14, 00, 00),
                    EndDateTime = new DateTime(2017, 12, 21, 16, 30, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 9).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 12, 21, 18, 00, 00),
                    EndDateTime = new DateTime(2017, 12, 21, 20, 00, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 10).ActivityCategoryId},

                new Event() { StartDateTime= new DateTime(2017, 12, 22, 8, 00, 00),
                    EndDateTime = new DateTime(2017, 12, 22, 9, 30, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 9).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 12, 22, 11, 00, 00),
                    EndDateTime = new DateTime(2017, 12, 22, 12, 00, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 10).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 12, 22, 14, 00, 00),
                    EndDateTime = new DateTime(2017, 12, 22, 16, 30, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 11).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 12, 22, 18, 00, 00),
                    EndDateTime = new DateTime(2017, 12, 22, 20, 00, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 12).ActivityCategoryId},
                
                new Event() { StartDateTime= new DateTime(2017, 12, 27, 14, 00, 00),
                    EndDateTime = new DateTime(2017, 12, 27, 16, 30, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 1).ActivityCategoryId},
                new Event() { StartDateTime= new DateTime(2017, 12, 28, 18, 00, 00),
                    EndDateTime = new DateTime(2017, 12, 28, 20, 00, 00),
                    Username = "a",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    ActivityCategoryId = context.ActivityCategories.FirstOrDefault(
                        a => a.ActivityCategoryId == 2).ActivityCategoryId}
            };
            return events;
        }
    }

}
