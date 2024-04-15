namespace Patients.Api.Models.FilterPatients;

public class FilterPatientsRequest
{
    public DateTime Date { get; set; }
    
    public long Page { get; set; }
    public long PageSize { get; set; }
}