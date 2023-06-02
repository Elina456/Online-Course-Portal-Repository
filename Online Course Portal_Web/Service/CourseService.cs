using Newtonsoft.Json;
using Online_Course_Portal_DataAccess;
using Online_Course_Portal_DataAccess.Model;
using Online_Course_Portal_DataAccess.Model.DTO;
using Online_Course_Portal_Web.Service.IService;
using System.Net;
using System.Reflection.Metadata;
using System.Text;

namespace Online_Course_Portal_Web.Service
{
    public class CourseService:ICourseService
    {
        private readonly HttpClient _httpClient;
        private const string Baseurl = "https://localhost:7228";
        public CourseService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(Baseurl);
            
        }
        


      
        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("/api/Course/");
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(content);

            if (apiResponse.IsSuccess)
            {
                IEnumerable<Course> courses = JsonConvert.DeserializeObject<IEnumerable<Course>>(apiResponse.Result.ToString());
                return courses;
            }

            
            return null;
        }

       
        public async Task<Course> GetByIdAsync(int courseId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/Course/{courseId}");
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(content);

            if (apiResponse.IsSuccess)
            {
                Course course = JsonConvert.DeserializeObject<Course>(apiResponse.Result.ToString());
                return course;
            }

            
            return null;
        }
        
        public async Task<Course> CreateCourseAsync(CourseCreateDTO course)
        {
            string json = JsonConvert.SerializeObject(course);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("/api/Course/", content);
            response.EnsureSuccessStatusCode();
            string responseContent = await response.Content.ReadAsStringAsync();
            ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseContent);

            if (apiResponse.IsSuccess)
            {
                Course createdCourse = JsonConvert.DeserializeObject<Course>(apiResponse.Result.ToString());
                return createdCourse;
            }

            
            return null;
        }

        public async Task<Course> UpdateCourseAsync(int id, Course course)
        {
            string json = JsonConvert.SerializeObject(course);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync($"/api/Course/{id}", content);
            response.EnsureSuccessStatusCode();
            string responseContent = await response.Content.ReadAsStringAsync();
            Course updatedCourse = JsonConvert.DeserializeObject<Course>(responseContent);
            return updatedCourse;
        }

        public async Task DeleteCourseAsync(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"/api/Course/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
