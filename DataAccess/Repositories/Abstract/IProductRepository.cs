using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
    public interface IProductRepository: IRepository<Product>
    {
        Task<List<Product>> GetAllByCategoryAsync(int id);
        Task<IQueryable<Product>> PaginateProductsAsync(int page, int take,IQueryable<Product> products);
        Task<int> GetPageCountAsync(int take,IQueryable<Product> products);
        IQueryable<Product> FilterByName(string name);
        Task<Product> GetProductAsync(int productId);



        Task<List<Product>> GetAllWithCategoriesAsync();
    }
}
