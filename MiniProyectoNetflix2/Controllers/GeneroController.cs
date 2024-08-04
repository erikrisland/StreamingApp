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
    public class GeneroController : Controller
    {
        private readonly GeneroService _generoService;
        public GeneroController(ApplicationContext dbContext)
        {
			_generoService = new(dbContext);
        }

        public async Task<IActionResult> Index()
        {
            var list = await _generoService.GetAllViewModel();
            return View(list);
        }

        public IActionResult Create()
        {
            return View("SaveGenero", new SaveGeneroViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveGeneroViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveGenero", vm);
            }

            await _generoService.Add(vm);
            return RedirectToRoute(new { controller = "Home", action = "Generos" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View("SaveGenero", await _generoService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveGeneroViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveGenero", vm);
            }

            await _generoService.Update(vm);
            return RedirectToRoute(new { controller = "Home", action = "Generos" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _generoService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {              
            await _generoService.Delete(id);

            return RedirectToRoute(new { controller = "Home", action = "Generos" });
        }
    }
}
