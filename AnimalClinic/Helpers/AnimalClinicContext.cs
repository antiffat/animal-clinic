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
        modelBuilder.Entity<Animal>()
            .HasOne(a => a.AnimalType)
            .WithMany(at => at.Animals)
            .HasForeignKey(a => a.AnimalTypesId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Visit>()
            .HasOne(v => v.Employee)
            .WithMany(e => e.Visits)
            .HasForeignKey(v => v.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Visit>()
            .HasOne(v => v.Animal)
            .WithMany(a => a.Visits)
            .HasForeignKey(v => v.AnimalId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Animal>()
            .Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Animal>()
            .Property(a => a.Description)
            .HasMaxLength(2000);

        modelBuilder.Entity<AnimalTypes>()
            .Property(at => at.Name)
            .IsRequired()
            .HasMaxLength(150);

        modelBuilder.Entity<Employee>()
            .Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        modelBuilder.Entity<Employee>()
            .Property(e => e.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);

        modelBuilder.Entity<Employee>()
            .Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(200);

        modelBuilder.Entity<Visit>()
            .Property(v => v.Date)
            .IsRequired();

        modelBuilder.Entity<AnimalTypes>()
            .HasIndex(at => at.Name)
            .IsUnique();

        modelBuilder.Entity<Employee>()
            .HasIndex(e => new { e.PhoneNumber, e.Email })
            .IsUnique();
    }
}