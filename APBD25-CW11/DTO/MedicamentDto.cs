using System.ComponentModel.DataAnnotations;

namespace APBD25_CW11.DTO;

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