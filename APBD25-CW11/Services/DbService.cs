using APBD25_CW11.Data;
using APBD25_CW11.DTO;
using APBD25_CW11.Exceptions;
using APBD25_CW11.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD25_CW11.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<int> InsertPrescription(int doctorId ,PrescriptionDto prescription, CancellationToken cancellationToken)
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
}