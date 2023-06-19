using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Course_Portal_Web.Service.IService;

namespace Online_Course_Portal_Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IHttpContextAccessor _contextAccessor;

        public AdminController(IAdminService adminService,IHttpContextAccessor contextAccessor)
        {
            _adminService = adminService;
            _contextAccessor = contextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _adminService.GetAllAsync(_contextAccessor.HttpContext.Session.GetString("JWTToken"));
            return View(courses);
        }
        [HttpPost]
        public async Task<IActionResult> ApprovedCourse(int id)
        {
            try
            {
                await _adminService.CourseApproved(id, _contextAccessor.HttpContext.Session.GetString("JWTToken"));
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
                await _adminService.CourseRejected(id, _contextAccessor.HttpContext.Session.GetString("JWTToken"));
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
