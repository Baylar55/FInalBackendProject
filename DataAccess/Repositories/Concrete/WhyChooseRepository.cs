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
    public class WhyChooseRepository : Repository<WhyChoose>, IWhyChooseRepository
    {
        private readonly AppDbContext _context;

        public WhyChooseRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<WhyChoose> GetAsync()
        {
            return await _context.WhyChoose.FirstOrDefaultAsync();
        }
    }
}
