// <copyright company="University Of Westminster">
//     Contactless Loyalty All rights reserved.
// </copyright>
// <author>Shouyi Cui</author>

using ContactlessLoyalty.Data;
using ContactlessLoyalty.Enumeration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ContactlessLoyalty.Controllers
{
    public class CardController : Controller
    {
        private readonly UserManager<AccountContactlessLoyaltyUser> _userManager;
        private readonly SignInManager<AccountContactlessLoyaltyUser> _signInManager;
        private readonly DatabaseContext _context;
        private readonly ILogger<Card> _logger;
        private readonly IConfiguration _configuration;

        public CardController(
            DatabaseContext context,
            UserManager<AccountContactlessLoyaltyUser> userManager,
            SignInManager<AccountContactlessLoyaltyUser> signInManager,
            ILogger<Card> logger,
            IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _configuration = configuration;
        }

        // GET: Dashboards
        public async Task<IActionResult> Index()
        {
            // Scenario where the user has only one card at the moment
            AccountContactlessLoyaltyUser user = await _userManager.GetUserAsync(User);

            List<Card> userCards = await _context.LoyaltyCards.ToListAsync();

            // At the moment is getting only the first card found in the DB. TODO: Check the correspondent card 
            Card dashboard = userCards.Where(x => x.User == user).FirstOrDefault();

            // Maybe worth checking again
            if (dashboard == null)
            {
                return RedirectToAction("ErrorDetails", "Card");
            }

            return View(dashboard);
        }

        // GET: Dashboards/ErrorDetails/
        public async Task<IActionResult> ErrorDetails()
        {
            return View();
        }

        // GET: Dashboards/Create
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> CreateCard()
        {

            // Get the user id to store with the new card
            AccountContactlessLoyaltyUser user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            // Check if the user has already a card
            List<Card> userCards = await _context.LoyaltyCards.Where(x => x.User.Id == user.Id).ToListAsync();

            if (userCards.Count > 0)
            {
                return RedirectToAction("OneCardLimit", "Card");
            }

            // Parameters for creating a new card
            Card newCard = new Card(
                0,                                 // Number of initial vouchers
                new DateTime(2013, 05, 26),        // Initial date
                1,                                 // Number of initial stamps
                "Wembley Stadium Coffee Emporium", // Store name
                _configuration.GetValue<string>("CustomSettings:StoreSchemeCode"), // Number of stamps required for reward
                user);

            _context.Add(newCard);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception error)
            {
                _logger.LogError(error.Message);
            }

            return RedirectToAction("Index", "Card");
        }

        // GET: Card/OneCardLimit
        public async Task<IActionResult> OneCardLimit()
        {
            return View();
        }

        // GET: Card/VoucherSent
        public async Task<IActionResult> VoucherSent()
        {
            return View();
        }

        // GET: Card/Write - This is a hidden page used for writing into the Tag appropriate details
        public async Task<IActionResult> Write()
        {
            return View();
        }

        /// <summary>
        /// Method called when the user attempts to collect a stamp
        /// </summary>
        /// <param name="StoreSchemeCode">Is the scheme code stored in the tag</param>
        /// <param name="TagSR">Is the TAG serial number</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CollectStamp(string StoreSchemeCode, string TagSR)
        {
            // Get the user id to store with the new card
            AccountContactlessLoyaltyUser user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            // Console.WriteLine(StoreSchemeCode); // Value from the tag to be checked

            // Find detail of existing loyalty card of the person
            Card editCard = await _context.LoyaltyCards
                .FirstOrDefaultAsync(m => m.User.Id == user.Id);

            DateTime currentTime = DateTime.Now.ToLocalTime();

            // Check store scheme date. 
            if (editCard.StoreSchemeCode != StoreSchemeCode && "DefaultValueStore" != StoreSchemeCode && "NFC_TagEmpty" != StoreSchemeCode)
            {
                _logger.LogError("Customer attempt to collect stamp with invalid scheme code. Expected: {0} . from tag: {1}", editCard.StoreSchemeCode, StoreSchemeCode);
                ModelState.AddModelError("SchemeInvalid", "Collection attempted with invalid scheme code. " + StoreSchemeCode);
                return View("Index", editCard);
            }

            // Manual check for expected tags - Check for SR. Only if TagSR is expected value, continue.
            if ("04:15:8a:62:81:65:81" != TagSR && "DefaultValueTag" != TagSR && "NFC_EmptySR" != TagSR)
            {
                _logger.LogError("Unexpected tag serial number: " + TagSR);
                ModelState.AddModelError("Invalid_Tag", "Collection attempted with invalid tag: " + TagSR);
                return View("Index", editCard);
            }

            // Check for valid collection rate. Customer can collect depending on the key value in the app settings
            if (isTimeValid(currentTime, editCard.LastStampDateTime, _configuration.GetValue<string>("CustomSettings:CollectionRate")))
            {
                editCard.LastStampDateTime = currentTime;
                editCard.NumberOfStamps++;
            }
            else
            {
                _logger.LogError("Customer attempt to collect stamp on invalid date. Last stamp: {0}. Collection Rate:{1}", editCard.LastStampDateTime, _configuration.GetValue<string>("CustomSettings:CollectionRate"));
                ModelState.AddModelError("RateInvalid", "Collection attempted on invalid day.");
                return View("Index", editCard);
            }


            // The following case is to prevent user from collecting more stamps before collecting the voucher.
            if (editCard.NumberOfStamps > (int)SchemeLimit.WembleyEmporium)
            {
                _logger.LogWarning("User {0} attempt to collect new stamp but reached limit {1}", user.Id, (int)SchemeLimit.WembleyEmporium);
                editCard.NumberOfStamps--; // Remove stamp collected over limit
            }

            _context.Update(editCard);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }

            return RedirectToAction("Index", "Card");
        }

        public async Task<IActionResult> ResetStamp()
        {
            // Get the user id to store with the new card
            AccountContactlessLoyaltyUser user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            // Find detail of existing loyalty card of the person
            Card editDashboard = await _context.LoyaltyCards
                .FirstOrDefaultAsync(m => m.User.Id == user.Id);

            // Get the storeName
            editDashboard.LastStampDateTime = DateTime.Now.ToLocalTime();
            editDashboard.NumberOfStamps = 0;
            editDashboard.NumberOfVouchers++;

            _context.Update(editDashboard);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception error)
            {
                _logger.LogError(error.Message);
            }

            // Make the api call to send out the voucher
            if (ApiVoucherRequest(user.PhoneNumber, editDashboard.StoreSchemeCode, editDashboard.LastStampDateTime))
            {
                _logger.LogError("Voucher SMS unable to be sent. API Returned FAIL. Check phone number{0} and scheme code: {1}", user.PhoneNumber, editDashboard.StoreSchemeCode);
            }

            return RedirectToAction("VoucherSent", "Card");
        }

        public bool ApiVoucherRequest(string phone, string schemeCode, DateTime timeRequest)
        {
            string apiBase = "http://testext.i-movo.com/Api/receivesms.aspx?";

            string apiRequestUrl = string.Format("{0}From={1}&To={2}&Msg={3}&ReceivedTimeStamp={4}&Mode={5}", apiBase, phone, phone, schemeCode, timeRequest, "sync");

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiRequestUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync(apiRequestUrl).Result; // Adding Result make the call to synchronous
                string content = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("API call made for receiveSMS.");
                    return true;
                }
            }

            return false;
        }

        public bool isTimeValid(DateTime currentTime, DateTime lastStampDateTime, string redemptionRate)
        {
            switch (redemptionRate.ToLower())
            {
                case "hourly":
                    if ((currentTime - lastStampDateTime).TotalHours >= 1)
                    {
                        return true;
                    }
                    break;
                case "daily":
                    if ((currentTime - lastStampDateTime).TotalDays >= 1)
                    {
                        return true;
                    }
                    break;
                case "weekly":
                    if ((currentTime - lastStampDateTime).TotalDays >= 7)
                    {
                        return true;
                    }
                    break;
                case "unlimited":
                    return true;
                default:
                    return false;
            }

            return false;
        }
    }
}
