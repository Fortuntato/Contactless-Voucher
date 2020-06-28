using System;
using ContactlessLoyalty.Areas.Identity.Data;
using ContactlessLoyalty.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(ContactlessLoyalty.Areas.Identity.IdentityHostingStartup))]
namespace ContactlessLoyalty.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<AuthenticationContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AuthenticationContextConnection")));

                services.AddDefaultIdentity<AccountContactlessLoyaltyUser>(
                    options =>
                    {
                        options.SignIn.RequireConfirmedEmail = false;
                        options.SignIn.RequireConfirmedPhoneNumber = false;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequireNonAlphanumeric = false;
                    }
                ).AddEntityFrameworkStores<AuthenticationContext>();
            });
        }
    }
}