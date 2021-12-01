using Microsoft.AspNetCore.Identity;

namespace ProductsSite
{
    public class SiteUsers : IdentityUser
    {
        public int Age { get; set; }
    }
}