using Maxim.Core.Models;
using Maxim.Core.RepositoryAbstracts;
using Maxim.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Maxim.Data.RepositoryConcretes
{
    public class WorkerRepository : GenericRepository<Worker>, IWorkerRepository
    {
        private readonly AppDbContext _appDbContext;
        public WorkerRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IPagedList<Worker>> GetPaginatedServiceAsync(int pageIndex, int pageSize)
        {
            var query = _appDbContext.Workers.ToPagedListAsync(pageIndex, pageSize);
            return await query;

        }
    }
}
