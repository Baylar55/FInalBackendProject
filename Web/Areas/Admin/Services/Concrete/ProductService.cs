using Core.Entities;
using Core.Utilities.Helpers;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Product;

namespace Web.Areas.Admin.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileService _fileService;
        private readonly IProductCategoryRepository _categoryRepository;
        private readonly ModelStateDictionary _modelState;

        public ProductService(IProductRepository productRepository,
                              IActionContextAccessor actionContextAccessor,
                              IFileService fileService,
                              IProductCategoryRepository categoryRepository)

        {
            _productRepository = productRepository;
            _fileService = fileService;
            _categoryRepository = categoryRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<ProductIndexVM> GetAllAsync()
        {
            var model = new ProductIndexVM()
            {
                Products = await _productRepository.GetAllWithCategoriesAsync()
            };
            return model;
        }

        public async Task<ProductCreateVM> GetCreateModelAsync()
        {
            var category = await _categoryRepository.GetAllAsync();
            var model = new ProductCreateVM
            {
                Categories = category.Select(c => new SelectListItem
                {
                    Text = c.Title,
                    Value = c.Id.ToString()
                }).ToList()
            };
            return model;
        }

        public async Task<ProductUpdateVM> GetUpdateModelAsync(int id)
        {
            var category = await _categoryRepository.GetAllAsync();

            var product = await _productRepository.GetAsync(id);

            if (product == null) return null;

            var model = new ProductUpdateVM
            {
                Name = product.Name,
                CategoryId = product.CategoryId,
                Price = product.Price,
                Categories = category.Select(c => new SelectListItem
                {
                    Text = c.Title,
                    Value = c.Id.ToString()
                }).ToList()

            };
            return model;
        }

        public async Task<bool> CreateAsync(ProductCreateVM model)
        {
            var category = await _categoryRepository.GetAllAsync();
            model.Categories = category.Select(c => new SelectListItem
            {
                Text = c.Title,
                Value = c.Id.ToString()
            }).ToList();

            bool isExist = await _productRepository.AnyAsync(p => p.Name.ToLower().Trim() == model.Name.ToLower().Trim());
            if (isExist)
            {
                _modelState.AddModelError("Name", "This name is already exist");
                return false;
            }

            if (await _categoryRepository.GetAsync(model.CategoryId) == null)
            {
                _modelState.AddModelError("CategoryId", "This category isn't exist");
                return false;
            }
            if (model.Photo != null)
            {
                if (!_fileService.IsImage(model.Photo))
                {
                    _modelState.AddModelError("Photo", $"{model.Photo.FileName} should be in image format");
                    return false;
                }
                else if (!_fileService.CheckSize(model.Photo, 400))
                {
                    _modelState.AddModelError("Photo", $"{model.Photo.FileName}'s size sould be smaller than 400kb");
                    return false;
                }
            }

            var product = new Product
            {
                Name = model.Name,
                CategoryId = model.CategoryId,
                Price = model.Price,
                CreatedAt = DateTime.Now,
                PhotoName = await _fileService.UploadAsync(model.Photo)
            };
            await _productRepository.CreateAsync(product);
            return true;
        }
        
        public async Task<bool> UpdateAsync(ProductUpdateVM model)
        {
            var category = await _categoryRepository.GetAllAsync();
            var dbProduct = await _productRepository.GetAsync(model.Id);
            bool isExist = await _productRepository.AnyAsync(p => p.Name.ToLower().Trim() == model.Name.ToLower().Trim() && p.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "this title is already exist");
                return false;
            }
            model.Categories = category.Select(c => new SelectListItem
            {
                Text = c.Title,
                Value = c.Id.ToString()
            }).ToList();
            if (!_modelState.IsValid) return false;
            if (dbProduct == null) return false;
            dbProduct.Name = model.Name;
            dbProduct.CategoryId = model.CategoryId;
            dbProduct.Price = model.Price;
            if (model.Photo != null)
            {
                if (!_fileService.IsImage(model.Photo))
                {
                    _modelState.AddModelError("Photo", $"{model.Photo.FileName} should be in image format");
                }
                else if (!_fileService.CheckSize(model.Photo, 400))
                {
                    _modelState.AddModelError("Photo", $"{model.Photo.FileName}'s size sould be smaller than 400kb");
                }
                _fileService.Delete(dbProduct.PhotoName);
                dbProduct.PhotoName = await _fileService.UploadAsync(model.Photo);
            }

            await _productRepository.UpdateAsync(dbProduct);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _productRepository.GetAsync(id);
            if (product != null)
            {
                await _productRepository.DeleteAsync(product);
                _fileService.Delete(product.PhotoName);
                return true;
            }
            return false;
        }

    }
}
