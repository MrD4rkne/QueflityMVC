using Microsoft.EntityFrameworkCore;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using QueflityMVC.Persistence.Common;

namespace QueflityMVC.Persistence.Repositories;

public class CategoryRepository(Context dbContext) : BaseRepository<Category>(dbContext), ICategoryRepository
{
    public async Task<bool> IsAnyItemWithCategory(int categoryId)
    {
        return !await DbContext.Items.AnyAsync(x => x.CategoryId == categoryId);
    }

    public IQueryable<Category> GetFiltered(string? nameFilter)
    {
        var filteredCategories = GetAll();
        if (!string.IsNullOrEmpty(nameFilter))
            filteredCategories = filteredCategories.Where(ct => ct.Name.StartsWith(nameFilter));
        return filteredCategories;
    }
}