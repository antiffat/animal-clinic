using AnimalClinic.DTOs;
using AnimalClinic.Helpers;
using AnimalClinic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnimalClinic.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalController : ControllerBase
{
    private readonly AnimalClinicContext _context;

    public AnimalController(AnimalClinicContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Animal>>> GetAllAnimals([FromQuery] string queryBy = "Name")
    {
        IQueryable<Animal> query = _context.Animals.Include(a => a.AnimalType);

        query = queryBy switch
        {
            "Description" => query.OrderBy(a => a.Description), _ => query.OrderBy(a => a.Name)
        };

        var animals = await query.ToListAsync();

        var animalDtos = animals.Select(animal => new AnimalDto
        {
            Id = animal.Id,
            Name = animal.Name,
            Description = animal.Description,
            AnimalType = animal.AnimalType.Name
        }).ToList();

        return Ok(animalDtos);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<AnimalDto>> GetAnimal(int id)
    {
        var animal = await _context.Animals.Include(a => a.AnimalType)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (animal == null)
        {
            return NotFound();
        }

        var animalDto = new AnimalDto
        {
            Id = animal.Id,
            Name = animal.Name,
            Description = animal.Description,
            AnimalType = animal.AnimalType.Name
        };

        return Ok(animalDto);
    }
    

    [HttpPost]
    public async Task<ActionResult<AnimalDto>> CreateAnimal(CreateAnimalDto createAnimalDto)
    {
        var animalType = await _context.AnimalTypes.FirstOrDefaultAsync(at => at.Name == createAnimalDto.AnimalType);

        if (animalType == null)
        {
            return BadRequest("Invalid AnimalType"); // Http Status Code 400
        }

        var animal = new Animal
        {
            Name = createAnimalDto.Name,
            Description = createAnimalDto.Description,
            AnimalType = animalType
        };

        _context.Animals.Add(animal);
        await _context.SaveChangesAsync();

        var animalDto = new AnimalDto
        {
            Id = animal.Id,
            Name = animal.Name,
            Description = animal.Description,
            AnimalType = animalType.Name
        };
        
        return CreatedAtAction(nameof(GetAnimal), new { id = animal.Id }, animalDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAnimal(int id, UpdateAnimalDto updateAnimalDto)
    {
        var animal = await _context.Animals.FindAsync(id);

        if (animal == null)
        {
            return NotFound(); // HTTP 404
        }

        animal.Name = updateAnimalDto.Name;
        animal.Description = updateAnimalDto.Description;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!_context.Animals.Any(e => e.Id == id))
        {
            return NotFound(); // HTTP 404
        }

        return NoContent(); // HTTP 204
    }
}