﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QueflityMVC.Persistence;

#nullable disable

namespace QueflityMVC.Infrastructure.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ComponentItem", b =>
                {
                    b.Property<int>("ComponentsId")
                        .HasColumnType("int");

                    b.Property<int>("ItemsId")
                        .HasColumnType("int");

                    b.HasKey("ComponentsId", "ItemsId");

                    b.HasIndex("ItemsId");

                    b.ToTable("ComponentItem");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("QueflityMVC.Domain.Interfaces.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<int?>("ImageId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("OrderNo")
                        .HasColumnType("bigint");

                    b.Property<bool>("ShouldBeShown")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.ToTable("Product");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Product");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("QueflityMVC.Domain.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("QueflityMVC.Domain.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Movies"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Industrial"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Movies"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Sports"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Garden"
                        });
                });

            modelBuilder.Entity("QueflityMVC.Domain.Models.Component", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Components");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Soft"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Wooden"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Plastic"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Metal"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Wooden"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Frozen"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Cotton"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Metal"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Metal"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Steel"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Steel"
                        },
                        new
                        {
                            Id = 12,
                            Name = "Granite"
                        },
                        new
                        {
                            Id = 13,
                            Name = "Rubber"
                        },
                        new
                        {
                            Id = 14,
                            Name = "Soft"
                        },
                        new
                        {
                            Id = 15,
                            Name = "Rubber"
                        },
                        new
                        {
                            Id = 16,
                            Name = "Cotton"
                        },
                        new
                        {
                            Id = 17,
                            Name = "Metal"
                        },
                        new
                        {
                            Id = 18,
                            Name = "Soft"
                        },
                        new
                        {
                            Id = 19,
                            Name = "Rubber"
                        },
                        new
                        {
                            Id = 20,
                            Name = "Steel"
                        });
                });

            modelBuilder.Entity("QueflityMVC.Domain.Models.Element", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<long>("ItemsAmount")
                        .HasColumnType("bigint");

                    b.Property<int>("KitId")
                        .HasColumnType("int");

                    b.Property<decimal>("PricePerItem")
                        .HasPrecision(14, 2)
                        .HasColumnType("decimal(14,2)");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("KitId");

                    b.ToTable("SetElements");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ItemId = 6,
                            ItemsAmount = 4L,
                            KitId = 17,
                            PricePerItem = 103.11m
                        },
                        new
                        {
                            Id = 2,
                            ItemId = 4,
                            ItemsAmount = 7L,
                            KitId = 14,
                            PricePerItem = 20.54m
                        },
                        new
                        {
                            Id = 3,
                            ItemId = 2,
                            ItemsAmount = 3L,
                            KitId = 16,
                            PricePerItem = 12.13m
                        },
                        new
                        {
                            Id = 4,
                            ItemId = 5,
                            ItemsAmount = 8L,
                            KitId = 20,
                            PricePerItem = 36.52m
                        },
                        new
                        {
                            Id = 5,
                            ItemId = 8,
                            ItemsAmount = 6L,
                            KitId = 19,
                            PricePerItem = 43.38m
                        },
                        new
                        {
                            Id = 6,
                            ItemId = 1,
                            ItemsAmount = 10L,
                            KitId = 12,
                            PricePerItem = 8.76m
                        },
                        new
                        {
                            Id = 7,
                            ItemId = 4,
                            ItemsAmount = 9L,
                            KitId = 12,
                            PricePerItem = 63.18m
                        },
                        new
                        {
                            Id = 8,
                            ItemId = 1,
                            ItemsAmount = 3L,
                            KitId = 19,
                            PricePerItem = 66.24m
                        },
                        new
                        {
                            Id = 9,
                            ItemId = 2,
                            ItemsAmount = 7L,
                            KitId = 12,
                            PricePerItem = 62.95m
                        },
                        new
                        {
                            Id = 10,
                            ItemId = 1,
                            ItemsAmount = 7L,
                            KitId = 17,
                            PricePerItem = 142.35m
                        },
                        new
                        {
                            Id = 11,
                            ItemId = 1,
                            ItemsAmount = 6L,
                            KitId = 13,
                            PricePerItem = 18.83m
                        },
                        new
                        {
                            Id = 12,
                            ItemId = 5,
                            ItemsAmount = 7L,
                            KitId = 11,
                            PricePerItem = 42.96m
                        },
                        new
                        {
                            Id = 14,
                            ItemId = 4,
                            ItemsAmount = 5L,
                            KitId = 20,
                            PricePerItem = 52.88m
                        },
                        new
                        {
                            Id = 15,
                            ItemId = 4,
                            ItemsAmount = 5L,
                            KitId = 19,
                            PricePerItem = 33.16m
                        },
                        new
                        {
                            Id = 16,
                            ItemId = 8,
                            ItemsAmount = 4L,
                            KitId = 13,
                            PricePerItem = 7.35m
                        });
                });

            modelBuilder.Entity("QueflityMVC.Domain.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AltDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Images");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AltDescription = "itaque",
                            FileUrl = "https://picsum.photos/640/480/?image=860"
                        },
                        new
                        {
                            Id = 2,
                            AltDescription = "sit",
                            FileUrl = "https://picsum.photos/640/480/?image=723"
                        },
                        new
                        {
                            Id = 3,
                            AltDescription = "non",
                            FileUrl = "https://picsum.photos/640/480/?image=775"
                        },
                        new
                        {
                            Id = 4,
                            AltDescription = "libero",
                            FileUrl = "https://picsum.photos/640/480/?image=1056"
                        },
                        new
                        {
                            Id = 5,
                            AltDescription = "et",
                            FileUrl = "https://picsum.photos/640/480/?image=771"
                        },
                        new
                        {
                            Id = 6,
                            AltDescription = "quo",
                            FileUrl = "https://picsum.photos/640/480/?image=538"
                        },
                        new
                        {
                            Id = 7,
                            AltDescription = "ut",
                            FileUrl = "https://picsum.photos/640/480/?image=687"
                        },
                        new
                        {
                            Id = 8,
                            AltDescription = "sed",
                            FileUrl = "https://picsum.photos/640/480/?image=911"
                        },
                        new
                        {
                            Id = 9,
                            AltDescription = "beatae",
                            FileUrl = "https://picsum.photos/640/480/?image=906"
                        },
                        new
                        {
                            Id = 10,
                            AltDescription = "quidem",
                            FileUrl = "https://picsum.photos/640/480/?image=231"
                        },
                        new
                        {
                            Id = 11,
                            AltDescription = "alias",
                            FileUrl = "https://picsum.photos/640/480/?image=312"
                        },
                        new
                        {
                            Id = 12,
                            AltDescription = "et",
                            FileUrl = "https://picsum.photos/640/480/?image=29"
                        },
                        new
                        {
                            Id = 13,
                            AltDescription = "quia",
                            FileUrl = "https://picsum.photos/640/480/?image=793"
                        },
                        new
                        {
                            Id = 14,
                            AltDescription = "quam",
                            FileUrl = "https://picsum.photos/640/480/?image=900"
                        },
                        new
                        {
                            Id = 15,
                            AltDescription = "aut",
                            FileUrl = "https://picsum.photos/640/480/?image=32"
                        },
                        new
                        {
                            Id = 16,
                            AltDescription = "culpa",
                            FileUrl = "https://picsum.photos/640/480/?image=915"
                        },
                        new
                        {
                            Id = 17,
                            AltDescription = "ut",
                            FileUrl = "https://picsum.photos/640/480/?image=645"
                        },
                        new
                        {
                            Id = 18,
                            AltDescription = "magnam",
                            FileUrl = "https://picsum.photos/640/480/?image=445"
                        },
                        new
                        {
                            Id = 19,
                            AltDescription = "voluptatem",
                            FileUrl = "https://picsum.photos/640/480/?image=1005"
                        },
                        new
                        {
                            Id = 20,
                            AltDescription = "non",
                            FileUrl = "https://picsum.photos/640/480/?image=1021"
                        });
                });

            modelBuilder.Entity("QueflityMVC.Domain.Models.Item", b =>
                {
                    b.HasBaseType("QueflityMVC.Domain.Interfaces.Product");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasIndex("CategoryId");

                    b.HasDiscriminator().HasValue("Item");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ImageId = 1,
                            Name = "Handmade Rubber Cheese",
                            OrderNo = 1L,
                            ShouldBeShown = true,
                            CategoryId = 4,
                            Price = 87.53m
                        },
                        new
                        {
                            Id = 2,
                            ImageId = 2,
                            Name = "Incredible Frozen Bacon",
                            ShouldBeShown = false,
                            CategoryId = 4,
                            Price = 17.01m
                        },
                        new
                        {
                            Id = 3,
                            ImageId = 3,
                            Name = "Generic Plastic Car",
                            ShouldBeShown = false,
                            CategoryId = 2,
                            Price = 105.78m
                        },
                        new
                        {
                            Id = 4,
                            ImageId = 4,
                            Name = "Awesome Metal Soap",
                            ShouldBeShown = false,
                            CategoryId = 2,
                            Price = 117.39m
                        },
                        new
                        {
                            Id = 5,
                            ImageId = 5,
                            Name = "Awesome Metal Mouse",
                            ShouldBeShown = false,
                            CategoryId = 1,
                            Price = 27.65m
                        },
                        new
                        {
                            Id = 6,
                            ImageId = 6,
                            Name = "Sleek Fresh Towels",
                            OrderNo = 6L,
                            ShouldBeShown = true,
                            CategoryId = 5,
                            Price = 161.68m
                        },
                        new
                        {
                            Id = 7,
                            ImageId = 7,
                            Name = "Rustic Frozen Sausages",
                            ShouldBeShown = false,
                            CategoryId = 4,
                            Price = 18.83m
                        },
                        new
                        {
                            Id = 8,
                            ImageId = 8,
                            Name = "Tasty Granite Salad",
                            ShouldBeShown = false,
                            CategoryId = 2,
                            Price = 3.10m
                        },
                        new
                        {
                            Id = 9,
                            ImageId = 9,
                            Name = "Practical Plastic Bacon",
                            OrderNo = 9L,
                            ShouldBeShown = true,
                            CategoryId = 5,
                            Price = 7.99m
                        },
                        new
                        {
                            Id = 10,
                            ImageId = 10,
                            Name = "Fantastic Metal Hat",
                            OrderNo = 5L,
                            ShouldBeShown = true,
                            CategoryId = 2,
                            Price = 10.78m
                        });
                });

            modelBuilder.Entity("QueflityMVC.Domain.Models.Kit", b =>
                {
                    b.HasBaseType("QueflityMVC.Domain.Interfaces.Product");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ItemId")
                        .HasColumnType("int");

                    b.HasIndex("ItemId");

                    b.HasDiscriminator().HasValue("Kit");

                    b.HasData(
                        new
                        {
                            Id = 11,
                            ImageId = 11,
                            Name = "Unbranded Metal Keyboard",
                            ShouldBeShown = false,
                            Description = "Et dolore aut."
                        },
                        new
                        {
                            Id = 12,
                            ImageId = 12,
                            Name = "Incredible Fresh Bike",
                            OrderNo = 8L,
                            ShouldBeShown = true,
                            Description = "Ad voluptas aut repudiandae omnis aliquam."
                        },
                        new
                        {
                            Id = 13,
                            ImageId = 13,
                            Name = "Fantastic Rubber Ball",
                            ShouldBeShown = false,
                            Description = "Voluptatum consequatur sint eaque animi expedita."
                        },
                        new
                        {
                            Id = 14,
                            ImageId = 14,
                            Name = "Awesome Granite Cheese",
                            OrderNo = 0L,
                            ShouldBeShown = true,
                            Description = "Quia dolor tenetur aut delectus doloremque eum quas et."
                        },
                        new
                        {
                            Id = 15,
                            ImageId = 15,
                            Name = "Incredible Steel Table",
                            ShouldBeShown = false,
                            Description = "Laudantium ut omnis ex expedita sunt facere repellat."
                        },
                        new
                        {
                            Id = 16,
                            ImageId = 16,
                            Name = "Incredible Concrete Pizza",
                            OrderNo = 3L,
                            ShouldBeShown = true,
                            Description = "Tempore nam dolor non."
                        },
                        new
                        {
                            Id = 17,
                            ImageId = 17,
                            Name = "Generic Concrete Sausages",
                            ShouldBeShown = false,
                            Description = "Placeat harum fugiat provident inventore rerum voluptate natus id non."
                        },
                        new
                        {
                            Id = 18,
                            ImageId = 18,
                            Name = "Tasty Wooden Computer",
                            OrderNo = 7L,
                            ShouldBeShown = true,
                            Description = "Ut nam eos corrupti et id quia velit."
                        },
                        new
                        {
                            Id = 19,
                            ImageId = 19,
                            Name = "Awesome Cotton Sausages",
                            OrderNo = 2L,
                            ShouldBeShown = true,
                            Description = "Quis aperiam ut dignissimos excepturi quo."
                        },
                        new
                        {
                            Id = 20,
                            ImageId = 20,
                            Name = "Handmade Wooden Tuna",
                            OrderNo = 4L,
                            ShouldBeShown = true,
                            Description = "Voluptatem fugiat odit rerum ratione."
                        });
                });

            modelBuilder.Entity("ComponentItem", b =>
                {
                    b.HasOne("QueflityMVC.Domain.Models.Component", null)
                        .WithMany()
                        .HasForeignKey("ComponentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QueflityMVC.Domain.Models.Item", null)
                        .WithMany()
                        .HasForeignKey("ItemsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("QueflityMVC.Domain.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("QueflityMVC.Domain.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QueflityMVC.Domain.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("QueflityMVC.Domain.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("QueflityMVC.Domain.Interfaces.Product", b =>
                {
                    b.HasOne("QueflityMVC.Domain.Models.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("QueflityMVC.Domain.Models.Element", b =>
                {
                    b.HasOne("QueflityMVC.Domain.Models.Item", "Item")
                        .WithMany("SetElements")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QueflityMVC.Domain.Models.Kit", "Kit")
                        .WithMany("Elements")
                        .HasForeignKey("KitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Kit");
                });

            modelBuilder.Entity("QueflityMVC.Domain.Models.Item", b =>
                {
                    b.HasOne("QueflityMVC.Domain.Models.Category", "Category")
                        .WithMany("Items")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("QueflityMVC.Domain.Models.Kit", b =>
                {
                    b.HasOne("QueflityMVC.Domain.Models.Item", null)
                        .WithMany("Kits")
                        .HasForeignKey("ItemId");
                });

            modelBuilder.Entity("QueflityMVC.Domain.Models.Category", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("QueflityMVC.Domain.Models.Item", b =>
                {
                    b.Navigation("Kits");

                    b.Navigation("SetElements");
                });

            modelBuilder.Entity("QueflityMVC.Domain.Models.Kit", b =>
                {
                    b.Navigation("Elements");
                });
#pragma warning restore 612, 618
        }
    }
}
