using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueflityMVC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ItemSetfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemSets_ItemSetId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_ItemSetId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ItemSetId",
                table: "Items");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemSetId",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemSetId",
                table: "Items",
                column: "ItemSetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemSets_ItemSetId",
                table: "Items",
                column: "ItemSetId",
                principalTable: "ItemSets",
                principalColumn: "Id");
        }
    }
}
