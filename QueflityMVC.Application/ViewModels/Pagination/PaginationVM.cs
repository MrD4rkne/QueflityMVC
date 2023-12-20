namespace QueflityMVC.Application.ViewModels.Pagination
{
    public record PaginationVM<T> where T : class
    {
        public PaginationInfo Info { get; set; }

        public List<T> Entities { get; set; }

        public PaginationVM()
        {
            Entities = Enumerable.Empty<T>().ToList();
            Info = new PaginationInfo();
        }

    }
}
