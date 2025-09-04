using Dtos.UserDto;

namespace Services.Interfaces
{
    public interface IUserService
    {
        string LoginUser(LoginUserDto loginUser);
        void RegisterUser(RegisterUserDto registerUser);
    }
}
