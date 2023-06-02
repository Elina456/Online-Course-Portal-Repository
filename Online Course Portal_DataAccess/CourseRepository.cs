using Online_Course_Portal_DataAccess.Data;
using Online_Course_Portal_DataAccess.IRepository;
using Online_Course_Portal_DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Course_Portal_DataAccess
{
    public class CourseRepository :Repository<Course>,ICourseRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;


        public CourseRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public void update(Course obj)
        {
           _applicationDbContext.Courses.Update(obj);
        }
    }
}
