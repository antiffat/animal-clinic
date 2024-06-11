using System.ComponentModel.DataAnnotations;

namespace AnimalClinic.Models;

public class AnimalTypes
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public ICollection<Animal> Animals { get; set; }
    
    [Timestamp]
    public byte[] RowVersion { get; set; }
}