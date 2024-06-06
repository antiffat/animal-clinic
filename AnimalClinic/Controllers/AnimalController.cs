using AnimalClinic.DTOs;
using AnimalClinic.Helpers;
using AnimalClinic.Models;
using Microsoft.AspNetCore.Http.HttpResults;
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
}