using Online_Course_Portal_DataAccess.Model;
using Online_Course_Portal_DataAccess.Model.DTO;
using System.Reflection.Metadata;

namespace Online_Course_Portal_Web.Service.IService
{
    public interface ICourseService
    {
        Task <IEnumerable<Course>> GetAllAsync(string token);
        Task<Course> GetByIdAsync(int id,string token);
        Task<Course> CreateCourseAsync(CourseCreateDTO createDTO, string token );
        Task<Course> UpdateCourseAsync(int id, Course course,string token);
        Task DeleteCourseAsync(int id, string token);
    }
}
