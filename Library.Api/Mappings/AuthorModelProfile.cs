using AutoMapper;
using Library.Api.Contracts;
using Library.Application.Models;

namespace Library.Api.Mappings
{
    public class AuthorModelProfile : Profile
    {
        public AuthorModelProfile()
        {
            CreateMap<AuthorContract, AuthorModel>();
        }
    }
}
