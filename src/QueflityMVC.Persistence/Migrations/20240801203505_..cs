using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QueflityMVC.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Conversations_ConversationId",
                table: "Messages");

            migrationBuilder.DeleteData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Product",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Conversations",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "AspNetUserTokens",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleId",
                table: "AspNetUserRoles",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "AspNetUserRoles",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "AspNetUserLogins",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "AspNetUserClaims",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "AspNetRoles",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleId",
                table: "AspNetRoleClaims",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Toys");

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
                value: "Music");

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
                value: "Outdoors");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Granite");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Granite");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Rubber");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Rubber");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Cotton");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Soft");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Fresh");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Frozen");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 11,
                column: "Name",
                value: "Wooden");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 12,
                column: "Name",
                value: "Cotton");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 13,
                column: "Name",
                value: "Plastic");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 14,
                column: "Name",
                value: "Granite");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 15,
                column: "Name",
                value: "Granite");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 16,
                column: "Name",
                value: "Granite");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 17,
                column: "Name",
                value: "Wooden");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 18,
                column: "Name",
                value: "Frozen");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 19,
                column: "Name",
                value: "Frozen");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 20,
                column: "Name",
                value: "Plastic");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "et", "https://picsum.photos/640/480/?image=85" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "sunt", "https://picsum.photos/640/480/?image=673" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "occaecati", "https://picsum.photos/640/480/?image=301" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "perferendis", "https://picsum.photos/640/480/?image=322" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "ipsa", "https://picsum.photos/640/480/?image=860" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "ex", "https://picsum.photos/640/480/?image=691" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "sint", "https://picsum.photos/640/480/?image=737" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "ex", "https://picsum.photos/640/480/?image=1069" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "facilis", "https://picsum.photos/640/480/?image=929" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "et", "https://picsum.photos/640/480/?image=1066" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "adipisci", "https://picsum.photos/640/480/?image=356" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "quo", "https://picsum.photos/640/480/?image=332" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "quo", "https://picsum.photos/640/480/?image=869" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "at", "https://picsum.photos/640/480/?image=425" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "ea", "https://picsum.photos/640/480/?image=172" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "quam", "https://picsum.photos/640/480/?image=864" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "sapiente", "https://picsum.photos/640/480/?image=1084" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "qui", "https://picsum.photos/640/480/?image=224" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "quo", "https://picsum.photos/640/480/?image=746" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "alias", "https://picsum.photos/640/480/?image=314" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CategoryId", "Name", "OrderNo", "Price" },
                values: new object[] { 3, "Handmade Concrete Bike", 7L, 24.93m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategoryId", "Name", "OrderNo", "Price", "ShouldBeShown" },
                values: new object[] { 1, "Intelligent Frozen Soap", null, 29.69m, false });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 4, "Licensed Plastic Hat", 98.13m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CategoryId", "Name", "OrderNo", "Price", "ShouldBeShown" },
                values: new object[] { 1, "Fantastic Rubber Car", null, 56.64m, false });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CategoryId", "Name", "OrderNo", "Price" },
                values: new object[] { 1, "Practical Steel Salad", 6L, 5.76m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CategoryId", "Name", "OrderNo", "Price", "ShouldBeShown" },
                values: new object[] { 5, "Handcrafted Soft Hat", null, 30.09m, false });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 1, "Handmade Soft Salad", 13.07m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Name", "OrderNo", "Price", "ShouldBeShown" },
                values: new object[] { "Gorgeous Steel Fish", 1L, 13.12m, true });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CategoryId", "Name", "OrderNo", "Price" },
                values: new object[] { 4, "Incredible Metal Cheese", 5L, 12.99m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CategoryId", "Name", "OrderNo", "Price", "ShouldBeShown" },
                values: new object[] { 1, "Incredible Cotton Shirt", null, 36.99m, false });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Description", "Name", "OrderNo" },
                values: new object[] { "Quibusdam dolores quibusdam.", "Gorgeous Soft Gloves", 0L });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Description", "Name", "OrderNo", "ShouldBeShown" },
                values: new object[] { "Hic quia perspiciatis voluptas repudiandae et id mollitia voluptas.", "Sleek Plastic Shirt", null, false });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Voluptatem expedita consectetur sit officia maxime reiciendis accusantium odit alias.", "Fantastic Metal Soap" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "Name", "OrderNo", "ShouldBeShown" },
                values: new object[] { "In unde consequatur magnam et molestias dolores est optio et.", "Fantastic Frozen Cheese", 8L, true });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Description", "Name", "OrderNo", "ShouldBeShown" },
                values: new object[] { "Dolorem est sit porro fuga qui quibusdam sit sapiente labore.", "Sleek Frozen Chips", 3L, true });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Description", "Name", "OrderNo" },
                values: new object[] { "Possimus qui consequatur omnis voluptas illum in itaque similique ratione.", "Small Plastic Shirt", 4L });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Description", "Name", "OrderNo", "ShouldBeShown" },
                values: new object[] { "Voluptas similique architecto placeat.", "Sleek Granite Chair", 9L, true });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Laudantium omnis voluptas voluptate optio aspernatur error.", "Handcrafted Plastic Hat" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Description", "Name", "OrderNo", "ShouldBeShown" },
                values: new object[] { "Velit earum accusantium vitae quia dolores quae et.", "Handmade Rubber Chicken", 2L, true });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Voluptatem quia impedit ab voluptatibus error numquam.", "Incredible Plastic Towels" });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 1, 1L, 15, 85.11m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 8, 6L, 15, 41.69m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 8, 9L, 13, 26.82m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 2, 3L, 15, 109.46m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 1L, 17, 159.32m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 5, 2L, 12, 29.04m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 8, 7L, 17, 19.69m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 5L, 13, 8.66m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 4, 10L, 13, 141.48m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 3, 8L, 11, 43.92m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 4, 9L, 18, 10.30m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 7, 4L, 18, 29.48m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 3, 5L, 12, 37.82m });

            migrationBuilder.InsertData(
                table: "SetElements",
                columns: new[] { "Id", "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[,]
                {
                    { 7, 7, 6L, 16, 4.10m },
                    { 17, 4, 2L, 11, 85.25m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserId",
                table: "Messages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_UserId",
                table: "Conversations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_AspNetUsers_UserId",
                table: "Conversations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_UserId",
                table: "Messages",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Conversations_ConversationId",
                table: "Messages",
                column: "ConversationId",
                principalTable: "Conversations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_AspNetUsers_UserId",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_UserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Conversations_ConversationId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_UserId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_UserId",
                table: "Conversations");

            migrationBuilder.DeleteData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Product",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Conversations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "AspNetUserRoles",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUserRoles",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUserClaims",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "AspNetRoles",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "AspNetRoleClaims",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Computers");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Clothing");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Outdoors");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Jewelery");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Sports");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Concrete");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Concrete");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Steel");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Plastic");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Steel");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Cotton");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Plastic");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Soft");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 11,
                column: "Name",
                value: "Metal");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 12,
                column: "Name",
                value: "Soft");

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
                value: "Wooden");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 15,
                column: "Name",
                value: "Rubber");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 16,
                column: "Name",
                value: "Steel");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 17,
                column: "Name",
                value: "Concrete");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 18,
                column: "Name",
                value: "Plastic");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 19,
                column: "Name",
                value: "Plastic");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 20,
                column: "Name",
                value: "Cotton");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "adipisci", "https://picsum.photos/640/480/?image=440" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "eius", "https://picsum.photos/640/480/?image=922" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "ut", "https://picsum.photos/640/480/?image=783" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "et", "https://picsum.photos/640/480/?image=982" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "perferendis", "https://picsum.photos/640/480/?image=960" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "sapiente", "https://picsum.photos/640/480/?image=592" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "quisquam", "https://picsum.photos/640/480/?image=989" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "dolores", "https://picsum.photos/640/480/?image=630" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "et", "https://picsum.photos/640/480/?image=118" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "neque", "https://picsum.photos/640/480/?image=611" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "molestiae", "https://picsum.photos/640/480/?image=853" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "non", "https://picsum.photos/640/480/?image=807" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "dicta", "https://picsum.photos/640/480/?image=690" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "enim", "https://picsum.photos/640/480/?image=691" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "sunt", "https://picsum.photos/640/480/?image=10" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "doloribus", "https://picsum.photos/640/480/?image=612" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "nemo", "https://picsum.photos/640/480/?image=890" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "vero", "https://picsum.photos/640/480/?image=517" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "molestias", "https://picsum.photos/640/480/?image=223" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "tenetur", "https://picsum.photos/640/480/?image=885" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CategoryId", "Name", "OrderNo", "Price" },
                values: new object[] { 2, "Tasty Concrete Bacon", 9L, 136.54m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategoryId", "Name", "OrderNo", "Price", "ShouldBeShown" },
                values: new object[] { 5, "Licensed Soft Shirt", 7L, 11.97m, true });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 5, "Rustic Steel Car", 28.78m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CategoryId", "Name", "OrderNo", "Price", "ShouldBeShown" },
                values: new object[] { 2, "Tasty Fresh Bike", 4L, 12.79m, true });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CategoryId", "Name", "OrderNo", "Price" },
                values: new object[] { 2, "Handcrafted Rubber Chips", 1L, 40.27m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CategoryId", "Name", "OrderNo", "Price", "ShouldBeShown" },
                values: new object[] { 2, "Gorgeous Wooden Soap", 8L, 12.55m, true });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 5, "Intelligent Soft Computer", 34.70m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Name", "OrderNo", "Price", "ShouldBeShown" },
                values: new object[] { "Gorgeous Granite Salad", null, 7.96m, false });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CategoryId", "Name", "OrderNo", "Price" },
                values: new object[] { 5, "Ergonomic Granite Fish", 0L, 105.15m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CategoryId", "Name", "OrderNo", "Price", "ShouldBeShown" },
                values: new object[] { 3, "Small Wooden Gloves", 2L, 6.44m, true });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Description", "Name", "OrderNo" },
                values: new object[] { "Animi odio nemo nisi error qui repudiandae excepturi rerum.", "Small Concrete Table", 5L });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Description", "Name", "OrderNo", "ShouldBeShown" },
                values: new object[] { "Minima quam ut dolores perferendis cupiditate aspernatur velit.", "Handmade Cotton Chair", 3L, true });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Aut dignissimos quidem eaque quasi mollitia enim mollitia nobis.", "Tasty Cotton Car" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "Name", "OrderNo", "ShouldBeShown" },
                values: new object[] { "Dolore et voluptas iusto nihil accusantium nihil mollitia maxime.", "Handcrafted Rubber Bacon", null, false });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Description", "Name", "OrderNo", "ShouldBeShown" },
                values: new object[] { "Voluptatem fugiat corrupti quo praesentium molestiae ullam dolores.", "Generic Cotton Towels", null, false });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Description", "Name", "OrderNo" },
                values: new object[] { "Voluptatem fugit pariatur.", "Practical Cotton Computer", 6L });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Description", "Name", "OrderNo", "ShouldBeShown" },
                values: new object[] { "Suscipit velit vel officiis reiciendis ut.", "Refined Wooden Chair", null, false });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Officiis similique voluptas.", "Handcrafted Wooden Car" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Description", "Name", "OrderNo", "ShouldBeShown" },
                values: new object[] { "Velit tempore unde ullam vitae ratione perferendis excepturi.", "Ergonomic Frozen Shirt", null, false });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Ea quaerat omnis.", "Sleek Fresh Mouse" });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 10, 7L, 20, 77.60m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 2, 5L, 14, 162.43m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 5, 4L, 11, 26.51m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 3, 2L, 17, 35.49m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 5L, 12, 16.42m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 1, 10L, 19, 2.06m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 9, 4L, 15, 23.01m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 2L, 11, 115.35m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 10, 2L, 19, 19.55m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 8, 6L, 14, 32.74m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 1, 7L, 16, 125.58m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 3, 7L, 16, 18.01m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 1, 6L, 11, 60.49m });

            migrationBuilder.InsertData(
                table: "SetElements",
                columns: new[] { "Id", "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[,]
                {
                    { 6, 9, 2L, 19, 18.13m },
                    { 10, 5, 6L, 20, 47.75m }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Conversations_ConversationId",
                table: "Messages",
                column: "ConversationId",
                principalTable: "Conversations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
