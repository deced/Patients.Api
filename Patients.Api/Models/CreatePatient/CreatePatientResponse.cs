using Patients.Api.Models.Shared;

namespace Patients.Api.Models.CreatePatient;

public class CreatePatientResponse : ResponseBase
{
    public Guid PatientId { get; set; }
}