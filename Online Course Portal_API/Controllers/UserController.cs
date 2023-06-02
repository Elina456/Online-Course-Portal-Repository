using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Online_Course_Portal_DataAccess.IRepository;
using Online_Course_Portal_DataAccess.Model.DTO;
using Online_Course_Portal_DataAccess;
using System.Net;

namespace Online_Course_Portal_API.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _dbUser;
        private readonly IMapper _mapper;
        protected ApiResponse _response;
        public UserController(IUserRepository dbUser, IMapper mapper)
        {
            _dbUser = dbUser;
            _mapper = mapper;
            _response = new();

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var loginResponse = await _dbUser.Login(model);
            if (loginResponse.user == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username or Password is incorrect");
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = loginResponse;
            return Ok(_response);


        }



        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserCreateDTO model)
        {
            bool ifUserNameUnique = _dbUser.IsUniqueUser(model.UserName);
            if (!ifUserNameUnique)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username already Exist");
                return BadRequest(_response);

            }
            var user = await _dbUser.Register(model);
            if (user == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Error While Registering");
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            return Ok(_response);

        }


    }
}
