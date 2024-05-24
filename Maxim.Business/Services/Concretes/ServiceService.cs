using Maxim.Business.Exceptions;
using Maxim.Business.Extensions;
using Maxim.Business.Services.Abstracts;
using Maxim.Core.Models;
using Maxim.Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxim.Business.Services.Concretes
{
	public class ServiceService : IServiceService
	{
		private readonly IServiceRepository _serviceRepository;
		private readonly IWebHostEnvironment _env;
        public ServiceService(IServiceRepository serviceRepository, IWebHostEnvironment env)
        {
            _serviceRepository = serviceRepository;
            _env = env;
        }

        public async Task AddServiceAsync(Service service)
		{
			if (service.ImageFile == null)
				throw new ImageFileException("Image olmalidir!");

			service.ImageUrl = Helper.SaveFile(_env.WebRootPath, @"uploads\services", service.ImageFile);

			await _serviceRepository.AddAsync(service);
			await _serviceRepository.CommitAsync();
		}

		public void DeleteService(int id)
		{
			var existService = _serviceRepository.Get(x => x.Id == id);

			if (existService == null)
				throw new EntityNotFoundException("Service tapilmadi!");

			Helper.DeleteFile(_env.WebRootPath, @"uploads\services", existService.ImageUrl);

			_serviceRepository.Delete(existService);
			_serviceRepository.Commit();
		}

		public List<Service> GetAllServices(Func<Service, bool>? func = null)
		{
			return _serviceRepository.GetAll(func);
		}

		public Service GetService(Func<Service, bool>? func = null)
		{
			return _serviceRepository.Get(func);
		}

		public void UpdateService(int id, Service newService)
		{
			var oldService = _serviceRepository.Get(x => x.Id == id);

			if (oldService == null)
				throw new EntityNotFoundException("Service yoxdur!");

			if(newService.ImageFile != null)
			{
				if (newService.ImageFile.ContentType != "image/png")
					throw new FileContentTypeException("File png formatinda ola biler!");

				Helper.DeleteFile(_env.WebRootPath, @"uploads\services", oldService.ImageUrl);

				oldService.ImageUrl = Helper.SaveFile(_env.WebRootPath, @"uploads\services", newService.ImageFile);
			} 

			oldService.Title = newService.Title;	
			oldService.Description = newService.Description;

			_serviceRepository.Commit();
		}
	}
}
