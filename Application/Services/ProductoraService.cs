using Application.Repository;
using Application.ViewModels;
using Database;
using Database.Contexts;
using Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
	public class ProductoraService
	{
		private readonly ProductoraRepository _productoraRepository;

		public ProductoraService(ApplicationContext dbContext)
		{
			_productoraRepository = new(dbContext);
		}

		public async Task Add(SaveProductoraViewModel vm)
		{
			Productora productora = new();
			productora.Nombre = vm.Nombre;

			await _productoraRepository.AddAsync(productora);
		}

		public async Task Update(SaveProductoraViewModel vm)
		{
			Productora productora = new();
			productora.Id = vm.Id;
			productora.Nombre = vm.Nombre;

			await _productoraRepository.UpdateAsync(productora);
		}

		public async Task Delete(int id)
		{
			var productora = await _productoraRepository.GetByIdAsync(id);
			await _productoraRepository.DeleteAsync(productora);
		}

		public async Task<SaveProductoraViewModel> GetByIdSaveViewModel(int id)
		{
			var productora = await _productoraRepository.GetByIdAsync(id);
			SaveProductoraViewModel vm = new();
			vm.Id = productora.Id;
			vm.Nombre = productora.Nombre;

			return vm;
		}

		public async Task<List<ProductoraViewModel>> GetAllViewModel()
		{
			var list = await _productoraRepository.GetAllAsync();
			return list.Select(p => new ProductoraViewModel
			{
				Id = p.Id,
				Nombre = p.Nombre,

			}).ToList();
		}
	}
}