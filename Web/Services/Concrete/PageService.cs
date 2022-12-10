using Core.Entities;
using DataAccess.Repositories.Abstract;
using Web.Services.Abstract;
using Web.ViewModels.Pages;

namespace Web.Services.Concrete
{
    public class PageService : IPageService
    {
        private readonly IPricingCardRepository _pricingCardRepository;
        private readonly IFAQCategoryRepository _categoryRepository;
        private readonly IQuestionRepository _questionRepository;

        public PageService(IPricingCardRepository pricingCardRepository,
                           IFAQCategoryRepository categoryRepository,
                           IQuestionRepository questionRepository)
        {
            _pricingCardRepository = pricingCardRepository;
            _categoryRepository = categoryRepository;
            _questionRepository = questionRepository;
        }

        public async Task<PagesIndexVM> GetAsync()
        {
            var model = new PagesIndexVM()
            {
             PricingCards= await _pricingCardRepository.GetAllAsync(),
             FAQCategories=await _categoryRepository.GetAllAsync(),
             Questions=await _questionRepository.GetAllAsync(),
            };
            return model;
        }

        public async Task<List<Question>> LoadQuestionsAsync(int id)
        {
            return await _questionRepository.GetAllByCategoryAsync(id);
        }
    }
}
