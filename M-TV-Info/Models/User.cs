using Microsoft.AspNetCore.Identity;
using System;

namespace M_TV_Info.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public String Country { get; set; }
    }
}
