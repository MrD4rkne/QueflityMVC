using QueflityMVC.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Domain.Common
{
    public class BaseImage : BaseEntity
    {
        public string FileUrl { get; set; }

        public string Title { get; set; }

        public string AltDescription { get; set; }
    }
}
