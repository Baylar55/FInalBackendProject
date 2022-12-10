using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;

namespace Web.Controllers
{
    public class PagesController : Controller
    {
        private readonly IPageService _pageService;

        public PagesController(IPageService pageService)
        {
            _pageService = pageService;
        }
        public async Task<IActionResult> Faq()
        {
            var model = await _pageService.GetAsync();
            return View(model);
        }
        public async Task<IActionResult> Pricing()
        {
            var model = await _pageService.GetAsync();
            return View(model);
        }
        public async Task<IActionResult> LoadQuestions(int id)
        {
            var questions = await _pageService.LoadQuestionsAsync(id);
            return PartialView("_QuestionPartial", questions);
            //return Json(questions);
        }

    }
}
