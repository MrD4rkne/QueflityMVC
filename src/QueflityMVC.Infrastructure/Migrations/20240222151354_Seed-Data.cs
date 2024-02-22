using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QueflityMVC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Music" },
                    { 2, "Games" },
                    { 3, "Jewelery" },
                    { 4, "Garden" },
                    { 5, "Grocery" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "AltDescription", "FileUrl" },
                values: new object[,]
                {
                    { 1, "aut", "https://picsum.photos/640/480/?image=204" },
                    { 2, "quo", "https://picsum.photos/640/480/?image=298" },
                    { 3, "omnis", "https://picsum.photos/640/480/?image=1032" },
                    { 4, "doloremque", "https://picsum.photos/640/480/?image=68" },
                    { 5, "eius", "https://picsum.photos/640/480/?image=381" },
                    { 6, "tempora", "https://picsum.photos/640/480/?image=489" },
                    { 7, "amet", "https://picsum.photos/640/480/?image=684" },
                    { 8, "aut", "https://picsum.photos/640/480/?image=857" },
                    { 9, "eligendi", "https://picsum.photos/640/480/?image=439" },
                    { 10, "nam", "https://picsum.photos/640/480/?image=684" },
                    { 11, "eos", "https://picsum.photos/640/480/?image=653" },
                    { 12, "dolor", "https://picsum.photos/640/480/?image=1018" },
                    { 13, "quis", "https://picsum.photos/640/480/?image=484" },
                    { 14, "soluta", "https://picsum.photos/640/480/?image=436" },
                    { 15, "recusandae", "https://picsum.photos/640/480/?image=651" },
                    { 16, "impedit", "https://picsum.photos/640/480/?image=304" },
                    { 17, "doloremque", "https://picsum.photos/640/480/?image=885" },
                    { 18, "quo", "https://picsum.photos/640/480/?image=457" },
                    { 19, "id", "https://picsum.photos/640/480/?image=268" },
                    { 20, "doloremque", "https://picsum.photos/640/480/?image=166" }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Plastic" },
                    { 2, "Granite" },
                    { 3, "Frozen" },
                    { 4, "Steel" },
                    { 5, "Frozen" },
                    { 6, "Steel" },
                    { 7, "Concrete" },
                    { 8, "Plastic" },
                    { 9, "Fresh" },
                    { 10, "Wooden" },
                    { 11, "Steel" },
                    { 12, "Metal" },
                    { 13, "Plastic" },
                    { 14, "Steel" },
                    { 15, "Wooden" },
                    { 16, "Concrete" },
                    { 17, "Rubber" },
                    { 18, "Fresh" },
                    { 19, "Frozen" },
                    { 20, "Fresh" }
                });

            migrationBuilder.InsertData(
                table: "BasePurchasableEntity",
                columns: new[] { "Id", "CategoryId", "Discriminator", "ImageId", "Name", "Price", "ShouldBeShown" },
                values: new object[,]
                {
                    { 1, 4, "Item", 1, "Ergonomic Cotton Sausages", 100.03m, false },
                    { 2, 3, "Item", 2, "Intelligent Wooden Pants", 34.74m, false },
                    { 3, 2, "Item", 3, "Ergonomic Wooden Towels", 44.30m, false },
                    { 4, 2, "Item", 4, "Licensed Plastic Cheese", 4.27m, false },
                    { 5, 4, "Item", 5, "Refined Rubber Chair", 18.71m, false },
                    { 6, 2, "Item", 6, "Awesome Plastic Pants", 14.02m, false },
                    { 7, 2, "Item", 7, "Rustic Steel Sausages", 52.57m, false },
                    { 8, 1, "Item", 8, "Awesome Soft Chips", 131.47m, false },
                    { 9, 3, "Item", 9, "Intelligent Concrete Ball", 25.95m, false },
                    { 10, 5, "Item", 10, "Awesome Soft Bike", 14.77m, false }
                });

            migrationBuilder.InsertData(
                table: "BasePurchasableEntity",
                columns: new[] { "Id", "Description", "Discriminator", "ImageId", "ItemId", "Name", "Price", "ShouldBeShown" },
                values: new object[,]
                {
                    { 11, "Iste eaque consequatur repellendus.", "Kit", 11, null, "Tasty Cotton Car", 652.89m, false },
                    { 12, "Tempora sit odit nihil.", "Kit", 12, null, "Fantastic Granite Salad", 685.80m, false },
                    { 13, "Aperiam id dolorum cum.", "Kit", 13, null, "Fantastic Cotton Computer", 0m, false },
                    { 14, "Dolorum sit voluptas unde ad natus qui.", "Kit", 14, null, "Licensed Steel Tuna", 1090.35m, false },
                    { 15, "Tempore quo modi omnis delectus saepe possimus voluptatibus ut.", "Kit", 15, null, "Sleek Frozen Chicken", 310.10m, false },
                    { 16, "Animi dolor a.", "Kit", 16, null, "Fantastic Steel Chicken", 0m, false },
                    { 17, "Laudantium praesentium sit inventore non voluptatem deserunt doloremque.", "Kit", 17, null, "Awesome Wooden Towels", 224.34m, false },
                    { 18, "Qui rerum consequatur autem adipisci dolorem voluptate.", "Kit", 18, null, "Gorgeous Granite Ball", 451.67m, false },
                    { 19, "Et eius nemo numquam aperiam.", "Kit", 19, null, "Practical Steel Pizza", 394.10m, false },
                    { 20, "Quis quo omnis aut ducimus vitae corrupti.", "Kit", 20, null, "Handmade Steel Sausages", 0m, false }
                });

            migrationBuilder.InsertData(
                table: "SetElements",
                columns: new[] { "Id", "ItemId", "ItemsAmmount", "KitId", "PricePerItem" },
                values: new object[,]
                {
                    { 1, 3, 2L, 19, 9.77m },
                    { 2, 9, 2L, 19, 118.28m },
                    { 3, 3, 1L, 14, 36.30m },
                    { 4, 4, 9L, 12, 76.20m },
                    { 5, 5, 10L, 14, 18.26m },
                    { 6, 9, 9L, 11, 61.67m },
                    { 7, 4, 6L, 18, 63.88m },
                    { 8, 4, 2L, 19, 69.00m },
                    { 9, 3, 2L, 11, 48.93m },
                    { 12, 9, 4L, 15, 62.12m },
                    { 13, 6, 7L, 18, 9.77m },
                    { 14, 9, 5L, 14, 78.13m },
                    { 15, 2, 2L, 15, 30.81m },
                    { 16, 2, 10L, 14, 48.08m },
                    { 18, 6, 3L, 17, 74.78m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 19);
        }
    }
}
