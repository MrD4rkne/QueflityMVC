namespace QueflityMVC.Application.ViewModels.Pagination
{
    public record PaginationInfo
    {
        public int TotalCount { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPageNo { get; set; }

        public int PageSize { get; set; }
    }
}