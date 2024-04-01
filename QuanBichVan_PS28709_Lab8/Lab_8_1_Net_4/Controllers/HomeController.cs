using Microsoft.AspNetCore.Mvc;

namespace Lab_8_1_Net_4.Controllers
{
    public class HomeController: Controller
    {
        [Route("/")]
        [Route("/Home")]
        [Route("/Home/Index")]
        public IActionResult Index()
        {
            HttpContext.Session.SetString("name", "Quan Bich Van");
            HttpContext.Session.SetString("email", "quanbichvan1@gmail.com");
            return View();
        }

        [Route("/Home/About")]
        public IActionResult About()
        {
            ViewBag.Name = HttpContext.Session.GetString("name");
            ViewBag.Email = HttpContext.Session.GetString("email");

            ViewData["Message"] = "Your about page, please refresh page after one minune";
            ViewData["Title"] = "Demo Sesstion Login";

            return View();
        }
    }
}
