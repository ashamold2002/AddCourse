using LXP.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXP.Data.IRepository
{
    public interface ICourseRepository
    {
      Task AddCourse(Course course);

      public Task<List<Course>> getCourse();
    }
}
