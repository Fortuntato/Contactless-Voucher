using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ContactlessLoyalty.Data
{
    public class DatabaseContext : IdentityDbContext<AccountContactlessLoyaltyUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Dashboard> Dashboard { get; set; }
    }
}
