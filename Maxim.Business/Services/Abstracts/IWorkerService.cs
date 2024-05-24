using Maxim.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Maxim.Business.Workers.Abstracts
{
    public interface IWorkerService
    {
        Task AddWorkerAsync(Worker Worker);  
        void DeleteWorker(int id);
        void UpdateWorker(int id, Worker newWorker);
        Worker GetWorker(Func<Worker, bool>? func = null);
        List<Worker> GetAllWorkers(Func<Worker, bool>? func = null);
        Task<IPagedList<Worker>> GetPaginatedWorkerAsync(int pageIndex, int pageSize);
    }
}
