using Azure;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Infrastructure
{
    public class Context : IdentityDbContext
    {
        public DbSet<Image> Images { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<ItemCategory> ItemCategories { get; set; }

        public DbSet<ItemSet> ItemSets { get; set; }

        public Context(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Item>()
                .HasOne(it => it.Image)
                .WithOne(img => img.Item)
                .HasForeignKey<Item>(it => it.ImageId);

            builder.Entity<Item>()
                .HasMany(it => it.Ingredients)
                .WithMany(ing => ing.Items);

            builder.Entity<Item>()
                .HasMany(it => it.ItemSets)
                .WithMany(itSet => itSet.Items);

            builder.Entity<Item>()
                .HasOne(it => it.ItemCategory)
                .WithMany(itCtgr => itCtgr.Items)
                .HasForeignKey(it => it.ItemCategoryId);
        }
    }
}
