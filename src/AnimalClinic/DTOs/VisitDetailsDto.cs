namespace AnimalClinic.DTOs;

public class VisitDetailsDto
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int AnimalId { get; set; }
    public DateTime Date { get; set; }
}