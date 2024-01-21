using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using QueflityMVC.Infrastructure.Common;

namespace QueflityMVC.Infrastructure.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(Context dbContext) : base(dbContext)
    {
    }

    public bool CanDeleteCategory(int categoryId)
    {
        return !_dbContext.Items.Any(x => x.CategoryId == categoryId);
    }

    public IQueryable<Category> GetFiltered(string? nameFilter)
    {
        var filteredCategories = GetAll();
        if (!string.IsNullOrEmpty(nameFilter))
        {
            filteredCategories = filteredCategories.Where(ct => ct.Name.StartsWith(nameFilter));
        }
        return filteredCategories;
    }
}