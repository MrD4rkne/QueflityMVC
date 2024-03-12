using QueflityMVC.Application.Common.ArgumentGuard;
using QueflityMVC.Application.ViewModels.Pagination;

namespace QueflityMVC.Application.Common.Pagination;

public static class PaginationFactory
{
    public static PaginationVm<T> CreatePagination<T>(int pageSize, int totalCount, int currPageNo, List<T> entities)
        where T : class
    {
        PaginationInfo paginationBase = new()
        {
            CurrentPageNo = currPageNo,
            PageSize = pageSize,
            TotalCount = totalCount
        };
        paginationBase.FillInfoWhenNull();
        paginationBase.PagesCount = CalcPagesCount(pageSize, totalCount);

        PaginationVm<T> paginationVm = new()
        {
            Info = paginationBase,
            Entities = entities
        };

        return paginationVm;
    }

    public static PaginationVm<T> Default<T>(int currentPageNo = 1, int pageSize = 2) where T : class
    {
        PaginationInfo paginationBase = new()
        {
            CurrentPageNo = currentPageNo,
            PageSize = pageSize,
            PagesCount = 1
        };

        PaginationVm<T> paginationVm = new()
        {
            Info = paginationBase,
            Entities = Enumerable.Empty<T>().ToList()
        };

        return paginationVm;
    }

    public static int CalcPagesCount(int pageSize, int totalCount)
    {
        pageSize.MustBe(ArgumentGuardType.GreaterThan, 0);
        totalCount.MustBe(ArgumentGuardType.GreaterThanOrEquals, 0);

        var pageRatio = totalCount * 1.0 / pageSize;
        return (int)Math.Ceiling(pageRatio);
    }
}