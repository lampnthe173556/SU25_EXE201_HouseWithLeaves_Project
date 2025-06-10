using AutoMapper;
using ProjectHouseWithLeaves.Dtos;
using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Helper.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserRegisterDtos, User>().AfterMap((src, dest) => dest.CreatedAt = DateTime.Now);
        }
    }
}
