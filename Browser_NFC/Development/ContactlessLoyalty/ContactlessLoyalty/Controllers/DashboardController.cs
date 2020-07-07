using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContactlessLoyalty.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.Http;

namespace ContactlessLoyalty.Controllers
{
    public class DashboardController : Controller
    {
        private readonly UserManager<AccountContactlessLoyaltyUser> _userManager;
        private readonly SignInManager<AccountContactlessLoyaltyUser> _signInManager;
        private readonly DatabaseContext _context;

        public DashboardController(DatabaseContext context, UserManager<AccountContactlessLoyaltyUser> userManager,
            SignInManager<AccountContactlessLoyaltyUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Dashboards
        public async Task<IActionResult> Index()
        {
            // Scenario where the user has only one card at the moment
            var user = await _userManager.GetUserAsync(User);

            List<Dashboard> userCards = await _context.Dashboard.ToListAsync();

            //At the moment is getting only the first card found in the DB. TODO: Check the correspondent card 
            Dashboard dashboard = userCards.Where(x => x.User == user).FirstOrDefault();

            if (dashboard != null) // Maybe worth checking again
            {
                return View(dashboard);
            }

            return RedirectToAction("Details", "Dashboard"); // TODO: Create view page stating there are no card associated with it
        }

        // GET: Dashboards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dashboard = await _context.Dashboard
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dashboard == null)
            {
                return NotFound();
            }

            return View(dashboard);
        }

        // GET: Dashboards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dashboards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumberOfVouchers,LastStampDateTime,NumberOfStamps,StoreName")] Dashboard dashboard)
        {
            if (ModelState.IsValid)
            {
                // Get the user id to store with the new card
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }
                    
                dashboard.User = user;

                _context.Add(dashboard);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception error)
                {
                    Console.WriteLine(error);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dashboard);
        }

        public async Task<IActionResult> CreateCard()
        {
            // Logic for creating a new card
            Dashboard dashboard = new Dashboard(); // Manually creating new dashboard instance

            // Get the user id to store with the new card
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            dashboard.User = user;

            // Everything to be set to 0 except the store name (Campaign)
            dashboard.NumberOfStamps = 1;
            dashboard.NumberOfVouchers = 0;
            dashboard.LastStampDateTime = new DateTime(2013, 05, 26);
            dashboard.StoreName = "Wembley Stadium";
            dashboard.StoreSchemeCode = "PAYIN";

            _context.Add(dashboard);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception error)
                {
                    Console.WriteLine(error);
                }
                return RedirectToAction("Index", "Dashboard");
            
        }

        // GET: Dashboards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dashboard = await _context.Dashboard.FindAsync(id);
            if (dashboard == null)
            {
                return NotFound();
            }
            return View(dashboard);
        }

        // POST: Dashboards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NumberOfVouchers,LastStampDateTime,NumberOfStamps,StoreName")] Dashboard dashboard)
        {
            if (id != dashboard.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dashboard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DashboardExists(dashboard.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dashboard);
        }

        // GET: Dashboards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dashboard = await _context.Dashboard
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dashboard == null)
            {
                return NotFound();
            }

            return View(dashboard);
        }

        // POST: Dashboards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dashboard = await _context.Dashboard.FindAsync(id);
            _context.Dashboard.Remove(dashboard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DashboardExists(int id)
        {
            return _context.Dashboard.Any(e => e.Id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CollectStamp([FromForm] dynamic formValue)
        {

            Console.WriteLine(formValue.uniqueStoreCode);
            // Get the user id to store with the new card
            AccountContactlessLoyaltyUser user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            //Console.WriteLine(dashboardValue.StoreName); // Value from the tag to be checked

            // Find detail of existing loyalty card of the person
            Dashboard editDashboard = await _context.Dashboard
                .FirstOrDefaultAsync(m => m.User.Id == user.Id);

            // Get the storeName
            editDashboard.LastStampDateTime = DateTime.Now.ToLocalTime();
            editDashboard.NumberOfStamps++;

            // The following case should not happen because the button for this feature should be hidden
            if (editDashboard.NumberOfStamps > 10)
            {
                editDashboard.NumberOfVouchers++;
                editDashboard.NumberOfStamps = 0;
            }
            
            _context.Update(editDashboard);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
            return RedirectToAction("Index", "Dashboard");
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
            Dashboard editDashboard = await _context.Dashboard
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
                Console.WriteLine(error);
            }

            //Make the api call to send out the voucher
            ApiVoucherRequest(user.PhoneNumber, editDashboard.StoreName, editDashboard.LastStampDateTime);

            return RedirectToAction("Index", "Dashboard");
        }

        public bool ApiVoucherRequest(string phone, string storeName, DateTime timeRequest)
        {
            string apiBase = "http://testext.i-movo.com/Api/receivesms.aspx?";

            string apiRequestUrl = string.Format("{0}From={1}&To={2}&Msg={3}&ReceivedTimeStamp={4}&Mode={5}", apiBase, phone, phone, storeName, timeRequest, "sync");

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiRequestUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync(apiRequestUrl).Result; // Adding Result make the call to synchronous
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
