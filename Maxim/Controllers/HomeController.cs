using Maxim.Business.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Maxim.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceService _serviceService;

		public HomeController(IServiceService serviceService)
		{
			_serviceService = serviceService;
		}

		public IActionResult Index()
        {
            var services = _serviceService.GetAllServices();
            return View(services);
        }

    }
}
