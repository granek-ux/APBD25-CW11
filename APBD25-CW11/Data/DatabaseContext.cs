using APBD25_CW11.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD25_CW11.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription_Medicament> Prescription_Medicaments { get; set; }

    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>(a =>
        {
            a.ToTable("Patients");
            a.HasKey(x => x.IdPatient);
            a.Property(e => e.FirstName).HasMaxLength(100);
            a.Property(e => e.LastName).HasMaxLength(100);
        });

        modelBuilder.Entity<Doctor>().HasData(new List<Doctor>()
        {
            new Doctor()
                { IdDoctor = 1, FirstName = "Anna", LastName = "Kowalska", Email = "anna.kowalska@clinic.com" },
            new Doctor() { IdDoctor = 2, FirstName = "Jan", LastName = "Nowak", Email = "jan.nowak@clinic.com" },
            new Doctor()
                { IdDoctor = 3, FirstName = "Ewa", LastName = "Wiśniewska", Email = "ewa.wisniewska@clinic.com" },
        });

        modelBuilder.Entity<Patient>().HasData(new List<Patient>()
        {
            new Patient()
                { IdPatient = 1, FirstName = "Tomasz", LastName = "Mazur", Birthdate = new DateTime(1990, 5, 12) },
            new Patient()
            {
                IdPatient = 2, FirstName = "Katarzyna", LastName = "Zielińska", Birthdate = new DateTime(1985, 3, 20)
            },
            new Patient()
                { IdPatient = 3, FirstName = "Michał", LastName = "Krawczyk", Birthdate = new DateTime(2000, 11, 2) },
        });

        modelBuilder.Entity<Medicament>().HasData(new List<Medicament>()
        {
            new Medicament() { IdMedicament = 1, Name = "Paracetamol", Description = "Pain reliever", Type = "Tablet" },
            new Medicament()
                { IdMedicament = 2, Name = "Ibuprofen", Description = "Anti-inflammatory", Type = "Capsule" },
            new Medicament()
                { IdMedicament = 3, Name = "Amoxicillin", Description = "Antibiotic", Type = "Suspension" },
        });

        modelBuilder.Entity<Prescription>().HasData(new List<Prescription>()
        {
            new Prescription()
            {
                IdPrescription = 1, Date = new DateTime(2025, 5, 1), DueDate = new DateTime(2025, 6, 1), IdPatient = 1,
                IdDoctor = 1
            },
            new Prescription()
            {
                IdPrescription = 2, Date = new DateTime(2025, 5, 10), DueDate = new DateTime(2025, 6, 10),
                IdPatient = 2, IdDoctor = 2
            },
            new Prescription()
            {
                IdPrescription = 3, Date = new DateTime(2025, 5, 15), DueDate = new DateTime(2025, 6, 15),
                IdPatient = 3, IdDoctor = 3
            },
        });

        modelBuilder.Entity<Prescription_Medicament>().HasData(new List<Prescription_Medicament>()
        {
            new Prescription_Medicament()
                { IdPrescription = 1, IdMedicament = 1, Dose = 500, Details = "Take twice daily after meal" },
            new Prescription_Medicament()
                { IdPrescription = 2, IdMedicament = 2, Dose = 200, Details = "Take once daily" },
            new Prescription_Medicament()
                { IdPrescription = 3, IdMedicament = 3, Dose = 250, Details = "Take three times daily" },
        });
    }
}