using QueflityMVC.Domain.Common;

namespace QueflityMVC.Domain.Models
{
    public class KitImage : BaseImage
    {
        public virtual required Kit? Kit { get; set; }
    }
}
