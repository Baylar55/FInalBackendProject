using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
    public class BasketRepository : Repository<Basket>, IBasketRepository
    {
        private readonly AppDbContext context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public BasketRepository(AppDbContext context,
                                IHttpContextAccessor httpContextAccessor,
                                UserManager<User> userManager) : base(context)
        {
            this.context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        //public async Task<Basket> GetBasketAsync(ClaimsPrincipal claimsPrincipal)
        //{
        //    var user = await _userManager.GetUserAsync(claimsPrincipal);
        //    if (user == null) return null;
        //    return await context.Baskets
        //                           .Include(b => b.BasketProducts)
        //                           .ThenInclude(bp => bp.Product)
        //                           .FirstOrDefaultAsync(b => b.UserId == user.Id);

        //}

        //public async Task<Basket> GetBasketWithProductsAsync()
        //{
        //    return await context.Baskets
        //                    .Include(b => b.BasketProducts)
        //                    .ThenInclude(bp => bp.Product)
        //                    .FirstOrDefaultAsync(b => b.UserId == _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //}

        public async Task<Basket> GetBasketWithProductsAsync(string userId)
        {
            var basket = await context.Baskets
                .Include(b => b.BasketProducts)
                .ThenInclude(bp => bp.Product)
                .FirstOrDefaultAsync(b => b.UserId == userId);
            return basket;
        }
    }
}
