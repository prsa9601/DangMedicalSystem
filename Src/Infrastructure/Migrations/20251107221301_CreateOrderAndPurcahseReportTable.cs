using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateOrderAndPurcahseReportTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "order");

            migrationBuilder.AlterColumn<string>(
                name: "ImageName",
                schema: "user",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Default.png",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Default");

            migrationBuilder.CreateTable(
                name: "OrderItems",
                schema: "order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfPurchase = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalPrice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalDang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalProfit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchasePrice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchaseDang = table.Column<int>(type: "int", nullable: false),
                    Profit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfitPerDang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchasePricePerDang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchaseDangPerDang = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                schema: "order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PricePerDong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DongAmount = table.Column<int>(type: "int", nullable: false),
                    InventoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => new { x.OrderId, x.Id });
                    table.ForeignKey(
                        name: "FK_OrderItem_OrderItems_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "order",
                        principalTable: "OrderItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem",
                schema: "order");

            migrationBuilder.DropTable(
                name: "PurchaseReports");

            migrationBuilder.DropTable(
                name: "OrderItems",
                schema: "order");

            migrationBuilder.AlterColumn<string>(
                name: "ImageName",
                schema: "user",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Default",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Default.png");
        }
    }
}
