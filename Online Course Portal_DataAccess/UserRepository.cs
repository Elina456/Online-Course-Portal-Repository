using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Online_Course_Portal_DataAccess.Data;
using Online_Course_Portal_DataAccess.IRepository;
using Online_Course_Portal_DataAccess.Model;
using Online_Course_Portal_DataAccess.Model.DTO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Online_Course_Portal_DataAccess
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private string SecretKey;
        private readonly IMapper _mapper;
        
        public UserRepository(ApplicationDbContext db, IConfiguration iconfiguration, IMapper mapper) : base(db)
        {
            _db = db;

            SecretKey = iconfiguration.GetValue<string>("ApiSettings:Secret");
            _mapper = mapper;
        }

        public bool IsUniqueUser(string userName)
        {
            var user = _db.user.FirstOrDefault(x => x.userName == userName);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _db.user
                .FirstOrDefault(u => u.userName.ToLower() == loginRequestDTO.userName.ToLower());




            bool isValidPassword = BCrypt.Net.BCrypt.Verify(loginRequestDTO.Password, user.Password);
            if (user == null || isValidPassword == false)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    user = null
                };
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.userName.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                Token = tokenHandler.WriteToken(token),

                user = _mapper.Map<User>(user),

            };
            return loginResponseDTO;


        }

        public async Task<User> Register(UserCreateDTO userCreateDTO)
        {
            User user = new()
            {
                userName = userCreateDTO.UserName,
                Password = BCrypt.Net.BCrypt.HashPassword(userCreateDTO.Password),
               
                Role = userCreateDTO.Role
            };
            _db.user.Add(user);
            await _db.SaveChangesAsync();
            user.Password = "";
            return user;
        }



        public async Task<User> UpdateAsync(User entity)
        {

            _db.user.Update(entity);
            await _db.SaveChangesAsync();
            return entity;

        }
    }
}
