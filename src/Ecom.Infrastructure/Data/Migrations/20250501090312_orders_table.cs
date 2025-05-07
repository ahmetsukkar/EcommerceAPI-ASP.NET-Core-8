using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecom.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class orders_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("0af29222-5f00-4b2c-8386-e85084bbf2e3"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("432b3965-f08b-4074-944a-f7d352d77745"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("638a6249-7376-4f78-9a83-f59da9f5fe65"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("777ac8dd-3025-4ff4-90ad-c14f2c0cb390"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("bd4d6b63-56be-435b-88ea-8c3f3e1fccf5"));

            migrationBuilder.CreateTable(
                name: "DeliveryMethods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuyerEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShipToAddress_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShipToAddress_LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShipToAddress_Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShipToAddress_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShipToAddress_State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShipToAddress_ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_DeliveryMethods_DeliveryMethodId",
                        column: x => x.DeliveryMethodId,
                        principalTable: "DeliveryMethods",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    productItemOrdered_ProductItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    productItemOrdered_ProductItemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    productItemOrdered_PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DeliveryMethods",
                columns: new[] { "Id", "DeliveryTime", "Description", "Price", "ShortName" },
                values: new object[,]
                {
                    { new Guid("3155b100-f20b-49a9-b95b-ed04ec19bd6e"), "4-7 days", "Slower but cheap and secure", 13.99m, "FEDEX" },
                    { new Guid("36fe2842-fc7f-4adc-968d-3bef55497205"), "3-6 days", "Maximum get with 6 work days", 11.99m, "ARAMEX" },
                    { new Guid("a745249e-437c-42e7-a953-db03b4f88dad"), "2-3 days", "Fastest Delivery Time", 19.99m, "DHL" },
                    { new Guid("b1b9ac11-0abd-4b86-a1eb-98c10b67935c"), "Free, 7-15 days", "Standard delivery", 0.99m, "Standard" }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryMethodId",
                table: "Orders",
                column: "DeliveryMethodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "DeliveryMethods");

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

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Picture", "Price" },
                values: new object[,]
                {
                    { new Guid("0af29222-5f00-4b2c-8386-e85084bbf2e3"), new Guid("fc4e00f1-b435-4aac-8663-87a5ad2920e8"), "Jeans", "Jeans", "/images/products/Jeans.jpg", 50m },
                    { new Guid("432b3965-f08b-4074-944a-f7d352d77745"), new Guid("fc4e00f1-b435-4aac-8663-87a5ad2920e8"), "T-Shirt", "T-Shirt", "/images/products/T-Shirt.jpg", 20m },
                    { new Guid("638a6249-7376-4f78-9a83-f59da9f5fe65"), new Guid("601542f2-78d1-4451-a54d-fc213b3c95c4"), "Rice", "Rice", "/images/products/Rice.jpg", 10m },
                    { new Guid("777ac8dd-3025-4ff4-90ad-c14f2c0cb390"), new Guid("5bf54ecd-e88b-469b-8f35-fb3e942e208e"), "Laptop", "Laptop", "/images/products/Laptop.jpg", 1000m },
                    { new Guid("bd4d6b63-56be-435b-88ea-8c3f3e1fccf5"), new Guid("5bf54ecd-e88b-469b-8f35-fb3e942e208e"), "Mobile", "Mobile", "/images/products/Mobile.jpg", 500m }
                });
        }
    }
}
