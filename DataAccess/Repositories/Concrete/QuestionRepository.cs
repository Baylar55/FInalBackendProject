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
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        private readonly AppDbContext context;

        public QuestionRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<List<Question>> GetAllByCategoryAsync(int id)
        {
            return await context.Question.Where(q=>q.FAQCategoryId== id).ToListAsync();
        }






        public async Task<List<Question>> GetAllWithCategoriesAsync()
        {
            return await context.Question
                            .Include(q => q.FAQCategory)
                            .ToListAsync();
        }
    }
}
