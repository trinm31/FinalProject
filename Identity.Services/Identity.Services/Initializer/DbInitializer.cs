using System.Security.Claims;
using Identity.Services.Data;
using Identity.Services.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace Identity.Services.Initializer;

    public static class DbInitializer
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureCreated();

                var _userManager =
                    serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                var _roleManager =
                    serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                // try
                // {
                //     if (context.Database.GetPendingMigrations().Count() > 0)
                //     {
                //         context.Database.Migrate();
                //     }
                // }
                // catch(Exception ex)
                // {
                //
                // }
                
                if (_roleManager.FindByNameAsync(SD.Admin).Result == null)
                {
                    _roleManager.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(SD.Staff)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(SD.Student)).GetAwaiter().GetResult();
                }
                else
                {
                    return;
                }
                // user admin
                ApplicationUser adminUser = new ApplicationUser()
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "111111111111",
                    FirstName = "Admin",
                    LastName = "Admin",
                    Position = "Adminstrator"
                };

                _userManager.CreateAsync(adminUser, "Admin123@").GetAwaiter().GetResult();
                
                ApplicationUser userAdmin = context.Users.Where(u => u.Email == "admin@gmail.com").FirstOrDefault();
                
                _userManager.AddToRoleAsync(userAdmin, SD.Admin).GetAwaiter().GetResult();

                var temp1 = _userManager.AddClaimsAsync(adminUser, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, adminUser.FirstName + " " + adminUser.LastName),
                    new Claim(JwtClaimTypes.GivenName, adminUser.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, adminUser.LastName),
                    new Claim(JwtClaimTypes.Role, SD.Admin),
                }).Result;
                // Staff user
                ApplicationUser StaffUser = new ApplicationUser()
                {
                    UserName = "staff@gmail.com",
                    Email = "staff@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "111111111111",
                    FirstName = "Staff",
                    LastName = "Staff",
                    Position = "Staff"
                };

                _userManager.CreateAsync(StaffUser, "Staff123@").GetAwaiter().GetResult();
                ApplicationUser userStaff = context.Users.Where(u => u.Email == "staff@gmail.com").FirstOrDefault();
                
                _userManager.AddToRoleAsync(userStaff, SD.Staff).GetAwaiter().GetResult();

                var temp2 = _userManager.AddClaimsAsync(StaffUser, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, StaffUser.FirstName + " " + StaffUser.LastName),
                    new Claim(JwtClaimTypes.GivenName, StaffUser.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, StaffUser.LastName),
                    new Claim(JwtClaimTypes.Role, SD.Staff),
                }).Result;
                
                // Student User
                
                ApplicationUser StudentUser = new ApplicationUser()
                {
                    UserName = "student@gmail.com",
                    Email = "student@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "111111111111",
                    FirstName = "Student",
                    LastName = "Student",
                    Position = "Student"
                };

                _userManager.CreateAsync(StudentUser, "Student123@").GetAwaiter().GetResult();
                ApplicationUser userStudent = context.Users.Where(u => u.Email == "student@gmail.com").FirstOrDefault();
                
                _userManager.AddToRoleAsync(userStudent, SD.Student).GetAwaiter().GetResult();

                var temp3 = _userManager.AddClaimsAsync(StudentUser, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, StudentUser.FirstName + " " + StudentUser.LastName),
                    new Claim(JwtClaimTypes.GivenName, StudentUser.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, StudentUser.LastName),
                    new Claim(JwtClaimTypes.Role, SD.Student),
                }).Result;
            }
        }
    }