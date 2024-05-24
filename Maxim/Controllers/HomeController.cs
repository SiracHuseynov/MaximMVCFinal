using Maxim.Business.Services.Abstracts;
using Maxim.Business.Workers.Abstracts;
using Maxim.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Maxim.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceService _serviceService;
        private readonly IWorkerService _workerService;
        public HomeController(IServiceService serviceService, IWorkerService workerService)
        {
            _serviceService = serviceService;
            _workerService = workerService;
        }

        public IActionResult Index()
        {
            HomeVm home = new HomeVm()
            {
                Services = _serviceService.GetAllServices(),
                Workers = _workerService.GetAllWorkers()
            };

            return View(home);
        }

    }
}
