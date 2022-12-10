using Core.Entities;
using Core.Utilities.Helpers;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.OurVision;

namespace Web.Areas.Admin.Services.Concrete
{
    public class OurVisionService : IOurVisionService
    {
        private readonly ModelStateDictionary _modelState;
        private readonly IOurVisionRepository _ourVisionRepository;
        private readonly IFileService _fileService;

        public OurVisionService(IActionContextAccessor actionContextAccessor, 
                                IOurVisionRepository ourVisionRepository,  
                                IFileService fileService)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _ourVisionRepository = ourVisionRepository;
            _fileService = fileService;
        }

        public async Task<OurVisionIndexVM> GetAsync()
        {
            var model = new OurVisionIndexVM()
            {
                OurVisions = await _ourVisionRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<OurVisionUpdateVM> GetUpdateModelAsync(int id)
        {
            var dbOurVision = await _ourVisionRepository.GetAsync(id);
            if (dbOurVision == null) return null;
            var model = new OurVisionUpdateVM()
            {
                Title = dbOurVision.Title,
                Description = dbOurVision.Description,
            };
            return model;   
        }

        public async Task<bool> CreateAsync(OurVisionCreateVM model)
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
            var ourVision = new OurVision
            {
                Title = model.Title,
                Description = model.Description,
                PhotoName = await _fileService.UploadAsync(model.Photo),
                CreatedAt = DateTime.Now,
            };
            await _ourVisionRepository.CreateAsync(ourVision);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ourVision = await _ourVisionRepository.GetAsync(id);
            if (ourVision == null) return false;
            _fileService.Delete(ourVision.PhotoName);
            await _ourVisionRepository.DeleteAsync(ourVision);
            return true;
        }

        public async Task<bool> UpdateAsync(OurVisionUpdateVM model, int id)
        {
            var ourVision = await _ourVisionRepository.GetAsync(id);
            if (ourVision == null) return false;
            if (!_modelState.IsValid) return false;
            if (model == null) return false;
            ourVision.Title = model.Title;
            ourVision.Description = model.Description;
            ourVision.ModifiedAt = DateTime.Now;

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
                _fileService.Delete(ourVision.PhotoName);
                ourVision.PhotoName = await _fileService.UploadAsync(model.Photo);
            }
            await _ourVisionRepository.UpdateAsync(ourVision);
            return true;
        }
    }
}
