using Backend_Final.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Backend_Final.Database.Configurations
{
    public class ProductTagConfigurations : IEntityTypeConfiguration<ProductTag>
    {
        public void Configure(EntityTypeBuilder<ProductTag> builder)
        {
            builder
                .ToTable("ProductTags");

            builder
                .HasKey(pt => new { pt.TagId, pt.ProductId });

            builder
               .HasOne(pt => pt.Product)
               .WithMany(p => p.ProductTags)
               .HasForeignKey(pt => pt.ProductId);

            builder
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.ProductTags)
                .HasForeignKey(pt => pt.TagId);
        }
    }
}
