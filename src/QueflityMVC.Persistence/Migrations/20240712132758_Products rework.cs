using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueflityMVC.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Productsrework : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                keyValue: 7,
                column: "Name",
                value: "Cotton");

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
                keyValue: 15,
                column: "Name",
                value: "Rubber");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 16,
                column: "Name",
                value: "Cotton");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 17,
                column: "Name",
                value: "Metal");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 19,
                column: "Name",
                value: "Rubber");

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
                columns: new[] { "CategoryId", "Name", "OrderNo", "Price" },
                values: new object[] { 4, "Handmade Rubber Cheese", 1L, 87.53m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategoryId", "Name", "OrderNo", "Price", "ShouldBeShown" },
                values: new object[] { 4, "Incredible Frozen Bacon", null, 17.01m, false });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Name", "OrderNo", "Price", "ShouldBeShown" },
                values: new object[] { "Generic Plastic Car", null, 105.78m, false });

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
                columns: new[] { "Name", "Price" },
                values: new object[] { "Awesome Metal Mouse", 27.65m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CategoryId", "Name", "OrderNo", "Price", "ShouldBeShown" },
                values: new object[] { 5, "Sleek Fresh Towels", 6L, 161.68m, true });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 4, "Rustic Frozen Sausages", 18.83m });

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
                columns: new[] { "CategoryId", "Name", "OrderNo", "Price" },
                values: new object[] { 5, "Practical Plastic Bacon", 9L, 7.99m });

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
                columns: new[] { "Description", "Name" },
                values: new object[] { "Et dolore aut.", "Unbranded Metal Keyboard" });

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
                columns: new[] { "Description", "Name" },
                values: new object[] { "Voluptatum consequatur sint eaque animi expedita.", "Fantastic Rubber Ball" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "Name", "OrderNo" },
                values: new object[] { "Quia dolor tenetur aut delectus doloremque eum quas et.", "Awesome Granite Cheese", 0L });

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
                columns: new[] { "Description", "Name", "OrderNo", "ShouldBeShown" },
                values: new object[] { "Tempore nam dolor non.", "Incredible Concrete Pizza", 3L, true });

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
                columns: new[] { "Description", "Name", "OrderNo", "ShouldBeShown" },
                values: new object[] { "Quis aperiam ut dignissimos excepturi quo.", "Awesome Cotton Sausages", 2L, true });

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
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 6, 4L, 17, 103.11m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ItemId", "ItemsAmount", "PricePerItem" },
                values: new object[] { 4, 7L, 20.54m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ItemId", "KitId", "PricePerItem" },
                values: new object[] { 2, 16, 12.13m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 5, 8L, 20, 36.52m });

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
                columns: new[] { "ItemsAmount", "PricePerItem" },
                values: new object[] { 9L, 63.18m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ItemId", "ItemsAmount", "PricePerItem" },
                values: new object[] { 1, 3L, 66.24m });

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
                columns: new[] { "ItemId", "KitId", "PricePerItem" },
                values: new object[] { 1, 17, 142.35m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 6L, 13, 18.83m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ItemId", "ItemsAmount", "PricePerItem" },
                values: new object[] { 5, 7L, 42.96m });

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

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 8, 4L, 13, 7.35m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Books");

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
                value: "Tools");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Games");

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
                value: "Steel");

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
                value: "Soft");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Wooden");

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
                value: "Fresh");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Rubber");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Wooden");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Wooden");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Rubber");

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
                value: "Steel");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 13,
                column: "Name",
                value: "Fresh");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 14,
                column: "Name",
                value: "Frozen");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 15,
                column: "Name",
                value: "Steel");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 16,
                column: "Name",
                value: "Rubber");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 17,
                column: "Name",
                value: "Granite");

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Id",
                keyValue: 19,
                column: "Name",
                value: "Granite");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "ut", "https://picsum.photos/640/480/?image=598" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "voluptates", "https://picsum.photos/640/480/?image=902" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "nihil", "https://picsum.photos/640/480/?image=433" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "molestiae", "https://picsum.photos/640/480/?image=1078" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "vel", "https://picsum.photos/640/480/?image=493" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "ratione", "https://picsum.photos/640/480/?image=510" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "molestiae", "https://picsum.photos/640/480/?image=107" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "non", "https://picsum.photos/640/480/?image=815" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "dignissimos", "https://picsum.photos/640/480/?image=509" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "totam", "https://picsum.photos/640/480/?image=1035" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "eius", "https://picsum.photos/640/480/?image=223" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "accusamus", "https://picsum.photos/640/480/?image=823" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "tenetur", "https://picsum.photos/640/480/?image=773" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "ullam", "https://picsum.photos/640/480/?image=594" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "perspiciatis", "https://picsum.photos/640/480/?image=537" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "rem", "https://picsum.photos/640/480/?image=561" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "placeat", "https://picsum.photos/640/480/?image=218" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "maxime", "https://picsum.photos/640/480/?image=960" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "consectetur", "https://picsum.photos/640/480/?image=359" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "AltDescription", "FileUrl" },
                values: new object[] { "vitae", "https://picsum.photos/640/480/?image=160" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CategoryId", "Name", "OrderNo", "Price" },
                values: new object[] { 3, "Gorgeous Granite Ball", 9L, 6.03m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategoryId", "Name", "OrderNo", "Price", "ShouldBeShown" },
                values: new object[] { 3, "Awesome Wooden Chicken", 1L, 46.54m, true });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Name", "OrderNo", "Price", "ShouldBeShown" },
                values: new object[] { "Small Concrete Shirt", 8L, 68.02m, true });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 1, "Rustic Soft Towels", 6.61m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Name", "Price" },
                values: new object[] { "Handmade Granite Mouse", 168.20m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CategoryId", "Name", "OrderNo", "Price", "ShouldBeShown" },
                values: new object[] { 3, "Handmade Metal Tuna", null, 33.03m, false });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CategoryId", "Name", "Price" },
                values: new object[] { 5, "Unbranded Cotton Cheese", 14.14m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CategoryId", "Name", "OrderNo", "Price", "ShouldBeShown" },
                values: new object[] { 4, "Practical Frozen Hat", 2L, 72.52m, true });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CategoryId", "Name", "OrderNo", "Price" },
                values: new object[] { 3, "Ergonomic Soft Ball", 6L, 8.93m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CategoryId", "Name", "OrderNo", "Price" },
                values: new object[] { 5, "Incredible Metal Computer", 3L, 44.96m });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Quidem repellendus ab quam facilis vero repudiandae omnis qui.", "Practical Rubber Tuna" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Description", "Name", "OrderNo", "ShouldBeShown" },
                values: new object[] { "Sint nam expedita nihil qui delectus exercitationem.", "Intelligent Fresh Shoes", null, false });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Delectus atque qui.", "Incredible Rubber Soap" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "Name", "OrderNo" },
                values: new object[] { "Magnam commodi perspiciatis velit eum et sapiente.", "Incredible Cotton Gloves", 4L });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Description", "Name", "OrderNo", "ShouldBeShown" },
                values: new object[] { "Sed sed sit distinctio.", "Rustic Granite Mouse", 0L, true });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Description", "Name", "OrderNo", "ShouldBeShown" },
                values: new object[] { "Et dignissimos iure.", "Rustic Cotton Ball", null, false });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Veritatis amet soluta.", "Practical Plastic Shirt" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Description", "Name", "OrderNo", "ShouldBeShown" },
                values: new object[] { "Ullam placeat qui voluptates aspernatur laborum commodi.", "Refined Metal Table", null, false });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Description", "Name", "OrderNo", "ShouldBeShown" },
                values: new object[] { "Ut est in quae consequuntur voluptatem.", "Generic Metal Soap", null, false });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Description", "Name", "OrderNo", "ShouldBeShown" },
                values: new object[] { "Non fugiat voluptas repudiandae.", "Gorgeous Metal Sausages", null, false });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 10, 6L, 14, 119.95m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ItemId", "ItemsAmount", "PricePerItem" },
                values: new object[] { 1, 6L, 34.39m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ItemId", "KitId", "PricePerItem" },
                values: new object[] { 10, 20, 13.08m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 1, 10L, 16, 68.54m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 1, 5L, 18, 2.83m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 2, 7L, 16, 59.77m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ItemsAmount", "PricePerItem" },
                values: new object[] { 5L, 19.19m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ItemId", "ItemsAmount", "PricePerItem" },
                values: new object[] { 4, 8L, 51.91m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 3, 2L, 20, 100.04m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ItemId", "KitId", "PricePerItem" },
                values: new object[] { 10, 12, 132.23m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 3L, 11, 8.80m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ItemId", "ItemsAmount", "PricePerItem" },
                values: new object[] { 2, 1L, 23.09m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 6, 4L, 15, 54.89m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 9, 2L, 13, 125.03m });

            migrationBuilder.UpdateData(
                table: "SetElements",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "ItemId", "ItemsAmount", "KitId", "PricePerItem" },
                values: new object[] { 1, 3L, 12, 115.31m });
        }
    }
}
