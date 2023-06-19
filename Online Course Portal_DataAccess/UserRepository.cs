using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private string SecretKey;
        private readonly IMapper _mapper;
        
        public UserRepository(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration iconfiguration, IMapper mapper) : base(db)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;

            SecretKey = iconfiguration.GetValue<string>("ApiSettings:Secret");
            _mapper = mapper;
        }

        public bool IsUniqueUser(string userName)
        {
            var user = _db.applicationUsers.FirstOrDefault(x => x.UserName == userName);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _db.applicationUsers
                .FirstOrDefault(u => u.UserName.ToLower() == loginRequestDTO.userName.ToLower());




           // bool isValidPassword = BCrypt.Net.BCrypt.Verify(loginRequestDTO.Password, user.Password);
           bool isValid = await _userManager.CheckPasswordAsync(user,loginRequestDTO.Password);
            if (user == null || isValid == false)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    user = null
                };
            }
            var roles = await _userManager.GetRolesAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                Token = tokenHandler.WriteToken(token),

                user = _mapper.Map<UserDTO>(user),
               // Role = roles.FirstOrDefault()

            };
            return loginResponseDTO;


        }

        //public async Task<User> Register(UserCreateDTO userCreateDTO)
        //{
        //    User user = new()
        //    {
        //        userName = userCreateDTO.UserName,
        //        Password = BCrypt.Net.BCrypt.HashPassword(userCreateDTO.Password),

        //        Role = userCreateDTO.Role
        //    };
        //    _db.user.Add(user);
        //    await _db.SaveChangesAsync();
        //    user.Password = "";
        //    return user;
        //}
        public async Task<UserDTO> Register(UserCreateDTO registrationRequestDTO)
        {
            ApplicationUser user = new()
            {
                UserName = registrationRequestDTO.UserName,
                Name = registrationRequestDTO.Name,
                Email = registrationRequestDTO.UserName,
                NormalizedEmail = registrationRequestDTO.UserName.ToUpper()



            };
            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDTO.Password);
                if (result.Succeeded)
                {
                    if (!_roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Admin"));
                        await _roleManager.CreateAsync(new IdentityRole("Student"));
                    }
                    await _userManager.AddToRoleAsync(user, "Admin");
                    var userToReturn = _db.applicationUsers.FirstOrDefault(u => u.UserName == registrationRequestDTO.UserName);
                    return _mapper.Map<UserDTO>(userToReturn);
                }

            }
            catch (Exception e)
            {
            }

            return new UserDTO();
        }



        public async Task<User> UpdateAsync(User entity)
        {

            _db.user.Update(entity);
            await _db.SaveChangesAsync();
            return entity;

        }
    }
}
