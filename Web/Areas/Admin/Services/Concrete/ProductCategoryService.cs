using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.ProductCategory;

namespace Web.Areas.Admin.Services.Concrete
{
    public class ProductCategoryService:IProductCategoryService
    {
        private readonly IProductCategoryRepository _categoryRepository;
        private readonly ModelStateDictionary _modelState;

        public ProductCategoryService(IProductCategoryRepository categoryRepository,
                                      IActionContextAccessor actionContextAccessor)
        {
            _categoryRepository = categoryRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<ProductCategoryIndexVM> GetAllAsync()
        {
            var model = new ProductCategoryIndexVM
            {
                ProductCategories = await _categoryRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<ProductCategoryUpdateVM> GetUpdateModelAsync(int id)
        {
            var category = await _categoryRepository.GetAsync(id);
            if (category == null) return null;
            var model = new ProductCategoryUpdateVM
            {
                Id = category.Id,
                Title = category.Title
            };
            return model;
        }

        public async Task<bool> CreateAsync(ProductCategoryCreateVM model)
        {
            bool isExist = await _categoryRepository.AnyAsync(c => c.Title.ToLower().Trim() == model.Title.ToLower().Trim());
            if (isExist)
            {
                _modelState.AddModelError("Title", "this title is already exist");
                return false;
            }
            var category = new ProductCategory
            {
                Title = model.Title,
                CreatedAt = DateTime.Now,
            };
            await _categoryRepository.CreateAsync(category);
            return true;
        }

        public async Task<bool> UpdateAsync(ProductCategoryUpdateVM model)
        {
            var category = await _categoryRepository.GetAsync(model.Id);
            if (category != null)
            {
                bool isExist = await _categoryRepository.AnyAsync(c => c.Title.ToLower().Trim() == model.Title.ToLower().Trim() && c.Id!=model.Id);
                if (isExist)
                {
                    _modelState.AddModelError("Title", "this title is already exist");
                    return false;
                }
                category.Title = model.Title;
                category.ModifiedAt = DateTime.Now;

                await _categoryRepository.UpdateAsync(category);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetAsync(id);
            if (category != null)
            {
                await _categoryRepository.DeleteAsync(category);
                return true;
            }
            return false;
        }

    }
}
