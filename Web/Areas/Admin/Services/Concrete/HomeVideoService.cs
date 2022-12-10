using Core.Entities;
using Core.Utilities.Helpers;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.HomeVideo;

namespace Web.Areas.Admin.Services.Concrete
{
    public class HomeVideoService : IHomeVideoService
    {
        private readonly IFileService _fileService;
        private readonly IHomeVideoRepository _homeVideoRepository;
        private readonly ModelStateDictionary _modelState;

        public HomeVideoService(IFileService fileService, 
                                IHomeVideoRepository homeVideoRepository,
                                IActionContextAccessor actionContextAccessor)
        {
            _fileService = fileService;
            _homeVideoRepository = homeVideoRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<HomeVideoIndexVM> GetAsync()
        {
            var model = new HomeVideoIndexVM()
            {
                HomeVideo = await _homeVideoRepository.GetAsync()
            };
            return model;
        }

        public async Task<HomeVideoUpdateVM> GetUpdateModelAsync()
        {
            var homeVideo = await _homeVideoRepository.GetAsync();
            if (homeVideo == null) return null;
            var model = new HomeVideoUpdateVM()
            {
                Url = homeVideo.VideoUrl,
            };
            return model;
        }

        public async Task<bool> CreateAsync(HomeVideoCreateVM model)
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
            var homeVideo = new HomeVideo
            {
                VideoUrl = model.VideoUrl,
                PhotoName = await _fileService.UploadAsync(model.Photo),
                CreatedAt = DateTime.Now,
            };
            await _homeVideoRepository.CreateAsync(homeVideo);
            return true;
        }

        public async Task<bool> DeleteAsync()
        {
            var ourVision = await _homeVideoRepository.GetAsync();
            if (ourVision == null) return false;
            _fileService.Delete(ourVision.PhotoName);
            await _homeVideoRepository.DeleteAsync(ourVision);
            return true;
        }

        public async Task<bool> UpdateAsync(HomeVideoUpdateVM model)
        {
            var homeVideo = await _homeVideoRepository.GetAsync();
            if (homeVideo == null) return false;
            if (!_modelState.IsValid) return false;
            if (model == null) return false;
            homeVideo.VideoUrl = model.Url;
            homeVideo.ModifiedAt = DateTime.Now;

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
                _fileService.Delete(homeVideo.PhotoName);
                homeVideo.PhotoName = await _fileService.UploadAsync(model.Photo);
            }
            await _homeVideoRepository.UpdateAsync(homeVideo);
            return true;
        }
    }
}
