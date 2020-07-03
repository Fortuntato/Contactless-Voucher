using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ContactlessLoyalty.Data
{
    public class LoyaltyUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(20)")]
        public string FirstName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(20)")]
        public string LastName { get; set; }

        //[PersonalData]
        //[Column(TypeName = "nvarchar(20)")]
        //public string MobilePhoneNumber { get; set; }

    }
}
