using Ecom.Core.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Data.Config
{
    class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(d => d.Price)
                .HasColumnType("decimal(18,2)");

            builder.HasData(
                new DeliveryMethod { Id = new Guid("B1B9AC11-0ABD-4B86-A1EB-98C10B67935C"), ShortName = "Standard", DeliveryTime = "Free, 7-15 days", Price = 0.99m, Description = "Standard delivery" },
                new DeliveryMethod { Id = new Guid("A745249E-437C-42E7-A953-DB03B4F88DAD"), ShortName = "DHL", DeliveryTime = "2-3 days", Price = 19.99m, Description = "Fastest Delivery Time" },
                new DeliveryMethod { Id = new Guid("36FE2842-FC7F-4ADC-968D-3BEF55497205"), ShortName = "ARAMEX", DeliveryTime = "3-6 days", Price = 11.99m, Description = "Maximum get with 6 work days" },
                new DeliveryMethod { Id = new Guid("3155B100-F20B-49A9-B95B-ED04EC19BD6E"), ShortName = "FEDEX", DeliveryTime = "4-7 days", Price = 13.99m, Description = "Slower but cheap and secure" }
                );
        }
    }
}
