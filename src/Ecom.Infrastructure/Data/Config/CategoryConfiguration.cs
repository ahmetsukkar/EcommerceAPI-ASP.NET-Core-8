using Ecom.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Data.Config
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(30);


            // SeedData

            builder.HasData(
                new Category
                {
                    Id = new Guid("5BF54ECD-E88B-469B-8F35-FB3E942E208E"),
                    Name = "Electronics",
                    Description = "Electronic Items"
                },
                new Category
                {
                    Id = new Guid("FC4E00F1-B435-4AAC-8663-87A5AD2920E8"),
                    Name = "Clothing",
                    Description = "Clothing Items"
                },
                new Category
                {
                    Id = new Guid("601542F2-78D1-4451-A54D-FC213B3C95C4"),
                    Name = "Grocery",
                    Description = "Grocery Items"
                }
            );
        }
    }
}
