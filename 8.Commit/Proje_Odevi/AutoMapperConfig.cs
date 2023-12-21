using AutoMapper;
using Proje_Odevi.Entities;
using Proje_Odevi.Models;
using System.Runtime.InteropServices;

namespace Proje_Odevi
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {

            CreateMap<User, UserModel>().ReverseMap();
        } 
    
    }
}
