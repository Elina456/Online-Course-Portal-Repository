using Microsoft.AspNetCore.Mvc;
using Online_Course_Portal_Web.Service.IService;

namespace Online_Course_Portal_Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _adminService.GetAllAsync();
            return View(courses);
        }
        [HttpPost]
        public async Task<IActionResult> ApprovedCourse(int id)
        {
            try
            {
                await _adminService.CourseApproved(id);
                TempData["success"] = "Course Approved Successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> RejectedCourse(int id)
        {
            try
            {
                await _adminService.CourseRejected(id);
                TempData["success"] = "Course Rejected Successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index");
            }
        }
    }
}
