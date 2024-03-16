using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using QueflityMVC.Application.Common.ArgumentGuard;
using QueflityMVC.Application.ViewModels.Pagination;

namespace QueflityMVC.Application.Common.Pagination;

public static class PaginationExtensions
{
    private const int DEFAULT_PAGE_SIZE = 2;
    private const int DEFAULT_PAGE_NO = 1;

    /// <summary>
    ///     Create viewmodel for easy-handling your pagination with automapping
    /// </summary>
    /// <typeparam name="T1">Original entity type</typeparam>
    /// <typeparam name="T2">Mapped entity type</typeparam>
    /// <param name="entitiesSource">Queryable with original entities</param>
    /// <param name="pageNo">Demanded page's number</param>
    /// <param name="pageSize">Page size</param>
    /// <param name="configurationProvider">Automapper configuration provider</param>
    /// <returns></returns>
    public static async Task<PaginationVm<T2>> Paginate<T1, T2>(this IQueryable<T1> entitiesSource, int pageNo,
        int pageSize, IConfigurationProvider configurationProvider)
        where T1 : class
        where T2 : class
    {
        pageNo.MustBe(ArgumentGuardType.GreaterThan, 0);
        pageSize.MustBe(ArgumentGuardType.GreaterThan, 0);

        var totalCount = await entitiesSource.CountAsync();
        var pagesCount = PaginationFactory.CalcPagesCount(pageSize, totalCount);
        pageNo = Math.Max(Math.Min(pageNo, pagesCount), 1);
        var itemsCountToSkip = (pageNo - 1) * pageSize;
        var itemsForPage = entitiesSource.Skip(itemsCountToSkip).Take(pageSize);

        var itemsList = await itemsForPage.ProjectTo<T2>(configurationProvider).ToListAsync();

        var paginationVm = PaginationFactory.CreatePagination(pageSize, totalCount, pageNo, itemsList);
        return paginationVm;
    }

    public static async Task<PaginationVm<T2>> Paginate<T1, T2>(this IQueryable<T1> entitiesSource,
        PaginationVm<T2> pagination, IConfigurationProvider configurationProvider)
        where T1 : class
        where T2 : class
    {
        ArgumentNullException.ThrowIfNull(pagination);

        return await entitiesSource.Paginate<T1, T2>(pagination.Info, configurationProvider);
    }

    public static async Task<PaginationVm<T2>> Paginate<T1, T2>(this IQueryable<T1> entitiesSource,
        PaginationInfo paginationInfo, IConfigurationProvider configurationProvider)
        where T1 : class
        where T2 : class
    {
        ArgumentNullException.ThrowIfNull(paginationInfo);
        return await entitiesSource.Paginate<T1, T2>(paginationInfo.CurrentPageNo, paginationInfo.PageSize,
            configurationProvider);
    }

    /// <summary>
    ///     Create viewmodel for easy-handling your pagination
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <param name="entitiesSource">Queryable with original entities</param>
    /// <param name="pageNo">Demanded page's number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns></returns>
    public static async Task<PaginationVm<T>> Paginate<T>(this IQueryable<T> entitiesSource, int pageNo, int pageSize)
        where T : class
    {
        pageNo.MustBe(ArgumentGuardType.OtherThan, 0);
        pageNo.MustBe(ArgumentGuardType.GreaterThan, 0);
        pageSize.MustBe(ArgumentGuardType.OtherThan, 0);
        pageSize.MustBe(ArgumentGuardType.GreaterThan, 0);

        var totalCount = await entitiesSource.CountAsync();
        var pagesCount = PaginationFactory.CalcPagesCount(pageSize, totalCount);
        pageNo = Math.Max(Math.Min(pageNo, pagesCount), 1);
        var itemsCountToSkip = (pageNo - 1) * pageSize;
        var itemsForPage = entitiesSource.Skip(itemsCountToSkip).Take(pageSize);
        var itemsList = await itemsForPage.ToListAsync();

        var paginationVm = PaginationFactory.CreatePagination(pageSize, totalCount, pageNo, itemsList);
        return paginationVm;
    }

    public static void FillInfoWhenNull(this PaginationVm<object> paginationVm)
    {
        paginationVm.Info ??= new PaginationInfo();
        paginationVm.Info.FillInfoWhenNull();
    }

    public static void FillInfoWhenNull(this PaginationInfo paginationInfo)
    {
        if (paginationInfo.CurrentPageNo <= 0) paginationInfo.CurrentPageNo = DEFAULT_PAGE_NO;
        if (paginationInfo.PageSize <= 1) paginationInfo.PageSize = DEFAULT_PAGE_SIZE;
    }
}