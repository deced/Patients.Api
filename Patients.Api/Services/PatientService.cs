using Patients.Api.Data.Entities;
using Patients.Api.Data.Repository;
using Patients.Api.Models.CreateManyPatients;
using Patients.Api.Models.CreatePatient;
using Patients.Api.Models.DeletePatient;
using Patients.Api.Models.FilterPatients;
using Patients.Api.Models.GetPatientById;
using Patients.Api.Models.Shared;
using Patients.Api.Models.UpdatePatient;
using Patients.Api.Services.Interfaces;

namespace Patients.Api.Services;

public class PatientService : IPatientService
{
    private readonly IRepository<Patient> _repository;

    public PatientService(IRepository<Patient> repository)
    {
        _repository = repository;
    }

    public Task CreateMany(CreatePatientsRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<CreatePatientResponse> Create(CreatePatientRequest request)
    {
        var patient = new Patient()
        {
            Active = request.Active,
            Gender = GetGender(request.Gender),
            Family = request.Family,
            Given = request.Given ?? new List<string>(),
            Use = request.Use,
            BirthDate = request.BirthDate
        };

        await _repository.InsertOneAsync(patient);
        var response = new CreatePatientResponse()
        {
            Result = ResultCode.Success,
            PatientId = patient.Id
        };

        return response;
    }

    public Task<FilterPatientsResponse> Filter(FilterPatientsRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<GetPatientByIdResponse> GetById(Guid guid)
    {
        var patient = await _repository.GetByIdAsync(guid);

        var response = new GetPatientByIdResponse();
        
        if (patient == null)
        {
            response.Result = ResultCode.PatientNotFound;
            return response;
        }

        response.Result = ResultCode.Success;
        response.Patient = new PatientModel(patient);

        return response;
    }

    public async Task<UpdatePatientResponse> Update(UpdatePatientRequest request)
    {
        var patient = await _repository.GetByIdAsync(request.Guid);

        var response = new UpdatePatientResponse();
        
        if (patient == null)
        {
            response.Result = ResultCode.PatientNotFound;
            return response;
        }

        patient.BirthDate = request.BirthDate;
        patient.Active = request.Active;
        patient.Family = request.Family;
        patient.Given = request.Given ?? new List<string>();
        patient.Use = request.Use;

        await _repository.UpdateOneAsync(patient);

        response.Result = ResultCode.Success;

        return response;
    }

    public async Task<DeletePatientResponse> Delete(Guid guid)
    {
        var deletedPatient = await _repository.DeleteOneByIdAsync(guid);
        var response = new DeletePatientResponse();
        
        if (deletedPatient == null)
        {
            response.Result = ResultCode.PatientNotFound;
            return response;
        }

        response.Result = ResultCode.Success;
        return response;
    }

    private static Gender GetGender(string? gender)
    {
        if (string.IsNullOrEmpty(gender))
            return Gender.Unknown;

        gender = gender.ToLower();

        return gender switch
        {
            "male" => Gender.Male,
            "female" => Gender.Female,
            "other" => Gender.Other,
            _ => Gender.Unknown
        };
    }
}