using Backend_Final.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Backend_Final.Database.Configurations
{
    public class ProductColorConfigurations : IEntityTypeConfiguration<ProductColor>
    {
        public void Configure(EntityTypeBuilder<ProductColor> builder)
        {
            builder
                .ToTable("ProductColors");

            builder
                .HasKey(pc => new { pc.ProductId, pc.ColorId });

            builder
               .HasOne(pc => pc.Product)
               .WithMany(p => p.ProductColors)
               .HasForeignKey(pc => pc.ProductId);

            builder
                .HasOne(pc => pc.Color)
                .WithMany(c => c.ProductColors)
                .HasForeignKey(pc => pc.ColorId);
        }
    }
}
