namespace QueflityMVC.Domain.Common
{
    public class BaseImage : BaseEntity
    {
        public string FileUrl { get; set; }

        public string AltDescription { get; set; }
    }
}