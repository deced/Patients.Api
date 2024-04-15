using Patients.Api.Models.Shared;

namespace Patients.Api.Models.FilterPatients;

public class FilterPatientsResponse : ResponseBase
{
    public List<PatientModel> Items { get; set; } = new();
}
