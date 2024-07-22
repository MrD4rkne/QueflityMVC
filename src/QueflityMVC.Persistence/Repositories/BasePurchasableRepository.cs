using Microsoft.EntityFrameworkCore;
using QueflityMVC.Domain.Common;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Persistence.Common;

namespace QueflityMVC.Persistence.Repositories;

public class BaseProductRepository<T>(Context dbContext)
    : BaseRepository<T>(dbContext), IBaseProductRepository<T>
    where T : Product
{
    public Task BulkUpdateOrderAsync(uint pivot)
    {
        return DbContext.Set<T>()
            .Where(x => x.OrderNo >= pivot)
            .ForEachAsync(x => x.OrderNo--);
    }
}