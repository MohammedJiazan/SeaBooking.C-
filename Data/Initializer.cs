using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using sea_booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sea_booking.Data
{
    public class Initializer
    {
        private static UserManager<IdentityUser> _userManager;
        private static RoleManager<IdentityRole> _roleManager;


        //public Initializer(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        //{

        //    _userManager = userManager;
        //    _roleManager = roleManager;
        //}
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider
                .GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureCreated();
            var roleManager = serviceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();
            var roleName2 = "emp";
            IdentityResult result2;
            var roleExist2 = await roleManager.RoleExistsAsync(roleName2);
            if (!roleExist2)
            {
                result2 = await roleManager
                    .CreateAsync(new IdentityRole(roleName2));
            }

            var roleName = "Administrator";
            IdentityResult result;
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                result = await roleManager
                    .CreateAsync(new IdentityRole(roleName));
                if (result.Succeeded)
                {
                    var userManager = serviceProvider
                        .GetRequiredService<UserManager<IdentityUser>>();
                    var config = serviceProvider
                        .GetRequiredService<IConfiguration>();
                    var admin = await userManager
                        .FindByEmailAsync("admin@admin.com");

                    if (admin == null)
                    {
                        admin = new IdentityUser()
                        {
                            UserName = "admin",
                            Email = "admin@admin.com",
                            EmailConfirmed = true,

                        };
                        result = await userManager
                            .CreateAsync(admin, "admin");
                        if (result.Succeeded)
                        {
                            result = await userManager
                                .AddToRoleAsync(admin, roleName);
                            if (!result.Succeeded)
                            {
                                // todo: process errors
                            }
                        }
                    }
                }
            }
            else
            {
                var userManager = serviceProvider
                       .GetRequiredService<UserManager<IdentityUser>>();
                var config = serviceProvider
                    .GetRequiredService<IConfiguration>();
                var admin = await userManager
                    .FindByEmailAsync("admin@admin.com");

                if (admin == null)
                {
                    admin = new IdentityUser()
                    {
                        UserName = "admin",
                        Email = "admin@admin.com",
                        EmailConfirmed = true,

                    };
                    result = await userManager
                        .CreateAsync(admin,"admin");
                    if (result.Succeeded)
                    {
                        result = await userManager
                            .AddToRoleAsync(admin, roleName);
                        if (!result.Succeeded)
                        {
                            // todo: process errors
                        }
                    }
                }
            }
        }
        //public static void init(ApplicationDbContext db)
        //{
        //    db.Database.EnsureCreated();
        //    if (db.Roles.Any())
        //    {
        //        return;
        //    }
        //    var roleManger = new RoleStore<IdentityRole>(db);
        //    var userManger = new UserStore<ApplicationUser>(db);
        //    IdentityRole role = new IdentityRole();
        //    IdentityRole role2 = new IdentityRole();
        //    if (!roleManger.Roles.Any())
        //    {
        //        role.Name = "admin";
        //        role2.Name = "emp";
        //        roleManger.CreateAsync(role);
        //        roleManger.CreateAsync(role2);
        //        var user = new ApplicationUser { UserName = "admin", Email = "staduim@live.com", IsEnabeld = true };
        //        var result = _userManager.CreateAsync(user, "admin");
        //        if (result.IsCompletedSuccessfully)
        //        {
        //             userManger.AddToRoleAsync(user, "admin");
        //        }
        //    }           

        //}
    }
}
