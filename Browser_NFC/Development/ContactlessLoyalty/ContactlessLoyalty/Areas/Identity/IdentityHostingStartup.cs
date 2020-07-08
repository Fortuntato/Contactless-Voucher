// <copyright company="University Of Westminster">
//     Contactless Loyalty All rights reserved.
// </copyright>
// <author>Shouyi Cui</author>

using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(ContactlessLoyalty.Areas.Identity.IdentityHostingStartup))]

namespace ContactlessLoyalty.Areas.Identity
{
    using ContactlessLoyalty.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// This is the hosting startup
    /// </summary>
    public class IdentityHostingStartup : IHostingStartup
    {
        /// <summary>
        /// Configuration for the Database services and Web App. Contains rules for User Registration
        /// </summary>
        /// <param name="builder">A builder for WebHost</param>
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<DatabaseContext>(options =>
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
                    }).AddEntityFrameworkStores<DatabaseContext>();
            });
        }
    }
}