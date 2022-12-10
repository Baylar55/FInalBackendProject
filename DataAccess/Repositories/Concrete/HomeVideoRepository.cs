using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
    public class HomeVideoRepository : Repository<HomeVideo>, IHomeVideoRepository
    {
        private readonly AppDbContext context;

        public HomeVideoRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<HomeVideo> GetAsync()
        {
            return await context.HomeVideo.FirstOrDefaultAsync();
        }
    }
}
