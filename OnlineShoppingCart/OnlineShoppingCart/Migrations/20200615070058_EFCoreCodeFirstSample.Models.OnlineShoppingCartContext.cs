using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShoppingCart.Migrations
{
    public partial class EFCoreCodeFirstSampleModelsOnlineShoppingCartContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    ContactNumber = table.Column<string>(maxLength: 12, nullable: true),
                    AddressLine1 = table.Column<string>(nullable: true),
                    AddressLine2 = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(nullable: false),
                    UnitPrice = table.Column<float>(nullable: false),
                    UnitsInStock = table.Column<long>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CategoriesCategoryId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoriesCategoryId",
                        column: x => x.CategoriesCategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalAmount = table.Column<float>(nullable: false),
                    Discount = table.Column<float>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    CustomersCustomerId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomersCustomerId",
                        column: x => x.CustomersCustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitPrice = table.Column<float>(nullable: false),
                    Quantity = table.Column<long>(nullable: false),
                    TotalAmount = table.Column<float>(nullable: false),
                    OrdersOrderId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrdersOrderId",
                        column: x => x.OrdersOrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentType = table.Column<string>(nullable: true),
                    OrdersOrderId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Orders_OrdersOrderId",
                        column: x => x.OrdersOrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItemProduct",
                columns: table => new
                {
                    OrderItemProductID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(nullable: false),
                    ProductsProductId = table.Column<long>(nullable: true),
                    OrderItemID = table.Column<int>(nullable: false),
                    OrderItemsOrderItemId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemProduct", x => x.OrderItemProductID);
                    table.ForeignKey(
                        name: "FK_OrderItemProduct_OrderItems_OrderItemsOrderItemId",
                        column: x => x.OrderItemsOrderItemId,
                        principalTable: "OrderItems",
                        principalColumn: "OrderItemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItemProduct_Products_ProductsProductId",
                        column: x => x.ProductsProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemProduct_OrderItemsOrderItemId",
                table: "OrderItemProduct",
                column: "OrderItemsOrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemProduct_ProductsProductId",
                table: "OrderItemProduct",
                column: "ProductsProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrdersOrderId",
                table: "OrderItems",
                column: "OrdersOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomersCustomerId",
                table: "Orders",
                column: "CustomersCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrdersOrderId",
                table: "Payments",
                column: "OrdersOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoriesCategoryId",
                table: "Products",
                column: "CategoriesCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItemProduct");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
