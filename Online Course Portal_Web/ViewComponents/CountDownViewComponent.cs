using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Common;
using Online_Course_Portal_DataAccess.Model;
using Online_Course_Portal_DataAccess;
using Online_Course_Portal_Web.Service.IService;

namespace Online_Course_Portal_Web.ViewComponents
{
    public class CountDownViewComponent : ViewComponent
    {
        private readonly ICourseService _courseService;


        public CountDownViewComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string token = HttpContext.Session.GetString("JWTToken");

            var courses = await _courseService.GetAllAsync(token);


          //  IEnumerable<Course> model = JsonConvert.DeserializeObject<List<Course>>(Convert.ToString(courses));




            Course firstEvent = courses.OrderBy(e => e.StartDate).FirstOrDefault();

            if (firstEvent != null)
            {
                var countdownTime = DateTime.UtcNow - firstEvent.StartDate;

                return View("Default", countdownTime);
            }

            return Content("No courses found.");
        }

    }
}
