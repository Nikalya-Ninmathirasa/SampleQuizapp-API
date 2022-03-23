using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UserAuthentication.Entities;
using UserAuthentication.Models;
using UserAuthentication.Models.RequestModel;
using UserAuthentication.Repositories;

namespace UserAuthentication.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(DataContext dataContext, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;


        }

        public async Task<Models.ResponseModel.UserResponseModel> Login(UserLoginRequestModel user)
        {
            var msg = "";
            var existinguser = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email == user.UserName);
            if (existinguser == null)
            {
                msg = "User not found";  
                throw new Exception(msg);
            }
            if (!VerifyPasswordHash(user.Password, existinguser.PasswordHash, existinguser.PasswordSalt))
            {
                msg = "Wrong password";
               // throw new Exception(msg);
            }
                string token = CreateToken(existinguser);
            Models.ResponseModel.UserResponseModel responseModel = new Models.ResponseModel.UserResponseModel();
            
                responseModel.Email = existinguser.Email;
                responseModel.Role = existinguser.Role;
                responseModel.Name = existinguser.UserName;
                responseModel.Id = existinguser.Id;
                responseModel.Token = token;

            return responseModel;
            
           
        }

        public async Task<Models.ResponseModel.UserResponseModel> Register(UserRequestModel user)
        {
            if (user != null)
            {
                CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
                ClientEntity userEntity = new ClientEntity();
                userEntity.UserName = user.Name;
                userEntity.PasswordHash = passwordHash;
                userEntity.Email = user.Email;
                userEntity.Role = user.Role;
                userEntity.PasswordSalt = passwordSalt;
                _dataContext.Users.Add(userEntity);
                await _dataContext.SaveChangesAsync();
                var res = await _dataContext.Users.FirstOrDefaultAsync(u=>u.Email==user.Email);
                Models.ResponseModel.UserResponseModel responseModel = new Models.ResponseModel.UserResponseModel();
                if (res != null)
                {
                    responseModel.Email = res.Email;
                    responseModel.Role = res.Role;
                    responseModel.Name = res.UserName;
                    responseModel.Id = res.Id;

                    return responseModel;
                }
              else
                {
                    return null;
                }
            }
          else{
                return null;
            }
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }

      
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(ClientEntity user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var test = _configuration.GetSection("AppSettings.Token").Value;
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),

                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public string GetMyName()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return result;
        }
    }
}
