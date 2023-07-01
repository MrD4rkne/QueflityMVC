using QueflityMVC.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Domain.Common
{
    public class BaseBuyableEntity : BaseEntity
    {
        public bool ShouldBeShown { get; set; }

    }
}
