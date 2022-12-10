using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.PricingCard;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PricingCardController : Controller
    {
        private readonly IPricingCardService _pricingCardService;

        public PricingCardController(IPricingCardService pricingCardService) 
        {
            _pricingCardService = pricingCardService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _pricingCardService.GetAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            var model = new PricingCardCreateVM();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(PricingCardCreateVM model)
        {
            var isSucceeded = await _pricingCardService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAsync(int id)
        {
            var model = await _pricingCardService.GetUpdateModelAsync(id);
            if (model != null) return View(model);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(PricingCardUpdateVM model, int id)
        {
            if (model.Id != id) return BadRequest();
            var isSucceeded = await _pricingCardService.UpdateAsync(model, id);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var isSucceeded = await _pricingCardService.DeleteAsync(id);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _pricingCardService.DetailsAsync(id);
            if (model != null) return View(model);
            return NotFound();
        }
    }
}
