using System.ComponentModel.DataAnnotations;

namespace Patients.Api.Models.UpdatePatient;

public class UpdatePatientRequest
{
    [Required]
    public Guid Guid { get; set; }
    
    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string Family { get; set; } = string.Empty;

    [Required] 
    public DateTime BirthDate { get; set; }
    
    public string? Use { get; set; }
    public List<string>? Given { get; set; }
    public bool Active { get; set; }
    public string? Gender { get; set; }
}