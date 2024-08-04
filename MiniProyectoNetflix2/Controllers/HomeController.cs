using Application.Services;
using Application.ViewModels;
using Database.Contexts;
using Database.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MiniProyectoNetflix2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductoraService _productoraService;
        private readonly GeneroService _generoService;
		private readonly SerieService _serieService;
		public HomeController(ApplicationContext dbContext)
		{
			_productoraService = new(dbContext);
            _generoService = new(dbContext);
			_serieService = new(dbContext);
		}

        [HttpGet("")]
        [HttpGet("index")]
        public async Task<IActionResult> Index(string searchString, int? productoraFilter, List<int> generoFilter)
        {
            var series = await _serieService.GetAllSeriesAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                series = series.Where(s => s.Nombre.ToLower().Contains(searchString.ToLower())).ToList();
                ViewData["CurrentFilter"] = searchString;
            }

            if (productoraFilter.HasValue)
            {
                series = series.Where(s => s.ProductoraId == productoraFilter.Value).ToList();
            }

            if (generoFilter != null && generoFilter.Any())
            {
                series = series.Where(s => generoFilter.Contains(s.GeneroId) || (s.SubGeneroId.HasValue && generoFilter.Contains(s.SubGeneroId.Value))).ToList();

                if (generoFilter.Count > 1)
                {
                    series = series.Where(s => generoFilter.All(g => g == s.GeneroId || (s.SubGeneroId.HasValue && g == s.SubGeneroId.Value))).ToList();
                }
            }

            var subGeneros = await _generoService.GetAllViewModel();
            var productoras = await _productoraService.GetAllViewModel();
            var generos = await _generoService.GetAllViewModel();

            ViewBag.Productoras = productoras;
            ViewBag.Generos = generos;
            ViewBag.SubGeneros = subGeneros;

            return View(series);
        }

        public async Task<IActionResult> Series()
		{
			return View(await _serieService.GetAllViewModel());
		}

		public async Task<IActionResult> Productora()
		{
			return View(await _productoraService.GetAllViewModel());
		}

        public async Task<IActionResult> Generos()
        {
            return View(await _generoService.GetAllViewModel());
        }
    }
}
