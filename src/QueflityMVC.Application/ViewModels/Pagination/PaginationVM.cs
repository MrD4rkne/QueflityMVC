using QueflityMVC.Application.Common.Pagination;

namespace QueflityMVC.Application.ViewModels.Pagination;

public record PaginationVm<T> where T : class
{
    public PaginationVm() : this(new PaginationInfo())
    {
    }

    public PaginationVm(PaginationInfo info) : this(info, new List<T>())
    {
    }

    protected PaginationVm(PaginationInfo info, List<T> entities)
    {
        Info = info;
        Info.FillInfo();

        Entities = entities;
    }

    public PaginationInfo Info { get; set; }

    public List<T> Entities { get; set; }
}