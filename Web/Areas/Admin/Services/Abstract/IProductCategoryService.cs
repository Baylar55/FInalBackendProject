using Web.Areas.Admin.ViewModels.Product;
using Web.Areas.Admin.ViewModels.ProductCategory;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IProductCategoryService
    {
        Task<ProductCategoryIndexVM> GetAllAsync();
        Task<ProductCategoryUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> CreateAsync(ProductCategoryCreateVM model);
        Task<bool> UpdateAsync(ProductCategoryUpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}
