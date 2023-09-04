using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Infrastructure
{
    public class Context : IdentityDbContext
    {
        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Category> Categorie { get; set; }

        public DbSet<ItemImage> ItemImages { get; set; }

        public DbSet<ItemSet> ItemSets { get; set; }

        public DbSet<ItemSetImage> ItemSetImages { get; set; }

        public DbSet<SetElement> SetElements { get; set; }


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

            builder.Entity<ItemSet>()
                .HasOne(it => it.Image)
                .WithOne(img => img.ItemSet)
                .HasForeignKey<ItemSet>(it => it.ItemSetImageId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Item>()
                .HasMany(it => it.Ingredients)
                .WithMany(ing => ing.Items);

            builder.Entity<Item>()
                .HasOne(it => it.Category)
                .WithMany(itCtgr => itCtgr.Items)
                .HasForeignKey(it => it.CategoryId);

            builder.Entity<SetElement>()
                .HasOne(element => element.Item)
                .WithMany(it => it.SetElements)
                .HasForeignKey(element => element.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SetElement>()
                .HasOne(element => element.ItemSet)
                .WithMany(it => it.Elements)
                .HasForeignKey(membership => membership.ItemSetId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
