namespace AnimalClinic.Models;

public class Animal
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int AnimalTypesId { get; set; }
    public AnimalTypes AnimalType { get; set; }
    public ICollection<Visit> Visits { get; set; }
}