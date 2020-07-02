using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactlessLoyalty.Data;
using Microsoft.AspNetCore.Identity;
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
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            //builder.Entity<IdentityUser>().Ignore("PhoneNumberConfirmed")
            //    .Ignore("TwoFactorEnabled")
            //    .Ignore("LockoutEnd")
            //    .Ignore("AccessFailedCount")
            //    .Ignore("EmailConfirmed");
        }


        public DbSet<Dashboard> Dashboard { get; set; }
    }
}
