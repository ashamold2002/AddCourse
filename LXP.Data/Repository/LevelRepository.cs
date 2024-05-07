using LXP.Common;
using LXP.Common.Entities;
using LXP.Data.IRepository;
//using LXP.Data.DbContexts;

using LXP.Data.DBContexts;
using Microsoft.EntityFrameworkCore;

namespace LXP.Data.Repository
{
    public class LevelRepository : ILevelRepository
    {
        private readonly LXPDbContext _context;
        public LevelRepository(LXPDbContext context)
        {
            _context = context;

        }
        public async Task<CourseLevel> GetLevel(string level)
        {

                CourseLevel courseLevel = await _context.CourseLevels
                    .FirstOrDefaultAsync(c => c.LevelId.ToString() == level);
                return courseLevel;
            
            


        }

    }
}
