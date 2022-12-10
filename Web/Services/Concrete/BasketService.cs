using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using Web.Services.Abstract;
using Web.ViewModels.Basket;
using Web.ViewModels.BasketProduct;

namespace Web.Services.Concrete
{
    public class BasketService : IBasketService
    {
        private readonly IProductRepository _productRepository;
        private readonly IBasketRepository _basketRepository;
        private readonly IBasketProductRepository _basketProductRepository;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ModelStateDictionary _modelState;

        public BasketService(IProductRepository productRepository,
                             IBasketRepository basketRepository,
                             IBasketProductRepository basketProductRepository,
                             IActionContextAccessor actionContextAccessor,
                             UserManager<User> userManager,
                             IHttpContextAccessor httpContextAccessor)
        {
            _productRepository = productRepository;
            _basketRepository = basketRepository;
            _basketProductRepository = basketProductRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<BasketIndexVM> GetAsync()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user == null) return null;
            var basket = await _basketRepository.GetBasketWithProductsAsync(user.Id);
            var model = new BasketIndexVM();
            if (basket != null)
            {
                foreach (var basketProduct in basket.BasketProducts)
                {
                    var basketProducts = new BasketProductVM
                    {
                        Id = basketProduct.ProductId,
                        PhotoName = basketProduct.Product.PhotoName,
                        Price = basketProduct.Product.Price,
                        Quantity = basketProduct.Quantity,
                        Title = basketProduct.Product.Name,
                    };
                    model.BasketProducts.Add(basketProducts);
                }
            }
            else
            {
                basket = new Basket();
            }
            return model;
        }


        public async Task<bool> AddAsync(BasketAddVM model)
        {
            if (!_modelState.IsValid) return false;

            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user == null) return false;

            var product = await _productRepository.GetAsync(model.Id);
            if (product == null) return false;

            var basket = await _basketRepository.GetBasketWithProductsAsync(user.Id);
            if (basket == null)
            {
                basket = new Basket
                {
                    UserId = user.Id,
                };
                await _basketRepository.CreateAsync(basket);
            };
            var basketProduct = await _basketProductRepository.GetBasketProductAsync(product.Id, basket.Id);
            if (basketProduct != null)
            {
                basketProduct.Quantity++;
                await _basketProductRepository.UpdateAsync(basketProduct);
            }
            else
            {
                basketProduct = new BasketProduct
                {
                    ProductId = product.Id,
                    Quantity = 1,
                    BasketId = basket.Id
                };
                await _basketProductRepository.CreateAsync(basketProduct);
            }
            return true;
        }



        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user == null) return false;

            var basketProduct = await _basketProductRepository.GetBasketProductByUserIdAsync(id, user.Id);
            if (basketProduct == null) return false;

            var product = await _productRepository.GetProductAsync(basketProduct.ProductId);
            if (product == null) return false;

            await _basketProductRepository.DeleteAsync(basketProduct);

            return true;
        }
    }
}
