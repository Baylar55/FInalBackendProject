using Core.Entities;
using Web.ViewModels.Pages;

namespace Web.Services.Abstract
{
    public interface IPageService
    {
        Task<PagesIndexVM> GetAsync();
        Task<List<Question>> LoadQuestionsAsync(int id);
    }
}
