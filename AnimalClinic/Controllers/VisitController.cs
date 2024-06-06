using System.Collections;
using AnimalClinic.DTOs;
using AnimalClinic.Helpers;
using AnimalClinic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnimalClinic.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VisitController : ControllerBase
{
    private readonly AnimalClinicContext _context;

    public VisitController(AnimalClinicContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VisitDto>>> GetAllVisits()
    {
        var visits = await _context.Visits
            .Include(v => v.Employee)
            .Include(v => v.Animal)
            .OrderBy(v => v.Date)
            .ToListAsync();

        var visitDtos = visits.Select(v => new VisitDto
        {
            Id = v.Id,
            EmployeeName = v.Employee.Name,
            AnimalName = v.Animal.Name,
            Date = v.Date
        });

        return Ok(visitDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VisitDetailsDto>> GetVisit(int id)
    {
        var visit = await _context.Visits
            .Include(v => v.Employee)
            .Include(v => v.Animal)
            .FirstOrDefaultAsync(v => v.Id == id);

        if (visit == null)
        {
            return NotFound();
        }

        var visitDetailsDto = new VisitDetailsDto
        {
            Id = visit.Id,
            EmployeeId = visit.EmployeeId,
            AnimalId = visit.AnimalId,
            Date = visit.Date
        };

        return Ok(visitDetailsDto);
    }

    [HttpPost]
    public async Task<ActionResult<VisitDetailsDto>> CreateVisit(CreateVisitDto createVisitDto)
    {
        var employee = await _context.Employees.FindAsync(createVisitDto.EmployeeId);
        var animal = await _context.Animals.FindAsync(createVisitDto.AnimalId);

        if (employee == null || animal == null)
        {
            return BadRequest("Invalid EmployeeId or AnimalId");
        }

        var visit = new Visit
        {
            EmployeeId = createVisitDto.EmployeeId,
            AnimalId = createVisitDto.AnimalId,
            Date = createVisitDto.Date
        };

        _context.Visits.Add(visit);
        await _context.SaveChangesAsync();

        var visitDetailsDto = new VisitDetailsDto
        {
            Id = visit.Id,
            EmployeeId = visit.EmployeeId,
            AnimalId = visit.AnimalId,
            Date = visit.Date
        };

        return CreatedAtAction(nameof(GetVisit), new { id = visit.Id }, visitDetailsDto);
    }
}