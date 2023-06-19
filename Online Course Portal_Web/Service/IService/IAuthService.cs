using Online_Course_Portal_DataAccess.Model;
using Online_Course_Portal_DataAccess.Model.DTO;

namespace Online_Course_Portal_Web.Service.IService
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> Login(LoginRequestDTO model);
        Task<UserDTO> Register(UserCreateDTO model);
    }
}
