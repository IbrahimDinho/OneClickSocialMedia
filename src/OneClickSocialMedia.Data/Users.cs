using Microsoft.AspNetCore.Identity;

namespace OneClickSocialMedia.Data
{
    public class Users : IdentityUser
    {
        public string Name { get; set; }

    }
}
