using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Online_Course_Portal_DataAccess.Model;

namespace Online_Course_Portal_Web.Service.IService
{
    public interface IAdminService
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<bool> CourseApproved(int id);
        Task<bool> CourseRejected(int id);
    }
}
