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
    public class AboutUsRepository :Repository<About>, IAboutUsRepository
    {
        private readonly AppDbContext _context;

        public AboutUsRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<About> GetAsync()
        {
            return await _context.About.Include(a=>a.AboutPhotos).FirstOrDefaultAsync();
        }
    }
}
