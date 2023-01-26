using Backend_Final.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Backend_Final.Database.Configurations
{
    public class ProductSizeConfigurations : IEntityTypeConfiguration<ProductSize>
    {
        public void Configure(EntityTypeBuilder<ProductSize> builder)
        {
            builder
                .ToTable("ProductSizes");

            builder
                .HasKey(ps => new { ps.ProductId, ps.SizeId });

            builder
               .HasOne(ps => ps.Product)
               .WithMany(p => p.ProductSizes)
               .HasForeignKey(ps => ps.ProductId);

            builder
                .HasOne(ps => ps.Size)
                .WithMany(s => s.ProductSizes)
                .HasForeignKey(ps => ps.SizeId);
        }
    }
}
