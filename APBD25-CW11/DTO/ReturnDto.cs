using System.ComponentModel.DataAnnotations;

namespace APBD25_CW11.DTO;

public class ReturnDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
    public ICollection<PrescriptionReturnDto> prescriptions { get; set; }
}