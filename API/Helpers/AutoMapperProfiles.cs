using AutoMapper;
using Domain;
using Domain.Dtos;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateUserMappings();
        }

        private void CreateUserMappings()
        {
            CreateMap<UserForDetailsDto, User>();
            CreateMap<User, UserForDetailsDto>();
        }
    }
}