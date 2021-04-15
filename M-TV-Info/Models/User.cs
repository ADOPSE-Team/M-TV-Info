using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace M_TV_Info.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
    }
}
