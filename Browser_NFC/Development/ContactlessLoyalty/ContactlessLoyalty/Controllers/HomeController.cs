namespace ContactlessLoyalty.Controllers
{
    using ContactlessLoyalty.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Create", "Card");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "This is Contactless Loyalty.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Feel free to contact me by email for further information.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
