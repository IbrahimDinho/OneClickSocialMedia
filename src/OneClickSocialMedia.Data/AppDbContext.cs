using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OneClickSocialMedia.Data
{
    public class AppDbContext : IdentityDbContext<Users>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TwitterOAuthTokens> TwitterOAuthTokens { get; set; }

        public DbSet<InstagramOAuthTokens> InstagramOAuthTokens { get; set; }
    }
}
