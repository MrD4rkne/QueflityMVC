using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using QueflityMVC.Application.Common.ArgumentGuard;
using QueflityMVC.Application.ViewModels.Pagination;

namespace QueflityMVC.Application.Common.Pagination;

public static class PaginationExtensions
{
    /// <summary>
    ///     Paginates a queryable collection and maps the result to a view model using AutoMapper.
    /// </summary>
    /// <typeparam name="T1">The type of the original entity.</typeparam>
    /// <typeparam name="T2">The type of the entity after mapping.</typeparam>
    /// <param name="entitiesSource">The queryable source of entities.</param>
    /// <param name="paginationVm">Pagination view model to get Pagination info from.</param>
    /// <param name="configurationProvider">The AutoMapper configuration provider.</param>
    /// <returns>A pagination view model containing the requested page of mapped entities.</returns>
    public static Task<PaginationVm<T2>> Paginate<T1, T2>(
        this IQueryable<T1> entitiesSource,
        PaginationVm<T2> paginationVm,
        IConfigurationProvider configurationProvider)
        where T1 : class
        where T2 : class
    {
        return Paginate<T1, T2, PaginationVm<T2>>(entitiesSource, paginationVm.Info.CurrentPageNo,
            paginationVm.Info.PageSize, configurationProvider);
    }

    /// <summary>
    ///     Paginates a queryable collection and maps the result to a view model using AutoMapper.
    /// </summary>
    /// <typeparam name="T1">The type of the original entity.</typeparam>
    /// <typeparam name="T2">The type of the entity after mapping.</typeparam>
    /// <param name="entitiesSource">The queryable source of entities.</param>
    /// <param name="pageNo">The page number to retrieve.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <param name="configurationProvider">The AutoMapper configuration provider.</param>
    /// <returns>A pagination view model containing the requested page of mapped entities.</returns>
    public static Task<PaginationVm<T2>> Paginate<T1, T2>(
        this IQueryable<T1> entitiesSource,
        int pageNo,
        int pageSize,
        IConfigurationProvider configurationProvider)
        where T1 : class
        where T2 : class
    {
        return Paginate<T1, T2, PaginationVm<T2>>(entitiesSource, pageNo, pageSize, configurationProvider);
    }

    /// <summary>
    ///     Paginates a queryable collection and maps the result to a view model using AutoMapper.
    /// </summary>
    /// <typeparam name="T1">The type of the original entity.</typeparam>
    /// <typeparam name="T2">The type of the entity after mapping.</typeparam>
    /// <typeparam name="TPagination">The type of the pagination view model.</typeparam>
    /// <param name="entitiesSource">The queryable source of entities.</param>
    /// <param name="pageNo">The page number to retrieve.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <param name="configurationProvider">The AutoMapper configuration provider.</param>
    /// <returns>A pagination view model containing the requested page of mapped entities.</returns>
    public static async Task<TPagination> Paginate<T1, T2, TPagination>(
        this IQueryable<T1> entitiesSource,
        int pageNo,
        int pageSize,
        IConfigurationProvider configurationProvider)
        where T1 : class
        where T2 : class
        where TPagination : PaginationVm<T2>, new()
    {
        // Validate page number and page size
        pageNo.MustBe(ArgumentGuardType.GreaterThan, 0);
        pageSize.MustBe(ArgumentGuardType.GreaterThan, 0);

        // Calculate total count and determine pagination parameters
        int totalCount = await entitiesSource.CountAsync();
        int pagesCount = PaginationFactory.CalcPagesCount(pageSize, totalCount);
        pageNo = Math.Max(Math.Min(pageNo, pagesCount), 1);
        int itemsCountToSkip = (pageNo - 1) * pageSize;

        // Retrieve the items for the requested page
        var itemsForPage = entitiesSource.Skip(itemsCountToSkip).Take(pageSize);
        var itemsList = await itemsForPage.ProjectTo<T2>(configurationProvider).ToListAsync();

        // Create and return the pagination view model
        return PaginationFactory.CreatePagination<TPagination, T2>(pageSize, totalCount, pageNo, itemsList);
    }

