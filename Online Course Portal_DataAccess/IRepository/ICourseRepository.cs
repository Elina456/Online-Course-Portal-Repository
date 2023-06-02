using Online_Course_Portal_DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Course_Portal_DataAccess.IRepository
{
    public interface ICourseRepository:IRepository<Course>
    {
        void update(Course course);
    }
}
