using Backend_Final.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Backend_Final.Database.Configurations
{
    public class BasketProductConfigurations : IEntityTypeConfiguration<BasketProduct>
    {
        public void Configure(EntityTypeBuilder<BasketProduct> builder)
        {
            builder
                .ToTable("BasketProducts");

            builder
              .HasOne(bp => bp.Basket)
              .WithMany(basket => basket.BasketProducts)
              .HasForeignKey(bp => bp.BasketId);

            builder
              .HasOne(bp => bp.Product)
              .WithMany(Product => Product.BasketProducts)
              .HasForeignKey(bp => bp.ProductId);

        }
    }
}
