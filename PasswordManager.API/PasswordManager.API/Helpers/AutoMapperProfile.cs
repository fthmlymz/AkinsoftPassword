using AutoMapper;
using PasswordManager.API.Dtos.MyPasswords;
using PasswordManager.API.Dtos.User;
using PasswordManager.API.Models;

namespace PasswordManager.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserRegisterDto>();
            CreateMap<MyPassword, MyPasswordsDto>();
            CreateMap<MyPasswordCreateDto, MyPassword>();
        }
    }
}
