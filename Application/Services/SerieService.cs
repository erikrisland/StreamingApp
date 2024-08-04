using Application.Repository;
using Application.ViewModels;
using Database;
using Database.Contexts;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
	public class SerieService
	{
		private readonly SerieRepository _serieRepository;
		private readonly ApplicationContext _dbContext;

		public SerieService(ApplicationContext dbContext)
		{
			_dbContext = dbContext;
			_serieRepository = new SerieRepository(dbContext);
		}

		public async Task Add(SaveSerieViewModel vm)
		{
			Serie serie = new();
			serie.Nombre = vm.Nombre;
			serie.ImagenUrl = vm.ImagenUrl;
			serie.LinkVideo = vm.LinkVideo;
			serie.GeneroId = vm.GeneroId;
			serie.SubGeneroId = vm.SubGeneroId;
			serie.ProductoraId = vm.ProductoraId;

			await _serieRepository.AddAsync(serie);
		}

		public async Task Update(SaveSerieViewModel vm)
		{
			Serie serie = new();
			serie.Id = vm.Id;
			serie.Nombre = vm.Nombre;
			serie.ImagenUrl = vm.ImagenUrl;
			serie.LinkVideo = vm.LinkVideo;
			serie.GeneroId = vm.GeneroId;
			serie.SubGeneroId = vm.SubGeneroId;
			serie.ProductoraId = vm.ProductoraId;

			await _serieRepository.UpdateAsync(serie);
		}

		public async Task Delete(int id)
		{
			var serie = await _serieRepository.GetByIdAsync(id);
			await _serieRepository.DeleteAsync(serie);
		}

		public async Task<SaveSerieViewModel> GetByIdSaveViewModel(int id)
		{
			var serie = await _serieRepository.GetByIdAsync(id);
			var generos = await _dbContext.Generos.ToListAsync();
			var productoras = await _dbContext.Productoras.ToListAsync();

			SaveSerieViewModel vm = new()
			{
				Id = serie.Id,
				Nombre = serie.Nombre,
				ImagenUrl = serie.ImagenUrl,
				LinkVideo = serie.LinkVideo,
				GeneroId = serie.GeneroId,
				SubGeneroId = serie.SubGeneroId,
				ProductoraId = serie.ProductoraId,
				GenerosDisponibles = generos,
				ProductorasDisponibles = productoras
			};

			return vm;
		}


        public async Task<List<SaveSerieViewModel>> GetAllViewModel()
        {
            var generosDisponibles = await _dbContext.Generos.ToListAsync();
            var productorasDisponibles = await _dbContext.Productoras.ToListAsync();

            var series = await _dbContext.Series
                .Include(s => s.Genero)
                .Include(s => s.SubGenero)
                .Include(s => s.Productora)
                .ToListAsync();

            return series.Select(serie => new SaveSerieViewModel
            {
                Id = serie.Id,
                Nombre = serie.Nombre,
                ImagenUrl = serie.ImagenUrl,
                LinkVideo = serie.LinkVideo,
                GeneroId = serie.GeneroId,
                SubGeneroId = serie.SubGeneroId,
                ProductoraId = serie.ProductoraId,
                GenerosDisponibles = generosDisponibles,
                ProductorasDisponibles = productorasDisponibles
            }).ToList();
        }

        public async Task<List<SaveSerieViewModel>> GetAllSeriesAsync()
        {
            var series = await _dbContext.Series.ToListAsync();
            var generos = await _dbContext.Generos.ToListAsync();
            var productoras = await _dbContext.Productoras.ToListAsync();

            var seriesViewModels = series.Select(s => new SaveSerieViewModel
            {
                Id = s.Id,
                Nombre = s.Nombre,
                ImagenUrl = s.ImagenUrl,
                LinkVideo = s.LinkVideo,
                GeneroId = s.GeneroId,
                SubGeneroId = s.SubGeneroId,
                ProductoraId = s.ProductoraId,
                GenerosDisponibles = generos,
                ProductorasDisponibles = productoras
            }).ToList();

            return seriesViewModels;
        }


    }



}