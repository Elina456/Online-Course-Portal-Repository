using Online_Course_Portal_DataAccess.Model.DTO;
using Online_Course_Portal_DataAccess.Model;

namespace Online_Course_Portal_Web.Service.IService
{
    public interface ICourseBookingService
    {
        Task<IEnumerable<CourseBooking>> GetAllAsync(string token);
        Task<CourseBooking> GetByIdAsync(int id, string token);
        Task<CourseBooking> CreateCourseAsync(CourseBookingDTO createDTO, string token);
        
        Task DeleteCourseAsync(int id, string token);
    }
}
