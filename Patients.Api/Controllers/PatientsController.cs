using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePatientRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var response = await _patientService.Create(request);
        return Json(response);
    }

    [HttpPost("create-many")]
    public async Task<IActionResult> CreateMany([FromBody] CreatePatientsRequest request)
    {
        var response = await _patientService.CreateMany(request);
        return Json(response);
    }

    [HttpGet]
    public async Task<IActionResult> Filter([FromQuery]FilterPatientsRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var response = await _patientService.Filter(request);
        return Json(response);
    }
    
    [HttpGet("{patientId}")]
    public async Task<IActionResult> GetById([FromRoute]Guid patientId)
    {
        var response = await _patientService.GetById(patientId);
        return Json(response);
    }
    
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdatePatientRequest request)
    {
        if (ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var response = await _patientService.Update(request);
        return Json(response);
    }
    
    [HttpDelete("{patientId}")]
    public async Task<IActionResult> Delete([FromRoute] Guid patientId)
    {
        var response = await _patientService.Delete(patientId);
        return Json(response);
    }
}