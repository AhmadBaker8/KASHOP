using KASHOP.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Category> Categories {  get; set; }
        public DbSet<Brand> Brands {  get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //اذا حابب اغير اسماء الجداول 
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            //هس الجداول كلهن 7 جداول اذا بدي الغي بعض الجداول
            builder.Ignore<IdentityUserClaim<string>>();
            builder.Ignore<IdentityUserLogin<string>>();
            builder.Ignore<IdentityUserToken<string>>();
            builder.Ignore<IdentityRoleClaim<string>>(); 
        }
    }
}
