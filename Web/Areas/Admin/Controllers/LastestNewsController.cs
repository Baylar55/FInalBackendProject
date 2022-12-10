using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.Services.Concrete;
using Web.Areas.Admin.ViewModels.LastestNews;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class LastestNewsController : Controller
    {
        private readonly ILastestNewsService _lastestNewsService;

        public LastestNewsController(ILastestNewsService lastestNewsService)
        {
            _lastestNewsService = lastestNewsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _lastestNewsService.GetAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            var model = new LastestNewsCreateVM();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(LastestNewsCreateVM model)
        {
            var isSucceeded = await _lastestNewsService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAsync(int id)
        {
            var model = await _lastestNewsService.GetUpdateModelAsync(id);
            if (model != null) return View(model);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(LastestNewsUpdateVM model, int id)
        {
            if (model.Id != id) return BadRequest();
            var isSucceeded = await _lastestNewsService.UpdateAsync(model, id);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var isSucceeded = await _lastestNewsService.DeleteAsync(id);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _lastestNewsService.DetailsAsync(id);
            if (model != null) return View(model);
            return NotFound();
        }
    }
}
