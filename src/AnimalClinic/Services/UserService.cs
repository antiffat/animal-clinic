using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AnimalClinic.DTOs;
using AnimalClinic.Helpers;
using AnimalClinic.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AnimalClinic.Services;

public class UserService : IUserService
{
    private readonly AnimalClinicContext _context;
    private readonly IConfiguration _configuration;

    public UserService(AnimalClinicContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
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

    public async Task<(bool IsSuccess, string AccessToken, string RefreshToken)> LoginUserAsync(
        LoginUserDto loginUserDto)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == loginUserDto.Username);

        if (user == null)
        {
            return (false, null, null);
        }

        var passwordHasher = new PasswordHasher<User>();
        var result = passwordHasher.VerifyHashedPassword(null, user.PasswordHash, loginUserDto.Password);

        if (result == PasswordVerificationResult.Failed)
        {
            return (false, null, null);
        }

        var accessToken = GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        await _context.SaveChangesAsync();

        return (true, accessToken, refreshToken);
    }
    
    public async Task<(bool IsSuccess, string AccessToken)> RefreshTokenAsync(RefreshTokenDto refreshTokenDto)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.RefreshToken == refreshTokenDto.RefreshToken);

        if (user == null)
        {
            return (false, null);
        }

        var newAccessToken = GenerateAccessToken(user);
        return (true, newAccessToken);
    }

    private string GenerateAccessToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(15),
            Issuer = _configuration["JwtSettings:Issuer"],
            Audience = _configuration["JwtSettings:Audience"],
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}