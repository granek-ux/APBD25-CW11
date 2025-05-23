using APBD25_CW11.DTO;

namespace APBD25_CW11.Services;

public interface IDbService
{
    public Task<int> InsertPrescription(int doctorId, PrescriptionDto prescription, CancellationToken cancellationToken);
    
    public Task<ReturnDto> getPatient(int patientId, CancellationToken cancellationToken);
}