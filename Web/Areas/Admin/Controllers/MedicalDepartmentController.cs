using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.MedicalDepartment;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MedicalDepartmentController : Controller
    {
        private readonly IMedicalDepartmentService _medicalDepartmentService;
        public MedicalDepartmentController(IMedicalDepartmentService medicalDepartmentService)
        {
            _medicalDepartmentService=medicalDepartmentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _medicalDepartmentService.GetAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            var model = new MedicalDepartmentCreateVM();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(MedicalDepartmentCreateVM model)
        {
            var isSucceeded = await _medicalDepartmentService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAsync(int id)
        {
            var model = await _medicalDepartmentService.GetUpdateModelAsync(id);
            if (model != null) return View(model);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(MedicalDepartmentUpdateVM model, int id)
        {
            if (model.Id != id) return BadRequest();
            var isSucceeded = await _medicalDepartmentService.UpdateAsync(model, id);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var isSucceeded = await _medicalDepartmentService.DeleteAsync(id);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return NotFound();
        }

    }
}
