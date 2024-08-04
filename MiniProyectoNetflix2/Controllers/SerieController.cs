using Application.Services;
using Application.ViewModels;
using Database;
using Database.Contexts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseFirstExample.Controllers
{
	public class SerieController : Controller
	{
		private readonly SerieService _serieService;
		private readonly ApplicationContext _dbContext;

		public SerieController(ApplicationContext dbContext)
		{
			_dbContext = dbContext;
			_serieService = new SerieService(dbContext);
		}

		public async Task<IActionResult> Index()
        {
            var list = await _serieService.GetAllViewModel();
            return View(list);
        }

		public IActionResult Create()
		{
			var serieViewModel = new SaveSerieViewModel
			{
				GenerosDisponibles = _dbContext.Generos.ToList(),
				ProductorasDisponibles = _dbContext.Productoras.ToList()
			};

			return View("SaveSerie", serieViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Create(SaveSerieViewModel vm)
		{
			if (!ModelState.IsValid)
			{
				vm.GenerosDisponibles = _dbContext.Generos.ToList();
				vm.ProductorasDisponibles = _dbContext.Productoras.ToList();
				return View("SaveSerie", vm);
			}

			await _serieService.Add(vm);
			return RedirectToRoute(new { controller = "Home", action = "Series" });
		}

		public async Task<IActionResult> Edit(int id)
		{
			var serieViewModel = await _serieService.GetByIdSaveViewModel(id);
			return View("SaveSerie", serieViewModel);
		}

		[HttpPost]
        public async Task<IActionResult> Edit(SaveSerieViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveSerie", vm);
            }

            await _serieService.Update(vm);
            return RedirectToRoute(new { controller = "Home", action = "Series" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _serieService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {              
            await _serieService.Delete(id);

            return RedirectToRoute(new { controller = "Home", action = "Series" });
        }

        public async Task<IActionResult> Detalles(int id)
        {
            var serie = await _serieService.GetByIdSaveViewModel(id);
            if (serie == null)
            {
                return NotFound();
            }
            return View(serie);
        }
    }
}
