using BlogApi.Models;

namespace BlogApi.Services
{
    public interface IUserService
    {
        Task<User> RegisterUser(UserRegisterDto userRegisterDto);
        Task<User?> ValidateUserCredentials(UserLoginDto userLoginDto);
    }

}
