using QueflityMVC.Domain.Common;

namespace QueflityMVC.Domain.Models
{
    public class ItemSetImage : BaseImage
    {
        public virtual ItemSet ItemSet { get; set; }
    }
}
