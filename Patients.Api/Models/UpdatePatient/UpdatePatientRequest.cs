using System.ComponentModel.DataAnnotations;

namespace Patients.Api.Models.UpdatePatient;

public class UpdatePatientRequest
{
    [Required]
    public Guid Guid { get; set; }
    
    [Required(ErrorMessage = "Family is required")]
    public string Family { get; set; } = string.Empty;

    [Range(typeof(DateTime), "1/1/1900", "1/1/2100", ErrorMessage = "Wrong birth date")] 
    public DateTime BirthDate { get; set; }
    
    public string? Use { get; set; }
    public List<string>? Given { get; set; }
    public bool Active { get; set; }
    public string? Gender { get; set; }
}