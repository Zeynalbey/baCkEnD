﻿using Backend_Final.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Backend_Final.Database.Configurations
{
    public class ProductImageConfigurations : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder
                .ToTable("ProductImages");

            builder
                .HasOne(bi => bi.Product)
                .WithMany(b => b.ProductImages)
                .HasForeignKey(bi => bi.ProductId);
        }
    }
}
