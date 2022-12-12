using Core.Entities;
using Core.Utilities.Helpers;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Doctor;

namespace Web.Areas.Admin.Services.Concrete
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly ModelStateDictionary _modelState;
        private readonly IFileService _fileService;
        public DoctorService(IActionContextAccessor actionContextAccessor,
                                       IFileService fileService,
                                       IDoctorRepository doctorRepository)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _fileService = fileService;
            _doctorRepository = doctorRepository;
        }

        public async Task<DoctorIndexVM> GetAsync()
        {
            var model = new DoctorIndexVM()
            {
                Doctor = await _doctorRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<DoctorUpdateVM> GetUpdateModelAsync(int id)
        {
            var doctor = await _doctorRepository.GetAsync(id);
            if (doctor == null) return null;
            var model = new DoctorUpdateVM()
            {
                Name = doctor.Name,
                SubTitle = doctor.SubTitle,
                Qualification = doctor.Qualification,
                Skill = doctor.Skill,
                Specialty = doctor.Speciality,
                ContactInfo = doctor.ContactInfo,
                IntroductingSubTitle = doctor.IntroductingSubTitle,
                IntroductingText = doctor.IntroductingText,
                WorkingTime = doctor.WorkingTime,
                ShowInHome= doctor.ShowInHome,
            };
            return model;
        }

        public async Task<DoctorDetailsVM> DetailsAsync(int id)
        {
            var doctor = await _doctorRepository.GetAsync(id);
            if (doctor == null) return null;
            var model = new DoctorDetailsVM()
            {
                Name = doctor.Name,
                SubTitle = doctor.SubTitle,
                Qualification = doctor.Qualification,
                Skill = doctor.Skill,
                Specialty = doctor.Speciality,
                ContactInfo = doctor.ContactInfo,
                IntroductingSubTitle = doctor.IntroductingSubTitle,
                IntroductingText = doctor.IntroductingText,
                WorkingTime = doctor.WorkingTime,
                CreatedAt = doctor.CreatedAt,
                ModifiedAt = doctor.ModifiedAt,
                PhotoName = doctor.PhotoName,
            };
            return model;
        }

        public async Task<bool> CreateAsync(DoctorCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            if (!_fileService.IsImage(model.Photo))
            {
                _modelState.AddModelError("Photo", "Photo should be in image format");
                return false;
            }
            else if (!_fileService.CheckSize(model.Photo, 400))
            {
                _modelState.AddModelError("Photo", $"Photo's size should be smaller than 400kb");
                return false;
            }
            var doctor = new Doctor
            {
                Name = model.Name,
                SubTitle = model.SubTitle,
                Qualification = model.Qualification,
                Skill = model.Skill,
                ContactInfo = model.ContactInfo,
                IntroductingSubTitle = model.IntroductingSubTitle,
                IntroductingText = model.IntroductingText,
                WorkingTime = model.WorkingTime,
                CreatedAt = DateTime.Now,
                PhotoName = await _fileService.UploadAsync(model.Photo),
                Speciality = model.Specialty,
                ShowInHome=model.ShowInHome,
            };
            await _doctorRepository.CreateAsync(doctor);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var doctor = await _doctorRepository.GetAsync(id);
            if (doctor == null) return false;
            _fileService.Delete(doctor.PhotoName);
            await _doctorRepository.DeleteAsync(doctor);
            return true;
        }

        public async Task<bool> UpdateAsync(DoctorUpdateVM model, int id)
        {
            var doctor = await _doctorRepository.GetAsync(id);
            if (doctor == null) return false;
            if (!_modelState.IsValid) return false;
            if (model == null) return false;

            doctor.Name = model.Name;
            doctor.SubTitle = model.SubTitle;
            doctor.Qualification = model.Qualification;
            doctor.Skill = model.Skill;
            doctor.Speciality = model.Specialty;
            doctor.ContactInfo = model.ContactInfo;
            doctor.IntroductingSubTitle = model.IntroductingSubTitle;
            doctor.IntroductingText = model.IntroductingText;
            doctor.WorkingTime = model.WorkingTime;
            doctor.ModifiedAt = DateTime.Now;
            doctor.ShowInHome=model.ShowInHome;
            if (model.Photo != null)
            {
                if (!_fileService.IsImage(model.Photo))
                {
                    _modelState.AddModelError("Photo", "Photo should be in image format");
                    return false;
                }
                else if (!_fileService.CheckSize(model.Photo, 400))
                {
                    _modelState.AddModelError("Photo", $"Photo's size should be smaller than 400kb");
                    return false;
                }
                _fileService.Delete(doctor.PhotoName);
                doctor.PhotoName = await _fileService.UploadAsync(model.Photo);
            }
            await _doctorRepository.UpdateAsync(doctor);
            return true;
        }
    }
}
