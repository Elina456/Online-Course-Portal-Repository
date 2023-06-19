using AutoMapper.Internal;
using Newtonsoft.Json;
using Online_Course_Portal_DataAccess.Model;
using Online_Course_Portal_DataAccess;
using Online_Course_Portal_DataAccess.Model.DTO;
using Online_Course_Portal_Web.Service.IService;
using OnlineCoursePortal.Utility;
using System.Text;
using System;
using Microsoft.AspNetCore.Identity;

namespace Online_Course_Portal_Web.Service
{
    public class AuthService:IAuthService
    {
        private readonly HttpClient _httpClient;
        private const string Baseurl = "https://localhost:7228";
        public AuthService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(Baseurl);
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO model)
        {
            string json = JsonConvert.SerializeObject(model);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("/api/User/login", content);
            response.EnsureSuccessStatusCode();
            string responseContent = await response.Content.ReadAsStringAsync();
            ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseContent);

            if (apiResponse.IsSuccess)
            {
                LoginResponseDTO result = JsonConvert.DeserializeObject<LoginResponseDTO>(apiResponse.Result.ToString());
                return result;
            }


            return null;


        }

        public async Task<UserDTO> Register(UserCreateDTO model)
        {



            string json = JsonConvert.SerializeObject(model);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("/api/User/Register", content);

            response.EnsureSuccessStatusCode();
            string responseContent = await response.Content.ReadAsStringAsync();
            ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseContent);

            if (apiResponse.IsSuccess)
            {
                UserDTO users = JsonConvert.DeserializeObject<UserDTO>(apiResponse.Result.ToString());
                return users;
            }


            return null;
        }
    }
}
    

