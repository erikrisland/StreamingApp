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
	public class GeneroService
	{
		private readonly GeneroRepository _generoRepository;

		public GeneroService(ApplicationContext dbContext)
		{
            _generoRepository = new(dbContext);
		}

		public async Task Add(SaveGeneroViewModel vm)
		{
            Genero genero = new();
            genero.Nombre = vm.Nombre;

			await _generoRepository.AddAsync(genero);
		}

		public async Task Update(SaveGeneroViewModel vm)
		{
			Genero genero = new();
            genero.Id = vm.Id;
            genero.Nombre = vm.Nombre;

			await _generoRepository.UpdateAsync(genero);
		}

		public async Task Delete(int id)
		{
			var genero = await _generoRepository.GetByIdAsync(id);
			await _generoRepository.DeleteAsync(genero);
		}

		public async Task<SaveGeneroViewModel> GetByIdSaveViewModel(int id)
		{
			var genero = await _generoRepository.GetByIdAsync(id);
            SaveGeneroViewModel vm = new();
			vm.Id = genero.Id;
			vm.Nombre = genero.Nombre;

			return vm;
		}

		public async Task<List<GeneroViewModel>> GetAllViewModel()
		{
			var list = await _generoRepository.GetAllAsync();
			return list.Select(g => new GeneroViewModel
            {
				Id = g.Id,
				Nombre = g.Nombre,

			}).ToList();
		}
	}
}