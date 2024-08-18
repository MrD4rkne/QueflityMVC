using QueflityMVC.Application.Common.ArgumentGuard;
using QueflityMVC.Application.ViewModels.Pagination;

namespace QueflityMVC.Application.Common.Pagination;

public static class PaginationFactory
{
    public const int DEFAULT_PAGE_SIZE = 2;
    public const int DEFAULT_PAGE_NO = 1;

    /// <summary>
    ///     Creates a pagination view model with the given parameters.
    /// </summary>
    /// <typeparam name="T">The type of the entities in the pagination.</typeparam>
    /// <param name="pageSize">The number of items per page.</param>
    /// <param name="totalCount">The total number of items.</param>
    /// <param name="currPageNo">The current page number.</param>
    /// <param name="entities">The list of entities to be displayed on the current page.</param>
    /// <returns>A pagination view model containing the provided parameters and entities.</returns>
    public static PaginationVm<T> CreatePagination<T>(
        int pageSize,
        int totalCount,
        int currPageNo,
        List<T> entities)
        where T : class
    {
        return CreatePagination<PaginationVm<T>, T>(pageSize, totalCount, currPageNo, entities);
    }

    /// <summary>
    ///     Creates a pagination view model with the given parameters.
    /// </summary>
    /// <typeparam name="TPagination">The type of the pagination view model.</typeparam>
    /// <typeparam name="T">The type of the entities in the pagination.</typeparam>
    /// <param name="pageSize">The number of items per page.</param>
    /// <param name="totalCount">The total number of items.</param>
    /// <param name="currPageNo">The current page number.</param>
    /// <param name="entities">The list of entities to be displayed on the current page.</param>
    /// <returns>A pagination view model containing the provided parameters and entities.</returns>
    public static TPagination CreatePagination<TPagination, T>(
        int pageSize,
        int totalCount,
        int currPageNo,
        List<T> entities)
        where T : class
        where TPagination : PaginationVm<T>, new()
    {
        // Create and initialize pagination information
        PaginationInfo paginationBase = new()
        {
            CurrentPageNo = currPageNo,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        paginationBase.FillInfo(); // Ensure pagination info has default values if needed
        paginationBase.PagesCount = CalcPagesCount(pageSize, totalCount); // Calculate total pages

        // Create and initialize the pagination view model
        TPagination paginationVm = new()
        {
            Info = paginationBase,
            Entities = entities
        };

        return paginationVm;
    }

    /// <summary>
    ///     Creates a default pagination view model with the given parameters.
    /// </summary>
    /// <typeparam name="T">The type of the entities in the pagination.</typeparam>
    /// <param name="currentPageNo">The current page number. Default is 1.</param>
    /// <param name="pageSize">The number of items per page. Default is 2.</param>
    /// <returns>A default pagination view model with empty entities.</returns>
    public static PaginationVm<T> Default<T>(
        int currentPageNo = DEFAULT_PAGE_NO,
        int pageSize = DEFAULT_PAGE_SIZE)
        where T : class
    {
        return Default<PaginationVm<T>, T>(currentPageNo, pageSize);
    }

    /// <summary>
    ///     Creates a default pagination view model with the given parameters.
    /// </summary>
    /// <typeparam name="TPagination">The type of the pagination view model.</typeparam>
    /// <typeparam name="T">The type of the entities in the pagination.</typeparam>
    /// <param name="currentPageNo">The current page number. Default is 1.</param>
    /// <param name="pageSize">The number of items per page. Default is 2.</param>
    /// <returns>A default pagination view model with empty entities.</returns>
    public static TPagination Default<TPagination, T>(
        int currentPageNo = DEFAULT_PAGE_NO,
        int pageSize = DEFAULT_PAGE_SIZE)
        where T : class
        where TPagination : PaginationVm<T>, new()
    {
        // Create and initialize pagination information with default values
        PaginationInfo paginationBase = new()
        {
            CurrentPageNo = currentPageNo,
            PageSize = pageSize,
            PagesCount = 1 // Default to 1 page
        };

        // Create and initialize the pagination view model with empty entities
        TPagination paginationVm = new()
        {
            Info = paginationBase,
            Entities = Enumerable.Empty<T>().ToList()
        };

        return paginationVm;
    }

    /// <summary>
    ///     Calculates the total number of pages based on page size and total item count.
    /// </summary>
    /// <param name="pageSize">The number of items per page.</param>
    /// <param name="totalCount">The total number of items.</param>
    /// <returns>The total number of pages.</returns>
    public static int CalcPagesCount(int pageSize, int totalCount)
    {
        // Validate input parameters
        pageSize.MustBe(ArgumentGuardType.GreaterThan, 0);
        totalCount.MustBe(ArgumentGuardType.GreaterThanOrEquals, 0);

        // Calculate and return the number of pages needed
        double pageRatio = totalCount * 1.0 / pageSize;
        return (int)Math.Ceiling(pageRatio);
    }
}