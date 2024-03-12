namespace QueflityMVC.Application.ViewModels.Pagination;

public record PaginationVm<T> where T : class
{
    public PaginationVm()
    {
        Entities = Enumerable.Empty<T>().ToList();
        Info = new PaginationInfo();
    }

    public PaginationInfo Info { get; set; }

    public List<T> Entities { get; set; }
}