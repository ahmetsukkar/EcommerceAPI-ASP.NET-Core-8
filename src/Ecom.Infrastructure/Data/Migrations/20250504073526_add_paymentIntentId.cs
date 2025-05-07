using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecom.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class add_paymentIntentId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("49636757-1ce4-42f2-a475-fbec514711cc"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8606ae7c-3a47-4b17-a5bc-ee054a3b831c"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c0578187-44ca-4c6e-85c8-703293490101"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c1c0deab-2aab-4bf8-b093-561edd0fa09b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c5a22064-2d9d-48b7-86f3-de9be3f5822f"));

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentIntentId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Picture", "Price" },
                values: new object[,]
                {
                    { new Guid("935b6c88-6562-4931-9377-70b9ee09c06f"), new Guid("fc4e00f1-b435-4aac-8663-87a5ad2920e8"), "Jeans", "Jeans", "/images/products/Jeans.jpg", 50m },
                    { new Guid("afee1c9d-0c92-443a-85db-481ae10dbf18"), new Guid("5bf54ecd-e88b-469b-8f35-fb3e942e208e"), "Laptop", "Laptop", "/images/products/Laptop.jpg", 1000m },
                    { new Guid("ba3be2fd-d0d3-4178-8c47-a4fd8e418a41"), new Guid("601542f2-78d1-4451-a54d-fc213b3c95c4"), "Rice", "Rice", "/images/products/Rice.jpg", 10m },
                    { new Guid("be0cd305-ed7a-4b52-af7a-c0d65fa88880"), new Guid("5bf54ecd-e88b-469b-8f35-fb3e942e208e"), "Mobile", "Mobile", "/images/products/Mobile.jpg", 500m },
                    { new Guid("e10fdd13-b82e-408e-85a9-a2566357028e"), new Guid("fc4e00f1-b435-4aac-8663-87a5ad2920e8"), "T-Shirt", "T-Shirt", "/images/products/T-Shirt.jpg", 20m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("935b6c88-6562-4931-9377-70b9ee09c06f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("afee1c9d-0c92-443a-85db-481ae10dbf18"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ba3be2fd-d0d3-4178-8c47-a4fd8e418a41"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("be0cd305-ed7a-4b52-af7a-c0d65fa88880"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e10fdd13-b82e-408e-85a9-a2566357028e"));

            migrationBuilder.DropColumn(
                name: "PaymentIntentId",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Picture", "Price" },
                values: new object[,]
                {
                    { new Guid("49636757-1ce4-42f2-a475-fbec514711cc"), new Guid("5bf54ecd-e88b-469b-8f35-fb3e942e208e"), "Laptop", "Laptop", "/images/products/Laptop.jpg", 1000m },
                    { new Guid("8606ae7c-3a47-4b17-a5bc-ee054a3b831c"), new Guid("5bf54ecd-e88b-469b-8f35-fb3e942e208e"), "Mobile", "Mobile", "/images/products/Mobile.jpg", 500m },
                    { new Guid("c0578187-44ca-4c6e-85c8-703293490101"), new Guid("fc4e00f1-b435-4aac-8663-87a5ad2920e8"), "Jeans", "Jeans", "/images/products/Jeans.jpg", 50m },
                    { new Guid("c1c0deab-2aab-4bf8-b093-561edd0fa09b"), new Guid("fc4e00f1-b435-4aac-8663-87a5ad2920e8"), "T-Shirt", "T-Shirt", "/images/products/T-Shirt.jpg", 20m },
                    { new Guid("c5a22064-2d9d-48b7-86f3-de9be3f5822f"), new Guid("601542f2-78d1-4451-a54d-fc213b3c95c4"), "Rice", "Rice", "/images/products/Rice.jpg", 10m }
                });
        }
    }
}
