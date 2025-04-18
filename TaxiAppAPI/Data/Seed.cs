﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using TaxiAppApi.Model;












namespace TaxiAppApi.Data
{
    public static class Seed
    {
        public async static void EnsureIdentity(WebApplication app)
        {
            var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {

                var roleManagerService = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManagerService.RoleExistsAsync("Admin"))

                {
                    IdentityRole adminRole = new IdentityRole { Name = "Admin" };

                    await roleManagerService.CreateAsync(adminRole);
                }
                var userManagerService = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                var user = new AppUser

                {
                    Email = "admin@TaxiApp.com",
                    UserName = "admin",

                };


                if (userManagerService.FindByNameAsync(user.UserName).Result == null)
                {
                    var results = await userManagerService.CreateAsync(user, "Secret123$");

                    if (results.Succeeded)
                    {
                        await userManagerService.AddToRoleAsync(user, "Admin");
                    }
                }
            }
        }
        //public async static void Populate(WebApplication app)


        //{
        //    var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
        //    using (var scope = scopeFactory.CreateScope())
        //    {
        //         var _appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        //      if (!await _appDbContext.Bookings.AnyAsync())
        //        {

        //          _appDbContext.Bookings.AddRange(new List<Bookings>()
        //            {
        //                new Bookings() { Id = 1 },
        //            });
        //        }
        //    } 
        //}



    }

}
