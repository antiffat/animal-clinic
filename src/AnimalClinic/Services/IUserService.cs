using AnimalClinic.DTOs;

namespace AnimalClinic.Services;

public interface IUserService
{
    Task<bool> RegisterUserAsync(RegisterUserDto registerUserDto);
}