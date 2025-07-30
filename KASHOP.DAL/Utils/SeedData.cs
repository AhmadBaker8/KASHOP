using KASHOP.DAL.Data;
using Microsoft.EntityFrameworkCore;
using KASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace KASHOP.DAL.Utils
{
    public class SeedData : ISeedData
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public SeedData(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        } 
        public async Task DataSeedingAsync()
        {
            if((await _context.Database.GetPendingMigrationsAsync()).Any())
            {
                await _context.Database.MigrateAsync();
            }
            if(!await _context.Categories.AnyAsync())
            {
                await _context.Categories.AddRangeAsync(
                    new Category { Name="Clothes" },
                    new Category { Name="Mobiles" }
                    );
            }

            if (!await _context.Brands.AnyAsync())
            {
                await _context.Brands.AddRangeAsync(
                    new Brand { Name = "Samsung" },
                    new Brand { Name = "Apple" }
                    );
            }
            await _context.SaveChangesAsync();
        }

        public async Task IdentityDataSeedingAsync()
        {
            if(!await _roleManager.Roles.AnyAsync())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                await _roleManager.CreateAsync(new IdentityRole("Customer"));
            }
            if (!await _userManager.Users.AnyAsync())
            {
                var user1 = new ApplicationUser()
                {
                    Email = "Ahmad@gmail.com",
                    FullName = "AhmadBaker",
                    PhoneNumber = "0568178976",
                    UserName = "ABaker"
                };
                var user2 = new ApplicationUser()
                {
                    Email = "anas@gmail.com",
                    FullName = "AnasAnas",
                    PhoneNumber = "0568125658",
                    UserName = "A_Anas"
                };
                var user3 = new ApplicationUser()
                {
                    Email = "abd@gmail.com",
                    FullName = "abdabd",
                    PhoneNumber = "0568456852",
                    UserName = "A_Abd"
                };
                await _userManager.CreateAsync(user1,"Pass@1212");
                await _userManager.CreateAsync(user2,"Pass@1212");
                await _userManager.CreateAsync(user3,"Pass@1212");

                await _userManager.AddToRoleAsync(user1, "Admin");
                await _userManager.AddToRoleAsync(user2, "SuperAdmin");
                await _userManager.AddToRoleAsync(user3, "Customer");
            }

            await _context.SaveChangesAsync();
        }

        
    }
}
