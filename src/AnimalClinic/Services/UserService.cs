using AnimalClinic.DTOs;
using AnimalClinic.Helpers;
using AnimalClinic.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AnimalClinic.Services;

public class UserService : IUserService
{
    private readonly AnimalClinicContext _context;

    public UserService(AnimalClinicContext context)
    {
        _context = context;
    }

    public async Task<bool> RegisterUserAsync(RegisterUserDto registerUserDto)
    {
        if (await _context.Users.AnyAsync(u => u.Username == registerUserDto.Username))
        {
            return false;
        }

        var user = new User
        {
            Username = registerUserDto.Username,
            PasswordHash = new PasswordHasher<User>().HashPassword(null, registerUserDto.Password)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return true;
    }
}