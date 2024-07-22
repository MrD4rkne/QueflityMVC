#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QueflityMVC.Persistence.Migrations;

/// <inheritdoc />
public partial class SeedData : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            "Categories",
            ["Id", "Name"],
            new object[,]
            {
                { 1, "Jewelery" },
                { 2, "Movies" },
                { 3, "Beauty" },
                { 4, "Computers" },
                { 5, "Toys" }
            });

        migrationBuilder.InsertData(
            "Components",
            ["Id", "Name"],
            new object[,]
            {
                { 1, "Granite" },
                { 2, "Frozen" },
                { 3, "Plastic" },
                { 4, "Concrete" },
                { 5, "Plastic" },
                { 6, "Frozen" },
                { 7, "Plastic" },
                { 8, "Granite" },
                { 9, "Rubber" },
                { 10, "Steel" },
                { 11, "Concrete" },
                { 12, "Soft" },
                { 13, "Concrete" },
                { 14, "Concrete" },
                { 15, "Metal" },
                { 16, "Plastic" },
                { 17, "Metal" },
                { 18, "Metal" },
                { 19, "Fresh" },
                { 20, "Wooden" }
            });

        migrationBuilder.InsertData(
            "Images",
            ["Id", "AltDescription", "FileUrl"],
            new object[,]
            {
                { 1, "repellendus", "https://picsum.photos/640/480/?image=630" },
                { 2, "voluptatum", "https://picsum.photos/640/480/?image=421" },
                { 3, "et", "https://picsum.photos/640/480/?image=619" },
                { 4, "accusantium", "https://picsum.photos/640/480/?image=74" },
                { 5, "sed", "https://picsum.photos/640/480/?image=721" },
                { 6, "doloribus", "https://picsum.photos/640/480/?image=571" },
                { 7, "aliquid", "https://picsum.photos/640/480/?image=160" },
                { 8, "quis", "https://picsum.photos/640/480/?image=506" },
                { 9, "necessitatibus", "https://picsum.photos/640/480/?image=590" },
                { 10, "amet", "https://picsum.photos/640/480/?image=62" },
                { 11, "mollitia", "https://picsum.photos/640/480/?image=86" },
                { 12, "similique", "https://picsum.photos/640/480/?image=633" },
                { 13, "voluptates", "https://picsum.photos/640/480/?image=315" },
                { 14, "dolore", "https://picsum.photos/640/480/?image=567" },
                { 15, "ullam", "https://picsum.photos/640/480/?image=611" },
                { 16, "quasi", "https://picsum.photos/640/480/?image=14" },
                { 17, "dolor", "https://picsum.photos/640/480/?image=116" },
                { 18, "facere", "https://picsum.photos/640/480/?image=492" },
                { 19, "quae", "https://picsum.photos/640/480/?image=866" },
                { 20, "accusamus", "https://picsum.photos/640/480/?image=948" }
            });

        migrationBuilder.InsertData(
            "BaseProductEntity",
            ["Id", "CategoryId", "Discriminator", "ImageId", "Name", "OrderNo", "Price", "ShouldBeShown"],
            new object[,]
            {
                { 1, 3, "Item", 1, "Tasty Wooden Chips", 4L, 143.18m, true },
                { 2, 2, "Item", 2, "Incredible Wooden Cheese", 8L, 1.68m, true },
                { 3, 4, "Item", 3, "Tasty Concrete Bike", 9L, 3.06m, true },
                { 4, 3, "Item", 4, "Ergonomic Steel Table", null, 25.90m, false },
                { 5, 3, "Item", 5, "Practical Plastic Chair", 7L, 17.79m, true },
                { 6, 5, "Item", 6, "Handmade Steel Tuna", null, 32.53m, false },
                { 7, 5, "Item", 7, "Licensed Fresh Mouse", 1L, 29.96m, true },
                { 8, 1, "Item", 8, "Fantastic Concrete Shirt", null, 52.06m, false },
                { 9, 5, "Item", 9, "Rustic Rubber Sausages", null, 42.29m, false },
                { 10, 1, "Item", 10, "Awesome Frozen Hat", 3L, 65.26m, true }
            });

        migrationBuilder.InsertData(
            "BaseProductEntity",
            [
                "Id", "Description", "Discriminator", "ImageId", "ItemId", "Name", "OrderNo", "Price", "ShouldBeShown"
            ],
            new object[,]
            {
                {
                    11, "Corrupti impedit fuga velit harum iure ullam culpa.", "Kit", 11, null, "Awesome Soft Bacon",
                    null, 237.50m, false
                },
                { 12, "Quibusdam corrupti optio.", "Kit", 12, null, "Handmade Metal Hat", 5L, 71.92m, true },
                {
                    13, "Enim maxime tenetur perspiciatis temporibus cupiditate ut quas.", "Kit", 13, null,
                    "Small Wooden Table", null, 982.40m, false
                },
                { 14, "Odio sunt id possimus voluptate.", "Kit", 14, null, "Practical Frozen Pizza", null, 0m, false },
                {
                    15, "Voluptatem autem consequatur id ipsam.", "Kit", 15, null, "Awesome Cotton Car", 2L, 459.80m,
                    true
                },
                {
                    16, "Possimus magnam at quibusdam in voluptas saepe eos.", "Kit", 16, null,
                    "Intelligent Cotton Pants", null, 769.18m, false
                },
                { 17, "Et et illum sunt.", "Kit", 17, null, "Small Fresh Keyboard", 0L, 782.36m, true },
                {
                    18, "Omnis consequatur porro corporis sed dolor.", "Kit", 18, null, "Incredible Soft Shirt", 6L,
                    459.20m, true
                },
                {
                    19, "Asperiores odit velit aut quo neque.", "Kit", 19, null, "Handmade Concrete Chicken", null,
                    1524.61m, false
                },
                {
                    20, "Provident voluptas earum voluptatem cum ad est eum.", "Kit", 20, null,
                    "Incredible Plastic Sausages", null, 0m, false
                }
            });

        migrationBuilder.InsertData(
            "SetElements",
            ["Id", "ItemId", "ItemsAmount", "KitId", "PricePerItem"],
            new object[,]
            {
                { 1, 8, 8L, 16, 46.90m },
                { 2, 10, 4L, 13, 21.43m },
                { 3, 4, 8L, 18, 57.40m },
                { 4, 8, 5L, 11, 47.50m },
                { 5, 8, 4L, 13, 53.41m },
                { 6, 6, 8L, 13, 85.38m },
                { 7, 10, 2L, 16, 127.19m },
                { 8, 1, 8L, 17, 72.87m },
                { 9, 3, 10L, 15, 45.98m },
                { 10, 4, 7L, 19, 20.53m },
                { 11, 10, 8L, 12, 8.99m },
                { 12, 9, 1L, 19, 90.48m },
                { 13, 6, 4L, 16, 34.90m },
                { 14, 2, 9L, 19, 143.38m },
                { 15, 4, 10L, 17, 19.94m }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            "BaseProductEntity",
            "Id",
            5);

        migrationBuilder.DeleteData(
            "BaseProductEntity",
            "Id",
            7);

        migrationBuilder.DeleteData(
            "BaseProductEntity",
            "Id",
            14);

        migrationBuilder.DeleteData(
            "BaseProductEntity",
            "Id",
            20);

        migrationBuilder.DeleteData(
            "Components",
            "Id",
            1);

        migrationBuilder.DeleteData(
            "Components",
            "Id",
            2);

        migrationBuilder.DeleteData(
            "Components",
            "Id",
            3);

        migrationBuilder.DeleteData(
            "Components",
            "Id",
            4);

        migrationBuilder.DeleteData(
            "Components",
            "Id",
            5);

        migrationBuilder.DeleteData(
            "Components",
            "Id",
            6);

        migrationBuilder.DeleteData(
            "Components",
            "Id",
            7);

        migrationBuilder.DeleteData(
            "Components",
            "Id",
            8);

        migrationBuilder.DeleteData(
            "Components",
            "Id",
            9);

        migrationBuilder.DeleteData(
            "Components",
            "Id",
            10);

        migrationBuilder.DeleteData(
            "Components",
            "Id",
            11);

        migrationBuilder.DeleteData(
            "Components",
            "Id",
            12);

        migrationBuilder.DeleteData(
            "Components",
            "Id",
            13);

        migrationBuilder.DeleteData(
            "Components",
            "Id",
            14);

        migrationBuilder.DeleteData(
            "Components",
            "Id",
            15);

        migrationBuilder.DeleteData(
            "Components",
            "Id",
            16);

        migrationBuilder.DeleteData(
            "Components",
            "Id",
            17);

        migrationBuilder.DeleteData(
            "Components",
            "Id",
            18);

        migrationBuilder.DeleteData(
            "Components",
            "Id",
            19);

        migrationBuilder.DeleteData(
            "Components",
            "Id",
            20);

        migrationBuilder.DeleteData(
            "SetElements",
            "Id",
            1);

        migrationBuilder.DeleteData(
            "SetElements",
            "Id",
            2);

        migrationBuilder.DeleteData(
            "SetElements",
            "Id",
            3);

        migrationBuilder.DeleteData(
            "SetElements",
            "Id",
            4);

        migrationBuilder.DeleteData(
            "SetElements",
            "Id",
            5);

        migrationBuilder.DeleteData(
            "SetElements",
            "Id",
            6);

        migrationBuilder.DeleteData(
            "SetElements",
            "Id",
            7);

        migrationBuilder.DeleteData(
            "SetElements",
            "Id",
            8);

        migrationBuilder.DeleteData(
            "SetElements",
            "Id",
            9);

        migrationBuilder.DeleteData(
            "SetElements",
            "Id",
            10);

        migrationBuilder.DeleteData(
            "SetElements",
            "Id",
            11);

        migrationBuilder.DeleteData(
            "SetElements",
            "Id",
            12);

        migrationBuilder.DeleteData(
            "SetElements",
            "Id",
            13);

        migrationBuilder.DeleteData(
            "SetElements",
            "Id",
            14);

        migrationBuilder.DeleteData(
            "SetElements",
            "Id",
            15);

        migrationBuilder.DeleteData(
            "BaseProductEntity",
            "Id",
            1);

        migrationBuilder.DeleteData(
            "BaseProductEntity",
            "Id",
            2);

        migrationBuilder.DeleteData(
            "BaseProductEntity",
            "Id",
            3);

        migrationBuilder.DeleteData(
            "BaseProductEntity",
            "Id",
            4);

        migrationBuilder.DeleteData(
            "BaseProductEntity",
            "Id",
            6);

        migrationBuilder.DeleteData(
            "BaseProductEntity",
            "Id",
            8);

        migrationBuilder.DeleteData(
            "BaseProductEntity",
            "Id",
            9);

        migrationBuilder.DeleteData(
            "BaseProductEntity",
            "Id",
            10);

        migrationBuilder.DeleteData(
            "BaseProductEntity",
            "Id",
            11);

        migrationBuilder.DeleteData(
            "BaseProductEntity",
            "Id",
            12);

        migrationBuilder.DeleteData(
            "BaseProductEntity",
            "Id",
            13);

        migrationBuilder.DeleteData(
            "BaseProductEntity",
            "Id",
            15);

        migrationBuilder.DeleteData(
            "BaseProductEntity",
            "Id",
            16);

        migrationBuilder.DeleteData(
            "BaseProductEntity",
            "Id",
            17);

        migrationBuilder.DeleteData(
            "BaseProductEntity",
            "Id",
            18);

        migrationBuilder.DeleteData(
            "BaseProductEntity",
            "Id",
            19);

        migrationBuilder.DeleteData(
            "Images",
            "Id",
            5);

        migrationBuilder.DeleteData(
            "Images",
            "Id",
            7);

        migrationBuilder.DeleteData(
            "Images",
            "Id",
            14);

        migrationBuilder.DeleteData(
            "Images",
            "Id",
            20);

        migrationBuilder.DeleteData(
            "Categories",
            "Id",
            1);

        migrationBuilder.DeleteData(
            "Categories",
            "Id",
            2);

        migrationBuilder.DeleteData(
            "Categories",
            "Id",
            3);

        migrationBuilder.DeleteData(
            "Categories",
            "Id",
            4);

        migrationBuilder.DeleteData(
            "Categories",
            "Id",
            5);

        migrationBuilder.DeleteData(
            "Images",
            "Id",
            1);

        migrationBuilder.DeleteData(
            "Images",
            "Id",
            2);

        migrationBuilder.DeleteData(
            "Images",
            "Id",
            3);

        migrationBuilder.DeleteData(
            "Images",
            "Id",
            4);

        migrationBuilder.DeleteData(
            "Images",
            "Id",
            6);

        migrationBuilder.DeleteData(
            "Images",
            "Id",
            8);

        migrationBuilder.DeleteData(
            "Images",
            "Id",
            9);

        migrationBuilder.DeleteData(
            "Images",
            "Id",
            10);

        migrationBuilder.DeleteData(
            "Images",
            "Id",
            11);

        migrationBuilder.DeleteData(
            "Images",
            "Id",
            12);

        migrationBuilder.DeleteData(
            "Images",
            "Id",
            13);

        migrationBuilder.DeleteData(
            "Images",
            "Id",
            15);

        migrationBuilder.DeleteData(
            "Images",
            "Id",
            16);

        migrationBuilder.DeleteData(
            "Images",
            "Id",
            17);

        migrationBuilder.DeleteData(
            "Images",
            "Id",
            18);

        migrationBuilder.DeleteData(
            "Images",
            "Id",
            19);
    }
}