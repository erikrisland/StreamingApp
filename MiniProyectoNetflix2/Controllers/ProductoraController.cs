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
    public class ProductoraController : Controller
    {
        private readonly ProductoraService _productoraService;
        public ProductoraController(ApplicationContext dbContext)
        {
			_productoraService = new(dbContext);
        }

        public async Task<IActionResult> Index()
        {
            var list = await _productoraService.GetAllViewModel();
            return View(list);
        }

        public IActionResult Create()
        {
            return View("SaveProductora", new SaveProductoraViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveProductoraViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveProductora", vm);
            }

            await _productoraService.Add(vm);
            return RedirectToRoute(new { controller = "Home", action = "Productora" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View("SaveProductora", await _productoraService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveProductoraViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveProductora", vm);
            }

            await _productoraService.Update(vm);
            return RedirectToRoute(new { controller = "Home", action = "Productora" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _productoraService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {              
            await _productoraService.Delete(id);

            return RedirectToRoute(new { controller = "Home", action = "Productora" });
        }


    }
}
