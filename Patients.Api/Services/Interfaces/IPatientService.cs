using Patients.Api.Models.CreateManyPatients;
using Patients.Api.Models.CreatePatient;
using Patients.Api.Models.DeletePatient;
using Patients.Api.Models.FilterPatients;
using Patients.Api.Models.GetPatientById;
using Patients.Api.Models.UpdatePatient;

namespace Patients.Api.Services.Interfaces;

public interface IPatientService
{
    Task<CreatePatientsResponse> CreateMany(CreatePatientsRequest request);
    Task<CreatePatientResponse> Create(CreatePatientRequest request);
    Task<FilterPatientsResponse> Filter(FilterPatientsRequest request);
    Task<GetPatientByIdResponse> GetById(Guid guid);
    Task<UpdatePatientResponse> Update(UpdatePatientRequest request);
    Task<DeletePatientResponse> Delete(Guid guid);
}