using QueflityMVC.Domain.Common;

namespace QueflityMVC.Domain.Models
{
    public class KitImage : BaseImage
    {
        public required virtual Kit? Kit { get; set; }
    }
}
