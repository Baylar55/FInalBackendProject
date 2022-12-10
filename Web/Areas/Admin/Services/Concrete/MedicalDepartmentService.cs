using Core.Entities;
using Core.Utilities.Helpers;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.MedicalDepartment;

namespace Web.Areas.Admin.Services.Concrete
{
    public class MedicalDepartmentService : IMedicalDepartmentService
    {
        private readonly ModelStateDictionary _modelState;
        private readonly IFileService _fileService;
        private readonly IMedicalDepartmentRepository _medicalDepartmentRepository;
        public MedicalDepartmentService(IActionContextAccessor actionContextAccessor,
                                       IFileService fileService,
                                       IMedicalDepartmentRepository medicalDepartmentRepository)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _fileService = fileService;
            _medicalDepartmentRepository = medicalDepartmentRepository;
        }

        public async Task<MedicalDepartmentIndexVM> GetAsync()
        {
            var model = new MedicalDepartmentIndexVM()
            {
                MedicalDepartments = await _medicalDepartmentRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<MedicalDepartmentUpdateVM> GetUpdateModelAsync(int id)
        {
            var medicalDepartment = await _medicalDepartmentRepository.GetAsync(id);
            if (medicalDepartment == null) return null;
            var model = new MedicalDepartmentUpdateVM()
            {
                Title = medicalDepartment.Title,
                Description = medicalDepartment.Description,
            };
            return model;
        }

        public async Task<bool> CreateAsync(MedicalDepartmentCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            bool isExist = await _medicalDepartmentRepository.AnyAsync(d => d.Title.ToLower().Trim() == model.Title.ToLower().Trim());
            if (isExist)
            {
                _modelState.AddModelError("Title", "This title is already exist");
                return false;
            }
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
            var medicalDepartment = new MedicalDepartment
            {
                Title = model.Title,
                Description = model.Description,
                PhotoName = await _fileService.UploadAsync(model.Photo),
                CreatedAt = DateTime.Now,
            };
            await _medicalDepartmentRepository.CreateAsync(medicalDepartment);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var medicalDepartment = await _medicalDepartmentRepository.GetAsync(id);
            if (medicalDepartment == null) return false;
            _fileService.Delete(medicalDepartment.PhotoName);
            await _medicalDepartmentRepository.DeleteAsync(medicalDepartment);
            return true;
        }

        public async Task<bool> UpdateAsync(MedicalDepartmentUpdateVM model, int id)
        {
            var medicalDepartment = await _medicalDepartmentRepository.GetAsync(id);
            if (medicalDepartment == null) return false;
            if (!_modelState.IsValid) return false;
            if (model == null) return false;
            bool isExist = await _medicalDepartmentRepository.AnyAsync(d => d.Title.ToLower().Trim() == model.Title.ToLower().Trim());
            if (isExist)
            {
                _modelState.AddModelError("Title", "This title is already exist");
                return false;
            }
            medicalDepartment.Title = model.Title;
            medicalDepartment.Description = model.Description;
            medicalDepartment.ModifiedAt = DateTime.Now;

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
                _fileService.Delete(medicalDepartment.PhotoName);
                medicalDepartment.PhotoName = await _fileService.UploadAsync(model.Photo);
            }
            await _medicalDepartmentRepository.UpdateAsync(medicalDepartment);
            return true;
        }
    }
}
