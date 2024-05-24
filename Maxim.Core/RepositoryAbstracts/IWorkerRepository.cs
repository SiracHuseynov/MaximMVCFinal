using Maxim.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Maxim.Core.RepositoryAbstracts
{
    public interface IWorkerRepository : IGenericRepository<Worker>
    {
        Task<IPagedList<Worker>> GetPaginatedServiceAsync(int pageIndex, int pageSize);

    }
}
