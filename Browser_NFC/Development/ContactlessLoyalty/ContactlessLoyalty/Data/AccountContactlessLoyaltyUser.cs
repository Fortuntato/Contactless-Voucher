// <copyright company="University Of Westminster">
//     Contactless Loyalty All rights reserved.
// </copyright>
// <author>Shouyi Cui</author>

namespace ContactlessLoyalty.Data
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    // Add profile data for application users by adding properties to the AccountContactlessLoyaltyUser class
    public class AccountContactlessLoyaltyUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(20)")]
        public string FirstName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(20)")]
        public string LastName { get; set; }

        // public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// A list of possible cards associated to a customer
        /// </summary>
        public List<Card> CardList { get; set; }
    }
}
