using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccess.Repositories.Concrete
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext context;

        public ProductRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<List<Product>> GetAllByCategoryAsync(int id)
        {
            return await context.Product.Where(p => p.CategoryId == id).ToListAsync();
        }

        public async Task<IQueryable<Product>> PaginateProductsAsync(int page, int take, IQueryable<Product> products)
        {
            return products.OrderByDescending(p => p.Id)
                           .Skip((page - 1) * take)
                           .Take(take);

        }

        public async Task<int> GetPageCountAsync(int take,IQueryable<Product> products)
        {
            var pageCount = products.Count();
            return (int)Math.Ceiling((decimal)pageCount / take);
        }

        public IQueryable<Product> FilterByName(string name)
        {
            return context.Product.Include(p => p.Category).Where(p => !string.IsNullOrEmpty(name) ? p.Name.Contains(name) : true);
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            return await context.Product.FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<List<Product>> GetAllWithCategoriesAsync()
        {
            return await context.Product.Include(p => p.Category).ToListAsync();
        }
    }
}
