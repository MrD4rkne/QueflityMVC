using QueflityMVC.Domain.Common;
using QueflityMVC.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Domain.Models
{
    public class ItemSetImage : BaseImage
    {
        public virtual ItemSet ItemSet { get; set; }
    }
}
