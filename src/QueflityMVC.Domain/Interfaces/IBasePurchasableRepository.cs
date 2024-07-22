using QueflityMVC.Domain.Common;

namespace QueflityMVC.Domain.Interfaces;

public interface IBaseProductRepository<T> : IBaseRepository<T> where T : Product
{
    Task BulkUpdateOrderAsync(uint pivot);
}