using System.ComponentModel.DataAnnotations;

namespace AnimalClinic.DTOs;

public class RegisterUserDto
{
    [Required(ErrorMessage = "Username is required.")]
    [StringLength(100, ErrorMessage = "Username can't be longer than 100 characters.")]
    public string Username { get; set; }
    
    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, ErrorMessage = "Password can't be longer than 100 characters.")]
    public string Password { get; set; }
}