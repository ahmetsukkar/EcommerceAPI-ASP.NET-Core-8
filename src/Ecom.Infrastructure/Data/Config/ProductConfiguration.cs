using Ecom.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(30);
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
            builder.HasOne(x => x.Category).WithMany().HasForeignKey(x => x.CategoryId);

            builder
             .HasOne(p => p.Category)
             .WithMany(c => c.Products)
             .HasForeignKey(p => p.CategoryId); // ✅ Explicitly set foreign key

            // SeedData
            builder.HasData(
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Laptop",
                    Description = "Laptop",
                    Price = 1000,
                    CategoryId = new Guid("5BF54ECD-E88B-469B-8F35-FB3E942E208E"),
                    Picture = $"/images/products/27a8a874-be16-4fef-b52f-a27ba8873c8a-Laptop.jpg"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "T-Shirt",
                    Description = "T-Shirt",
                    Price = 20,
                    CategoryId = new Guid("FC4E00F1-B435-4AAC-8663-87A5AD2920E8"),
                    Picture = $"/images/products/96751b89-8e8d-4a8e-957d-c3f4a9da152f-USPOLO TiShert.jpeg"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Rice",
                    Description = "Rice",
                    Price = 10,
                    CategoryId = new Guid("601542F2-78D1-4451-A54D-FC213B3C95C4"),
                    Picture = $"/images/products/a59654d1-ae55-4290-a846-0e7ebb0be68a-Rice.jpeg"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Mobile",
                    Description = "Mobile",
                    Price = 500,
                    CategoryId = new Guid("5BF54ECD-E88B-469B-8F35-FB3E942E208E"),
                    Picture = $"/images/products/cd6b0b57-75de-4fd6-8dca-6d37764b2bfc-iPhone.jpg"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Jeans",
                    Description = "Jeans",
                    Price = 50,
                    CategoryId = new Guid("FC4E00F1-B435-4AAC-8663-87A5AD2920E8"),
                    Picture = $"/images/products/592e9e73-379a-456f-b90b-00827f21ccad-Jeans.jpg"
                }
            );
        }
    }
}
