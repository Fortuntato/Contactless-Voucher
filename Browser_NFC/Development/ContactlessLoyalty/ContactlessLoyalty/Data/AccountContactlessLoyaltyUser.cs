namespace ContactlessLoyalty.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;

    // Add profile data for application users by adding properties to the AccountContactlessLoyaltyUser class
    public class AccountContactlessLoyaltyUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(20)")]
        public string FirstName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(20)")]
        public string LastName { get; set; }

        //public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// A list of possible cards associated to a customer
        /// </summary>
        public List<Dashboard> CardDashboards { get; set; }
    }
}
