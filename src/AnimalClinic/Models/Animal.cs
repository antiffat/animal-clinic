using System.ComponentModel.DataAnnotations;

namespace AnimalClinic.Models;

public class Animal
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [MaxLength(2000)]
    public string? Description { get; set; }
    
    public int AnimalTypesId { get; set; }
    
    public AnimalTypes AnimalType { get; set; }
    
    public ICollection<Visit> Visits { get; set; }
    
    [Timestamp]
    public byte[] RowVersion { get; set; }
}