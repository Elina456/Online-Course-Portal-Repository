using Newtonsoft.Json;
using Online_Course_Portal_DataAccess.Model.DTO;
using Online_Course_Portal_DataAccess.Model;
using Online_Course_Portal_DataAccess;
using Online_Course_Portal_Web.Service.IService;
using System.Text;

namespace Online_Course_Portal_Web.Service
{
    public class BookingService:ICourseBookingService
    {
        private readonly HttpClient _httpClient;
        private const string Baseurl = "https://localhost:7228";
        public BookingService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(Baseurl);

        }
        public async Task<IEnumerable<CourseBooking>> GetAllAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("/api/CourseBooking/");
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(content);

            if (apiResponse.IsSuccess)
            {
                IEnumerable<CourseBooking> courses = JsonConvert.DeserializeObject<IEnumerable<CourseBooking>>(apiResponse.Result.ToString());
                return courses;
            }


            return null;
        }


        public async Task<CourseBooking> GetByIdAsync(int courseId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/CourseBooking/{courseId}");
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(content);

            if (apiResponse.IsSuccess)
            {
                CourseBooking course = JsonConvert.DeserializeObject<CourseBooking>(apiResponse.Result.ToString());
                return course;
            }


            return null;
        }

        public async Task<CourseBooking> CreateCourseAsync(CourseBookingDTO course)
        {
            string json = JsonConvert.SerializeObject(course);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("/api/CourseBooking/", content);
            response.EnsureSuccessStatusCode();
            string responseContent = await response.Content.ReadAsStringAsync();
            ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseContent);

            if (apiResponse.IsSuccess)
            {
                CourseBooking createdCourse = JsonConvert.DeserializeObject<CourseBooking>(apiResponse.Result.ToString());
                return createdCourse;
            }


            return null;
        }

        

        public async Task DeleteCourseAsync(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"/api/CourseBooking/{id}");
            response.EnsureSuccessStatusCode();
        }

    }
}
