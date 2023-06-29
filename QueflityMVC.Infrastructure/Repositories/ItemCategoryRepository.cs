using Microsoft.EntityFrameworkCore;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using QueflityMVC.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Infrastructure.Repositories
{
    public class ItemCategoryRepository : BaseRepository<ItemCategory>, IItemCategoryRepository
    {
        public ItemCategoryRepository(Context dbContext) : base(dbContext) { }

        public bool CanDeleteItemCategory(int categoryId)
        {
            return !_dbContext.Items.Any(x => x.ItemCategoryId == categoryId); 
        }
    }
}
