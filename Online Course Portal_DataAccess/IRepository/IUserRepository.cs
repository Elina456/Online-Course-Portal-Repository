using Online_Course_Portal_DataAccess.Model.DTO;
using Online_Course_Portal_DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Course_Portal_DataAccess.IRepository
{
    public interface IUserRepository:IRepository<User>
    {
        bool IsUniqueUser(string userName);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<UserDTO> Register(UserCreateDTO registrationRequestDTO);
        Task<User> UpdateAsync(User entity);
    }
}
