namespace ContactlessLoyalty.Areas.Identity.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Threading.Tasks;
    using ContactlessLoyalty.Models;
    using Microsoft.AspNetCore.Identity;

    // Add profile data for application users by adding properties to the AccountContactlessLoyaltyUser class
    public class LoyaltyCardUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(20)")]
        public string FirstName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(20)")]
        public string LastName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(20)")]
        public string MobilePhoneNumber { get; set; }

        [Column(TypeName = "nvarchar(40)")]
        public string CardID { get; set; }
    }
}
