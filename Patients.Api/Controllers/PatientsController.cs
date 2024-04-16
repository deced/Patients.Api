using Microsoft.AspNetCore.Mvc;
using Patients.Api.Models.CreateManyPatients;
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
    
    /// <summary>
    /// Creates a patient
    /// </summary>
    /// <remarks>
    /// Properties <b>name.family</b> and <b>birthDate</b> are required!
    /// <br/>Gender: male | female | other | unknown
    /// <br/>Active: true | false
    /// </remarks>
    /// <response code="200">Patient created</response>
    /// <response code="400">Patient has missing/invalid values</response>
    /// <response code="500">Server error</response>
    [HttpPost]
    [ProducesResponseType(typeof(CreatePatientResponse), 200)]
    [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Create([FromBody] CreatePatientRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var response = await _patientService.Create(request);
        return Ok(response);
    }

    /// <summary>
    /// Creates many patients, only for database seeding
    /// </summary>
    /// <response code="200">Patients created</response>
    /// <response code="500">Server error</response>
    [HttpPost("create-many")]
    [ProducesResponseType(typeof(CreatePatientsResponse), 200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> CreateMany([FromBody] CreatePatientsRequest request)
    {
        var response = await _patientService.CreateMany(request);
        return Ok(response);
    }

    /// <summary>
    /// Filters patients by their birthday
    /// </summary>
    /// <remarks> DateFilters have specific format, for more info: <a target="_blank" href="https://www.hl7.org/fhir/search.html#date">hl7.org</a></remarks>
    /// <response code="200">Success</response>
    /// <response code="400">Invalid filters</response>
    /// <response code="500">Server error</response>
    [HttpGet]
    [ProducesResponseType(typeof(FilterPatientsResponse), 200)]
    [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Filter([FromQuery]FilterPatientsRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var response = await _patientService.Filter(request);
        return Ok(response);
    }
    
    /// <summary>
    /// Retrieves patient by id
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="400">Invalid filters</response>
    /// <response code="500">Server error</response>
    [HttpGet("{patientId}")]
    [ProducesResponseType(typeof(GetPatientByIdResponse), 200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetById([FromRoute]Guid patientId)
    {
        var response = await _patientService.GetById(patientId);
        return Ok(response);
    }
    
    /// <summary>
    /// Updates patient
    /// </summary>
    /// <remarks>
    /// Properties <b>name.family</b> and <b>birthDate</b> are required!
    /// <br/>Gender: male | female | other | unknown
    /// <br/>Active: true | false
    /// </remarks>
    /// <response code="200">Success</response>
    /// <response code="400">Patient has missing/invalid values</response>
    /// <response code="500">Server error</response>
    [HttpPut]
    [ProducesResponseType(typeof(UpdatePatientResponse), 200)]
    [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Update([FromBody] UpdatePatientRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var response = await _patientService.Update(request);
        return Ok(response);
    }
    
    /// <summary>
    /// Deletes patient
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="500">Server error</response>
    [HttpDelete("{patientId}")]
    [ProducesResponseType(typeof(DeletePatientResponse), 200)]
    [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Delete([FromRoute] Guid patientId)
    {
        var response = await _patientService.Delete(patientId);
        return Ok(response);
    }
}