using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PasswordManager.API.Dtos.MyPasswords;
using PasswordManager.API.Helpers;
using PasswordManager.API.Models;

namespace PasswordManager.API.Services.MyPasswords
{
    public class MyPasswordRepository : IMyPasswordRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;


        public MyPasswordRepository(DataContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }


        /// <summary>
        /// kullanıcı sifresi olsutur
        /// </summary>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<List<MyPasswordsDto>>> CreatePasswords(MyPasswordCreateDto newPassword)
        {
            var serviceResponse = new ServiceResponse<List<MyPasswordsDto>>();

            _context.MyPasswords.Add(_mapper.Map<MyPassword>(newPassword));
            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.MyPasswords.Select(c => _mapper.Map<MyPasswordsDto>(c)).ToListAsync();

            return serviceResponse;
        }


        /// <summary>
        /// Şifre listesi
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResponse<List<MyPasswordsDto>>> GetPasswords()
        {
            var serviceResponse = new ServiceResponse<List<MyPasswordsDto>>();

            try
            {
                var dbCustomers = await _context.MyPasswords.ToListAsync();

                serviceResponse.Data = dbCustomers
                                                .Select(c => _mapper.Map<MyPasswordsDto>(new MyPasswordsDto
                                                {
                                                    Password = EncyrptionDecryption.DecryptString(c.Password, _configuration.GetSection("AppSettings:EncryptionDecriyptionKey").Value),
                                                    UserName = c.UserName,
                                                    Category = c.Category,
                                                    Description = c.Description,
                                                    Id = c.Id,
                                                    Url = c.Url
                                                })).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

    }
}
