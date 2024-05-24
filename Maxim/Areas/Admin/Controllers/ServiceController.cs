using Maxim.Business.Exceptions;
using Maxim.Business.Services.Abstracts;
using Maxim.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maxim.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class ServiceController : Controller
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        //public IActionResult Index()
        //{
        //    var services = _serviceService.GetAllServices();
        //    return View(services);
        //}

        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 2)
        {
            if(pageIndex <= 0 || pageSize <= 0)
            {
				var paginatedServices = await _serviceService.GetPaginatedServiceAsync(pageIndex = 1, pageSize = 2);
				return View(paginatedServices);
			} 

            var paginatedService = await _serviceService.GetPaginatedServiceAsync(pageIndex, pageSize);
            return View(paginatedService);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Service service)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                await _serviceService.AddServiceAsync(service);
            }
            catch(ImageFileException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (FileContentTypeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var existService = _serviceService.GetService(x => x.Id == id);
            if (existService == null)
                return NotFound();
            return View(existService);
        }

        [HttpPost]
        public IActionResult Update(Service service)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _serviceService.UpdateService(service.Id, service);
            }
            catch(EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch(FileeNotFoundException ex)
            {
                return NotFound();
            }
            catch (FileContentTypeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var existService = _serviceService.GetService(x => x.Id == id);
            if (existService == null)
                return NotFound();

            return View(existService);
        }

        [HttpPost]
        public IActionResult DeletePost(int id)
        {
            if (!ModelState.IsValid)
                return View();


            try
            {
                _serviceService.DeleteService(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch (FileeNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Index");
        }


    }
}
