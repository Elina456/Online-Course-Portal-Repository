using AutoMapper;
using Online_Course_Portal_DataAccess.Model;
using Online_Course_Portal_DataAccess.Model.DTO;

namespace Online_Course_Portal_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Course, CourseCreateDTO>().ReverseMap();
            CreateMap<User, UserCreateDTO>().ReverseMap();
            CreateMap<CourseBooking,CourseBookingDTO>().ReverseMap();
        }
    }
}
