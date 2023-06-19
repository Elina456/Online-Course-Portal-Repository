using Microsoft.AspNetCore.Mvc;
using Online_Course_Portal_DataAccess.Model.DTO;
using Online_Course_Portal_DataAccess.Model;
using Online_Course_Portal_Web.Service.IService;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Globalization;
using System.Security.AccessControl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Online_Course_Portal_DataAccess.Model.ViewModel;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Online_Course_Portal_DataAccess.Data;

namespace Online_Course_Portal_Web.Controllers
{
    public class CourseBookingController : Controller
    {
        private readonly ICourseBookingService _courseBookingService;
        private readonly ICourseService _courseService;
        private readonly IAdminService _adminService;
        private readonly IHttpContextAccessor _contextAccessor;
       
        
        
        public CourseBookingController(ICourseBookingService courseBookingService, ICourseService courseService,IHttpContextAccessor contextAccessor,IAdminService adminService)
        {

            _courseBookingService = courseBookingService;
            _courseService = courseService;
            _contextAccessor = contextAccessor;
            _adminService = adminService;
        }


        public async Task<IActionResult> Index()
        {
            var course = await _courseBookingService.GetAllAsync(_contextAccessor.HttpContext.Session.GetString("JWTToken"));
            
            var booking = _courseService.GetAllAsync(_contextAccessor.HttpContext.Session.GetString("JWTToken"));
           // var bokings = _adminService.GetAllApproveCourseAsync(_contextAccessor.HttpContext.Session.GetString("JWTToken"));
            ViewBag.booking = booking;


            return View(course);
        }
        
        public async Task<IActionResult> CreateBooking()
        {
            //var result = await _courseService.GetByIdAsync(id, HttpContext.Session.GetString("JWTToken"));

            //ViewData["a"] = result.AvailableSeats;
            //ViewData["b"] = result.courseName;
            //ViewData["c"] = result.EndDate.ToString("yyyy-MM-dd");

            //ViewData["d"] = result.StartDate.ToString("yyyy-MM-dd");
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateBooking(CourseBooking course)
        //{
        //    //if (ModelState.IsValid)
        //    //{
        //        var result = await _courseBookingService.CreateCourseAsync(course);
        //        TempData["success"] = "Course Enrolled Successfully";
        //        return RedirectToAction(nameof(Index), new { id = result.Id });
        //   // }

        //    return View(course);
        //}

        [HttpPost]
        public async Task<IActionResult> CreateBooking(int courseId)
        {
            var course = await _courseService.GetByIdAsync(courseId, _contextAccessor.HttpContext.Session.GetString("JWTToken"));
            CourseBookingDTO booking = new CourseBookingDTO
            {
                CourseId = courseId,


            };


            await _courseBookingService.CreateCourseAsync(booking, _contextAccessor.HttpContext.Session.GetString("JWTToken"));
            TempData["Success"] = "Enrollment Done Successfully";
            return RedirectToAction(nameof(Index));



        }





        public async Task<IActionResult> DeleteCourseBooking(int id)
        {
            var course = await _courseBookingService.GetByIdAsync(id,_contextAccessor .HttpContext.Session.GetString("JWTToken"));
            return View(course);
        }

        [HttpPost]
        [ActionName("DeleteBooking")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            await _courseBookingService.DeleteCourseAsync(id, _contextAccessor.HttpContext.Session.GetString("JWTToken"));
            TempData["success"] = "Course UnEnrolled Successfully";

            return RedirectToAction(nameof(Index));
        }

    }
}
