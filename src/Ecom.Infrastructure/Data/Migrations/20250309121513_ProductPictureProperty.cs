using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecom.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProductPictureProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1812a3fb-1b21-42aa-b7c2-b3651a5ca1f7"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3e2dc740-fc6b-499f-abf2-f012063b9faf"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("7e76b793-21ad-43d1-bd5c-afd8d373b513"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("965b6b09-266f-4fb1-a683-a45751cb4e2a"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a64ea1bc-1b90-4138-ba48-2303af5e41c7"));

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Picture", "Price" },
                values: new object[,]
                {
                    { new Guid("0cb15aa5-6f0a-4622-9ca6-8aab9fe2a669"), new Guid("5bf54ecd-e88b-469b-8f35-fb3e942e208e"), "Laptop", "Laptop", null, 1000m },
                    { new Guid("2f09e29d-3e8d-4803-8f05-2292c31bfcbf"), new Guid("fc4e00f1-b435-4aac-8663-87a5ad2920e8"), "T-Shirt", "T-Shirt", null, 20m },
                    { new Guid("80eba83e-25c3-4f5a-87ef-f93b09b94de6"), new Guid("fc4e00f1-b435-4aac-8663-87a5ad2920e8"), "Jeans", "Jeans", null, 50m },
                    { new Guid("906a08df-dc68-4383-9703-14a81fce3041"), new Guid("5bf54ecd-e88b-469b-8f35-fb3e942e208e"), "Mobile", "Mobile", null, 500m },
                    { new Guid("b8058830-6986-4b6a-8741-353238f889b0"), new Guid("601542f2-78d1-4451-a54d-fc213b3c95c4"), "Rice", "Rice", null, 10m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("0cb15aa5-6f0a-4622-9ca6-8aab9fe2a669"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2f09e29d-3e8d-4803-8f05-2292c31bfcbf"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("80eba83e-25c3-4f5a-87ef-f93b09b94de6"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("906a08df-dc68-4383-9703-14a81fce3041"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b8058830-6986-4b6a-8741-353238f889b0"));

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Products");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("1812a3fb-1b21-42aa-b7c2-b3651a5ca1f7"), new Guid("5bf54ecd-e88b-469b-8f35-fb3e942e208e"), "Mobile", "Mobile", 500m },
                    { new Guid("3e2dc740-fc6b-499f-abf2-f012063b9faf"), new Guid("fc4e00f1-b435-4aac-8663-87a5ad2920e8"), "Jeans", "Jeans", 50m },
                    { new Guid("7e76b793-21ad-43d1-bd5c-afd8d373b513"), new Guid("601542f2-78d1-4451-a54d-fc213b3c95c4"), "Rice", "Rice", 10m },
                    { new Guid("965b6b09-266f-4fb1-a683-a45751cb4e2a"), new Guid("5bf54ecd-e88b-469b-8f35-fb3e942e208e"), "Laptop", "Laptop", 1000m },
                    { new Guid("a64ea1bc-1b90-4138-ba48-2303af5e41c7"), new Guid("fc4e00f1-b435-4aac-8663-87a5ad2920e8"), "T-Shirt", "T-Shirt", 20m }
                });
        }
    }
}
