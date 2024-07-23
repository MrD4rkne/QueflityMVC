using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using QueflityMVC.Domain.Common;
using QueflityMVC.Domain.Models;
using QueflityMVC.Persistence.Seeding;
using QueflityMVC.Persistence.Setup;

namespace QueflityMVC.Persistence;

public class Context(DbContextOptions options, IOptions<PersistenceConfig> persistenceOptions)
    : IdentityDbContext<ApplicationUser>(options)
{
    private readonly PersistenceConfig _config = persistenceOptions.Value;

    public Context() : this(new DbContextOptions<Context>(),
        new OptionsWrapper<PersistenceConfig>(new PersistenceConfig()))
    {
    }

    public DbSet<Component> Components { get; set; }

    public DbSet<Item> Items { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Kit> Kits { get; set; }

    public DbSet<Image> Images { get; set; }

    public DbSet<Conversation> Conversations { get; set; }

    public DbSet<Message> Messages { get; set; }

    public DbSet<Element> SetElements { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Product>()
            .Ignore(product => product.Price)
            .HasOne(it => it.Image);

        builder.Entity<Item>()
            .Property(it => it.Price)
            .HasColumnType("decimal(10, 2)");

        builder.Entity<Item>()
            .HasMany(it => it.Components)
            .WithMany(ing => ing.Items);

        builder.Entity<Item>()
            .HasOne(it => it.Category)
            .WithMany(itCtgr => itCtgr.Items)
            .HasForeignKey(it => it.CategoryId);

        builder.Entity<Element>()
            .HasOne(se => se.Kit)
            .WithMany(kit => kit.Elements)
            .HasForeignKey(se => se.KitId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Element>()
            .HasOne(se => se.Kit)
            .WithMany(kit => kit.Elements)
            .HasForeignKey(se => se.KitId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Message>()
            .HasOne(m => m.Conversation)
            .WithMany(c => c.Messages)
            .HasForeignKey(m => m.ConversationId)
            .OnDelete(DeleteBehavior.Cascade);

        EntitySeeder entitySeeder = new();
        builder.Entity<Element>().HasData(entitySeeder.Elements);
        builder.Entity<Kit>().HasData(entitySeeder.Kits);
        builder.Entity<Item>().HasData(entitySeeder.Items);
        builder.Entity<Component>().HasData(entitySeeder.Components);
        builder.Entity<Category>().HasData(entitySeeder.Categories);
        builder.Entity<Image>().HasData(entitySeeder.Images);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer(_config.ConnectionString, sqlOptions =>
            {
                if (_config.ShouldRetry)
                    sqlOptions.EnableRetryOnFailure();
            });
    }
}