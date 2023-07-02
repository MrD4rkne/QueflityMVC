using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueflityMVC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewEntitesTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Images_ItemImageId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSets_ItemSetImage_ItemSetImageId",
                table: "ItemSets");

            migrationBuilder.DropForeignKey(
                name: "FK_SetMembership_ItemSets_ItemSetId",
                table: "SetMembership");

            migrationBuilder.DropForeignKey(
                name: "FK_SetMembership_Items_ItemId",
                table: "SetMembership");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SetMembership",
                table: "SetMembership");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemSetImage",
                table: "ItemSetImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.RenameTable(
                name: "SetMembership",
                newName: "SetMemberships");

            migrationBuilder.RenameTable(
                name: "ItemSetImage",
                newName: "ItemSetImages");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "ItemImages");

            migrationBuilder.RenameIndex(
                name: "IX_SetMembership_ItemSetId",
                table: "SetMemberships",
                newName: "IX_SetMemberships_ItemSetId");

            migrationBuilder.RenameIndex(
                name: "IX_SetMembership_ItemId",
                table: "SetMemberships",
                newName: "IX_SetMemberships_ItemId");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "ItemSets",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Items",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "PricePerItem",
                table: "SetMemberships",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SetMemberships",
                table: "SetMemberships",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemSetImages",
                table: "ItemSetImages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemImages",
                table: "ItemImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemImages_ItemImageId",
                table: "Items",
                column: "ItemImageId",
                principalTable: "ItemImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSets_ItemSetImages_ItemSetImageId",
                table: "ItemSets",
                column: "ItemSetImageId",
                principalTable: "ItemSetImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SetMemberships_ItemSets_ItemSetId",
                table: "SetMemberships",
                column: "ItemSetId",
                principalTable: "ItemSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SetMemberships_Items_ItemId",
                table: "SetMemberships",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemImages_ItemImageId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSets_ItemSetImages_ItemSetImageId",
                table: "ItemSets");

            migrationBuilder.DropForeignKey(
                name: "FK_SetMemberships_ItemSets_ItemSetId",
                table: "SetMemberships");

            migrationBuilder.DropForeignKey(
                name: "FK_SetMemberships_Items_ItemId",
                table: "SetMemberships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SetMemberships",
                table: "SetMemberships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemSetImages",
                table: "ItemSetImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemImages",
                table: "ItemImages");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "ItemSets");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Items");

            migrationBuilder.RenameTable(
                name: "SetMemberships",
                newName: "SetMembership");

            migrationBuilder.RenameTable(
                name: "ItemSetImages",
                newName: "ItemSetImage");

            migrationBuilder.RenameTable(
                name: "ItemImages",
                newName: "Images");

            migrationBuilder.RenameIndex(
                name: "IX_SetMemberships_ItemSetId",
                table: "SetMembership",
                newName: "IX_SetMembership_ItemSetId");

            migrationBuilder.RenameIndex(
                name: "IX_SetMemberships_ItemId",
                table: "SetMembership",
                newName: "IX_SetMembership_ItemId");

            migrationBuilder.AlterColumn<decimal>(
                name: "PricePerItem",
                table: "SetMembership",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,2)",
                oldPrecision: 14,
                oldScale: 2);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SetMembership",
                table: "SetMembership",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemSetImage",
                table: "ItemSetImage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Images_ItemImageId",
                table: "Items",
                column: "ItemImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSets_ItemSetImage_ItemSetImageId",
                table: "ItemSets",
                column: "ItemSetImageId",
                principalTable: "ItemSetImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SetMembership_ItemSets_ItemSetId",
                table: "SetMembership",
                column: "ItemSetId",
                principalTable: "ItemSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SetMembership_Items_ItemId",
                table: "SetMembership",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
