using Newtonsoft.Json;
using Online_Course_Portal_DataAccess.Model;
using Online_Course_Portal_Web.Service.IService;
using System.Net;
using System.Net.Http.Headers;

namespace Online_Course_Portal_Web.Service
{
    public class AdminService : IAdminService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7228";

        public AdminService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(BaseUrl);
           
        }
        public async Task<IEnumerable<Course>> GetAllAsync(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            HttpResponseMessage response = await _httpClient.GetAsync("/api/Admin/GetAllCourses");
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            IEnumerable<Course> course = JsonConvert.DeserializeObject<IEnumerable<Course>>(content);
            return course;
        }
        public async  Task<IEnumerable<Course>> GetAllApproveCourseAsync(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
    HttpResponseMessage response = await  _httpClient.GetAsync("/api/Admin/GetAllApprovedCourse");
    response.EnsureSuccessStatusCode();
            string content =  await response.Content.ReadAsStringAsync();
    IEnumerable<Course> course = JsonConvert.DeserializeObject<IEnumerable<Course>>(content);
            return course;

        }

public async Task<bool> CourseApproved(int id, string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.GetAsync($"api/Admin/CourseApproved?id={id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new Exception("Course not found.");
            }
            else
            {
                throw new Exception("Error  while approving course");
            }
        }

        public async Task<bool> CourseRejected(int id, string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var response = await _httpClient.GetAsync($"api/Admin/CourseRejected?id={id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new Exception("Course not found.");
            }
            else
            {
                throw new Exception("Error  while rejecting course");
            }

        }

        
    }
}
