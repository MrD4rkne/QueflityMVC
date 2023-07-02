using QueflityMVC.Domain.Common;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Domain.Interfaces
{
    public interface IItemCategoryRepository : IBaseRepository<ItemCategory>
    {
        /// <summary>
        /// Returns True if no Item has CategoryId equal to 'id'
        /// </summary>
        /// <param name="categoryId">Category's id</param>
        /// <returns></returns>
        bool CanDeleteItemCategory(int categoryId);
    }
}
