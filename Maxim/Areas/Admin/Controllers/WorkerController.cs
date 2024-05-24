using Maxim.Business.Exceptions;
using Maxim.Business.Workers.Abstracts;
using Maxim.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maxim.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "SuperAdmin")]

	public class WorkerController : Controller
    {
        private readonly IWorkerService _workerService;

        public WorkerController(IWorkerService workerService)
        {
            _workerService = workerService;
        }

        //public IActionResult Index()
        //{
        //    var workers = _workerService.GetAllWorkers();   
        //    return View(workers);
        //}

        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 2)
        {
            if (pageIndex <= 0 || pageSize <= 0)
            {
                var paginatedWorker = await _workerService.GetPaginatedWorkerAsync(pageIndex = 1, pageSize = 2);
                return View(paginatedWorker);
            }

            var paginatedWorkers = await _workerService.GetPaginatedWorkerAsync(pageIndex, pageSize);
            return View(paginatedWorkers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Worker worker)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                await _workerService.AddWorkerAsync(worker);
            }
            catch (ImageFileException ex)
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
            var existWorker = _workerService.GetWorker(x => x.Id == id);

            if (existWorker == null)
                return NotFound();

            return View(existWorker);
        }

        [HttpPost]
        public IActionResult Update(Worker worker)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _workerService.UpdateWorker(worker.Id, worker);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch (FileeNotFoundException ex)
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
            var existWorker = _workerService.GetWorker(x => x.Id == id);

            if (existWorker == null)
                return NotFound();

            return View(existWorker);
        }

        [HttpPost] 
        public IActionResult DeletePost(int id)
        {
            if (!ModelState.IsValid)
                return View();


            try
            {
                _workerService.DeleteWorker(id);
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
