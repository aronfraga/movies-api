using MoviesApi.Models;
using MoviesApi.Models.Dtos;

namespace MoviesApi.Repository.IRepository {
    public interface IUserRepository {

        ICollection<User> GetUsers();

        User GetUser(int id);

        void ExistUser(string userName);

        Task<UserLoginResponseDto> LoginAsync(UserLoginDto userLoginDto);

        Task<User> RegisterAsync(UserRegisterDto userRegisterDto);

    }
}
