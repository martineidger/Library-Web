using AutoMapper;
using Library.Application.Models;
using Library.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Mappings
{
    public class UserEntProfile : Profile
    {
        public UserEntProfile()
        {
            CreateMap<UserEntity, UserModel>().ReverseMap();
        }
    }
}
