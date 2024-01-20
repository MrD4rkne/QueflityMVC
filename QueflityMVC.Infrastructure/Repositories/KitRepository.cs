using Microsoft.EntityFrameworkCore;
using QueflityMVC.Application.Errors.Common;
using QueflityMVC.Domain.Errors;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using QueflityMVC.Infrastructure.Common;

namespace QueflityMVC.Infrastructure.Repositories
{
    public class KitRepository : BaseRepository<Kit>, IKitRepository
    {
        public KitRepository(Context dbContext) : base(dbContext) { }

        public IQueryable<int> GetComponenetsIdsForSet(int setId)
        {
            var set = GetFullKitWithMembershipsById(setId);
            if (set is null)
            {
                throw new ResourceNotFoundException();
            }

            var componentsIds = GetKitComponents(setId).Select(x => x.ItemId);
            return componentsIds;
        }

        public IQueryable<Element> GetKitComponents(int setId)
        {
            return _dbContext.SetElements.Where(x => x.KitId == setId);
        }

        public IQueryable<Kit> GetFilteredByName(string? searchName = default)
        {
            var itemsSource = GetAll();

            if (!string.IsNullOrEmpty(searchName))
            {
                itemsSource = itemsSource.Where(x => x.Name.StartsWith(searchName));
            }
            return itemsSource;
        }

        public Kit? GetFullKitWithMembershipsById(int id)
        {
            return _dbContext.Set<Kit>()
                .Include(z => z.Image)
                .Include(x => x.Elements)
                    .ThenInclude(x => x.Item)
                        .ThenInclude(item => item!.Image)
                 .FirstOrDefault(y => y.Id == id);
        }

        public IQueryable<decimal> GetPricesOfKitComponents(int kitId)
        {
            return _dbContext.Set<Element>()
                .Where(x => x.KitId == kitId)
                .Select(x => x.PricePerItem * x.ItemsAmmount);
        }

        public void AddComponent(Element componentToCreate)
        {
            if (componentToCreate is null)
            {
                throw new ArgumentNullException(nameof(componentToCreate));
            }

            _dbContext.Add(componentToCreate);
            _dbContext.SaveChanges();

            UpdateKitPrice(componentToCreate.KitId);
        }

        public void UpdateKitPrice(int kitId)
        {
            Kit? kit = GetById(kitId);
            if (kit is null)
            {
                throw new EntityNotFoundException(entityName: nameof(Kit));
            }

            var componentsPrices = GetPricesOfKitComponents(kitId);
            decimal sumOfComponentPrices = componentsPrices.Sum();
            kit.Price = sumOfComponentPrices;
            Update(kit);
        }

        public Element? GetElement(int kitId, int itemId)
        {
            return _dbContext.Set<Element>()
                .Include(elem => elem.Kit)
                    .ThenInclude(kit => kit!.Image)
                .Include(elem => elem.Item)
                    .ThenInclude(item => item!.Image)
                .Where(x => x.ItemId == itemId && x.KitId == kitId)
                .FirstOrDefault();
        }

        public void UpdateElement(Element element)
        {
            Element? elementToEdit = GetElement(element.KitId, element.ItemId);
            if (elementToEdit is null)
            {
                throw new EntityNotFoundException(entityName: nameof(Element));
            }

            elementToEdit.PricePerItem = elementToEdit.PricePerItem;
            elementToEdit.ItemsAmmount = elementToEdit.ItemsAmmount;

            _dbContext.Entry(elementToEdit).State = EntityState.Modified;
            _dbContext.SaveChanges();

            UpdateKitPrice(elementToEdit.KitId);
        }

        public void DeleteElement(int kitId, int itemId)
        {
            Element? elemToDelete = GetElement(kitId, itemId);
            if (elemToDelete is null)
            {
                throw new EntityNotFoundException(nameof(Element));
            }

            _dbContext.Remove(elemToDelete);
            UpdateKitPrice(elemToDelete.KitId);
            _dbContext.SaveChanges();
        }
    }
}
