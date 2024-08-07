﻿using QueflityMVC.Domain.Models;

namespace QueflityMVC.Domain.Interfaces;

public interface ICategoryRepository : IBaseRepository<Category>
{
    /// <summary>
    ///     Returns True if no Item has CategoryId equal to 'id'
    /// </summary>
    /// <param name="categoryId">Category's id</param>
    /// <returns></returns>
    Task<bool> IsAnyItemWithCategory(int categoryId);

    IQueryable<Category> GetFiltered(string? nameFilter);
}