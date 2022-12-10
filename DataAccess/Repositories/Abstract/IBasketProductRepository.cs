using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
    public interface IBasketProductRepository:IRepository<BasketProduct>
    {
        Task<BasketProduct> GetBasketProductAsync(int productId, int basketId);
        Task<BasketProduct> GetBasketProductByUserIdAsync(int Id, string userId);
    }
}
