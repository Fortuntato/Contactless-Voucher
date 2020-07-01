using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LoyaltyCardApi.Models;

namespace LoyaltyCardApi.Data
{
    public class LoyaltyCardApiContext : DbContext
    {
        public LoyaltyCardApiContext (DbContextOptions<LoyaltyCardApiContext> options)
            : base(options)
        {
        }

        public DbSet<LoyaltyCardApi.Models.LoyaltyDetails> Dashboard { get; set; }
    }
}
