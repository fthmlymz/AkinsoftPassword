using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.API.Dtos.User;
using PasswordManager.API.Helpers;
using PasswordManager.API.Models;
using PasswordManager.API.Services.AuthService;

namespace PasswordManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }



        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
        {
            var response = await _authRepository.Register(
                new User
                {
                    Username = request.Username,
                    Email = request.Email,
                    Firstname = request.Firstname,
                    Surname = request.Surname,
                    RegisterDate = DateTime.Now
                }, request.Password
                );


            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }



        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDto request)
        {
            var response = await _authRepository.Login(request.Email, request.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }




    }
}
