using ContactlessLoyaltyWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactlessLoyaltyWebApp.Data
{
    public class LoyaltyDatabaseContext : DbContext
    {
        public LoyaltyDatabaseContext(DbContextOptions<LoyaltyDatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<LoyaltyCardModel> LoyaltyCards { get; set; }
    }
}
