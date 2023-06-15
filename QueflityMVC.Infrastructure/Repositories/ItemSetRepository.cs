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
    public class ItemSetRepository : GenericRepository<ItemSet>, IItemSetRepository
    {
        public override DbSet<ItemSet> Table() => _dbContext.ItemSets;

        public ItemSetRepository(Context dbContext) : base(dbContext) { }
    }
}
