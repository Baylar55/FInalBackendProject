using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;

namespace DataAccess.Repositories.Concrete
{
    public class AboutPhotoRepository : Repository<AboutPhoto>, IAboutPhotoRepository
    {
        public AboutPhotoRepository(AppDbContext context) : base(context)
        {
        }
    }
}
