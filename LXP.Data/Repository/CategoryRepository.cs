using LXP.Common.Entities;
using LXP.Data.DBContexts;
using Microsoft.EntityFrameworkCore;

using LXP.Data.IRepository;
using static Mysqlx.Notice.Warning.Types;


namespace LXP.Data.Repository
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly LXPDbContext _lXPDbContext;
        public CategoryRepository(LXPDbContext lXPDbContext)
        {
            _lXPDbContext = lXPDbContext;
        }
        public async Task AddCategory(CourseCategory category)
        {
            await _lXPDbContext.CourseCategories.AddAsync(category);
            await _lXPDbContext.SaveChangesAsync();
        }
        public async Task<List<CourseCategory>> GetAllCategory()
        {
            return await _lXPDbContext.CourseCategories.ToListAsync();
        }
        public async Task<bool> AnyCategoryByCategoryName(string Category)
        {
            return await _lXPDbContext.CourseCategories.AnyAsync(category => category.Category == Category);
        }
        public async Task<CourseCategory> GetCategory(string category)
        {
            
                CourseCategory courseCategory =await  _lXPDbContext.CourseCategories
                    .FirstOrDefaultAsync(c => c.CatagoryId.ToString() == category);
             return courseCategory;
                
           
        }

    }
}


