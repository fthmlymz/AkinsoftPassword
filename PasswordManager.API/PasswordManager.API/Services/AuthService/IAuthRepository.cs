using PasswordManager.API.Models;
using PasswordManager.API.Helpers;
using PasswordManager.API.Dtos.User;

namespace PasswordManager.API.Services.AuthService
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string email, string password);
        Task<bool> UserExists(string email); // kullanıcı daha önce kaydedildi mi ?
    }
}
