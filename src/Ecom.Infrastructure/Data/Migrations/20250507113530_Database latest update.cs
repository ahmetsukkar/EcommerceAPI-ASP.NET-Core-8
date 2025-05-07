using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecom.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Databaselatestupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "PaymentIntentId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Picture", "Price" },
                values: new object[,]
                {
                    { new Guid("b051c7b5-3e38-40c8-a8bf-8d299a5fb4b2"), new Guid("fc4e00f1-b435-4aac-8663-87a5ad2920e8"), "Jeans", "Jeans", "/images/products/592e9e73-379a-456f-b90b-00827f21ccad-Jeans.jpg", 50m },
                    { new Guid("c56f8414-190a-4671-8f7e-4c25a5c1fc2d"), new Guid("601542f2-78d1-4451-a54d-fc213b3c95c4"), "Rice", "Rice", "/images/products/a59654d1-ae55-4290-a846-0e7ebb0be68a-Rice.jpeg", 10m },
                    { new Guid("c74aebf5-37bb-4fe1-b311-6db04847c52e"), new Guid("5bf54ecd-e88b-469b-8f35-fb3e942e208e"), "Mobile", "Mobile", "/images/products/cd6b0b57-75de-4fd6-8dca-6d37764b2bfc-iPhone.jpg", 500m },
                    { new Guid("cc293d32-687d-4eed-b1b6-db047b1cd34f"), new Guid("5bf54ecd-e88b-469b-8f35-fb3e942e208e"), "Laptop", "Laptop", "/images/products/27a8a874-be16-4fef-b52f-a27ba8873c8a-Laptop.jpg", 1000m },
                    { new Guid("ff0b8250-f58c-48e7-a52b-a74be64c8450"), new Guid("fc4e00f1-b435-4aac-8663-87a5ad2920e8"), "T-Shirt", "T-Shirt", "/images/products/96751b89-8e8d-4a8e-957d-c3f4a9da152f-USPOLO TiShert.jpeg", 20m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b051c7b5-3e38-40c8-a8bf-8d299a5fb4b2"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c56f8414-190a-4671-8f7e-4c25a5c1fc2d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c74aebf5-37bb-4fe1-b311-6db04847c52e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("cc293d32-687d-4eed-b1b6-db047b1cd34f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ff0b8250-f58c-48e7-a52b-a74be64c8450"));

            migrationBuilder.AlterColumn<Guid>(
                name: "PaymentIntentId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
    }
}
