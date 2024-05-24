using Maxim.Core.Models;
using Maxim.Core.RepositoryAbstracts;
using Maxim.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxim.Data.RepositoryConcretes
{
	public class ServiceRepository : GenericRepository<Service>, IServiceRepository
	{
		public ServiceRepository(AppDbContext appDbContext) : base(appDbContext)
		{

		}
	}
}

