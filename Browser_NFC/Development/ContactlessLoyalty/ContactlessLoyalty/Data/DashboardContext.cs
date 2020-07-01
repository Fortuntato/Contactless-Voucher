using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactlessLoyalty.Models;

namespace ContactlessLoyalty.Data
{
    public class DashboardContext : DbContext
    {
        public DashboardContext(DbContextOptions<DashboardContext> options)
            : base(options)
        {
        }

        public DbSet<Dashboard> Dashboard { get; set; }
    }
}
