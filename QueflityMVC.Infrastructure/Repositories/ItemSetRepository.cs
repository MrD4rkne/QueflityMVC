using Microsoft.EntityFrameworkCore;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using QueflityMVC.Infrastructure.Common;

namespace QueflityMVC.Infrastructure.Repositories
{
    public class ItemSetRepository : BaseRepository<ItemSet>, IItemSetRepository
    {
        public ItemSetRepository(Context dbContext) : base(dbContext) { }

        public IQueryable<int> GetComponenetsIdsForSet(int setId)
        {
            var set = GetFullItemSetWithMembershipsById(setId);
            if (set is null)
            {
                throw new ArgumentException(nameof(setId));
            }

            var componentsIds = GetSetComponents(setId).Select(x => x.Id);

            return componentsIds;

        }

        public IQueryable<SetElement> GetSetComponents(int setId)
        {
            return _dbContext.SetElements.Where(x => x.ItemSetId == setId);
        }

        public IQueryable<ItemSet> GetFilteredByName(string? searchName = default)
        {
            var itemsSource = GetAll();

            if (!string.IsNullOrEmpty(searchName))
            {
                itemsSource = itemsSource.Where(x => x.Name.StartsWith(searchName));
            }

            return itemsSource;
        }

        public ItemSet? GetFullItemSetWithMembershipsById(int id)
        {
            return _dbContext.Set<ItemSet>().Include(x => x.Elements).Include(z => z.Image).FirstOrDefault(y => y.Id == id);
        }
    }
}
