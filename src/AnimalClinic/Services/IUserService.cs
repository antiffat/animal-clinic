using AnimalClinic.DTOs;

namespace AnimalClinic.Services;

public interface IUserService
{
    Task<bool> RegisterUserAsync(RegisterUserDto registerUserDto);
    Task<(bool IsSuccess, string AccessToken, string RefreshToken)> LoginUserAsync(LoginUserDto loginUserDto);
}