using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QueflityMVC.Domain.Common;
using QueflityMVC.Domain.Models;
using QueflityMVC.Infrastructure.Seeding;

namespace QueflityMVC.Infrastructure;

public class Context : IdentityDbContext<ApplicationUser>
{
    public DbSet<Component> Components { get; set; }

    public DbSet<Item> Items { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Kit> Kits { get; set; }

    public DbSet<Image> Images { get; set; }

    public DbSet<Element> SetElements { get; set; }

    public Context(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<BasePurchasableEntity>()
            .HasOne(it => it.Image);

        builder.Entity<Item>()
            .HasMany(it => it.Components)
            .WithMany(ing => ing.Items);

        builder.Entity<Item>()
            .HasOne(it => it.Category)
            .WithMany(itCtgr => itCtgr.Items)
            .HasForeignKey(it => it.CategoryId);

        builder.Entity<Item>()
            .HasMany(it => it.SetElements)
            .WithOne(elem => elem.Item)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Kit>()
            .HasMany(it => it.Elements)
            .WithOne(elem => elem.Kit)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        EntitySeeder entitySeeder = new();
        builder.Entity<Element>().HasData(entitySeeder.Elements);
        builder.Entity<Kit>().HasData(entitySeeder.Kits);
        builder.Entity<Item>().HasData(entitySeeder.Items);
        builder.Entity<Component>().HasData(entitySeeder.Components);
        builder.Entity<Category>().HasData(entitySeeder.Categories);
        builder.Entity<Image>().HasData(entitySeeder.Images);
    }
}