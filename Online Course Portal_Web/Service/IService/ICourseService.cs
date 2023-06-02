using Online_Course_Portal_DataAccess.Model;
using Online_Course_Portal_DataAccess.Model.DTO;
using System.Reflection.Metadata;

namespace Online_Course_Portal_Web.Service.IService
{
    public interface ICourseService
    {
        Task <IEnumerable<Course>> GetAllAsync();
        Task<Course> GetByIdAsync(int id);
        Task<Course> CreateCourseAsync(CourseCreateDTO createDTO );
        Task<Course> UpdateCourseAsync(int id, Course course);
        Task DeleteCourseAsync(int id);
    }
}
