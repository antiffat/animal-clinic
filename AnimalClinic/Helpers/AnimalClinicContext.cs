using AnimalClinic.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimalClinic.Helpers;

public class AnimalClinicContext : DbContext
{
    public AnimalClinicContext(DbContextOptions<AnimalClinicContext> options) : base(options)
    {
    }
    
    public DbSet<Animal> Animals { get; set; }
    public DbSet<AnimalTypes> AnimalTypes { get; set; }
    public DbSet<Visit> Visits { get; set; }
    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Animal>()
            .HasOne(a => a.AnimalType)
            .WithMany(at => at.Animals)
            .HasForeignKey(a => a.AnimalTypesId)
            .OnDelete(DeleteBehavior.Cascade); // if an animal type is deleted, all Animal entities that reference to this AnimalType will also be deleted.

        modelBuilder.Entity<Visit>()
            .HasOne(v => v.Employee)
            .WithMany(e => e.Visits)
            .HasForeignKey(v => v.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict); // an Employee cannot be deleted if there are any Visit entities that reference this Employee

        modelBuilder.Entity<Visit>()
            .HasOne(v => v.Animal)
            .WithMany(a => a.Visits)
            .HasForeignKey(v => v.AnimalId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Animal>()
            .Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100)
            .IsConcurrencyToken(); // Concurrency tokens helps to manage concurrent access to data and prevent conflicts
        // when multiple users attempt to update the same data simultaneously. Concurrency token value could be a
        // timestamp or a version number. 
        // When you try to update a record with a concurrency token, the system checks if the token in your update matches
        // the token in the database. If not, it means someone else has changed the record since you last read it,
        // causing a conflict.

        modelBuilder.Entity<Animal>()
            .Property(a => a.Description)
            .HasMaxLength(2000)
            .IsConcurrencyToken();

        modelBuilder.Entity<AnimalTypes>()
            .Property(at => at.Name)
            .IsRequired()
            .HasMaxLength(150)
            .IsConcurrencyToken();
        
        modelBuilder.Entity<AnimalTypes>().HasData(
            new AnimalTypes { Id = 1, Name = "Dog" },
            new AnimalTypes { Id = 2, Name = "Cat" },
            new AnimalTypes { Id = 3, Name = "Bird" }
        );

        modelBuilder.Entity<Employee>()
            .Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200)
            .IsConcurrencyToken();

        modelBuilder.Entity<Employee>()
            .Property(e => e.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20)
            .IsConcurrencyToken();

        modelBuilder.Entity<Employee>()
            .Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(200)
            .IsConcurrencyToken();

        modelBuilder.Entity<Visit>()
            .Property(v => v.Date)
            .IsRequired();

        modelBuilder.Entity<AnimalTypes>()
            .HasIndex(at => at.Name)
            .IsUnique();

        modelBuilder.Entity<Employee>()
            .HasIndex(e => new { e.PhoneNumber, e.Email })
            .IsUnique();
        
        modelBuilder.Entity<Employee>().HasData(
            new Employee { Id = 1, Name = "John Doe", PhoneNumber = "1234567890", Email = "john.doe@example.com" },
            new Employee { Id = 2, Name = "Jane Smith", PhoneNumber = "0987654321", Email = "jane.smith@example.com" }
        );
        
        modelBuilder.Entity<Animal>().HasData(
            new Animal { Id = 5, Name = "Buddy", Description = "A friendly dog", AnimalTypesId = 1 },
            new Animal { Id = 6, Name = "Mittens", Description = "A curious cat", AnimalTypesId = 2 }
        );
    }
}