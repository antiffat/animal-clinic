namespace AnimalClinic.Models;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public ICollection<Visit> Visits { get; set; }
}