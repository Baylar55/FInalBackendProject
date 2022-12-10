using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;
using Web.ViewModels.Basket;

namespace Web.Controllers
{
    [Authorize]
    public class BasketController:Controller
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        
        public async Task<IActionResult> Index()
        {
            var model = await _basketService.GetAsync();
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(BasketAddVM model)
        {
            var result = await _basketService.AddAsync(model);
            if (result) return Ok();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(BasketAddVM model)
        {
            var result = await _basketService.DeleteAsync(model.Id);
            if (result) return Ok();
            return View(model);
        }

    }
}
