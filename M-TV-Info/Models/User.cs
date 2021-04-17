using Microsoft.AspNetCore.Identity;

namespace M_TV_Info.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
