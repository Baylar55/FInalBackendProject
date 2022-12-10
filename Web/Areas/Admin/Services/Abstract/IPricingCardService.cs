using Web.Areas.Admin.ViewModels.PricingCard;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IPricingCardService
    {
        Task<PricingCardIndexVM> GetAsync();
        Task<PricingCardUpdateVM> GetUpdateModelAsync(int id);
        Task<PricingCardDetailsVM> DetailsAsync(int id);
        Task<bool> CreateAsync(PricingCardCreateVM model);
        Task<bool> UpdateAsync(PricingCardUpdateVM model, int id);
        Task<bool> DeleteAsync(int id);
    }
}
