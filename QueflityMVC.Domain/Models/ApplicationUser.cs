using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool? IsEnabled { get; set; }

        public ApplicationUser()
        {

        }
    }
}
