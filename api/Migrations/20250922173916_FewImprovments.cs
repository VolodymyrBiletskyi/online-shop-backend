using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class FewImprovments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_ProductVariants_productVariantsId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDiscounts_Coupons_CouponId",
                table: "OrderDiscounts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ProductVariants_ProductVariantsId",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "Tax_amount",
                table: "Orders",
                newName: "TaxAmount");

            migrationBuilder.RenameColumn(
                name: "Subtotal_amount",
                table: "Orders",
                newName: "SubtotalAmount");

            migrationBuilder.RenameColumn(
                name: "Disscount_amount",
                table: "Orders",
                newName: "DisscountAmount");

            migrationBuilder.RenameColumn(
                name: "Sku_snapshot",
                table: "OrderItems",
                newName: "SkuSnapshot");

            migrationBuilder.RenameColumn(
                name: "Product_name_snapshot",
                table: "OrderItems",
                newName: "ProductNameSnapshot");

            migrationBuilder.RenameColumn(
                name: "ProductVariantsId",
                table: "OrderItems",
                newName: "ProductVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_ProductVariantsId",
                table: "OrderItems",
                newName: "IX_OrderItems_ProductVariantId");

            migrationBuilder.RenameColumn(
                name: "productVariantsId",
                table: "Inventory",
                newName: "ProductVariantId");

            migrationBuilder.RenameColumn(
                name: "Quantity_reserved",
                table: "Inventory",
                newName: "QuantityReserved");

            migrationBuilder.RenameColumn(
                name: "Quantity_on_hand",
                table: "Inventory",
                newName: "QuantityOnHand");

            migrationBuilder.RenameIndex(
                name: "IX_Inventory_productVariantsId",
                table: "Inventory",
                newName: "IX_Inventory_ProductVariantId");

            migrationBuilder.AlterColumn<Guid>(
                name: "CouponId",
                table: "OrderDiscounts",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartsAt",
                table: "Coupons",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndsAt",
                table: "Coupons",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_ProductVariants_ProductVariantId",
                table: "Inventory",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDiscounts_Coupons_CouponId",
                table: "OrderDiscounts",
                column: "CouponId",
                principalTable: "Coupons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ProductVariants_ProductVariantId",
                table: "OrderItems",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_ProductVariants_ProductVariantId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDiscounts_Coupons_CouponId",
                table: "OrderDiscounts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ProductVariants_ProductVariantId",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "TaxAmount",
                table: "Orders",
                newName: "Tax_amount");

            migrationBuilder.RenameColumn(
                name: "SubtotalAmount",
                table: "Orders",
                newName: "Subtotal_amount");

            migrationBuilder.RenameColumn(
                name: "DisscountAmount",
                table: "Orders",
                newName: "Disscount_amount");

            migrationBuilder.RenameColumn(
                name: "SkuSnapshot",
                table: "OrderItems",
                newName: "Sku_snapshot");

            migrationBuilder.RenameColumn(
                name: "ProductVariantId",
                table: "OrderItems",
                newName: "ProductVariantsId");

            migrationBuilder.RenameColumn(
                name: "ProductNameSnapshot",
                table: "OrderItems",
                newName: "Product_name_snapshot");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_ProductVariantId",
                table: "OrderItems",
                newName: "IX_OrderItems_ProductVariantsId");

            migrationBuilder.RenameColumn(
                name: "QuantityReserved",
                table: "Inventory",
                newName: "Quantity_reserved");

            migrationBuilder.RenameColumn(
                name: "QuantityOnHand",
                table: "Inventory",
                newName: "Quantity_on_hand");

            migrationBuilder.RenameColumn(
                name: "ProductVariantId",
                table: "Inventory",
                newName: "productVariantsId");

            migrationBuilder.RenameIndex(
                name: "IX_Inventory_ProductVariantId",
                table: "Inventory",
                newName: "IX_Inventory_productVariantsId");

            migrationBuilder.AlterColumn<Guid>(
                name: "CouponId",
                table: "OrderDiscounts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartsAt",
                table: "Coupons",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndsAt",
                table: "Coupons",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_ProductVariants_productVariantsId",
                table: "Inventory",
                column: "productVariantsId",
                principalTable: "ProductVariants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDiscounts_Coupons_CouponId",
                table: "OrderDiscounts",
                column: "CouponId",
                principalTable: "Coupons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ProductVariants_ProductVariantsId",
                table: "OrderItems",
                column: "ProductVariantsId",
                principalTable: "ProductVariants",
                principalColumn: "Id");
        }
    }
}
