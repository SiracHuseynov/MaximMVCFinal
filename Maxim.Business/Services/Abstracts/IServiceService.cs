using Maxim.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxim.Business.Services.Abstracts
{
	public interface IServiceService 
	{
		Task AddServiceAsync(Service service);
		void DeleteService(int id);
		void UpdateService(int id, Service newService);
		Service GetService(Func<Service, bool>? func = null);
		List<Service> GetAllServices(Func<Service, bool>? func = null);


	}
}
