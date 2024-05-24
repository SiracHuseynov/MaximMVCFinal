using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Maxim.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

    }
}
