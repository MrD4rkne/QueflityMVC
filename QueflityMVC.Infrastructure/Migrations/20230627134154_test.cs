using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueflityMVC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Images_ImageId",
                table: "Items");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Images_ImageId",
                table: "Items",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Images_ImageId",
                table: "Items");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Images_ImageId",
                table: "Items",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }
    }
}
