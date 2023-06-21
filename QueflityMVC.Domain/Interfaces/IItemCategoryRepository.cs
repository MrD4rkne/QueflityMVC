using QueflityMVC.Domain.Common;
using QueflityMVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
