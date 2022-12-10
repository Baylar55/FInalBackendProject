using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;
using Web.Services.Concrete;

namespace Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _departmentService.GetAsync();
            return View(model);
        }
    }
}
