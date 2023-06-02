using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Online_Course_Portal_DataAccess.Model;
using Online_Course_Portal_DataAccess.Model.DTO;
using Online_Course_Portal_Web.Service.IService;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace Online_Course_Portal_Web.Controllers
{
    public class CourseController : Controller
    {
        
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            
            _courseService = courseService;
        }


            public async Task<IActionResult> Index()
            {
                var posts = await _courseService.GetAllAsync();
                return View(posts);
            }
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateDTO course)
        {
            if (ModelState.IsValid)
            {
                var result = await _courseService.CreateCourseAsync(course);
                TempData["success"] = "Course Created Successfully";
                return RedirectToAction(nameof(Index), new { id = result.Id });
            }

            return View(course);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseService.GetByIdAsync(id);
            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Course course)
        {
            if (ModelState.IsValid)
            {
                await _courseService.UpdateCourseAsync(id, course);
                TempData["success"] = "Course Updated Successfully";
                return RedirectToAction(nameof(Index), new { id });
            }

            return View(course);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseService.GetByIdAsync(id);
            return View(course);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            await _courseService.DeleteCourseAsync(id);
            TempData["success"] = "Course Deleted Successfully";
            return RedirectToAction(nameof(Index));
        }


    }
}