    /// <summary>
    ///     Paginates a queryable collection using pagination information and maps the result to a view model.
    /// </summary>
    /// <typeparam name="T1">The type of the original entity.</typeparam>
    /// <typeparam name="T2">The type of the entity after mapping.</typeparam>
    /// <typeparam name="TPagination">The type of the pagination view model.</typeparam>
    /// <param name="entitiesSource">The queryable source of entities.</param>
    /// <param name="pagination">The pagination view model containing pagination info.</param>
    /// <param name="configurationProvider">The AutoMapper configuration provider.</param>
    /// <returns>A pagination view model containing the requested page of mapped entities.</returns>
    public static async Task<TPagination> Paginate<T1, T2, TPagination>(
        this IQueryable<T1> entitiesSource,
        PaginationVm<T2> pagination,
        IConfigurationProvider configurationProvider)
        where T1 : class
        where T2 : class
        where TPagination : PaginationVm<T2>, new()
    {
        // Ensure the pagination view model is not null
        ArgumentNullException.ThrowIfNull(pagination);

        return await entitiesSource.Paginate<T1, T2, TPagination>(
            pagination.Info.CurrentPageNo,
            pagination.Info.PageSize,
            configurationProvider);
    }

    /// <summary>
    ///     Paginates a queryable collection using pagination info and maps the result to a view model.
    /// </summary>
    /// <typeparam name="T1">The type of the original entity.</typeparam>
    /// <typeparam name="T2">The type of the entity after mapping.</typeparam>
    /// <typeparam name="TPagination">The type of the pagination view model.</typeparam>
    /// <param name="entitiesSource">The queryable source of entities.</param>
    /// <param name="paginationInfo">The pagination info object containing current page number and page size.</param>
    /// <param name="configurationProvider">The AutoMapper configuration provider.</param>
    /// <returns>A pagination view model containing the requested page of mapped entities.</returns>
    public static async Task<TPagination> Paginate<T1, T2, TPagination>(
        this IQueryable<T1> entitiesSource,
        PaginationInfo paginationInfo,
        IConfigurationProvider configurationProvider)
        where T1 : class
        where T2 : class
        where TPagination : PaginationVm<T2>, new()
    {
        // Ensure the pagination info is not null
        ArgumentNullException.ThrowIfNull(paginationInfo);

        return await entitiesSource.Paginate<T1, T2, TPagination>(
            paginationInfo.CurrentPageNo,
            paginationInfo.PageSize,
            configurationProvider);
    }

    /// <summary>
    ///     Paginates a queryable collection and maps the result to a view model without AutoMapper.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <typeparam name="TPagination">The type of the pagination view model.</typeparam>
    /// <param name="entitiesSource">The queryable source of entities.</param>
    /// <param name="pageNo">The page number to retrieve.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>A pagination view model containing the requested page of entities.</returns>
    public static async Task<TPagination> Paginate<T, TPagination>(
        this IQueryable<T> entitiesSource,
        int pageNo,
        int pageSize)
        where T : class
        where TPagination : PaginationVm<T>, new()
    {
        // Validate page number and page size
        pageNo.MustBe(ArgumentGuardType.GreaterThan, 0);
        pageSize.MustBe(ArgumentGuardType.GreaterThan, 0);

        // Calculate total count and determine pagination parameters
        int totalCount = await entitiesSource.CountAsync();
        int pagesCount = PaginationFactory.CalcPagesCount(pageSize, totalCount);
        pageNo = Math.Max(Math.Min(pageNo, pagesCount), 1);
        int itemsCountToSkip = (pageNo - 1) * pageSize;

        // Retrieve the items for the requested page
        var itemsForPage = entitiesSource.Skip(itemsCountToSkip).Take(pageSize);
        var itemsList = await itemsForPage.ToListAsync();

        // Create and return the pagination view model
        return PaginationFactory.CreatePagination<TPagination, T>(pageSize, totalCount, pageNo, itemsList);
    }

    /// <summary>
    ///     Ensures that pagination information is not null and sets default values if necessary.
    /// </summary>
    /// <param name="paginationVm">The pagination view model.</param>
    public static void FillInfo(this PaginationVm<object> paginationVm)
    {
        paginationVm.Info ??= new PaginationInfo();
        paginationVm.Info.FillInfo();
    }

    /// <summary>
    ///     Ensures that pagination info has valid values and sets default values if necessary.
    /// </summary>
    /// <param name="paginationInfo">The pagination info object.</param>
    public static void FillInfo(this PaginationInfo paginationInfo)
    {
        if (paginationInfo.CurrentPageNo <= 0)
        {
            paginationInfo.CurrentPageNo = PaginationFactory.DEFAULT_PAGE_NO;
        }

        if (paginationInfo.PageSize <= 1)
        {
            paginationInfo.PageSize = PaginationFactory.DEFAULT_PAGE_SIZE;
        }
    }
}