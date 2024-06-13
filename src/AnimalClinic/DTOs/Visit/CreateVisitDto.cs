namespace AnimalClinic.DTOs;

public class CreateVisitDto
{
    public int EmployeeId { get; set; }
    public int AnimalId { get; set; }
    public DateTime Date { get; set; }
}