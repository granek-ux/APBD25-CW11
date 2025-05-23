namespace APBD25_CW11.DTO;

public class PrescriptionReturnDto
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public ICollection<MedicamentReturnDto> Medicaments { get; set; }
    public DoctorReturnDto Doctor { get; set; }
}