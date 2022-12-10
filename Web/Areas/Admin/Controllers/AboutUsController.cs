using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.AboutUs;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = " Admin ")]
    public class AboutUsController : Controller
    {
        private readonly IAboutUsService _aboutUsService;

        public AboutUsController(IAboutUsService aboutUsService)
        {
            _aboutUsService = aboutUsService;
        }

        #region About

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _aboutUsService.GetAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            var model = new AboutUsCreateVM();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(AboutUsCreateVM model)
        {
            var isSucceeded = await _aboutUsService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAsync(int id)
        {
            var model = await _aboutUsService.GetUpdateModelAsync();
            if (model != null) return View(model);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(AboutUsUpdateVM model, int id)
        {
            if (model.Id != id) return BadRequest();
            var isSucceeded = await _aboutUsService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var isSucceeded = await _aboutUsService.DeleteAsync();
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return NotFound();
        }

        public async Task<IActionResult> Details()
        {
            var model = await _aboutUsService.DetailsAsync();
            if (model != null) return View(model);
            return NotFound();
        }

        #endregion 

        #region AboutPhoto

        [HttpGet]
        public async Task<IActionResult> UpdatePhoto(int id)
        {
            var model = await _aboutUsService.GetAboutPhotoUpdateAsync(id);
            if (model != null) return View(model);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePhoto(int id, AboutPhotoUpdateVM model)
        {
            if (id != model.Id) return BadRequest();
            var isSucceeded = await _aboutUsService.UpdatePhotoAsync(id, model);
            if (!isSucceeded) return NotFound();
            return RedirectToAction("update", "aboutus", new { id = model.AboutId });
        }

        [HttpPost]
        public async Task<IActionResult> DeletePhoto(int id, AboutPhotoDeleteVM model)
        {
            var isSucceeded = await _aboutUsService.DeletePhotoAsync(id, model);
            if (!isSucceeded) return NotFound();
            return RedirectToAction("update", "aboutus", new { id = model.AboutId });
        }

        #endregion
    }
}
