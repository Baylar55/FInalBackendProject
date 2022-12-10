
using Web.Areas.Admin.ViewModels.MedicalDepartment;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IMedicalDepartmentService
    {
        Task<MedicalDepartmentIndexVM> GetAsync();
        Task<MedicalDepartmentUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> CreateAsync(MedicalDepartmentCreateVM model);
        Task<bool> UpdateAsync(MedicalDepartmentUpdateVM model, int id);
        Task<bool> DeleteAsync(int id);
    }
}
