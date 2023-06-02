using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Online_Course_Portal_DataAccess;
using Online_Course_Portal_DataAccess.Data;
using Online_Course_Portal_DataAccess.IRepository;
using Online_Course_Portal_DataAccess.Model;
using Online_Course_Portal_DataAccess.Model.DTO;
using System.Data;
using System.Net;

namespace Online_Course_Portal_API.Controllers
{
    [Route("api/Course")]
    [ApiController]
    
    
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ApplicationDbContext _applicationDbContext;
        protected ApiResponse _response;
        private readonly IMapper _mapper;
        public CourseController(ICourseRepository courseRepository, ApplicationDbContext applicationDbContext, IMapper mapper)
        {

            _applicationDbContext = applicationDbContext;
            _courseRepository = courseRepository;
            _mapper = mapper;
            _response = new();
        }
        [HttpGet]
        public ActionResult<IEnumerable<Course>> GetCourses()

        {
            try
            {
                IEnumerable<Course> courseList = _courseRepository.GetAll().ToList();
                _response.Result = courseList;



            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);


        }
        [HttpGet("{id:int}", Name = "GetCourse")]

        public ActionResult<ApiResponse> GetCourse(int id)
        {
            try
            {

                if (id == 0)
                {

                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);

                }

                var course = _courseRepository.Get(v => v.Id == id);
                if (course == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);


                }
                _response.Result = course;

                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;

        }
        [HttpPost]
        public ActionResult<ApiResponse> CreateCourse( [FromBody]CourseCreateDTO CourseDTO)
        {
            try
            {
                if (_courseRepository.Get(u => u.courseName.ToLower() == CourseDTO.courseName.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Course already exists!");
                    return BadRequest(ModelState);
                }

                if (CourseDTO == null)
                {
                    return BadRequest();
                }


                Course course = _mapper.Map<Course>(CourseDTO);
                _courseRepository.Add(course);
                _courseRepository.save();



                _response.Result = course;
                _response.StatusCode = HttpStatusCode.OK;
                return CreatedAtRoute("GetCourse", new { id = course.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }
        [HttpPut("{id:int}")]
        public ActionResult<ApiResponse> UpdateCourse(int id, [FromBody] Course course)
        {
            try
            {

                if (course == null || id != course.Id)
                {
                    return BadRequest();
                }


                Course model = course;


                _courseRepository.update(model);
                _courseRepository.save();
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;

        }
        [HttpDelete("{id:int}")]
        public ActionResult<ApiResponse> DeleteCourse(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }

                var course = _courseRepository.Get(u => u.Id == id);
                if (course == null)
                {
                    return NotFound();
                }

                _courseRepository.Remove(course);
                _courseRepository.save();
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;

        }


    }
}
