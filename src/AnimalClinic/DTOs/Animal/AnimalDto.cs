using System.ComponentModel.DataAnnotations;

namespace AnimalClinic.DTOs;

public class AnimalDto
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    [Required]
    public string AnimalType { get; set; }
}