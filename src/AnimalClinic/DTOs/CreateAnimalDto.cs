using System.ComponentModel.DataAnnotations;

namespace AnimalClinic.DTOs;

public class CreateAnimalDto
{
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    public string AnimalType { get; set; }
}