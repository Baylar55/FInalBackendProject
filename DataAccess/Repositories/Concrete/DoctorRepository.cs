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
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        private readonly AppDbContext _context;

        public DoctorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Doctor>> GetFourDoctorAsync()
        {
            return await _context.Doctor.Take(4).ToListAsync();
        }

        #region Filter Methods

      

        public IQueryable<Doctor> FilterByName(string name)
        {
            return _context.Doctor.Where(d => !string.IsNullOrEmpty(name) ? d.Name.Contains(name) : true);
        }
        #endregion
    }
}
