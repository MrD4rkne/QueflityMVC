using QueflityMVC.Application.Common.Errors;
using QueflityMVC.Application.ViewModels.Pagination;

namespace QueflityMVC.Application.Common.Pagination
{
    public static class PaginationFactory
    {
        public static PaginationVM<T> CreatePagination<T>(int pageSize, int totalCount, int currPageNo, List<T> entities) where T : class
        {
            PaginationInfo paginationBase = new()
            {
                CurrentPageNo = currPageNo,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            paginationBase.FillInfoWhenNull();

            paginationBase.PagesCount = CalcPagesCount(pageSize, totalCount);

            PaginationVM<T> paginationVM = new()
            {
                Info = paginationBase,
                Entities = entities,
            };

            return paginationVM;
        }

        public static PaginationVM<T> Default<T>(int currentPageNo = 1, int pageSize = 2) where T : class
        {
            PaginationInfo paginationBase = new()
            {
                CurrentPageNo = currentPageNo,
                PageSize = pageSize,
                PagesCount = 1
            };

            PaginationVM<T> paginationVM = new()
            {
                Info = paginationBase,
                Entities = Enumerable.Empty<T>().ToList()
            };

            return paginationVM;
        }

        public static int CalcPagesCount(int pageSize, int totalCount)
        {
            pageSize.MustBe(ArgumentGuardType.GreaterThan, 0);
            totalCount.MustBe(ArgumentGuardType.GreaterThanOrEquals, 0);

            double pageRatio = (totalCount * 1.0) / pageSize;
            return (int)Math.Ceiling(pageRatio);
        }
    }
}
