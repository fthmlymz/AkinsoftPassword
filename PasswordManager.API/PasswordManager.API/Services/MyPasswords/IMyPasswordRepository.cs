using PasswordManager.API.Dtos.MyPasswords;
using PasswordManager.API.Helpers;

namespace PasswordManager.API.Services.MyPasswords
{
    public interface IMyPasswordRepository
    {
        Task<ServiceResponse<List<MyPasswordsDto>>> GetPasswords();
        Task<ServiceResponse<List<MyPasswordsDto>>> CreatePasswords(MyPasswordCreateDto newCustomer);
    }
}
