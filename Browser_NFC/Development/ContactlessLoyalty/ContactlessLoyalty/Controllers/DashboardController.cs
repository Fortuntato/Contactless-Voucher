using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContactlessLoyalty.Data;
using Microsoft.AspNetCore.Identity;

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
            foreach (Dashboard dash in userCards)
            {
                if(dash.User == user)
                {
                    return View(dash);
                }
            }

            return View(await _context.Dashboard.ToListAsync());
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

        public async Task<IActionResult> CreateCard(Dashboard dashboard)
        {
            // Logic for creating a new card

            // Get the user id to store with the new card
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            dashboard.User = user;

            // Everything to be set to 0 except the store name (Campaign)
            dashboard.NumberOfStamps = 0;
            dashboard.NumberOfVouchers = 0;
            dashboard.LastStampDateTime = new DateTime();
            dashboard.StoreName = "Diagon Alley";

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
    }
}
