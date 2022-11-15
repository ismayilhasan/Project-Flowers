using Microsoft.AspNetCore.Identity;

namespace FronttoBackFlowers.Models.IdentityModels
{
    public class User : IdentityUser
    {
        public string Fullname { get; set; }
    }
}
