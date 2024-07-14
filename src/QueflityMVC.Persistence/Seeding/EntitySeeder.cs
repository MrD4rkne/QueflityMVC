using Bogus;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Persistence.Seeding;

public class EntitySeeder
{
    private const string FAKER_LOCALE = "en";
    private const int CATEGORIES_COUNT = 5;
    private const int ITEMS_COUNT = 10;
    private const int COMPONENTS_COUNT = 20;
    private const int KITS_COUNT = 10;
    private const int IMAGES_COUNT = ITEMS_COUNT + KITS_COUNT;
    private const int ELEMENTS_COUNT = 15;
    private const int VISIBLE_PURCHASABLE = 10;

    private readonly HashSet<uint> _orderNumbers = new();
    private int _visiblePurchasable;

    public EntitySeeder()
    {
        Categories = GenerateCategories();
        Components = GenerateComponents();
        Images = GenerateImages();
        Items = GenerateItems();
        Elements = GenerateElements();
        Kits = GenerateKits();
    }

    public IReadOnlyCollection<Category> Categories { get; set; }

    public IReadOnlyCollection<Item> Items { get; set; }

    public IReadOnlyCollection<Component> Components { get; set; }

    public IReadOnlyCollection<Kit> Kits { get; set; }

    public IReadOnlyCollection<Image> Images { get; set; }

    public IReadOnlyCollection<Element> Elements { get; set; }

    private IReadOnlyCollection<Category> GenerateCategories()
    {
        var categoryFaker = new Faker<Category>(FAKER_LOCALE)
            .RuleFor(ctg => ctg.Id, f => f.GetPositiveIndexFaker())
            .RuleFor(ctg => ctg.Name, f => f.Commerce.Categories(1)[0]);
        return categoryFaker.Generate(CATEGORIES_COUNT);
    }

    private IReadOnlyCollection<Component> GenerateComponents()
    {
        var componentFaker = new Faker<Component>(FAKER_LOCALE)
            .RuleFor(ing => ing.Id, f => f.GetPositiveIndexFaker())
            .RuleFor(ing => ing.Name, f => f.Commerce.ProductMaterial());
        return componentFaker.Generate(COMPONENTS_COUNT);
    }

    /// <summary>
    ///     generate categories, components and images before items
    /// </summary>
    /// <returns></returns>
    private IReadOnlyCollection<Item> GenerateItems()
    {
        var itemFaker = new Faker<Item>(FAKER_LOCALE)
            .RuleFor(it => it.Id, f => f.GetPositiveIndexFaker())
            .RuleFor(it => it.Name, f => f.Commerce.ProductName())
            .RuleFor(it => it.Price, f => Math.Round(f.Random.Decimal(0.01m, 10) * f.Random.Number(1, 20), 2))
            .RuleFor(it => it.ImageId, f => f.GetPositiveIndexFaker())
            .RuleFor(it => it.ShouldBeShown, f => GetVisibility())
            .RuleFor(it => it.CategoryId, f => f.Random.Number(1, CATEGORIES_COUNT));
        var items = itemFaker.Generate(ITEMS_COUNT);
        foreach (var item in items) item.OrderNo = GetRandomOrderNumber(item.ShouldBeShown);
        return items;
    }

    private IReadOnlyCollection<Kit> GenerateKits()
    {
        var kitFaker = new Faker<Kit>(FAKER_LOCALE)
            .RuleFor(kit => kit.Id, f => ITEMS_COUNT + f.GetPositiveIndexFaker())
            .RuleFor(kit => kit.Name, f => f.Commerce.ProductName())
            .RuleFor(kit => kit.ImageId, f => ITEMS_COUNT + f.GetPositiveIndexFaker())
            .RuleFor(it => it.ShouldBeShown, f => GetVisibility())
            .RuleFor(kit => kit.Description, f => f.Lorem.Sentence());
        var kits = kitFaker.Generate(KITS_COUNT);
        foreach (var kit in kits) kit.OrderNo = GetRandomOrderNumber(kit.ShouldBeShown);

        return kits;
    }

    private IReadOnlyCollection<Element> GenerateElements()
    {
        var elementFaker = new Faker<Element>(FAKER_LOCALE)
            .RuleFor(elem => elem.Id, f => f.GetPositiveIndexFaker())
            .RuleFor(elem => elem.PricePerItem,
                f => Math.Round(f.Random.Decimal(0.01m, 10) * f.Random.Number(1, 20), 2))
            .RuleFor(elem => elem.ItemsAmount, f => (uint)f.Random.Number(1, ITEMS_COUNT))
            .RuleFor(elem => elem.KitId, f => ITEMS_COUNT + f.Random.Number(1, KITS_COUNT))
            .RuleFor(elem => elem.ItemId, f => f.Random.Number(1, ITEMS_COUNT));
        List<Element> elements = new(ELEMENTS_COUNT);
        var elementsCreatedCount = 0;
        while (elementsCreatedCount < ELEMENTS_COUNT)
        {
            var element = elementFaker.Generate();
            if (elements.Any(e => e.ItemId == element.ItemId && e.KitId == element.KitId)) continue;
            elements.Add(element);
            elementsCreatedCount++;
        }

        return elements;
    }

    private IReadOnlyCollection<Image> GenerateImages()
    {
        var imageFaker = new Faker<Image>(FAKER_LOCALE)
            .RuleFor(img => img.Id, f => f.GetPositiveIndexFaker())
            .RuleFor(img => img.FileUrl, f => f.Image.PicsumUrl())
            .RuleFor(img => img.AltDescription, f => f.Lorem.Word());
        return imageFaker.Generate(IMAGES_COUNT);
    }

    private uint? GetRandomOrderNumber(bool isVisible)
    {
        if (!isVisible) return null;
        uint orderNumber;
        do
        {
            orderNumber = (uint)Random.Shared.Next(0, VISIBLE_PURCHASABLE);
        } while (_orderNumbers.Contains(orderNumber));

        _orderNumbers.Add(orderNumber);
        return orderNumber;
    }

    private bool GetVisibility()
    {
        if (_visiblePurchasable >= VISIBLE_PURCHASABLE) return false;
        var shouldBeVisible = Random.Shared.Next(0, 2) == 0 ? true : false;
        if (shouldBeVisible) _visiblePurchasable++;
        return shouldBeVisible;
    }
}