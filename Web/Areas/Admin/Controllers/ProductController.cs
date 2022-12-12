using Core.Utilities.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.Services.Concrete;
using Web.Areas.Admin.ViewModels.Product;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IFileService _fileservice;
        private readonly IProductCategoryService _categoryService;
        public ProductController(IProductService productService, IFileService fileservice,
             IProductCategoryService categoryService)
        {
            _productService = productService;
            _fileservice = fileservice;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _productService.GetAllAsync();
            return View(model);
        }
        
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = await _productService.GetCreateModelAsync();
            return View(model);
        }
       
         [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _productService.GetUpdateModelAsync(id);
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM model)
        {
            var isSucceded = await _productService.CreateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index));
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Update(int id, ProductUpdateVM model)
        {
            if (model.Id != id) return BadRequest();
            bool isSucceded = await _productService.UpdateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var isSucceeded = await _productService.DeleteAsync(id);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return NotFound();
        }
    }
}
