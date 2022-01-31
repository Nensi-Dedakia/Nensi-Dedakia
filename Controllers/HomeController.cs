using Helperland.Models;
using Helperland.Models.Data;
using Helperland.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        private readonly HelperlandContext _helperlandContext;

        public HomeController(HelperlandContext helperlandContext)
        {
            _helperlandContext = helperlandContext;
        }
        public IActionResult contact()
        {

            ContactU contacts = new ContactU();
            return View(contacts);
        }
        [HttpPost]
        public IActionResult contact(ContactU contacts)
        {
            _helperlandContext.ContactUs.Add(contacts);

            _helperlandContext.SaveChanges();

            return RedirectToAction("contact");

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult faq()
        {
            return View();
        }
        public IActionResult price()
        {
            return View();
        }
        //public IActionResult contact()
        //{
        //    return View();
        //}
        public IActionResult aboutus()
        {
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
