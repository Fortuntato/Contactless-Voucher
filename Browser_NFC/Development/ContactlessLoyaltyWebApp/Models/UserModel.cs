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
        [Key]
        public int ID { get; set; }

        public LoyaltyCardModel LoyaltyCard { get; set; }

        [MaxLength(20)]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter a name")]
        public string Name{ get; set; }

        [MaxLength(15)]
        [Display(Name = "Mobile Phone")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Please enter a valid Mobile Phone")]
        public string MobilePhone { get; set; }

        [MaxLength(100)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter a password")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "You must enter a longer password")]
        public string Password { get; set; }

        //[Display(Name = "Confirm Password")]
        //[DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "Your password and confirm password do not match")]
        //public string ConfirmPassword { get; set; }

        //[Display(Name = "User Role")]
        //public string UserRole { get; set; }

    }
}
