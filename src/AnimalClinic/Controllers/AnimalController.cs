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
    public async Task<ActionResult<IEnumerable<AnimalDto>>> GetAllAnimals([FromQuery] string queryBy = "Name")
    {
        IQueryable<Animal> query = _context.Animals.Include(a => a.AnimalType); // using Include()
        // gives us option to prevent lazy loading. So related AnimalType entities will be loaded at the same time as Animal entities.

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
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
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
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
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
        catch (DbUpdateConcurrencyException ex)
        {
            var entry = ex.Entries.Single();
            var databaseEntry = entry.GetDatabaseValues();

            if (databaseEntry == null)
            {
                if (!_context.Animals.Any(a => a.Id == id))
                {
                    return NotFound(); // HTTP 404
                }
            }
            
            var databaseValues = (Animal)databaseEntry.ToObject();
            
            ModelState.AddModelError(string.Empty, "The record you attempted to edit was modified " +
                "by another user after you got the original value. The edit operation was cancelled and current " +
                "values in the database have been displayed. Try again to edit the new version of the record.");
            
            ModelState.AddModelError("DatabaseValues", databaseValues.ToString());

            return Conflict(ModelState);
        }

        return NoContent(); // HTTP 204
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAnimal(int id)
    {
        var animal = await _context.Animals.FindAsync(id);

        if (animal == null)
        {
            return NotFound();
        }

        _context.Animals.Remove(animal);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}