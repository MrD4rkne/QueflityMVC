using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using QueflityMVC.Infrastructure.Common;

namespace QueflityMVC.Infrastructure.Repositories
{
    public class ItemSetRepository : BaseRepository<ItemSet>, IItemSetRepository
    {
        public ItemSetRepository(Context dbContext) : base(dbContext) { }
    }
}
