using System.ComponentModel.DataAnnotations;

namespace APBD25_CW11.DTO;

public class PatientDto
{
    [Required] 
    public int IdPatient { get; set; }
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }
    [Required] 
    [MaxLength(100)]
    public string LastName { get; set; }
    [Required] 
    public DateTime Birthdate { get; set; }
}