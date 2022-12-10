using Web.ViewModels.Basket;

namespace Web.Services.Abstract
{
    public interface IBasketService
    {
        Task<bool> AddAsync(BasketAddVM model);
        Task<bool> DeleteAsync(int id);
        Task<BasketIndexVM> GetAsync();
    }
}
