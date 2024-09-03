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
        {
            filteredCategories = filteredCategories.Where(ct => ct.Name.StartsWith(nameFilter));
        }

        return filteredCategories;
    }

    public Task<bool> DoesCategoryWithNameExistAsync(string name)
    {
        return DbContext.Categories.AnyAsync(x => x.Name == name);
    }

    public Task<bool> DoesCategoryWithNameButNotIdExistAsync(int id, string name)
    {
        return DbContext.Categories.AnyAsync(x => x.Name == name && x.Id != id);
    }
}