using System.Linq;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public DbSet<UserFavouriteProvider> UserFavouriteProviders { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<FoodTag> FoodTags { get; set; }
        public DbSet<FoodItemTag> FoodItemTags { get; set; }
        public DbSet<FoodItemDepletedReport> FoodItemDepletedReports { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<FoodCategory> FoodCategories { get; set; }
        public DbSet<Comment> Comments { get; set; }

        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // disable cascade delete
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

    }
}