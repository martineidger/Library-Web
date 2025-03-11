using AutoMapper;
using Library.Api.Contracts;
using Library.Application.Models;

namespace Library.Api.Mappings
{
    public class BookModelProfile : Profile
    {
        public BookModelProfile()
        {
            CreateMap<BookModel, BookContract>().ReverseMap();
            AllowNullCollections = false;
        }
    }
}
