using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LXP.Common;
using Microsoft.EntityFrameworkCore;
using LXP.Common.Entities;
using LXP.Common.ViewModels;
using LXP.Data.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
//using LXP.Data.DbContexts;
using LXP.Data.DBContexts;


namespace LXP.Data.Repository
{
    public class AddCourseRepository : ICourseRepository
    {
        private readonly LXPDbContext _context;

        public AddCourseRepository(LXPDbContext context)
        {
            _context = context;
        }




        public async Task AddCourse(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Course>> getCourse()
        {
            var course = await _context.Courses
                .Include(ld => ld.Catagory)
                .Include(ld => ld.Level)
                .ToListAsync();
            return course;
        }

    }
}
