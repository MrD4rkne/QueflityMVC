#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace QueflityMVC.Persistence.Migrations;

/// <inheritdoc />
public partial class InitialMigration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "AspNetRoles",
            table => new
            {
                Id = table.Column<string>("nvarchar(450)", nullable: false),
                Name = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: true),
                NormalizedName = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: true),
                ConcurrencyStamp = table.Column<string>("nvarchar(max)", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_AspNetRoles", x => x.Id); });

        migrationBuilder.CreateTable(
            "AspNetUsers",
            table => new
            {
                Id = table.Column<string>("nvarchar(450)", nullable: false),
                IsEnabled = table.Column<bool>("bit", nullable: false),
                UserName = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: true),
                NormalizedUserName = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: true),
                Email = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: true),
                NormalizedEmail = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: true),
                EmailConfirmed = table.Column<bool>("bit", nullable: false),
                PasswordHash = table.Column<string>("nvarchar(max)", nullable: true),
                SecurityStamp = table.Column<string>("nvarchar(max)", nullable: true),
                ConcurrencyStamp = table.Column<string>("nvarchar(max)", nullable: true),
                PhoneNumber = table.Column<string>("nvarchar(max)", nullable: true),
                PhoneNumberConfirmed = table.Column<bool>("bit", nullable: false),
                TwoFactorEnabled = table.Column<bool>("bit", nullable: false),
                LockoutEnd = table.Column<DateTimeOffset>("datetimeoffset", nullable: true),
                LockoutEnabled = table.Column<bool>("bit", nullable: false),
                AccessFailedCount = table.Column<int>("int", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_AspNetUsers", x => x.Id); });

        migrationBuilder.CreateTable(
            "Categories",
            table => new
            {
                Id = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>("nvarchar(max)", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_Categories", x => x.Id); });

        migrationBuilder.CreateTable(
            "Components",
            table => new
            {
                Id = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>("nvarchar(max)", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_Components", x => x.Id); });

        migrationBuilder.CreateTable(
            "Images",
            table => new
            {
                Id = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                FileUrl = table.Column<string>("nvarchar(max)", nullable: false),
                AltDescription = table.Column<string>("nvarchar(max)", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_Images", x => x.Id); });

        migrationBuilder.CreateTable(
            "AspNetRoleClaims",
            table => new
            {
                Id = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                RoleId = table.Column<string>("nvarchar(450)", nullable: false),
                ClaimType = table.Column<string>("nvarchar(max)", nullable: true),
                ClaimValue = table.Column<string>("nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                table.ForeignKey(
                    "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                    x => x.RoleId,
                    "AspNetRoles",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "AspNetUserClaims",
            table => new
            {
                Id = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<string>("nvarchar(450)", nullable: false),
                ClaimType = table.Column<string>("nvarchar(max)", nullable: true),
                ClaimValue = table.Column<string>("nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                table.ForeignKey(
                    "FK_AspNetUserClaims_AspNetUsers_UserId",
                    x => x.UserId,
                    "AspNetUsers",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "AspNetUserLogins",
            table => new
            {
                LoginProvider = table.Column<string>("nvarchar(128)", maxLength: 128, nullable: false),
                ProviderKey = table.Column<string>("nvarchar(128)", maxLength: 128, nullable: false),
                ProviderDisplayName = table.Column<string>("nvarchar(max)", nullable: true),
                UserId = table.Column<string>("nvarchar(450)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                table.ForeignKey(
                    "FK_AspNetUserLogins_AspNetUsers_UserId",
                    x => x.UserId,
                    "AspNetUsers",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "AspNetUserRoles",
            table => new
            {
                UserId = table.Column<string>("nvarchar(450)", nullable: false),
                RoleId = table.Column<string>("nvarchar(450)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                table.ForeignKey(
                    "FK_AspNetUserRoles_AspNetRoles_RoleId",
                    x => x.RoleId,
                    "AspNetRoles",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_AspNetUserRoles_AspNetUsers_UserId",
                    x => x.UserId,
                    "AspNetUsers",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "AspNetUserTokens",
            table => new
            {
                UserId = table.Column<string>("nvarchar(450)", nullable: false),
                LoginProvider = table.Column<string>("nvarchar(128)", maxLength: 128, nullable: false),
                Name = table.Column<string>("nvarchar(128)", maxLength: 128, nullable: false),
                Value = table.Column<string>("nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                table.ForeignKey(
                    "FK_AspNetUserTokens_AspNetUsers_UserId",
                    x => x.UserId,
                    "AspNetUsers",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "BaseProductEntity",
            table => new
            {
                Id = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>("nvarchar(max)", nullable: false),
                ShouldBeShown = table.Column<bool>("bit", nullable: false),
                Price = table.Column<decimal>("decimal(14,2)", precision: 14, scale: 2, nullable: false),
                ImageId = table.Column<int>("int", nullable: true),
                OrderNo = table.Column<long>("bigint", nullable: true),
                Discriminator = table.Column<string>("nvarchar(21)", maxLength: 21, nullable: false),
                CategoryId = table.Column<int>("int", nullable: true),
                Description = table.Column<string>("nvarchar(max)", nullable: true),
                ItemId = table.Column<int>("int", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_BaseProductEntity", x => x.Id);
                table.ForeignKey(
                    "FK_BaseProductEntity_BaseProductEntity_ItemId",
                    x => x.ItemId,
                    "BaseProductEntity",
                    "Id");
                table.ForeignKey(
                    "FK_BaseProductEntity_Categories_CategoryId",
                    x => x.CategoryId,
                    "Categories",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_BaseProductEntity_Images_ImageId",
                    x => x.ImageId,
                    "Images",
                    "Id");
            });

        migrationBuilder.CreateTable(
            "ComponentItem",
            table => new
            {
                ComponentsId = table.Column<int>("int", nullable: false),
                ItemsId = table.Column<int>("int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ComponentItem", x => new { x.ComponentsId, x.ItemsId });
                table.ForeignKey(
                    "FK_ComponentItem_BaseProductEntity_ItemsId",
                    x => x.ItemsId,
                    "BaseProductEntity",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_ComponentItem_Components_ComponentsId",
                    x => x.ComponentsId,
                    "Components",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "SetElements",
            table => new
            {
                Id = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ItemsAmount = table.Column<long>("bigint", nullable: false),
                PricePerItem = table.Column<decimal>("decimal(14,2)", precision: 14, scale: 2, nullable: false),
                ItemId = table.Column<int>("int", nullable: false),
                KitId = table.Column<int>("int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_SetElements", x => x.Id);
                table.ForeignKey(
                    "FK_SetElements_BaseProductEntity_ItemId",
                    x => x.ItemId,
                    "BaseProductEntity",
                    "Id");
                table.ForeignKey(
                    "FK_SetElements_BaseProductEntity_KitId",
                    x => x.KitId,
                    "BaseProductEntity",
                    "Id");
            });

        migrationBuilder.CreateIndex(
            "IX_AspNetRoleClaims_RoleId",
            "AspNetRoleClaims",
            "RoleId");

        migrationBuilder.CreateIndex(
            "RoleNameIndex",
            "AspNetRoles",
            "NormalizedName",
            unique: true,
            filter: "[NormalizedName] IS NOT NULL");

        migrationBuilder.CreateIndex(
            "IX_AspNetUserClaims_UserId",
            "AspNetUserClaims",
            "UserId");

        migrationBuilder.CreateIndex(
            "IX_AspNetUserLogins_UserId",
            "AspNetUserLogins",
            "UserId");

        migrationBuilder.CreateIndex(
            "IX_AspNetUserRoles_RoleId",
            "AspNetUserRoles",
            "RoleId");

        migrationBuilder.CreateIndex(
            "EmailIndex",
            "AspNetUsers",
            "NormalizedEmail");

        migrationBuilder.CreateIndex(
            "UserNameIndex",
            "AspNetUsers",
            "NormalizedUserName",
            unique: true,
            filter: "[NormalizedUserName] IS NOT NULL");

        migrationBuilder.CreateIndex(
            "IX_BaseProductEntity_CategoryId",
            "BaseProductEntity",
            "CategoryId");

        migrationBuilder.CreateIndex(
            "IX_BaseProductEntity_ImageId",
            "BaseProductEntity",
            "ImageId");

        migrationBuilder.CreateIndex(
            "IX_BaseProductEntity_ItemId",
            "BaseProductEntity",
            "ItemId");

        migrationBuilder.CreateIndex(
            "IX_ComponentItem_ItemsId",
            "ComponentItem",
            "ItemsId");

        migrationBuilder.CreateIndex(
            "IX_SetElements_ItemId",
            "SetElements",
            "ItemId");

        migrationBuilder.CreateIndex(
            "IX_SetElements_KitId",
            "SetElements",
            "KitId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "AspNetRoleClaims");

        migrationBuilder.DropTable(
            "AspNetUserClaims");

        migrationBuilder.DropTable(
            "AspNetUserLogins");

        migrationBuilder.DropTable(
            "AspNetUserRoles");

        migrationBuilder.DropTable(
            "AspNetUserTokens");

        migrationBuilder.DropTable(
            "ComponentItem");

        migrationBuilder.DropTable(
            "SetElements");

        migrationBuilder.DropTable(
            "AspNetRoles");

        migrationBuilder.DropTable(
            "AspNetUsers");

        migrationBuilder.DropTable(
            "Components");

        migrationBuilder.DropTable(
            "BaseProductEntity");

        migrationBuilder.DropTable(
            "Categories");

        migrationBuilder.DropTable(
            "Images");
    }
}