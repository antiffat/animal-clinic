using System.ComponentModel.DataAnnotations;

namespace AnimalClinic.Models;

public class User
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Username { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string PasswordHash { get; set; }
}