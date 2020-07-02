using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactlessLoyaltyWebApp.Models
{
    public class UserModel
    {
        
        public int ID { get; set; }

        public int CardID { get; set; }

        [Required(ErrorMessage = "Please enter a name")]
        public string Name{ get; set; }

        [Required(ErrorMessage = "Please enter a valid Mobile Phone")]
        public string MobilePhone { get; set; }
        
        [Required(ErrorMessage = "Please enter a password")]
        public string Password { get; set; }

        public string UserRole { get; set; }

    }
}
