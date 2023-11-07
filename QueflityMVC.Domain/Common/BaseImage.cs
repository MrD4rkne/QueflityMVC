namespace QueflityMVC.Domain.Common
{
    public class BaseImage : BaseEntity
    {
        public required string FileUrl { get; set; }

        public required string AltDescription { get; set; }
    }
}
