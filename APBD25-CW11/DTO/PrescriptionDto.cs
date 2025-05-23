using System.ComponentModel.DataAnnotations;
using APBD25_CW11.Models;

namespace APBD25_CW11.DTO;

public class PrescriptionDto
{
    [Required]
    public PatientDto Patient { get; set; }
    [Required]
    public ICollection<MedicamentDto> Medicaments { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
    
    
}

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

public class MedicamentDto
{
    [Required] 
    public int IdMedicament { get; set; }
    [Required]
    public int Dose { get; set; }
    [Required] 
    [MaxLength(100)]
    public string Description { get; set; }
}