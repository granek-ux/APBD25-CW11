﻿using System.ComponentModel.DataAnnotations;
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