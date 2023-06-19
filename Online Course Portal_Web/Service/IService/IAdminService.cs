using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Online_Course_Portal_DataAccess.Model;

namespace Online_Course_Portal_Web.Service.IService
{
    public interface IAdminService
    {
        Task<IEnumerable<Course>> GetAllAsync(string token);
        Task<IEnumerable<Course>> GetAllApproveCourseAsync(string token);

        Task<bool> CourseApproved(int id,string token);
        Task<bool> CourseRejected(int id, string token);
    }
}
