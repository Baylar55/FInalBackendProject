using Core.Entities;
using Core.Utilities.Helpers;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.LastestNews;

namespace Web.Areas.Admin.Services.Concrete
{
    public class LastestNewsService : ILastestNewsService
    {
        private readonly IFileService _fileService;
        private readonly ILastestNewsRepository _lastestNewsRepository;
        private readonly ModelStateDictionary _modelState;

        public LastestNewsService(IFileService fileService,
                                  IActionContextAccessor actionContextAccessor,
                                  ILastestNewsRepository lastestNewsRepository)
        {
            _fileService = fileService;
            _lastestNewsRepository = lastestNewsRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<LastestNewsIndexVM> GetAsync()
        {
            var model = new LastestNewsIndexVM()
            {
                LastestNews = await _lastestNewsRepository.GetAllByLastDateAsync()
            };
            return model;
        }

        public async Task<LastestNewsUpdateVM> GetUpdateModelAsync(int id)
        {
            var lastestNews = await _lastestNewsRepository.GetAsync(id);
            if (lastestNews == null) return null;
            var model = new LastestNewsUpdateVM()
            {
                Title = lastestNews.Title,
                Type = lastestNews.Type,
            };
            return model;
        }

        public async Task<LastestNewsDetailsVM> DetailsAsync(int id)
        {
            var lastestNews = await _lastestNewsRepository.GetAsync(id);
            if (lastestNews == null) return null;
            var model = new LastestNewsDetailsVM()
            {
                Title = lastestNews.Title,
                Type = lastestNews.Type,
                CreatedAt = lastestNews.CreatedAt,
                ModifiedAt = lastestNews.ModifiedAt,
                PhotoName = lastestNews.PhotoName,
            };
            return model;
        }

        public async Task<bool> CreateAsync(LastestNewsCreateVM model)
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

            var lastestNews = new LastestNews
            {
                Title = model.Title,
                Type = model.Type,
                PhotoName = await _fileService.UploadAsync(model.Photo),
                CreatedAt = DateTime.Now,
            };
            await _lastestNewsRepository.CreateAsync(lastestNews);
            return true;
        }

        public async Task<bool> UpdateAsync(LastestNewsUpdateVM model, int id)
        {
            var lastestNews = await _lastestNewsRepository.GetAsync(id);
            if (lastestNews == null) return false;
            if (!_modelState.IsValid) return false;
            if (model == null) return false;
            lastestNews.Title = model.Title;
            lastestNews.Type = model.Type;
            lastestNews.ModifiedAt = DateTime.Now;

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
                _fileService.Delete(lastestNews.PhotoName);
                lastestNews.PhotoName = await _fileService.UploadAsync(model.Photo);
            }
            await _lastestNewsRepository.UpdateAsync(lastestNews);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var lastestNews = await _lastestNewsRepository.GetAsync(id);
            if (lastestNews == null) return false;
            _fileService.Delete(lastestNews.PhotoName);
            await _lastestNewsRepository.DeleteAsync(lastestNews);
            return true;
        }
    }
}
