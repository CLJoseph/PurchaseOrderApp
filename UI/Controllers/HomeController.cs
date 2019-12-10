using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using UI.Models;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(Secrets S)
        {
            _Test = S;
        }


        Secrets _Test;

        public IActionResult Index()
        {


            ViewData["Env"] = _Test.Enviroment;
            ViewData["Con"] = _Test.DBconnection;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }


        public IActionResult Instructions()
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
