using APBD25_CW11.DTO;

namespace APBD25_CW11.Services;

public interface IDbService
{
    public Task<int> InsertPrescription(PrescriptionDto prescription, CancellationToken cancellationToken);
}