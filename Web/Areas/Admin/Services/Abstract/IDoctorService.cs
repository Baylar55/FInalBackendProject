using Web.Areas.Admin.ViewModels.Doctor;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IDoctorService 
    {
        Task<DoctorIndexVM> GetAsync();
        Task<DoctorUpdateVM> GetUpdateModelAsync(int id);
        Task<DoctorDetailsVM> DetailsAsync(int id);
        Task<bool> CreateAsync(DoctorCreateVM model);
        Task<bool> UpdateAsync(DoctorUpdateVM model, int id);
        Task<bool> DeleteAsync(int id);
    }
}
