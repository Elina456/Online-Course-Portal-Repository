using Online_Course_Portal_DataAccess.Model.DTO;
using Online_Course_Portal_DataAccess.Model;

namespace Online_Course_Portal_Web.Service.IService
{
    public interface ICourseBookingService
    {
        Task<IEnumerable<CourseBooking>> GetAllAsync();
        Task<CourseBooking> GetByIdAsync(int id);
        Task<CourseBooking> CreateCourseAsync(CourseBookingDTO createDTO);
        
        Task DeleteCourseAsync(int id);
    }
}
