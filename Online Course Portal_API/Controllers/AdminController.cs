using Microsoft.AspNetCore.Mvc;
using Online_Course_Portal_DataAccess.IRepository;
using Online_Course_Portal_DataAccess.Model;
using System.Reflection.Metadata;

namespace Online_Course_Portal_API.Controllers
{
    [Route("api/Admin/[action]")]
    [ApiController]
    public class AdminController : Controller
    {
       
        
            private readonly ICourseRepository _courseRepository;
            public AdminController(ICourseRepository courseRepository)
            {
                _courseRepository = courseRepository;

            }
            [HttpGet]
            public IActionResult GetAllCourses()
            {
                IEnumerable<Course> course = _courseRepository.GetAll(b => b.IsApproved == false && b.IsRejected == false);
                if (course == null)
                {
                    return NotFound();

                }
                else
                {
                    return Ok(course);
                }

            }
            [HttpPut]
            public IActionResult CourseApproved(int id)
            {
                if (id == 0)
                {
                    return BadRequest();
                }

                Course course = _courseRepository.Get(b => b.Id == id);
                if (course == null)
                {
                    return NotFound();

                }
                course.IsApproved = true;
                _courseRepository.save();
                return Ok(course);
            }
            [HttpPut]
            public IActionResult CourseRejected(int id)
            {
                if (id == 0)
                {
                    return BadRequest();
                }

                Course course = _courseRepository.Get(b => b.Id == id);
                if (course== null)
                {
                    return NotFound();

                }
                course.IsRejected = true;
            _courseRepository.save();
                return Ok(course);
            }
        }
}
