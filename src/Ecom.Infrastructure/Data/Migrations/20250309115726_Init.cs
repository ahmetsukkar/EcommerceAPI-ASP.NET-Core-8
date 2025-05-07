using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecom.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("5bf54ecd-e88b-469b-8f35-fb3e942e208e"), "Electronic Items", "Electronics" },
                    { new Guid("601542f2-78d1-4451-a54d-fc213b3c95c4"), "Grocery Items", "Grocery" },
                    { new Guid("fc4e00f1-b435-4aac-8663-87a5ad2920e8"), "Clothing Items", "Clothing" }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
