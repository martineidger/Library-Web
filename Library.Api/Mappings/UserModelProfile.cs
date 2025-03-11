using AutoMapper;
using Library.Api.Contracts;
using Library.Application.Models;

namespace Library.Api.Mappings
{
    public class UserModelProfile : Profile
    {
        public UserModelProfile()
        {
            CreateMap<RegistrationContract, UserModel>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => "User"));

            CreateMap<UserModel, UserInfoContract>();

            
        }
    }
}
