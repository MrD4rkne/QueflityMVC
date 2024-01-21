using QueflityMVC.Domain.Common;

namespace QueflityMVC.Domain.Models
{
    public class ItemImage : BaseImage
    {
        public virtual Item? Item { get; set; }
    }
}