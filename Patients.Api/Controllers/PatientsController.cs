using Microsoft.AspNetCore.Mvc;
using Patients.Api.Models.CreatePatient;
using Patients.Api.Models.DeletePatient;
using Patients.Api.Models.FilterPatients;
using Patients.Api.Models.GetPatientById;
using Patients.Api.Models.UpdatePatient;
using Patients.Api.Services.Interfaces;

namespace Patients.Api.Controllers;

[Route("[controller]")]
public class PatientsController : Controller
{
    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }
    
    [HttpPost]
    public async Task<CreatePatientResponse> Create([FromBody] CreatePatientRequest request)
    {
        var response = await _patientService.Create(request);
        return response;
    }

    [HttpGet]
    public async Task<FilterPatientsResponse> Filter(
        [FromQuery]DateTime date,
        [FromQuery]long? pageSize, 
        [FromQuery] long? page)
    {
        var request = new FilterPatientsRequest()
        {
            Date = date,
            Page = page ?? 0,
            PageSize = pageSize ?? 0
        };
        
        var response = await _patientService.Filter(request);
        return response;
    }
    
    [HttpGet("{patientId}")]
    public async Task<GetPatientByIdResponse> GetById([FromRoute]Guid patientId)
    {
        var response = await _patientService.GetById(patientId);
        return response;
    }
    
    [HttpPut]
    public async Task<UpdatePatientResponse> Update([FromBody] UpdatePatientRequest request)
    {
        var response = await _patientService.Update(request);
        return response;
    }
    
    [HttpDelete("{patientId}")]
    public async Task<DeletePatientResponse> Delete([FromRoute] Guid patientId)
    {
        var response = await _patientService.Delete(patientId);
        return response;
    }
}