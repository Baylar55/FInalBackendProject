using Core.Entities;
using Core.Utilities.Helpers;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.WhyChoose;

namespace Web.Areas.Admin.Services.Concrete
{
    public class WhyChooseService : IWhyChooseService
    {
        private readonly IWhyChooseRepository _whyChooseRepository;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public WhyChooseService(IWhyChooseRepository whyChooseRepository,
                                IFileService fileService, 
                                IActionContextAccessor actionContextAccessor)
        {
            _whyChooseRepository = whyChooseRepository;
            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<WhyChooseIndexVM> GetAsync()
        {
            var model = new WhyChooseIndexVM()
            {
                WhyChoose = await _whyChooseRepository.GetAsync()
            };
            return model;
        }

        public async Task<WhyChooseUpdateVM> GetUpdateModelAsync()
        {
            var dbwhyChoose = await _whyChooseRepository.GetAsync();
            if (dbwhyChoose == null) return null;
            var model = new WhyChooseUpdateVM()
            {
                Title = dbwhyChoose.Title,
                Description = dbwhyChoose.Description,
                Check = dbwhyChoose.Check,
            };
            return model;
        }

        public async Task<WhyChooseDetailsVM> DetailsAsync()
        {
            var whyChoose = await _whyChooseRepository.GetAsync();
            if (whyChoose == null) return null;
            var model = new WhyChooseDetailsVM()
            {
                Title = whyChoose.Title,
                Description = whyChoose.Description,
                CreatedAt = whyChoose.CreatedAt,
                ModifiedAt = whyChoose.ModifiedAt,
                PhotoName = whyChoose.PhotoName,
                Check = whyChoose.Check,
            };
            return model;
        }

        public async Task<bool> CreateAsync(WhyChooseCreateVM model)
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
            var whyChoose = new WhyChoose
            {
                Title = model.Title,
                Description = model.Description,
                Check = model.Check,
                PhotoName = await _fileService.UploadAsync(model.Photo),
                CreatedAt = DateTime.Now,
            };
            await _whyChooseRepository.CreateAsync(whyChoose);
            return true;
        }

        public async Task<bool> DeleteAsync()
        {
            var whyChoose = await _whyChooseRepository.GetAsync();
            if (whyChoose == null) return false;
            _fileService.Delete(whyChoose.PhotoName);
            await _whyChooseRepository.DeleteAsync(whyChoose);
            return true;
        }

        public async Task<bool> UpdateAsync(WhyChooseUpdateVM model)
        {
            var whyChoose = await _whyChooseRepository.GetAsync();
            if (whyChoose == null) return false;
            if (!_modelState.IsValid) return false;
            if (model == null) return false;
            whyChoose.Title = model.Title;
            whyChoose.Description = model.Description;
            whyChoose.ModifiedAt = DateTime.Now;
            whyChoose.Check = model.Check;
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
                _fileService.Delete(whyChoose.PhotoName);
                whyChoose.PhotoName = await _fileService.UploadAsync(model.Photo);
            }
            await _whyChooseRepository.UpdateAsync(whyChoose);
            return true;
        }

        
    }
}
