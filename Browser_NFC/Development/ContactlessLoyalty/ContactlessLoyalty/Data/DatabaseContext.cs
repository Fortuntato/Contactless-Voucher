﻿// <copyright company="University Of Westminster">
//     Contactless Loyalty All rights reserved.
// </copyright>
// <author>Shouyi Cui</author>

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ContactlessLoyalty.Data
{
    /// <summary>
    /// Database context that creates the tables through Entity Core Framework
    /// </summary>
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

        public DbSet<Card> LoyaltyCards { get; set; }
    }
}
