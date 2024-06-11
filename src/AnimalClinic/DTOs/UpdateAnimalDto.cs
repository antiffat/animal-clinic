using System.ComponentModel.DataAnnotations;

namespace AnimalClinic.DTOs;

public class UpdateAnimalDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [MaxLength(2000)]
    public string? Description { get; set; }
}