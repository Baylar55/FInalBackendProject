using Web.Areas.Admin.ViewModels.HomeVideo;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IHomeVideoService
    {
        Task<HomeVideoIndexVM> GetAsync();
        Task<HomeVideoUpdateVM> GetUpdateModelAsync();
        Task<bool> CreateAsync(HomeVideoCreateVM model);
        Task<bool> UpdateAsync(HomeVideoUpdateVM model);
        Task<bool> DeleteAsync();
    }
}
