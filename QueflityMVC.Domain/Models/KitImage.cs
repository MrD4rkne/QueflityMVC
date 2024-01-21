using QueflityMVC.Domain.Common;

namespace QueflityMVC.Domain.Models
{
    public class KitImage : BaseImage
    {
        public virtual Kit? Kit { get; set; }
    }
}