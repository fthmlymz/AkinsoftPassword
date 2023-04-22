using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PasswordManager.API.Dtos.User;
using PasswordManager.API.Helpers;
using PasswordManager.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PasswordManager.API.Services.AuthService
{
    public class AuthRepository : IAuthRepository
    {

        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthRepository(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Kullanıcı login olur ve geriye token dönderir
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<string>> Login(string email, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Email.ToLower().Equals(email.ToLower()));

            //If the user is not found, return a null value.
            if (user == null)
            {
                response.Success = false;
                response.Message = "user not found.";
            }
            else if (!VerifyPasswordHash(password: password, passwordHash: user.PasswordHash, passwordSalt: user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "password error";
            }
            else
            {
                response.Data = CreateToken(user);
            }
            return response;
        }

        /// <summary>
        /// Kullanıcı kayıt ekranı
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();


            //Is the user registered before?
            if (await UserExists(user.Email))
            {
                response.Message = "Bu email daha önce kaydedilmiş.";
                response.Success = false;
                return response;
            }

            //Save password as hashed in db
            CreatePasswordHash(password: password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;


            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            response.Data = (int)user.Id;
            response.Message = $"Id: {user.Id}, Email: {user.Email}  -  {user.Firstname} {user.Surname} user created";
            return response;
        }






        /// <summary>
        /// Kullanıcı daha önce kayıt oldu mu ?
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> UserExists(string email)
        {
            if (await _context.Users.AnyAsync(c => c.Email.ToLower().Equals(email.ToLower())))
            {
                return true;
            }
            return false;
        }






        /// <summary>
        /// Password hash
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        /// <summary>
        /// Şifre doğrula
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        /// <returns></returns>
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }


        /// <summary>
        /// Token olustur
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("firstname", user.Firstname),
                new Claim("surname", user.Surname),
                new Claim("registerdate", user.RegisterDate.ToString()),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key: key, algorithm: SecurityAlgorithms.HmacSha512Signature);
            //var express = Convert.ToDouble(new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:TokenExpres").Value)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims: claims),
                Expires = DateTime.Now.AddHours(8), //System.DateTime.Now.AddHours(express),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor: tokenDescriptor);

            return tokenHandler.WriteToken(token: token);
        }

      
    }
}
