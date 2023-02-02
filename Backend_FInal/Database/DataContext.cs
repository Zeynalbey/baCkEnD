using Backend_Final.Database.Configurations;
using Backend_Final.Database.Models;
using Backend_Final.Database.Models.Common;
using Backend_Final.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Backend_Final.Database
{
    public partial class DataContext : DbContext
    {
        public DataContext(DbContextOptions options)
            : base(options)
        {

        }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Navbar> Navbars { get; set; }
        public DbSet<SubNavbar> SubNavbars { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketProduct> BasketProducts { get; set; }
        public DbSet<UserActivation> UserActivations { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly<Program>();
            modelBuilder.Entity<Category>().HasData(
               new() { Id = 1, Title = "Flowers" },
               new() { Id = 2, Title = "Trees" },
               new() { Id = 3, Title = "Grasses" });

            modelBuilder.Entity<Size>().HasData(
               new() { Id = 1, Name = "Small" },
               new() { Id = 2, Name = "Medium" },
               new() { Id = 3, Name = "Large" });

            modelBuilder.Entity<Color>().HasData(
               new() { Id = 1, Name = "Red" },
               new() { Id = 2, Name = "Green" },
               new() { Id = 3, Name = "Yellow" });

            modelBuilder.Entity<Tag>().HasData(
               new() { Id = 1, Name = "#bagdagul" },
               new() { Id = 2, Name = "#cemendegul" },
               new() { Id = 3, Name = "#sehradagul" });


        }
    }

    #region Auditing

    public partial class DataContext
    {
        public override int SaveChanges()
        {
            AutoAudit();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            AutoAudit();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AutoAudit();

            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            AutoAudit();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        private void AutoAudit()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is not IAuditable auditableEntry) //Short version
                {
                    continue;
                }

                //IAuditable auditableEntry = (IAuditable)entry;

                DateTime currentDate = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    auditableEntry.CreatedAt = currentDate;
                    auditableEntry.UpdatedAt = currentDate;
                }
                else if (entry.State == EntityState.Modified)
                {
                    auditableEntry.UpdatedAt = currentDate;
                }
            }
        }

    }

    #endregion


   

    
}
