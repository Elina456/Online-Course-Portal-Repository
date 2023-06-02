using Microsoft.AspNetCore.Mvc;
using Online_Course_Portal_DataAccess.Model.DTO;
using Online_Course_Portal_DataAccess.Model;
using Online_Course_Portal_Web.Service.IService;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Globalization;
using System.Security.AccessControl;

namespace Online_Course_Portal_Web.Controllers
{
    public class CourseBookingController : Controller
    {
        private readonly ICourseBookingService _courseBookingService;
        private readonly ICourseService _courseService;
        
        public CourseBookingController(ICourseBookingService courseBookingService, ICourseService courseService)
        {

            _courseBookingService = courseBookingService;
            _courseService = courseService;
        }


        public async Task<IActionResult> Index()
        {
            var course = await _courseBookingService.GetAllAsync();
            var booking = _courseService.GetAllAsync();
            ViewBag.booking = booking;
            return View(course);
        }
        
        public async Task<IActionResult> CreateBooking(int id)
        {
            var result = await _courseService.GetByIdAsync(id);
            
            ViewData["a"] = result.AvailableSeats;
            ViewData["b"] = result.courseName;
            ViewData["c"] =result.EndDate.ToString("yyyy-MM-dd");

            ViewData["d"] = result.StartDate.ToString("yyyy-MM-dd");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(CourseBooking course)
        {
            if (ModelState.IsValid)
            {
                var result = await _courseBookingService.CreateCourseAsync(course);
                TempData["success"] = "Course Enrolled Successfully";
                return RedirectToAction(nameof(Index), new { id = result.Id });
            }

            return View(course);
        }

        

        

        public async Task<IActionResult> DeleteCourseBooking(int id)
        {
            var course = await _courseBookingService.GetByIdAsync(id);
            return View(course);
        }

        [HttpPost]
        [ActionName("DeleteBooking")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            await _courseBookingService.DeleteCourseAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
