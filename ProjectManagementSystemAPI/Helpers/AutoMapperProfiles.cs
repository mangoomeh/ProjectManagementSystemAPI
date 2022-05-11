using AutoMapper;
using ProjectManagementSystemAPI.Dtos;
using ProjectManagementSystemAPI.Models;

namespace ProjectManagementSystemAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<SignupDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<ProjectDto, Project>();
        }
    }
}
