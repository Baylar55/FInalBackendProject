using DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Web.Services.Abstract;
using Web.ViewModels.Doctor;
using Web.ViewModels.FindDoctor;

namespace Web.Services.Concrete
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<FindDoctorIndexVM> GetAsync(FindDoctorIndexVM model)
        {
            var doctors = _doctorRepository.FilterByName(model.DoctorName);
            model = new FindDoctorIndexVM()
            {
                Doctor = await doctors.ToListAsync()
            };
            return model;
        }

        public async Task<SingleDoctorIndexVM> GetDoctorByIdAsync(int id)
        {
            var model = new SingleDoctorIndexVM()
            {
                Doctor = await _doctorRepository.GetAsync(id)
            };
            return model;
        }
    }
}
