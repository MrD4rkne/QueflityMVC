using QueflityMVC.Domain.Common;

namespace QueflityMVC.Domain.Models
{
    public class ItemSetImage : BaseImage
    {
        public required virtual ItemSet? ItemSet { get; set; }
    }
}
