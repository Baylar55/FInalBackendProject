using Web.ViewModels.Doctor;
using Web.ViewModels.FindDoctor;
using Web.ViewModels.Home;

namespace Web.Services.Abstract
{
    public interface IDoctorService
    {
        Task<FindDoctorIndexVM> GetAsync(FindDoctorIndexVM model);
        Task<SingleDoctorIndexVM> GetDoctorByIdAsync(int id);
    }
}
