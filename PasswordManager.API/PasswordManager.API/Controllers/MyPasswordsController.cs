using Microsoft.AspNetCore.Mvc;
using PasswordManager.API.Dtos.MyPasswords;
using PasswordManager.API.Helpers;
using PasswordManager.API.Services.MyPasswords;

namespace PasswordManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyPasswordsController : ControllerBase
    {
        private readonly IMyPasswordRepository _passwordRepository;
        private readonly IConfiguration _configuration;

        public MyPasswordsController(IMyPasswordRepository passwordRepository, IConfiguration configuration)
        {
            _passwordRepository = passwordRepository;
            _configuration = configuration;
        }


        [HttpPost("CreatePassword")]
        public async Task<ActionResult<ServiceResponse<MyPasswordsDto>>> CreatePassword(MyPasswordCreateDto dto)
        {
            var encryptedPassword = EncyrptionDecryption.EncryptString(dto.Password, _configuration.GetSection("AppSettings:EncryptionDecriyptionKey").Value);

            var response = await _passwordRepository.CreatePasswords(new MyPasswordCreateDto
            {
                Category = dto.Category,
                Description = dto.Description,
                Password = encryptedPassword,
                Url = dto.Url,
                UserName = dto.UserName,
            });

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        [HttpGet("GetPasswords")]
        public async Task<ActionResult<ServiceResponse<List<MyPasswordsDto>>>> GetAllMyPassword()
        {
            return Ok(await _passwordRepository.GetPasswords());
        }

    }
}
