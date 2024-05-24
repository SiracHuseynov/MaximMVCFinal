using Maxim.Business.Exceptions;
using Maxim.Business.Extensions;
using Maxim.Business.Workers.Abstracts;
using Maxim.Core.Models;
using Maxim.Core.RepositoryAbstracts;
using Maxim.Data.RepositoryConcretes;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Maxim.Business.Services.Concretes
{
    public class WorkerService : IWorkerService
    {
        private readonly IWorkerRepository _workerRepository;
        private readonly IWebHostEnvironment _env;
        public WorkerService(IWorkerRepository workerRepository, IWebHostEnvironment env)
        {
            _workerRepository = workerRepository;
            _env = env;
        }

        public async Task AddWorkerAsync(Worker worker)
        {
            if (worker.ImageFile == null)
                throw new ImageFileException("Image olmalidir");

            worker.ImageUrl = Helper.SaveFile(_env.WebRootPath, @"uploads\workers", worker.ImageFile);

            await _workerRepository.AddAsync(worker);
            await _workerRepository.CommitAsync();
        }

        public void DeleteWorker(int id)
        {
            var existWorker = _workerRepository.Get(x => x.Id == id);

            if (existWorker == null)
                throw new EntityNotFoundException("Isci yoxdur!");

            Helper.DeleteFile(_env.WebRootPath, @"uploads\workers", existWorker.ImageUrl);

            _workerRepository.Delete(existWorker);
            _workerRepository.Commit();
        }   

        public List<Worker> GetAllWorkers(Func<Worker, bool>? func = null)
        {
            return _workerRepository.GetAll(func);
        }

        public Task<IPagedList<Worker>> GetPaginatedWorkerAsync(int pageIndex, int pageSize)
        {
            return _workerRepository.GetPaginatedServiceAsync(pageIndex, pageSize); 
        }

        public Worker GetWorker(Func<Worker, bool>? func = null)
        {
            return _workerRepository.Get(func);
        }

        public void UpdateWorker(int id, Worker newWorker)
        {
            var oldWorker = _workerRepository.Get(x => x.Id == id);

            if (oldWorker == null)
                throw new EntityNotFoundException("Isci yoxdur!");

            if (newWorker.ImageFile != null) 
            {
                if (newWorker.ImageFile.ContentType != "image/png") 
                    throw new FileContentTypeException("File png formatinda ola biler!");

                Helper.DeleteFile(_env.WebRootPath, @"uploads\workers", oldWorker.ImageUrl);

                oldWorker.ImageUrl = Helper.SaveFile(_env.WebRootPath, @"uploads\workers", newWorker.ImageFile);
            }

            oldWorker.FullName = newWorker.FullName;
            oldWorker.Specialty = newWorker.Specialty;

            _workerRepository.Commit();
        }
    }
}
