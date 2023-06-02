using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Course_Portal_DataAccess;
using Online_Course_Portal_DataAccess.Data;
using Online_Course_Portal_DataAccess.IRepository;
using Online_Course_Portal_DataAccess.Model.DTO;
using Online_Course_Portal_DataAccess.Model;
using System.Net;

namespace Online_Course_Portal_API.Controllers
{
    [Route("api/CourseBooking")]
    [ApiController]

    public class CourseBookingController : ControllerBase
    {
        private readonly ICourseBookingRepository _bookingrepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        protected ApiResponse _response;
        public CourseBookingController(ICourseBookingRepository bookingrepository, ICourseRepository courseRepository, ApplicationDbContext db, IMapper mapper)
        {
            _bookingrepository = bookingrepository;
            _courseRepository = courseRepository;
            _db = db;
            _response = new();

            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CourseBooking>> GetCourseBookings()

        {
            try
            {
                IEnumerable<CourseBooking> courseBookingList = _bookingrepository.GetAll(includeProperties: "Course");
                _response.Result = courseBookingList;



            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);


        }
        [HttpGet("{id:int}", Name = "GetCourseBooking")]

        public ActionResult<ApiResponse> GetCourseBooking(int id)
        {
            try
            {

                if (id == 0)
                {

                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);

                }

                var course = _bookingrepository.Get(v => v.Id == id, includeProperties: "Course");
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
        public ActionResult<ApiResponse> CreateCourseBooking([FromBody] CourseBooking CourseBooking)
        {
            try
            {
                var course =_db.Courses.FirstOrDefault(c => c.Id == CourseBooking.CourseId);
                if (course == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);

                }


                if (course.AvailableSeats <= 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var courseBooking = new CourseBooking
                {
                    CourseId = CourseBooking.CourseId,
                    StudentName = CourseBooking.StudentName


                };

                //CourseBooking result = _mapper.Map<CourseBooking>(courseBooking);

                _bookingrepository.Add(courseBooking);
                course.AvailableSeats--;
                _courseRepository.update(course);
                
               
                _courseRepository.save();
                _bookingrepository.save();
                
               



                _response.Result = courseBooking;
                _response.StatusCode = HttpStatusCode.OK;


                return CreatedAtAction("GetCourseBooking", new { id = courseBooking.Id }, _response);
                


                
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }
        
        [HttpDelete("{id:int}")]
        public ActionResult<ApiResponse> DeleteCourseBooking(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }

                var booking = _bookingrepository.Get(u => u.Id == id);
                if (booking == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                   

                _bookingrepository.Remove(booking);
                _bookingrepository.save();

                var course = _db.Courses.FirstOrDefault(u => u.Id == booking.CourseId);
                if( course != null)
                {
                    course.AvailableSeats++;
                    _courseRepository.update(course);
                    _courseRepository.save();
                }

                
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
