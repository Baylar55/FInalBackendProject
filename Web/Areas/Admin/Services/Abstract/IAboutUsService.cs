using Web.Areas.Admin.ViewModels.AboutUs;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IAboutUsService
    {
        Task<AboutUsIndexVM> GetAsync();
        Task<AboutUsUpdateVM> GetUpdateModelAsync();
        Task<AboutUsDetailsVM> DetailsAsync();
        Task<AboutPhotoUpdateVM> GetAboutPhotoUpdateAsync(int id);
        Task<bool> CreateAsync(AboutUsCreateVM model);
        Task<bool> UpdateAsync(AboutUsUpdateVM model);
        Task<bool> DeleteAsync();
        Task<bool> UpdatePhotoAsync(int id, AboutPhotoUpdateVM model);
        Task<bool> DeletePhotoAsync(int id, AboutPhotoDeleteVM model);
    }
}
