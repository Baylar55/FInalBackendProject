using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;
using Web.ViewModels.FindDoctor;

namespace Web.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        public async Task<IActionResult> FindDoctor(FindDoctorIndexVM model)
        {
            return View(await _doctorService.GetAsync(model));
            
        }

        public async Task<IActionResult> SingleDoctor(int id)
        {
            var model = await _doctorService.GetDoctorByIdAsync(id);
            return View(model); 
        }

    }
}
