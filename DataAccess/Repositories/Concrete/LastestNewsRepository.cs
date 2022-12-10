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
    public class LastestNewsRepository : Repository<LastestNews>, ILastestNewsRepository
    {
        private readonly AppDbContext context;

        public LastestNewsRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<List<LastestNews>> GetAllByLastDateAsync()
        {
            return await context.LastestNews
                .OrderByDescending(ln => ln.CreatedAt)
                .ToListAsync();
        }
    }
}
