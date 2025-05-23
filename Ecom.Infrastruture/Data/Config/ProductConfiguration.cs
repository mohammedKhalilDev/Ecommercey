﻿using Ecom.Core.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastruture.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(30);
            builder.Property(p => p.NewPrice).IsRequired().HasPrecision(18, 2);
            builder.HasData(new Product { Id = 1, Name = "XO", Description = "Children Toys", CategoryId = 1, NewPrice = 40, OldPrice = 4 });

        }
    }

    public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.HasData(new Photo { Id = 1, ImageName = "XO Photo", ProductId = 1 });
        }
    }
}
