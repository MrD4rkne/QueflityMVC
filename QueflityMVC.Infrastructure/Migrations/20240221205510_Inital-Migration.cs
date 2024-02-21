using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueflityMVC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitalMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 2, "Unbranded Metal Chips", 8.81m });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "Price" },
                values: new object[] { "Awesome Plastic Car", 144.50m });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 3, "Tasty Metal Keyboard", 128.12m });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 4, "Handmade Cotton Pizza", 11.96m });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 2, "Generic Concrete Bacon", 40.45m });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 3, "Intelligent Concrete Chips", 52.47m });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 3, "Generic Soft Fish", 48.76m });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Name", "Price" },
                values: new object[] { "Gorgeous Cotton Pizza", 77.27m });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Name", "Price" },
                values: new object[] { "Gorgeous Soft Car", 127.44m });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 3, "Fantastic Concrete Hat", 46.13m });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Culpa aut illum architecto facere voluptatibus et aperiam rem.", "Tasty Cotton Computer" });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Fugit praesentium aut.", "Gorgeous Soft Pants", 1145.21m });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Voluptatem impedit eius eligendi.", "Licensed Metal Salad", 591.90m });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Accusantium dolorem ad iure repellendus asperiores illum magni corrupti odio.", "Rustic Plastic Mouse", 31.08m });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Qui omnis velit consequuntur.", "Gorgeous Rubber Tuna", 81.38m });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Fugit sed id et voluptas labore cumque sed dolorum ullam.", "Fantastic Metal Bike", 276.27m });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Et ducimus temporibus fugiat quas.", "Practical Rubber Shirt" });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Et non nesciunt qui inventore quis possimus libero voluptatum.", "Tasty Wooden Shoes", 500.24m });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Id ut ut aut ullam soluta aliquam qui iusto.", "Gorgeous Soft Chicken", 618.54m });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Ea quod dolores nihil doloremque commodi.", "Sleek Concrete Sausages", 877.00m });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Garden");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Games");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Books");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Toys");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "expedita", "https://picsum.photos/640/480/?image=210" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "voluptatem", "https://picsum.photos/640/480/?image=986" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "dolorem", "https://picsum.photos/640/480/?image=290" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "ut", "https://picsum.photos/640/480/?image=475" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "debitis", "https://picsum.photos/640/480/?image=252" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "nihil", "https://picsum.photos/640/480/?image=492" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "nisi", "https://picsum.photos/640/480/?image=963" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "voluptas", "https://picsum.photos/640/480/?image=389" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "maxime", "https://picsum.photos/640/480/?image=582" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "iure", "https://picsum.photos/640/480/?image=508" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "dolores", "https://picsum.photos/640/480/?image=1059" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "aliquid", "https://picsum.photos/640/480/?image=1051" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "voluptas", "https://picsum.photos/640/480/?image=153" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "quis", "https://picsum.photos/640/480/?image=631" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "et", "https://picsum.photos/640/480/?image=193" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "suscipit", "https://picsum.photos/640/480/?image=506" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "ullam", "https://picsum.photos/640/480/?image=621" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "nam", "https://picsum.photos/640/480/?image=493" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "et", "https://picsum.photos/640/480/?image=586" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "aspernatur", "https://picsum.photos/640/480/?image=563" });

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Concrete");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Wooden");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Metal");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Metal");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Concrete");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Soft");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Plastic");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Metal");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Plastic");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Frozen");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 11,
                column: "Name",
                value: "Plastic");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 12,
                column: "Name",
                value: "Concrete");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 14,
                column: "Name",
                value: "Rubber");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 15,
                column: "Name",
                value: "Rubber");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 16,
                column: "Name",
                value: "Cotton");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 17,
                column: "Name",
                value: "Wooden");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 18,
                column: "Name",
                value: "Rubber");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 19,
                column: "Name",
                value: "Wooden");

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ItemId", "ItemsAmmount", "KitId", "PricePerItem" },
                values: new object[] { 1, 10L, 16, 26.22m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ItemId", "PricePerItem" },
                values: new object[] { 5, 31.08m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ItemsAmmount", "KitId", "PricePerItem" },
                values: new object[] { 8L, 15, 2.86m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ItemId", "ItemsAmmount", "KitId", "PricePerItem" },
                values: new object[] { 6, 7L, 16, 2.01m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ItemId", "ItemsAmmount", "PricePerItem" },
                values: new object[] { 3, 10L, 87.70m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ItemId", "ItemsAmmount", "KitId", "PricePerItem" },
                values: new object[] { 5, 10L, 15, 5.85m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "KitId", "PricePerItem" },
                values: new object[] { 13, 98.65m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ItemId", "ItemsAmmount", "KitId", "PricePerItem" },
                values: new object[] { 8, 5L, 19, 95.38m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ItemId", "ItemsAmmount", "PricePerItem" },
                values: new object[] { 5, 6L, 44.77m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ItemId", "ItemsAmmount", "KitId", "PricePerItem" },
                values: new object[] { 6, 3L, 18, 7.72m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ItemId", "ItemsAmmount", "KitId", "PricePerItem" },
                values: new object[] { 4, 7L, 19, 12.86m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "ItemId", "ItemsAmmount", "KitId", "PricePerItem" },
                values: new object[] { 7, 1L, 19, 51.62m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "ItemId", "ItemsAmmount", "KitId", "PricePerItem" },
                values: new object[] { 2, 7L, 18, 29.78m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "ItemId", "ItemsAmmount", "KitId", "PricePerItem" },
                values: new object[] { 3, 5L, 12, 185.85m });

            migrationBuilder.InsertData(
                table: "SetElements",
                columns: new[] { "Id", "ItemId", "ItemsAmmount", "KitId", "PricePerItem" },
                values: new object[] { 12, 10, 2L, 12, 107.98m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 5, "Intelligent Rubber Soap", 19.65m });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "Price" },
                values: new object[] { "Handcrafted Fresh Towels", 30.19m });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 2, "Rustic Cotton Tuna", 56.99m });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 3, "Generic Granite Pants", 91.21m });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 1, "Sleek Soft Chips", 12.11m });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 5, "Practical Fresh Computer", 53.52m });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 2, "Small Metal Sausages", 83.75m });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Name", "Price" },
                values: new object[] { "Incredible Granite Soap", 50.42m });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Name", "Price" },
                values: new object[] { "Unbranded Soft Tuna", 128.86m });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 5, "Rustic Wooden Chips", 33.41m });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Aut cupiditate repellendus incidunt excepturi non excepturi voluptatum facilis possimus.", "Intelligent Concrete Shoes" });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Provident eligendi possimus sequi laboriosam.", "Refined Plastic Mouse", 0m });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Et libero sunt occaecati recusandae.", "Intelligent Metal Pizza", 0m });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Ab perspiciatis accusantium aut sit.", "Ergonomic Granite Computer", 0m });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Quaerat ipsum unde earum repudiandae.", "Incredible Granite Soap", 0m });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Aspernatur neque id architecto illum doloribus ipsum.", "Refined Rubber Bike", 0m });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Quisquam sit et incidunt voluptates assumenda quasi eos omnis cumque.", "Licensed Cotton Chair" });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Qui quo at.", "Tasty Concrete Keyboard", 0m });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Harum est aut facilis nulla quis.", "Sleek Concrete Mouse", 0m });

            migrationBuilder.UpdateData(
                table: "BasePurchasableEntity",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Sit at qui.", "Incredible Frozen Soap", 0m });

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
                value: "Grocery");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Computers");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Outdoors");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "ut", "https://picsum.photos/640/480/?image=36" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "sint", "https://picsum.photos/640/480/?image=221" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "facilis", "https://picsum.photos/640/480/?image=222" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "animi", "https://picsum.photos/640/480/?image=73" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "aliquam", "https://picsum.photos/640/480/?image=711" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "nemo", "https://picsum.photos/640/480/?image=1066" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "cupiditate", "https://picsum.photos/640/480/?image=1030" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "nihil", "https://picsum.photos/640/480/?image=73" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "omnis", "https://picsum.photos/640/480/?image=327" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "ut", "https://picsum.photos/640/480/?image=431" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "velit", "https://picsum.photos/640/480/?image=299" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "est", "https://picsum.photos/640/480/?image=832" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "nam", "https://picsum.photos/640/480/?image=890" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "eos", "https://picsum.photos/640/480/?image=176" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "corporis", "https://picsum.photos/640/480/?image=917" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "ad", "https://picsum.photos/640/480/?image=919" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "dolor", "https://picsum.photos/640/480/?image=789" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "dignissimos", "https://picsum.photos/640/480/?image=408" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "qui", "https://picsum.photos/640/480/?image=1027" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "illo", "https://picsum.photos/640/480/?image=291" });

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Soft");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Cotton");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Soft");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Soft");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Soft");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Metal");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Wooden");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Frozen");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Soft");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Plastic");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 11,
                column: "Name",
                value: "Soft");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 12,
                column: "Name",
                value: "Rubber");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 14,
                column: "Name",
                value: "Plastic");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 15,
                column: "Name",
                value: "Concrete");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 16,
                column: "Name",
                value: "Concrete");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 17,
                column: "Name",
                value: "Cotton");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 18,
                column: "Name",
                value: "Concrete");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 19,
                column: "Name",
                value: "Steel");

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ItemId", "ItemsAmmount", "KitId", "PricePerItem" },
                values: new object[] { 10, 4L, 19, 42.87m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ItemId", "PricePerItem" },
                values: new object[] { 6, 18.69m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ItemsAmmount", "KitId", "PricePerItem" },
                values: new object[] { 4L, 17, 2.78m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ItemId", "ItemsAmmount", "KitId", "PricePerItem" },
                values: new object[] { 8, 10L, 18, 132.10m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ItemId", "ItemsAmmount", "PricePerItem" },
                values: new object[] { 7, 7L, 22.07m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ItemId", "ItemsAmmount", "KitId", "PricePerItem" },
                values: new object[] { 1, 7L, 12, 12.08m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "KitId", "PricePerItem" },
                values: new object[] { 16, 13.42m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ItemId", "ItemsAmmount", "KitId", "PricePerItem" },
                values: new object[] { 10, 6L, 15, 152.17m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ItemId", "ItemsAmmount", "PricePerItem" },
                values: new object[] { 4, 4L, 3.37m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ItemId", "ItemsAmmount", "KitId", "PricePerItem" },
                values: new object[] { 2, 7L, 12, 55.00m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ItemId", "ItemsAmmount", "KitId", "PricePerItem" },
                values: new object[] { 3, 8L, 20, 28.26m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "ItemId", "ItemsAmmount", "KitId", "PricePerItem" },
                values: new object[] { 3, 8L, 12, 104.83m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "ItemId", "ItemsAmmount", "KitId", "PricePerItem" },
                values: new object[] { 6, 2L, 20, 32.67m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "ItemId", "ItemsAmmount", "KitId", "PricePerItem" },
                values: new object[] { 5, 2L, 14, 5.23m });

            migrationBuilder.InsertData(
                table: "SetElements",
                columns: new[] { "Id", "ItemId", "ItemsAmmount", "KitId", "PricePerItem" },
                values: new object[] { 13, 1, 1L, 16, 32.61m });
        }
    }
}
