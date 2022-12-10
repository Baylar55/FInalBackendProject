using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using Microsoft.EntityFrameworkCore;
using Web.Services.Abstract;
using Web.ViewModels.Shop;

namespace Web.Services.Concrete
{
    public class ShopService : IShopService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductRepository _productRepository;

        public ShopService(IProductCategoryRepository productCategoryRepository,
                           IProductRepository productRepository)
        {
            _productCategoryRepository = productCategoryRepository;
            _productRepository = productRepository;
        }
        public async Task<ShopIndexVM> GetAsync(ShopIndexVM model)
        {
            var products =  _productRepository.FilterByName(model.Name);
            var pageCount = await _productRepository.GetPageCountAsync( model.Take,products);
            products = await _productRepository.PaginateProductsAsync(model.Page,model.Take,products);

            model = new ShopIndexVM
            {
                Name= model.Name,
                ProductCategories = await _productCategoryRepository.GetAllAsync(),
                Products =await products.ToListAsync(),
                PageCount = pageCount,
                Page = model.Page
            };
            return model;
        }
        public async Task<List<Product>> LoadProductsAsync(int id)
        {
            return await _productRepository.GetAllByCategoryAsync(id);
        }
    }
}
