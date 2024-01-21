using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Infrastructure
{
    public class Context : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Category> Categorie { get; set; }

        public DbSet<ItemImage> ItemImages { get; set; }

        public DbSet<Kit> Kits { get; set; }

        public DbSet<KitImage> KitImages { get; set; }

        public DbSet<Element> SetElements { get; set; }

        public Context(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Item>()
                .HasOne(it => it.Image)
                .WithOne(img => img.Item)
                .HasForeignKey<Item>(it => it.ItemImageId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Kit>()
                .HasOne(it => it.Image)
                .WithOne(img => img.Kit)
                .HasForeignKey<Kit>(it => it.KitImageId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Item>()
                .HasMany(it => it.Ingredients)
                .WithMany(ing => ing.Items);

            builder.Entity<Item>()
                .HasOne(it => it.Category)
                .WithMany(itCtgr => itCtgr.Items)
                .HasForeignKey(it => it.CategoryId);

            builder.Entity<Element>()
                .HasOne(element => element.Item)
                .WithMany(it => it.SetElements)
                .HasForeignKey(element => element.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Element>()
                .HasOne(element => element.Kit)
                .WithMany(it => it.Elements)
                .HasForeignKey(membership => membership.KitId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}