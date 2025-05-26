using APBD25_CW11.Data;
using APBD25_CW11.DTO;
using APBD25_CW11.Exceptions;
using APBD25_CW11.Models;
using Microsoft.Build.Evaluation;
using Microsoft.EntityFrameworkCore;

namespace APBD25_CW11.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<int> InsertPrescription(int doctorId, PrescriptionDto prescription,
        CancellationToken cancellationToken)
    {
        if (prescription.Medicaments.Count > 10)
            throw new BadRequestException("Too many medicaments");
        if (prescription.DueDate < prescription.Date)
            throw new BadRequestException("The dueDate cannot be earlier than date");

        foreach (var medicament in prescription.Medicaments)
        {
            var checkM = _context.Medicaments.Any(m => m.IdMedicament == medicament.IdMedicament);

            if (!checkM)
                throw new NotFoundException($"Medicament {medicament.IdMedicament} not found");
        }

        var chechP = _context.Patients.Any(p => p.IdPatient == prescription.Patient.IdPatient);

        if (!chechP)
        {
            var pat = new Patient
            {
                IdPatient = prescription.Patient.IdPatient,
                Birthdate = prescription.Patient.Birthdate,
                FirstName = prescription.Patient.FirstName,
                LastName = prescription.Patient.LastName,
            };
            await _context.Patients.AddAsync(pat, cancellationToken);
        }

        var newId = _context.Prescriptions.Max(e => e.IdPrescription) + 1;

        var newPrescription = new Prescription
        {
            IdPrescription = newId,
            Date = prescription.Date,
            DueDate = prescription.DueDate,
            IdPatient = prescription.Patient.IdPatient,
            IdDoctor = doctorId
        };

        await _context.Prescriptions.AddAsync(newPrescription, cancellationToken);

        foreach (var medicament in prescription.Medicaments)
        {
            var tmpPresMed = new Prescription_Medicament
            {
                IdMedicament = medicament.IdMedicament,
                IdPrescription = newId,
                Dose = medicament.Dose,
                Details = medicament.Description
            };
            await _context.Prescription_Medicaments.AddAsync(tmpPresMed, cancellationToken);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return 0;
    }

    public async Task<ReturnDto> getPatient(int patientId, CancellationToken cancellationToken)
    {
        var check =await _context.Patients.AnyAsync(p => p.IdPatient == patientId, cancellationToken);

        if (!check)
            throw new NotFoundException($"Patient {patientId} not found");
        
        return await _context.Patients
            .Where(p => p.IdPatient == patientId)
            .Include(p => p.Prescriptions)
            .ThenInclude(p => p.Doctor)
            .Include(p => p.Prescriptions)
            .ThenInclude(p => p.Prescription_Medicaments)
            .ThenInclude(m => m.Medicament).Select(p => new ReturnDto
            {
                IdPatient = p.IdPatient,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Birthdate = p.Birthdate,
                prescriptions = p.Prescriptions.Select(pr => new PrescriptionReturnDto()
                {
                    IdPrescription = pr.IdPrescription,
                    Date = pr.Date,
                    DueDate = pr.DueDate,
                    Medicaments = pr.Prescription_Medicaments.Select(pm => new MedicamentReturnDto
                    {
                        IdMedicament = pm.IdMedicament,
                        Name = pm.Medicament.Name,
                        Dose = pm.Dose,
                        Description = pm.Details
                    }).ToList(),
                    Doctor = new DoctorReturnDto
                    {
                        IdDoctor = pr.Doctor.IdDoctor,
                        Firstname = pr.Doctor.FirstName,
                    }
                }).OrderBy(pr => pr.DueDate).ToList()
            }).FirstAsync(cancellationToken);
    }
}