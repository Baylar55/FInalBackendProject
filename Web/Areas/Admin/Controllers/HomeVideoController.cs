using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.HomeVideo;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeVideoController : Controller
    {
        private readonly IHomeVideoService _homeVideoService;

        public HomeVideoController(IHomeVideoService homeVideoService)
        {
            _homeVideoService = homeVideoService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _homeVideoService.GetAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            var model = new HomeVideoCreateVM();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAsync()
        {
            var model = await _homeVideoService.GetUpdateModelAsync();
            if (model != null) return View(model);
            return NotFound();
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateAsync(HomeVideoCreateVM model)
        {
            var isSucceeded = await _homeVideoService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(HomeVideoUpdateVM model, int id)
        {
            if (model.Id != id) return BadRequest();
            var isSucceeded = await _homeVideoService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync()
        {
            var isSucceeded = await _homeVideoService.DeleteAsync();
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return NotFound();
        }
    }
}
