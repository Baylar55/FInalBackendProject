using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.OurVision;
using Web.Areas.Admin.ViewModels.WhyChoose;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class WhyChooseController : Controller
    {
        private readonly IWhyChooseService _whyChooseService;

        public WhyChooseController(IWhyChooseService whyChooseService)
        {
            _whyChooseService = whyChooseService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _whyChooseService.GetAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            var model = new WhyChooseCreateVM();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(WhyChooseCreateVM model)
        {
            var isSucceeded = await _whyChooseService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAsync()
        {
            var model = await _whyChooseService.GetUpdateModelAsync();
            if (model != null) return View(model);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(WhyChooseUpdateVM model, int id)
        {
            if (model.Id != id) return BadRequest();
            var isSucceeded = await _whyChooseService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync()
        {
            var isSucceeded = await _whyChooseService.DeleteAsync();
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var model = await _whyChooseService.DetailsAsync();
            if (model != null) return View(model);
            return NotFound();
        }
    }
}
