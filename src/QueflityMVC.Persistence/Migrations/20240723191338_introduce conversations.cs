using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueflityMVC.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class introduceconversations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SetElements_Product_KitId",
                table: "SetElements");

            migrationBuilder.DeleteData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "Conversations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conversations_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConversationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Health");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Computers");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Beauty");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Toys");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Kids");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Rubber");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Frozen");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Cotton");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Soft");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Soft");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Metal");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Granite");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Soft");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Concrete");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 11,
                column: "Name",
                value: "Plastic");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 12,
                column: "Name",
                value: "Plastic");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 13,
                column: "Name",
                value: "Cotton");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 14,
                column: "Name",
                value: "Steel");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 16,
                column: "Name",
                value: "Plastic");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 18,
                column: "Name",
                value: "Cotton");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 19,
                column: "Name",
                value: "Granite");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 20,
                column: "Name",
                value: "Frozen");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "nulla", "https://picsum.photos/640/480/?image=367" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "cum", "https://picsum.photos/640/480/?image=986" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "quas", "https://picsum.photos/640/480/?image=764" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "laboriosam", "https://picsum.photos/640/480/?image=337" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "velit", "https://picsum.photos/640/480/?image=449" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "eos", "https://picsum.photos/640/480/?image=572" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "qui", "https://picsum.photos/640/480/?image=716" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "quos", "https://picsum.photos/640/480/?image=209" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "eveniet", "https://picsum.photos/640/480/?image=1023" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "consequatur", "https://picsum.photos/640/480/?image=149" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "qui", "https://picsum.photos/640/480/?image=953" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "vel", "https://picsum.photos/640/480/?image=981" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "vel", "https://picsum.photos/640/480/?image=797" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "adipisci", "https://picsum.photos/640/480/?image=558" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "veniam", "https://picsum.photos/640/480/?image=817" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "laborum", "https://picsum.photos/640/480/?image=495" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "necessitatibus", "https://picsum.photos/640/480/?image=700" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "quia", "https://picsum.photos/640/480/?image=1049" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "nostrum", "https://picsum.photos/640/480/?image=650" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "qui", "https://picsum.photos/640/480/?image=63" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CategoryId", "Name", "OrderNo", "Price", "ShouldBeShown" },
                values: new object[] { 5, "Rustic Soft Cheese", null, 53.15m, false });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 2, "Unbranded Steel Salad", 7.32m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 4, "Ergonomic Plastic Mouse", 15.51m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 3, "Rustic Granite Bike", 2.10m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CategoryId", "Name", "OrderNo", "Price", "ShouldBeShown" },
                values: new object[] { 2, "Rustic Rubber Mouse", 5L, 78.55m, true });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Name", "OrderNo", "Price" },
                values: new object[] { "Unbranded Concrete Shirt", 3L, 4.74m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Name", "OrderNo", "Price", "ShouldBeShown" },
                values: new object[] { "Fantastic Frozen Bacon", 1L, 52.40m, true });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CategoryId", "Name", "OrderNo", "Price", "ShouldBeShown" },
                values: new object[] { 4, "Sleek Cotton Car", 4L, 154.95m, true });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CategoryId", "Name", "OrderNo", "Price", "ShouldBeShown" },
                values: new object[] { 2, "Licensed Soft Computer", null, 114.06m, false });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CategoryId", "Name", "OrderNo", "Price" },
                values: new object[] { 1, "Incredible Rubber Keyboard", 2L, 141.39m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Description", "Name", "OrderNo", "ShouldBeShown" },
                values: new object[] { "Veniam laboriosam alias et cupiditate.", "Incredible Metal Chips", 9L, true });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Description", "Name", "OrderNo", "ShouldBeShown" },
                values: new object[] { "Quia voluptatibus quasi.", "Unbranded Soft Chicken", null, false });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Description", "Name", "OrderNo", "ShouldBeShown" },
                values: new object[] { "Suscipit cupiditate aut odio iusto hic.", "Licensed Metal Soap", 8L, true });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "Name", "OrderNo", "ShouldBeShown" },
                values: new object[] { "Officia illo enim iure consequatur perferendis.", "Unbranded Fresh Keyboard", null, false });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Description", "Name", "OrderNo", "ShouldBeShown" },
                values: new object[] { "Recusandae mollitia error.", "Gorgeous Metal Chair", 7L, true });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Description", "Name", "OrderNo" },
                values: new object[] { "Perferendis beatae soluta.", "Practical Soft Soap", 0L });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Unde quibusdam nobis.", "Ergonomic Plastic Chair" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Description", "Name", "OrderNo", "ShouldBeShown" },
                values: new object[] { "Voluptatem reiciendis ullam ut dolor et dolorum pariatur.", "Handcrafted Steel Tuna", null, false });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Description", "Name", "OrderNo" },
                values: new object[] { "Omnis consequatur veniam.", "Rustic Fresh Chicken", 6L });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Description", "Name", "OrderNo", "ShouldBeShown" },
                values: new object[] { "Qui et labore in voluptatem aspernatur doloremque.", "Refined Frozen Salad", null, false });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ItemId", "KitId", "PricePerItem" },
                values: new object[] { 10, 19, 88.79m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 6, 8L, 18, 35.31m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 9, 6L, 11, 105.83m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ItemId", "ItemsAmount", "PricePerItem" },
                values: new object[] { 1, 7L, 37.89m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 4, 7L, 20, 3.06m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 9, 3L, 15, 53.10m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ItemId", "KitId", "PricePerItem" },
                values: new object[] { 2, 11, 105.47m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 7, 2L, 15, 12.79m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 5, 1L, 17, 78.37m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 4, 1L, 12, 29.01m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 10, 8L, 14, 110.85m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 7, 4L, 13, 48.64m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 8, 3L, 15, 121.28m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 9, 10L, 20, 55.38m });

            migrationBuilder.InsertData(
                table: "SetElements",
                columns: new[] { "Id", "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 13, 5, 10L, 14, 13.03m });

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_ProductId",
                table: "Conversations",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ConversationId",
                table: "Messages",
                column: "ConversationId");

            migrationBuilder.AddForeignKey(
                name: "FK_SetElements_Product_KitId",
                table: "SetElements",
                column: "KitId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SetElements_Product_KitId",
                table: "SetElements");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Conversations");

            migrationBuilder.DeleteData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Movies");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Industrial");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Movies");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Sports");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Garden");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Soft");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Wooden");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Plastic");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Metal");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Wooden");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Frozen");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Metal");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Metal");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Steel");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 11,
                column: "Name",
                value: "Steel");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 12,
                column: "Name",
                value: "Granite");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 13,
                column: "Name",
                value: "Rubber");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 14,
                column: "Name",
                value: "Soft");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 16,
                column: "Name",
                value: "Cotton");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 18,
                column: "Name",
                value: "Soft");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 19,
                column: "Name",
                value: "Rubber");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 20,
                column: "Name",
                value: "Steel");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "itaque", "https://picsum.photos/640/480/?image=860" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "sit", "https://picsum.photos/640/480/?image=723" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "non", "https://picsum.photos/640/480/?image=775" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "libero", "https://picsum.photos/640/480/?image=1056" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "et", "https://picsum.photos/640/480/?image=771" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "quo", "https://picsum.photos/640/480/?image=538" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "ut", "https://picsum.photos/640/480/?image=687" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "sed", "https://picsum.photos/640/480/?image=911" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "beatae", "https://picsum.photos/640/480/?image=906" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "quidem", "https://picsum.photos/640/480/?image=231" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "alias", "https://picsum.photos/640/480/?image=312" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "et", "https://picsum.photos/640/480/?image=29" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "quia", "https://picsum.photos/640/480/?image=793" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "quam", "https://picsum.photos/640/480/?image=900" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "aut", "https://picsum.photos/640/480/?image=32" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "culpa", "https://picsum.photos/640/480/?image=915" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "ut", "https://picsum.photos/640/480/?image=645" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "magnam", "https://picsum.photos/640/480/?image=445" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "voluptatem", "https://picsum.photos/640/480/?image=1005" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "non", "https://picsum.photos/640/480/?image=1021" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CategoryId", "Name", "OrderNo", "Price", "ShouldBeShown" },
                values: new object[] { 4, "Handmade Rubber Cheese", 1L, 87.53m, true });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 4, "Incredible Frozen Bacon", 17.01m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 2, "Generic Plastic Car", 105.78m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 2, "Awesome Metal Soap", 117.39m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CategoryId", "Name", "OrderNo", "Price", "ShouldBeShown" },
                values: new object[] { 1, "Awesome Metal Mouse", null, 27.65m, false });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Name", "OrderNo", "Price" },
                values: new object[] { "Sleek Fresh Towels", 6L, 161.68m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Name", "OrderNo", "Price", "ShouldBeShown" },
                values: new object[] { "Rustic Frozen Sausages", null, 18.83m, false });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CategoryId", "Name", "OrderNo", "Price", "ShouldBeShown" },
                values: new object[] { 2, "Tasty Granite Salad", null, 3.10m, false });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CategoryId", "Name", "OrderNo", "Price", "ShouldBeShown" },
                values: new object[] { 5, "Practical Plastic Bacon", 9L, 7.99m, true });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CategoryId", "Name", "OrderNo", "Price" },
                values: new object[] { 2, "Fantastic Metal Hat", 5L, 10.78m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Description", "Name", "OrderNo", "ShouldBeShown" },
                values: new object[] { "Et dolore aut.", "Unbranded Metal Keyboard", null, false });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Description", "Name", "OrderNo", "ShouldBeShown" },
                values: new object[] { "Ad voluptas aut repudiandae omnis aliquam.", "Incredible Fresh Bike", 8L, true });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Description", "Name", "OrderNo", "ShouldBeShown" },
                values: new object[] { "Voluptatum consequatur sint eaque animi expedita.", "Fantastic Rubber Ball", null, false });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "Name", "OrderNo", "ShouldBeShown" },
                values: new object[] { "Quia dolor tenetur aut delectus doloremque eum quas et.", "Awesome Granite Cheese", 0L, true });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Description", "Name", "OrderNo", "ShouldBeShown" },
                values: new object[] { "Laudantium ut omnis ex expedita sunt facere repellat.", "Incredible Steel Table", null, false });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Description", "Name", "OrderNo" },
                values: new object[] { "Tempore nam dolor non.", "Incredible Concrete Pizza", 3L });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Placeat harum fugiat provident inventore rerum voluptate natus id non.", "Generic Concrete Sausages" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Description", "Name", "OrderNo", "ShouldBeShown" },
                values: new object[] { "Ut nam eos corrupti et id quia velit.", "Tasty Wooden Computer", 7L, true });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Description", "Name", "OrderNo" },
                values: new object[] { "Quis aperiam ut dignissimos excepturi quo.", "Awesome Cotton Sausages", 2L });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Description", "Name", "OrderNo", "ShouldBeShown" },
                values: new object[] { "Voluptatem fugiat odit rerum ratione.", "Handmade Wooden Tuna", 4L, true });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ItemId", "KitId", "PricePerItem" },
                values: new object[] { 6, 17, 103.11m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 4, 7L, 14, 20.54m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 2, 3L, 16, 12.13m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ItemId", "ItemsAmount", "PricePerItem" },
                values: new object[] { 5, 8L, 36.52m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 8, 6L, 19, 43.38m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 1, 10L, 12, 8.76m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ItemId", "KitId", "PricePerItem" },
                values: new object[] { 4, 12, 63.18m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 1, 3L, 19, 66.24m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 2, 7L, 12, 62.95m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 1, 7L, 17, 142.35m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 1, 6L, 13, 18.83m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 5, 7L, 11, 42.96m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 4, 5L, 20, 52.88m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 4, 5L, 19, 33.16m });

            migrationBuilder.InsertData(
                table: "SetElements",
                columns: new[] { "Id", "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 16, 8, 4L, 13, 7.35m });

            migrationBuilder.AddForeignKey(
                name: "FK_SetElements_Product_KitId",
                table: "SetElements",
                column: "KitId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
