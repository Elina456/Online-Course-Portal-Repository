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
    public class CourseBookingRepository:Repository<CourseBooking>,ICourseBookingRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;


        public CourseBookingRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public void UpdateCourseBooking(CourseBooking obj)
        {
            _applicationDbContext.CourseBook.Update(obj);
        }
    }
}
