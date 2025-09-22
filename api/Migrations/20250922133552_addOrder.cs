using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class addOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "quantity_reserved",
                table: "Inventory",
                newName: "Quantity_reserved");

            migrationBuilder.RenameColumn(
                name: "quantity_on_hand",
                table: "Inventory",
                newName: "Quantity_on_hand");

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderNumber = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Total_amount = table.Column<int>(type: "integer", nullable: false),
                    Subtotal_amount = table.Column<int>(type: "integer", nullable: false),
                    Disscount_amount = table.Column<int>(type: "integer", nullable: false),
                    Tax_amount = table.Column<int>(type: "integer", nullable: false),
                    Ship_amount = table.Column<int>(type: "integer", nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "Order",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.RenameColumn(
                name: "Quantity_reserved",
                table: "Inventory",
                newName: "quantity_reserved");

            migrationBuilder.RenameColumn(
                name: "Quantity_on_hand",
                table: "Inventory",
                newName: "quantity_on_hand");
        }
    }
}
