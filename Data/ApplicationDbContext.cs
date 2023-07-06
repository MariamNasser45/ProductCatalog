using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Models;
using System.Reflection.Emit;

namespace ProductCatalog.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ////Change Tables name
            builder.Entity<IdentityUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UsersRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UsersLogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");

            ////Ignor some Identity columns
           builder.Entity<IdentityUser>().ToTable("Users").Ignore(c => c.PhoneNumber);
           builder.Entity<IdentityUser>().ToTable("Users").Ignore(c => c.PhoneNumberConfirmed);

            //seed data to the table in DB
            builder.Entity<Category>().HasData(new Category[] 
            {
                new Category {    CategoryID = 1 , categoryName = "PC Accessories"},
               
                new Category {    CategoryID = 2 , categoryName = "Mobile Accessories"}
            });
   
        }
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

    }
}