using Core.Entities;
using Core.Utilities.Helpers;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.AboutUs;

namespace Web.Areas.Admin.Services.Concrete
{
    [Area("Admin")]
    public class AboutUsService : IAboutUsService
    {
        private readonly IAboutUsRepository _aboutUsRepository;
        private readonly IAboutPhotoRepository _aboutPhotoRepository;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public AboutUsService(IAboutUsRepository aboutUsRepository,
                              IAboutPhotoRepository aboutPhotoRepository,
                              IActionContextAccessor actionContextAccessor,
                              IFileService fileService)
                              
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _aboutPhotoRepository = aboutPhotoRepository;
            _aboutUsRepository = aboutUsRepository;
            _fileService = fileService;
        }

        #region About Crud

        public async Task<AboutUsIndexVM> GetAsync()
        {
            var model = new AboutUsIndexVM()
            {
                About = await _aboutUsRepository.GetAsync()
            };
            return model;
        }

        public async Task<AboutUsUpdateVM> GetUpdateModelAsync()
        {
            var about = await _aboutUsRepository.GetAsync();
            if (about == null) return null;
            var model = new AboutUsUpdateVM()
            {
                Title= about.Title,
                SubTitle = about.SubTitle,
                Content = about.Content,
                Description= about.Description,
                AboutPhotos=about.AboutPhotos
            };
            return model;
        }

        public async Task<AboutUsDetailsVM> DetailsAsync()
        {
            var about = await _aboutUsRepository.GetAsync();
            if (about == null) return null;
            var model = new AboutUsDetailsVM()
            {
                Title = about.Title,
                SubTitle = about.SubTitle,
                Content = about.Content,
                Description = about.Description,
                SignaturePhotoName = about.SignaturePhotoName,
                AboutPhotos = about.AboutPhotos,
                CreatedAt = about.CreatedAt,
                ModifiedAt = about.ModifiedAt,
            };
            return model;
        }

        public async Task<bool> CreateAsync(AboutUsCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            if (!_fileService.IsImage(model.SignaturePhoto))
            {
                _modelState.AddModelError("Photo", "Photo should be in image format");
                return false;
            }
            else if (!_fileService.CheckSize(model.SignaturePhoto, 400))
            {
                _modelState.AddModelError("Photo", $"Photo's size should be smaller than 400kb");
                return false;
            }
            var about = new About
            {
                Title= model.Title,
                SubTitle = model.SubTitle,
                Content= model.Content,
                Description= model.Description,
                CreatedAt = DateTime.Now,
                SignaturePhotoName = await _fileService.UploadAsync(model.SignaturePhoto)
            };
            await _aboutUsRepository.CreateAsync(about);

            bool hasError = false;
            foreach (var photo in model.Photos)
            {
                if (!_fileService.IsImage(photo))
                {
                    _modelState.AddModelError("Photo", $"{photo.FileName} should be in image format");
                    hasError = true;
                }
                else if (!_fileService.CheckSize(photo, 400))
                {
                    _modelState.AddModelError("Photo", $"{photo.FileName}'s size sould be smaller than 400kb");
                    hasError = true;
                }
            }
            if (hasError) return hasError;

            int order = 1;
            foreach (var photo in model.Photos)
            {
                var aboutPhoto = new AboutPhoto
                {
                    AboutId = about.Id,
                    Name = await _fileService.UploadAsync(photo),
                    Order = order++,
                    CreatedAt = DateTime.Now,
                };
                await _aboutPhotoRepository.CreateAsync(aboutPhoto);
            }

            return true;
        }

        public async Task<bool> DeleteAsync()
        {
            var about = await _aboutUsRepository.GetAsync();
            if (about == null) return false;
            _fileService.Delete(about.SignaturePhotoName);
            foreach (var item in about.AboutPhotos )
            {
                _fileService.Delete(item.Name);
            }
            await _aboutUsRepository.DeleteAsync(about);
            return true;
        }

        public async Task<bool> UpdateAsync(AboutUsUpdateVM model)
        {
            var about = await _aboutUsRepository.GetAsync();
            if (about == null) return false;
            if (!_modelState.IsValid) return false;
            if (model == null) return false;

            about.Title = model.Title;
            about.SubTitle = model.SubTitle;
            about.Content = model.Content;
            about.Description = model.Description;
            about.ModifiedAt = DateTime.Now;
            bool hasError = false;
            if (model.Photos != null)
            {
                foreach (var photo in model.Photos)
                {
                    if (!_fileService.IsImage(photo))
                    {
                        _modelState.AddModelError("Photos", $"{photo.Name} must be image");
                        hasError = true;
                    }
                    else if (!_fileService.CheckSize(photo, 400))
                    {
                        _modelState.AddModelError("Photos", $"{photo.Name} size must be less than 400kb");
                        hasError = true;
                    }

                    int order = 1;
                    var aboutPhoto = new AboutPhoto
                    {
                        Name = await _fileService.UploadAsync(photo),
                        Order = order,
                        AboutId = about.Id
                    };
                    order++;
                    await _aboutPhotoRepository.UpdateAsync(aboutPhoto);
                }
            }
            await _aboutUsRepository.UpdateAsync(about);
            return true;
        }


#endregion

        #region AboutPhoto Crud

        public async Task<AboutPhotoUpdateVM> GetAboutPhotoUpdateAsync(int id)
        {
            var aboutPhoto = await _aboutPhotoRepository.GetAsync(id);
            if (aboutPhoto == null) return null;
            var model = new AboutPhotoUpdateVM
            {
                Id = aboutPhoto.Id,
                Order = aboutPhoto.Order,
            };
            return model;
        }

        public async Task<bool> UpdatePhotoAsync(int id, AboutPhotoUpdateVM model)
        {
            if (!_modelState.IsValid) return false;
            var aboutPhoto = await _aboutPhotoRepository.GetAsync(id);
            if (aboutPhoto == null) return false;
            aboutPhoto.Order = model.Order;
            model.AboutId = aboutPhoto.AboutId;
            await _aboutPhotoRepository.UpdateAsync(aboutPhoto);
            return true;
        }

        public async Task<bool> DeletePhotoAsync(int id, AboutPhotoDeleteVM model)
        {
            var aboutPhoto = await _aboutPhotoRepository.GetAsync(id);
            if (aboutPhoto == null) return false;
            _fileService.Delete(aboutPhoto.Name);
            model.AboutId = aboutPhoto.AboutId;
            await _aboutPhotoRepository.DeleteAsync(aboutPhoto);
            return true;
        }

        #endregion
    }
}
