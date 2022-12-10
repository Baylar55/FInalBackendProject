using Web.Areas.Admin.ViewModels.OurVision;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IOurVisionService
    {
        Task<OurVisionIndexVM> GetAsync();
        Task<OurVisionUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> CreateAsync(OurVisionCreateVM model);
        Task<bool> UpdateAsync(OurVisionUpdateVM model, int id);
        Task<bool> DeleteAsync(int id);
    }
}
