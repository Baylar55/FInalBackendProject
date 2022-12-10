
using Web.Areas.Admin.ViewModels.WhyChoose;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IWhyChooseService
    {
        Task<WhyChooseIndexVM> GetAsync();
        Task<WhyChooseUpdateVM> GetUpdateModelAsync();
        Task<WhyChooseDetailsVM> DetailsAsync();
        Task<bool> CreateAsync(WhyChooseCreateVM model);
        Task<bool> UpdateAsync(WhyChooseUpdateVM model);
        Task<bool> DeleteAsync();
    }
}
