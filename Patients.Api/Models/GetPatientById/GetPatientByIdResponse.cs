using Patients.Api.Models.Shared;

namespace Patients.Api.Models.GetPatientById;

public class GetPatientByIdResponse : ResponseBase
{
    public PatientModel Patient { get; set; }
}