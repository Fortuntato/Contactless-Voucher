// <copyright company="University Of Westminster">
//     Contactless Loyalty All rights reserved.
// </copyright>
// <author>Shouyi Cui</author>

namespace ContactlessLoyalty.Areas.Identity.Pages.Account.Manage
{
    using ContactlessLoyalty.Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<AccountContactlessLoyaltyUser> _userManager;
        private readonly SignInManager<AccountContactlessLoyaltyUser> _signInManager;
        private readonly DatabaseContext _context;

        public IndexModel(
            DatabaseContext context,
            UserManager<AccountContactlessLoyaltyUser> userManager,
            SignInManager<AccountContactlessLoyaltyUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone Number")]
            //// TOENABLE[RegularExpression("^44[0-9]{9}|07[0-9]{9}$", ErrorMessage = "Please enter a valid UK phone number starting with 44 or 07")] // Commented out for testing purposes
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(AccountContactlessLoyaltyUser user)
        {
            string phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            FirstName = user.FirstName;
            LastName = user.LastName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            AccountContactlessLoyaltyUser user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            AccountContactlessLoyaltyUser user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            // Get the phone number saved from the database and check if change in the DB are needed
            string phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                // Prevent the user from phone number/username to an already existing number/username
                List<AccountContactlessLoyaltyUser> listofUsers = _context.Users.Where(x => x.UserName == Input.PhoneNumber || x.PhoneNumber == Input.PhoneNumber).ToList();
                if (listofUsers.Count > 0)
                {
                    // There is already a user with this number stored
                    StatusMessage = "This number has been used already. If you have forgotten the password, please contact us.";
                    return RedirectToPage();
                }

                // Update the username along with the phone number since username field is checked for login
                IdentityResult setUsername = await _userManager.SetUserNameAsync(user, Input.PhoneNumber);
                IdentityResult setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded || !setUsername.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number and username.";
                    return RedirectToPage();
                }

                await _signInManager.RefreshSignInAsync(user);
                StatusMessage = "Your profile has been updated";
                return RedirectToPage();
            }
            else
            {
                StatusMessage = "It seems you have not changed the phone number";
                return RedirectToPage();
            }
        }
    }
}
