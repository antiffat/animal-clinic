﻿// <auto-generated />
using System;
using AnimalClinic.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AnimalClinic.Migrations
{
    [DbContext(typeof(AnimalClinicContext))]
    [Migration("20240611150256_AddConcurrencyTokens")]
    partial class AddConcurrencyTokens
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AnimalClinic.Models.Animal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnimalTypesId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsConcurrencyToken()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("Name")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("AnimalTypesId");

                    b.ToTable("Animals");

                    b.HasData(
                        new
                        {
                            Id = 5,
                            AnimalTypesId = 1,
                            Description = "A friendly dog",
                            Name = "Buddy"
                        },
                        new
                        {
                            Id = 6,
                            AnimalTypesId = 2,
                            Description = "A curious cat",
                            Name = "Mittens"
                        });
                });

            modelBuilder.Entity("AnimalClinic.Models.AnimalTypes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("AnimalTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Dog"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Cat"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Bird"
                        });
                });

            modelBuilder.Entity("AnimalClinic.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("PhoneNumber")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("PhoneNumber", "Email")
                        .IsUnique();

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "john.doe@example.com",
                            Name = "John Doe",
                            PhoneNumber = "1234567890"
                        },
                        new
                        {
                            Id = 2,
                            Email = "jane.smith@example.com",
                            Name = "Jane Smith",
                            PhoneNumber = "0987654321"
                        });
                });

            modelBuilder.Entity("AnimalClinic.Models.Visit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnimalId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("AnimalId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Visits");
                });

            modelBuilder.Entity("AnimalClinic.Models.Animal", b =>
                {
                    b.HasOne("AnimalClinic.Models.AnimalTypes", "AnimalType")
                        .WithMany("Animals")
                        .HasForeignKey("AnimalTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnimalType");
                });

            modelBuilder.Entity("AnimalClinic.Models.Visit", b =>
                {
                    b.HasOne("AnimalClinic.Models.Animal", "Animal")
                        .WithMany("Visits")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AnimalClinic.Models.Employee", "Employee")
                        .WithMany("Visits")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Animal");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("AnimalClinic.Models.Animal", b =>
                {
                    b.Navigation("Visits");
                });

            modelBuilder.Entity("AnimalClinic.Models.AnimalTypes", b =>
                {
                    b.Navigation("Animals");
                });

            modelBuilder.Entity("AnimalClinic.Models.Employee", b =>
                {
                    b.Navigation("Visits");
                });
#pragma warning restore 612, 618
        }
    }
}
