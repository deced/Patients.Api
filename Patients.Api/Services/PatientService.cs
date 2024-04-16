using MongoDB.Driver;
using Patients.Api.Data.Entities;
using Patients.Api.Data.Repository;
using Patients.Api.Models;
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

    public async Task<CreatePatientsResponse> CreateMany(CreatePatientsRequest request)
    {
        var patients = new List<Patient>();

        foreach (var patientModel in request.Items)
        {
            patients.Add(new Patient()
            {
                Id = patientModel.Guid,
                Active = patientModel.Active,
                Gender = GetGender(patientModel.Gender),
                Family = patientModel.Family,
                Given = patientModel.Given ?? new List<string>(),
                Use = patientModel.Use,
                BirthDate = patientModel.BirthDate
            });
        }

        await _repository.InsertManyAsync(patients);

        var response = new CreatePatientsResponse();
        response.Result = ResultCode.Success;
        return response;
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

    public async Task<FilterPatientsResponse> Filter(FilterPatientsRequest request)
    {
        request.DateFilters ??= Array.Empty<string>();
        var filters = request.DateFilters.Select(GetFilter).ToList();

        var filter = filters.Any() ? Builders<Patient>.Filter.And(filters) : Builders<Patient>.Filter.Empty;
        var patients = await _repository.FilterByAsync(filter, x => x.BirthDate);

        var response = new FilterPatientsResponse
        {
            Items = patients.Select(x => new PatientModel(x)).ToList(),
            Result = ResultCode.Success
        };
        return response;
    }

    public async Task<GetPatientByIdResponse> GetById(Guid patientId)
    {
        var patient = await _repository.GetByIdAsync(patientId);

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
        patient.Gender = GetGender(request.Gender);

        await _repository.UpdateOneAsync(patient);

        response.Result = ResultCode.Success;

        return response;
    }

    public async Task<DeletePatientResponse> Delete(Guid patientId)
    {
        var deletedPatient = await _repository.DeleteOneByIdAsync(patientId);
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

    private static FilterDefinition<Patient> GetFilter(string filterString)
    {
        var filter = Builders<Patient>.Filter;
        var filterType = filterString[..2];
        var dateString = filterString[2..];
        var dateRange = new DateRange(dateString);
        
        if (filterType == "eq")
        {
            return filter.And(
                filter.Gt(x => x.BirthDate, dateRange.Start),
                filter.Lt(x => x.BirthDate, dateRange.End));
        }
        
        if (filterType == "ne")
        {
            return filter.Or(
                filter.Gt(x => x.BirthDate, dateRange.End),
                filter.Lt(x => x.BirthDate, dateRange.Start));
        }
        
        if(filterType == "lt")
        {
            return filter.Lt(x => x.BirthDate, dateRange.Start);   
        }
        
        if (filterType == "gt")
        {
            return filter.Gt(x => x.BirthDate, dateRange.End);
        }
        
        if(filterType == "ge")
        {
            return filter.Gte(x => x.BirthDate, dateRange.Start);
        }
        
        if(filterType == "le")
        {
            return filter.Lt(x => x.BirthDate, dateRange.Start);
        }
        
        if(filterType == "sa")
        {
            return filter.Gt(x => x.BirthDate, dateRange.End);
        }
        
        if(filterType == "eb")
        {
            return filter.Lt(x => x.BirthDate, dateRange.Start);
        }
        
        if (filterType == "ap")
        {
            var start = dateRange.Start.AddDays(-14);
            var end = dateRange.Start.AddDays(14);
                
            return filter.And(
                filter.Gt(x => x.BirthDate, start),
                filter.Lt(x => x.BirthDate, end));
        }

        return filter.Empty;
    }
    
}