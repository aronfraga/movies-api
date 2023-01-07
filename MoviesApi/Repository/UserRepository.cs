using Microsoft.IdentityModel.Tokens;
using MoviesApi.Models;
using MoviesApi.Data;
using MoviesApi.Models.Dtos;
using MoviesApi.Repository.IRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XSystem.Security.Cryptography;

namespace MoviesApi.Repository {
    public class UserRepository : IUserRepository {

        private readonly Context _dbcontext;
        private string Secret;

        public UserRepository(Context dbcontext, IConfiguration config) {
            _dbcontext = dbcontext;
            Secret = config.GetValue<string>("ApiSettings:Secret");
        }

        public void ExistUser(string userName) {
            var dbResponse = _dbcontext.Users.FirstOrDefault(data => data.UserName == userName);
            if (dbResponse != null) throw new Exception("The User is already into database");
        }

        public User GetUser(int id) {
            var dbResponse = _dbcontext.Users.FirstOrDefault(data => data.Id == id);
            if (dbResponse == null) throw new Exception("The User does not exist");
            return dbResponse;
        }

        public ICollection<User> GetUsers() {
            return _dbcontext.Users.ToList();
        }

        public async Task<UserLoginResponseDto> LoginAsync(UserLoginDto userLoginDto) {
            var PasswordEncrypted = EncryptMD5(userLoginDto.Password);

            var userResponse = _dbcontext.Users.FirstOrDefault(data => data.UserName.ToLower() == 
                                                               userLoginDto.UserName.ToLower() &&
                                                               data.Password == PasswordEncrypted);
            if (userResponse == null) throw new Exception("User or Password are incorrect");

            var handlerToken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, userResponse.UserName.ToString()),
                    new Claim(ClaimTypes.Role, userResponse.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = handlerToken.CreateToken(tokenDescriptor);

            UserLoginResponseDto userLoginResponseDto = new UserLoginResponseDto() {
                Token = handlerToken.WriteToken(token),
                User = userResponse
            };

            return userLoginResponseDto;
        }

        public async Task<User> RegisterAsync(UserRegisterDto userRegisterDto) {
            ExistUser(userRegisterDto.UserName);
    
            var PasswordEncrypted = EncryptMD5(userRegisterDto.Password);
            User user = new User() {
                UserName = userRegisterDto.UserName,
                Password = PasswordEncrypted,
                Name = userRegisterDto.Name,
                Role = userRegisterDto.Role
            };

            _dbcontext.Users.Add(user);
            await _dbcontext.SaveChangesAsync();
            user.Password = PasswordEncrypted;
            return user;
        }

        public static string EncryptMD5(string value) {
            MD5CryptoServiceProvider X = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(value);
            data = X.ComputeHash(data);
            string response = "";
            for (int i = 0; i < data.Length; i++) {
                response += data[i].ToString("x2").ToLower();
            }
            return response;
        }

    }
}
