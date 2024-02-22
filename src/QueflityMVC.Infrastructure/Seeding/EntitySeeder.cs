using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Infrastructure.Seeding;
public class EntitySeeder
{
    private const string FAKER_LOCALE = "en";
    private const int CATEGORIES_COUNT = 5;
    private const int ITEMS_COUNT = 10;
    private const int INGREDIENTS_COUNT = 20;
    private const int KITS_COUNT = 10;
    private const int IMAGES_COUNT = ITEMS_COUNT + KITS_COUNT;
    private const int ELEMENTS_COUNT = 15;

    public IReadOnlyCollection<Category> Categories { get; init; }

    public IReadOnlyCollection<Item> Items { get; init; }

    public IReadOnlyCollection<Ingredient> Ingredients { get; init; }

    public IReadOnlyCollection<Kit> Kits { get; init; }

    public IReadOnlyCollection<Image> Images { get; init; }

    public IReadOnlyCollection<Element> Elements { get; init; }

    public EntitySeeder()
    {
        Categories = GenerateCategories();
        Ingredients = GenerateIngredients();
        Images = GenerateImages();
        Items = GenerateItems();
        Elements = GenerateElements();
        Kits = GenerateKits();
    }

    private IReadOnlyCollection<Category> GenerateCategories()
    {
        var categoryFaker = new Faker<Category>(FAKER_LOCALE)
            .RuleFor(ctg => ctg.Id, f=> f.GetPositiveIndexFaker())
            .RuleFor(ctg => ctg.Name, f => f.Commerce.Categories(1)[0]);
        return categoryFaker.Generate(CATEGORIES_COUNT);
    }

    private IReadOnlyCollection<Ingredient> GenerateIngredients()
    {
        var ingredientFaker = new Faker<Ingredient>(FAKER_LOCALE)
            .RuleFor(ing => ing.Id, f => f.GetPositiveIndexFaker())
            .RuleFor(ing => ing.Name, f => f.Commerce.ProductMaterial());
        return ingredientFaker.Generate(INGREDIENTS_COUNT);
    }

    /// <summary>
    /// generate categories, ingredients and images before items
    /// </summary>
    /// <returns></returns>
    private IReadOnlyCollection<Item> GenerateItems()
    {
        var itemFaker = new Faker<Item>(FAKER_LOCALE)
            .RuleFor(it => it.Id, f => f.GetPositiveIndexFaker())
            .RuleFor(it => it.Name, f => f.Commerce.ProductName())
            .RuleFor(it => it.Price, f=> Math.Round(f.Random.Decimal(0.01m,10) * f.Random.Number(1,20),2))
            .RuleFor(it => it.ImageId, f=>f.GetPositiveIndexFaker())
            .RuleFor(it => it.CategoryId, f => f.Random.Number(1, CATEGORIES_COUNT));
        return itemFaker.Generate(ITEMS_COUNT);
    }

    private IReadOnlyCollection<Kit> GenerateKits()
    {
        var kitFaker = new Faker<Kit>(FAKER_LOCALE)
            .RuleFor(kit => kit.Id, f => ITEMS_COUNT+f.GetPositiveIndexFaker())
            .RuleFor(kit => kit.Name, f => f.Commerce.ProductName())
            .RuleFor(kit => kit.ImageId, f => ITEMS_COUNT + f.GetPositiveIndexFaker())
            .RuleFor(kit => kit.Description, f=> f.Lorem.Sentence());
        var kits = kitFaker.Generate(KITS_COUNT);
        foreach(var kit in kits)
        {
            kit.Price = Elements.Where(elem => elem.KitId == kit.Id).Sum(elem => elem.ItemsAmmount * elem.PricePerItem);
        }
        return kits;
    }

    private IReadOnlyCollection<Element> GenerateElements()
    {
        var elementFaker = new Faker<Element>(FAKER_LOCALE)
            .RuleFor(elem => elem.Id, f => f.GetPositiveIndexFaker())
            .RuleFor(elem => elem.PricePerItem, f => Math.Round(f.Random.Decimal(0.01m, 10) * f.Random.Number(1, 20),2))
            .RuleFor(elem => elem.ItemsAmmount, f => (uint)f.Random.Number(1, ITEMS_COUNT))
            .RuleFor(elem => elem.KitId, f => ITEMS_COUNT + f.Random.Number(1, KITS_COUNT))
            .RuleFor(elem => elem.ItemId, f => f.Random.Number(1, ITEMS_COUNT));
        List<Element> elements = new(ELEMENTS_COUNT);
        int elementsCreatedCount = 0;
        while(elementsCreatedCount < ELEMENTS_COUNT)
        {
            var element = elementFaker.Generate();
            if(elements.Any(e=>e.ItemId == element.ItemId && e.KitId == element.KitId))
            {
                continue;
            }
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
            .RuleFor(img => img.AltDescription, f=> f.Lorem.Word());
        return imageFaker.Generate(IMAGES_COUNT);
    }
}
