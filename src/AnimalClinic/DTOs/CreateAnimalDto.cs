using System.ComponentModel.DataAnnotations;

namespace AnimalClinic.DTOs;

public class CreateAnimalDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [MaxLength(2000)]
    public string? Description { get; set; }
    
    [Required]
    public string AnimalType { get; set; }
}