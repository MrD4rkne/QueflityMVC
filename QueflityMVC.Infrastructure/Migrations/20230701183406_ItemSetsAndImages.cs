using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueflityMVC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ItemSetsAndImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Images_ImageId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "ItemItemSet");

            migrationBuilder.DropIndex(
                name: "IX_Items_ImageId",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Items",
                newName: "ItemSetId");

            migrationBuilder.AddColumn<int>(
                name: "ItemSetImageId",
                table: "ItemSets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ShouldBeShown",
                table: "ItemSets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ItemImageId",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ShouldBeShown",
                table: "Items",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ItemSetImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AltDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemSetImage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SetMembership",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemsAmount = table.Column<long>(type: "bigint", nullable: false),
                    PricePerItem = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ItemSetId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetMembership", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SetMembership_ItemSets_ItemSetId",
                        column: x => x.ItemSetId,
                        principalTable: "ItemSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SetMembership_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemSets_ItemSetImageId",
                table: "ItemSets",
                column: "ItemSetImageId",
                unique: true,
                filter: "[ItemSetImageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemImageId",
                table: "Items",
                column: "ItemImageId",
                unique: true,
                filter: "[ItemImageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemSetId",
                table: "Items",
                column: "ItemSetId");

            migrationBuilder.CreateIndex(
                name: "IX_SetMembership_ItemId",
                table: "SetMembership",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SetMembership_ItemSetId",
                table: "SetMembership",
                column: "ItemSetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Images_ItemImageId",
                table: "Items",
                column: "ItemImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemSets_ItemSetId",
                table: "Items",
                column: "ItemSetId",
                principalTable: "ItemSets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSets_ItemSetImage_ItemSetImageId",
                table: "ItemSets",
                column: "ItemSetImageId",
                principalTable: "ItemSetImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Images_ItemImageId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemSets_ItemSetId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSets_ItemSetImage_ItemSetImageId",
                table: "ItemSets");

            migrationBuilder.DropTable(
                name: "ItemSetImage");

            migrationBuilder.DropTable(
                name: "SetMembership");

            migrationBuilder.DropIndex(
                name: "IX_ItemSets_ItemSetImageId",
                table: "ItemSets");

            migrationBuilder.DropIndex(
                name: "IX_Items_ItemImageId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_ItemSetId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ItemSetImageId",
                table: "ItemSets");

            migrationBuilder.DropColumn(
                name: "ShouldBeShown",
                table: "ItemSets");

            migrationBuilder.DropColumn(
                name: "ItemImageId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ShouldBeShown",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "ItemSetId",
                table: "Items",
                newName: "ImageId");

            migrationBuilder.CreateTable(
                name: "ItemItemSet",
                columns: table => new
                {
                    ItemSetsId = table.Column<int>(type: "int", nullable: false),
                    ItemsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemItemSet", x => new { x.ItemSetsId, x.ItemsId });
                    table.ForeignKey(
                        name: "FK_ItemItemSet_ItemSets_ItemSetsId",
                        column: x => x.ItemSetsId,
                        principalTable: "ItemSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemItemSet_Items_ItemsId",
                        column: x => x.ItemsId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_ImageId",
                table: "Items",
                column: "ImageId",
                unique: true,
                filter: "[ImageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ItemItemSet_ItemsId",
                table: "ItemItemSet",
                column: "ItemsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Images_ImageId",
                table: "Items",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
