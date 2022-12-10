using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.ViewModels.Shop;

namespace Web.Services.Abstract
{
    public interface IShopService
    {
        Task<ShopIndexVM> GetAsync(ShopIndexVM model);
        Task<List<Product>> LoadProductsAsync(int id);
    }
}
